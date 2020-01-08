using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.ArticleData.ArticleInfo;
using Reth.Itss2.Standard.Dialogs.ArticleData.ArticleMasterSet;
using Reth.Itss2.Standard.Dialogs.ArticleData.StockDeliveryInfo;
using Reth.Itss2.Standard.Dialogs.ArticleData.StockDeliverySet;
using Reth.Itss2.Standard.Dialogs.General.Hello;
using Reth.Itss2.Standard.Dialogs.General.Unprocessed;
using Reth.Itss2.Standard.Dialogs.Storage.InitiateInput;
using Reth.Itss2.Standard.Dialogs.Storage;
using Reth.Itss2.Standard.Dialogs.Storage.Output;
using Reth.Itss2.Standard.Dialogs.Storage.OutputInfo;
using Reth.Itss2.Standard.Dialogs.Storage.Status;
using Reth.Itss2.Standard.Dialogs.Storage.StockInfo;
using Reth.Itss2.Standard.Dialogs.Storage.StockLocationInfo;
using Reth.Itss2.Standard.Dialogs.Storage.TaskCancelOutput;
using Reth.Itss2.Standard.Workflows.StockAutomation.Server.Processing.InitiateInput;
using Reth.Itss2.Standard.Workflows.StockAutomation.Server.Processing.Input;
using Reth.Itss2.Standard.Workflows.StockAutomation.Server.Processing.Output;
using Reth.Protocols;
using Reth.Protocols.Diagnostics;
using Reth.Protocols.Dialogs;
using Reth.Protocols.Extensions.EventArgsExtensions;
using Reth.Protocols.Extensions.ObjectExtensions;
using Reth.Protocols.Transfer;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Server
{
    public class StorageSystem:Reth.Itss2.Standard.Workflows.StorageSystem, IStorageSystem
    {
        public event EventHandler<InitiateInputRequestReceivedEventArgs> InitiateInputRequestReceived;
        public event EventHandler<OutputRequestReceivedEventArgs> OutputRequestReceived;

        public event EventHandler Disconnected;
        
        private bool isRunning;
        private volatile bool isDisposed;

        public StorageSystem(   SubscriberInfo subscriberInfo,
                                IRemoteMessageClient messageClient,
                                IRemoteClientDialogProvider dialogProvider   )
        {
            dialogProvider.ThrowIfNull();

            this.Initialize(    subscriberInfo,
                                messageClient,
                                dialogProvider,
                                dialogProvider.GetSupportedDialogs()    );
        }
        
        public StorageSystem(   SubscriberInfo subscriberInfo,
                                IRemoteMessageClient messageClient,
                                IRemoteClientDialogProvider dialogProvider,
                                IEnumerable<IDialogName> supportedDialogs   )
        {
            this.Initialize(    subscriberInfo,
                                messageClient,
                                dialogProvider,
                                supportedDialogs    );
        }

        ~StorageSystem()
        {
            this.Dispose( false );
        }

        public IRemoteMessageClient MessageClient
        {
            get; private set;
        }

        public IRemoteClientDialogProvider DialogProvider
        {
            get; private set;
        }

        private Object SyncRoot
        {
            get;
        } = new Object();

        private bool IsRunning
        {
            get
            {
                lock( this.SyncRoot )
                {
                    return this.isRunning;
                }
            }

            set
            {
                lock( this.SyncRoot )
                {
                    this.isRunning = value;
                }
            }
        }

        public Func<IStorageSystem, ArticleMasterSetRequest, ArticleMasterSetResponse> ArticleMasterSetRequestReceivedCallback
        {
            get; set;
        }

        public Func<IStorageSystem, HelloRequest, HelloResponse> HelloRequestReceivedCallback
        {
            get; set;
        }

        public Func<IStorageSystem, OutputInfoRequest, OutputInfoResponse> OutputInfoRequestReceivedCallback
        {
            get; set;
        }

        public Func<IStorageSystem, StatusRequest, StatusResponse> StatusRequestReceivedCallback
        {
            get; set;
        }

        public Func<IStorageSystem, StockDeliveryInfoRequest, StockDeliveryInfoResponse> StockDeliveryInfoRequestReceivedCallback
        {
            get; set;
        }

        public Func<IStorageSystem, StockDeliverySetRequest, StockDeliverySetResponse> StockDeliverySetRequestReceivedCallback
        {
            get; set;
        }

        public Func<IStorageSystem, StockInfoRequest, StockInfoResponse> StockInfoRequestReceivedCallback
        {
            get; set;
        }

        public Func<IStorageSystem, StockLocationInfoRequest, StockLocationInfoResponse> StockLocationInfoRequestReceivedCallback
        {
            get; set;
        }

        public Func<IStorageSystem, TaskCancelOutputRequest, TaskCancelOutputResponse> TaskCancelOutputRequestReceivedCallback
        {
            get; set;
        }

        public void Initialize( SubscriberInfo subscriberInfo,
                                IRemoteMessageClient messageClient,
                                IRemoteClientDialogProvider dialogProvider,
                                IEnumerable<IDialogName> supportedDialogs   )
        {
            subscriberInfo.ThrowIfNull();
            messageClient.ThrowIfNull();
            dialogProvider.ThrowIfNull();

            ExecutionLogProvider.LogInformation( "Initializing server storage system." );

            UnhandledMessageHandler.UseCallback( this.OnMessageUnhandled );

            this.MessageClient = messageClient;
            this.MessageClient.Disconnected += this.MessageClient_Disconnected;

            this.DialogProvider = dialogProvider;
            this.DialogProvider.ArticleMasterSet.RequestReceived += this.ArticleMasterSet_RequestReceived;
            this.DialogProvider.Hello.RequestReceived += this.Hello_RequestReceived;
            this.DialogProvider.InitiateInput.RequestReceived += this.InitiateInput_RequestReceived;
            this.DialogProvider.OutputInfo.RequestReceived += this.OutputInput_RequestReceived;
            this.DialogProvider.Output.RequestReceived += this.Output_RequestReceived;
            this.DialogProvider.Status.RequestReceived += this.Status_RequestReceived;
            this.DialogProvider.StockDeliveryInfo.RequestReceived += this.StockDeliveryInfo_RequestReceived;
            this.DialogProvider.StockDeliverySet.RequestReceived += this.StockDeliverySet_RequestReceived;
            this.DialogProvider.StockInfo.RequestReceived += this.StockInfo_RequestReceived;
            this.DialogProvider.StockLocationInfo.RequestReceived += this.StockLocationInfo_RequestReceived;
            this.DialogProvider.TaskCancelOutput.RequestReceived += this.TaskCancelOutput_RequestReceived;

            this.LocalSubscriber = new Subscriber(  subscriberInfo,
                                                    supportedDialogs    );
        }

        public void Run()
        {
            if( this.IsRunning == false )
            {
                this.MessageClient.Run();

                this.isRunning = true;
            }
        }

        public void Terminate()
        {
            if( this.IsRunning == true )
            {
                this.MessageClient.Terminate();

                this.isRunning = false;
            }
        }

        protected override void SendUnprocessedMessage( UnprocessedMessage message )
        {
            this.DialogProvider.Unprocessed.SendMessage( message );
        }

        protected override Task SendUnprocessedMessageAsync( UnprocessedMessage message )
        {
            return this.SendUnprocessedMessageAsync( message, CancellationToken.None );
        }

        protected override Task SendUnprocessedMessageAsync( UnprocessedMessage message, CancellationToken cancellationToken )
        {
            return this.DialogProvider.Unprocessed.SendMessageAsync( message, cancellationToken );
        }

        private ArticleInfoRequest CreateArticleInfoRequest( IEnumerable<ArticleInfoRequestArticle> articles )
        {
            return new ArticleInfoRequest(  MessageId.NewId(),
                                            this.LocalSubscriber.Id,
                                            this.GetRemoteSubscriberId(),
                                            articles    );
        }

        private OutputMessage CreateOutputMessage(  OutputMessageDetails details,
                                                    IEnumerable<OutputArticle> articles,
                                                    IEnumerable<OutputBox> boxes    )
        {
            return new OutputMessage(   MessageId.NewId(),
                                        this.LocalSubscriber.Id,
                                        this.GetRemoteSubscriberId(),
                                        details,
                                        articles,
                                        boxes   );
        }

        private StockInfoMessage CreateStockInfoMessage( IEnumerable<StockInfoArticle> articles )
        {
            return new StockInfoMessage(    MessageId.NewId(),
                                            this.LocalSubscriber.Id,
                                            this.GetRemoteSubscriberId(),
                                            articles    );
        }

        private void MessageClient_Disconnected( Object sender, EventArgs e )
        {
            this.Disconnected?.SafeInvoke( this, e );
        }

        private void ArticleMasterSet_RequestReceived( Object sender, MessageReceivedEventArgs e )
        {
            if( !( this.ArticleMasterSetRequestReceivedCallback is null ) )
            {
                ArticleMasterSetResponse response = null;
                
                try
                {
                    response = this.ArticleMasterSetRequestReceivedCallback?.Invoke( this, ( ArticleMasterSetRequest )( e.Message ) );
                }catch( Exception ex )
                {
                    ExecutionLogProvider.LogError( ex );
                }

                if( !( response is null ) )
                {
                    this.DialogProvider.ArticleMasterSet.SendResponse( response );
                }
            }
        }

        private void Hello_RequestReceived( Object sender, MessageReceivedEventArgs e )
        {
            if( !( this.HelloRequestReceivedCallback is null ) )
            {
                HelloResponse response = null;
                
                try
                {
                    response = this.HelloRequestReceivedCallback?.Invoke( this, ( HelloRequest )( e.Message ) );
                }catch( Exception ex )
                {
                    ExecutionLogProvider.LogError( ex );
                }

                if( !( response is null ) )
                {
                    this.DialogProvider.Hello.SendResponse( response );
                }
            }
        }

        private void InitiateInput_RequestReceived( Object sender, MessageReceivedEventArgs e )
        {
            try
            {
                InitiateInputRequest request = ( InitiateInputRequest )( e.Message );

                InitiateInputProcess process = new InitiateInputProcess( this, request );

                this.InitiateInputRequestReceived?.Invoke( this, new InitiateInputRequestReceivedEventArgs( request, process ) );
            }catch( Exception ex )
            {
                ExecutionLogProvider.LogError( ex );
            }
        }

        private void OutputInput_RequestReceived( Object sender, MessageReceivedEventArgs e )
        {
            if( !( this.OutputInfoRequestReceivedCallback is null ) )
            {
                OutputInfoResponse response = null;
                
                try
                {
                    response = this.OutputInfoRequestReceivedCallback?.Invoke( this, ( OutputInfoRequest )( e.Message ) );
                }catch( Exception ex )
                {
                    ExecutionLogProvider.LogError( ex );
                }

                if( !( response is null ) )
                {
                    this.DialogProvider.OutputInfo.SendResponse( response );
                }
            }
        }

        private void Output_RequestReceived( Object sender, MessageReceivedEventArgs e )
        {
            try
            {
                OutputRequest request = ( OutputRequest )( e.Message );

                OutputProcess process = new OutputProcess( this, request );

                this.OutputRequestReceived?.Invoke( this, new OutputRequestReceivedEventArgs( request, process ) );
            }catch( Exception ex )
            {
                ExecutionLogProvider.LogError( ex );
            }
        }

        private void Status_RequestReceived( Object sender, MessageReceivedEventArgs e )
        {
            if( !( this.StatusRequestReceivedCallback is null ) )
            {
                StatusResponse response = null;
                
                try
                {
                    response = this.StatusRequestReceivedCallback?.Invoke( this, ( StatusRequest )( e.Message ) );
                }catch( Exception ex )
                {
                    ExecutionLogProvider.LogError( ex );
                }

                if( !( response is null ) )
                {
                    this.DialogProvider.Status.SendResponse( response );
                }
            }
        }

        private void StockDeliveryInfo_RequestReceived( Object sender, MessageReceivedEventArgs e )
        {
            if( !( this.StockDeliveryInfoRequestReceivedCallback is null ) )
            {
                StockDeliveryInfoResponse response = null;
                
                try
                {
                    response = this.StockDeliveryInfoRequestReceivedCallback?.Invoke( this, ( StockDeliveryInfoRequest )( e.Message ) );
                }catch( Exception ex )
                {
                    ExecutionLogProvider.LogError( ex );
                }

                if( !( response is null ) )
                {
                    this.DialogProvider.StockDeliveryInfo.SendResponse( response );
                }
            }
        }

        private void StockDeliverySet_RequestReceived( Object sender, MessageReceivedEventArgs e )
        {
            if( !( this.StockDeliverySetRequestReceivedCallback is null ) )
            {
                StockDeliverySetResponse response = null;
                
                try
                {
                    response = this.StockDeliverySetRequestReceivedCallback?.Invoke( this, ( StockDeliverySetRequest )( e.Message ) );
                }catch( Exception ex )
                {
                    ExecutionLogProvider.LogError( ex );
                }

                if( !( response is null ) )
                {
                    this.DialogProvider.StockDeliverySet.SendResponse( response );
                }
            }
        }

        private void StockInfo_RequestReceived( Object sender, MessageReceivedEventArgs e )
        {
            if( !( this.StockInfoRequestReceivedCallback is null ) )
            {
                StockInfoResponse response = null;
                
                try
                {
                    response = this.StockInfoRequestReceivedCallback?.Invoke( this, ( StockInfoRequest )( e.Message ) );
                }catch( Exception ex )
                {
                    ExecutionLogProvider.LogError( ex );
                }

                if( !( response is null ) )
                {
                    this.DialogProvider.StockInfo.SendResponse( response );
                }
            }
        }

        private void StockLocationInfo_RequestReceived( Object sender, MessageReceivedEventArgs e )
        {
            if( !( this.StockLocationInfoRequestReceivedCallback is null ) )
            {
                StockLocationInfoResponse response = null;
                
                try
                {
                    response = this.StockLocationInfoRequestReceivedCallback?.Invoke( this, ( StockLocationInfoRequest )( e.Message ) );
                }catch( Exception ex )
                {
                    ExecutionLogProvider.LogError( ex );
                }

                if( !( response is null ) )
                {
                    this.DialogProvider.StockLocationInfo.SendResponse( response );
                }
            }
        }

        private void TaskCancelOutput_RequestReceived( Object sender, MessageReceivedEventArgs e )
        {
            if( !( this.TaskCancelOutputRequestReceivedCallback is null ) )
            {
                TaskCancelOutputResponse response = null;
                
                try
                {
                    response = this.TaskCancelOutputRequestReceivedCallback?.Invoke( this, ( TaskCancelOutputRequest )( e.Message ) );
                }catch( Exception ex )
                {
                    ExecutionLogProvider.LogError( ex );
                }

                if( !( response is null ) )
                {
                    this.DialogProvider.TaskCancelOutput.SendResponse( response );
                }
            }
        }

        public ArticleInfoResponse GetArticleInfo( IEnumerable<ArticleInfoRequestArticle> articles )
        {
            ArticleInfoResponse result = null;

            ArticleInfoRequest request = this.CreateArticleInfoRequest( articles );

            if( this.VerifyCapability( DialogName.ArticleInfo, request ) == true )
            {
                result = this.DialogProvider.ArticleInfo.SendRequest( request );
            }

            return result;
        }

        public Task<ArticleInfoResponse> GetArticleInfoAsync( IEnumerable<ArticleInfoRequestArticle> articles )
        {
            return this.GetArticleInfoAsync( articles, CancellationToken.None );
        }

        public Task<ArticleInfoResponse> GetArticleInfoAsync( IEnumerable<ArticleInfoRequestArticle> articles, CancellationToken cancellationToken )
        {
            Task<ArticleInfoResponse> result = Task.FromResult<ArticleInfoResponse>( null );

            ArticleInfoRequest request = this.CreateArticleInfoRequest( articles );

            if( this.VerifyCapability( DialogName.ArticleInfo, request ) == true )
            {
                result = this.DialogProvider.ArticleInfo.SendRequestAsync( request, cancellationToken );
            }

            return result;
        }

        public void SendOutputMessage(  OutputMessageDetails details,
                                        IEnumerable<OutputArticle> articles,
                                        IEnumerable<OutputBox> boxes )
        {
            OutputMessage message = this.CreateOutputMessage( details, articles, boxes );

            if( this.VerifyCapability( DialogName.Output, message ) == true )
            {
                this.DialogProvider.Output.SendMessage( message );
            }
        }

        public Task SendOutputMessageAsync( OutputMessageDetails details,
                                            IEnumerable<OutputArticle> articles,
                                            IEnumerable<OutputBox> boxes )
        {
            return this.SendOutputMessageAsync( details, articles, boxes, CancellationToken.None );
        }

        public Task SendOutputMessageAsync( OutputMessageDetails details,
                                            IEnumerable<OutputArticle> articles,
                                            IEnumerable<OutputBox> boxes,
                                            CancellationToken cancellationToken )
        {
            Task result = Task.CompletedTask;

            OutputMessage message = this.CreateOutputMessage( details, articles, boxes );

            if( this.VerifyCapability( DialogName.Output, message ) == true )
            {
                result = this.DialogProvider.Output.SendMessageAsync( message, cancellationToken );
            }

            return result;
        }

        public void SendStockInfoMessage( IEnumerable<StockInfoArticle> articles )
        {
            StockInfoMessage message = this.CreateStockInfoMessage( articles );

            if( this.VerifyCapability( DialogName.StockInfo, message ) == true )
            {
                this.DialogProvider.StockInfo.SendMessage( message );
            }
        }

        public Task SendStockInfoMessageAsync( IEnumerable<StockInfoArticle> articles )
        {
            return this.SendStockInfoMessageAsync( articles, CancellationToken.None );
        }

        public Task SendStockInfoMessageAsync( IEnumerable<StockInfoArticle> articles, CancellationToken cancellationToken )
        {
            Task result = Task.CompletedTask;

            StockInfoMessage message = this.CreateStockInfoMessage( articles );

            if( this.VerifyCapability( DialogName.StockInfo, message ) == true )
            {
                result = this.DialogProvider.StockInfo.SendMessageAsync( message, cancellationToken );
            }

            return result;
        }

        public IInputProcess CreateInputProcess()
        {
            return new InputProcess( this );
        }

        public IInputProcess CreateInputProcess( IMessageId messageId )
        {
            return new InputProcess( this, messageId );
        }

        public void Dispose()
        {
            this.Dispose( true );

            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
            ExecutionLogProvider.LogInformation( "Disposing storage system." );

            if( this.isDisposed == false )
            {
                if( disposing == true )
                {
                    this.RemoteCapabilities.Clear();

                    this.MessageClient.Dispose();
                    this.DialogProvider.Dispose();
                }

                this.isDisposed = true;
            }
        }
    }
}
