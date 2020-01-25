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
        
        private bool isStarted;
        private volatile bool isDisposed;

        public StorageSystem(   SubscriberInfo subscriberInfo,
                                IRemoteMessageClient messageClient,
                                IRemoteClientDialogProvider dialogProvider   )
        :
            base( dialogProvider )
        {
            this.Initialize(    subscriberInfo,
                                messageClient,
                                dialogProvider,
                                dialogProvider.GetSupportedDialogs()    );
        }
        
        public StorageSystem(   SubscriberInfo subscriberInfo,
                                IRemoteMessageClient messageClient,
                                IRemoteClientDialogProvider dialogProvider,
                                IEnumerable<IDialogName> supportedDialogs   )
        :
            base( dialogProvider )
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

        private bool IsStarted
        {
            get
            {
                lock( this.SyncRoot )
                {
                    return this.isStarted;
                }
            }

            set
            {
                lock( this.SyncRoot )
                {
                    this.isStarted = value;
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
            this.DialogProvider.OutputInfo.RequestReceived += this.OutputInfo_RequestReceived;
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

        public void Start()
        {
            if( this.IsStarted == false )
            {
                this.MessageClient.Start();

                this.IsStarted = true;
            }
        }

        public void Terminate()
        {
            if( this.IsStarted == true )
            {
                this.MessageClient.Terminate();

                this.IsStarted = false;
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
            e.IsHandled = true;
                
            try
            {
                ArticleMasterSetResponse response = this.ArticleMasterSetRequestReceivedCallback?.Invoke( this, ( ArticleMasterSetRequest )( e.Message ) );

                this.DialogProvider.ArticleMasterSet.SendResponse( response );
            }catch( Exception ex )
            {
                ExecutionLogProvider.LogError( ex );
            }
        }

        private void Hello_RequestReceived( Object sender, MessageReceivedEventArgs e )
        {
            e.IsHandled = true;

            try
            {
                HelloRequest request = ( HelloRequest )( e.Message );

                HelloResponse response = this.HelloRequestReceivedCallback?.Invoke( this, request );

                this.DialogProvider.Hello.SendResponse( response );

                foreach( Capability capability in request.Subscriber.GetCapabilities() )
                {
                    this.RemoteCapabilities.Add( capability.Name );
                }
            }catch( Exception ex )
            {
                ExecutionLogProvider.LogError( ex );
            }
        }

        private void InitiateInput_RequestReceived( Object sender, MessageReceivedEventArgs e )
        {
            e.IsHandled = true;

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

        private void OutputInfo_RequestReceived( Object sender, MessageReceivedEventArgs e )
        {
            e.IsHandled = true;
                
            try
            {
                OutputInfoResponse response = this.OutputInfoRequestReceivedCallback?.Invoke( this, ( OutputInfoRequest )( e.Message ) );

                this.DialogProvider.OutputInfo.SendResponse( response );
            }catch( Exception ex )
            {
                ExecutionLogProvider.LogError( ex );
            }
        }

        private void Output_RequestReceived( Object sender, MessageReceivedEventArgs e )
        {
            e.IsHandled = true;

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
            e.IsHandled = true;

            try
            {
                StatusResponse response = this.StatusRequestReceivedCallback?.Invoke( this, ( StatusRequest )( e.Message ) );

                this.DialogProvider.Status.SendResponse( response );
            }catch( Exception ex )
            {
                ExecutionLogProvider.LogError( ex );
            }
        }

        private void StockDeliveryInfo_RequestReceived( Object sender, MessageReceivedEventArgs e )
        {
            e.IsHandled = true;

            try
            {
                StockDeliveryInfoResponse response = this.StockDeliveryInfoRequestReceivedCallback?.Invoke( this, ( StockDeliveryInfoRequest )( e.Message ) );

                this.DialogProvider.StockDeliveryInfo.SendResponse( response );
            }catch( Exception ex )
            {
                ExecutionLogProvider.LogError( ex );
            }
        }

        private void StockDeliverySet_RequestReceived( Object sender, MessageReceivedEventArgs e )
        {
            e.IsHandled = true;

            try
            {
                StockDeliverySetResponse response = this.StockDeliverySetRequestReceivedCallback?.Invoke( this, ( StockDeliverySetRequest )( e.Message ) );

                this.DialogProvider.StockDeliverySet.SendResponse( response );
            }catch( Exception ex )
            {
                ExecutionLogProvider.LogError( ex );
            }
        }

        private void StockInfo_RequestReceived( Object sender, MessageReceivedEventArgs e )
        {
            e.IsHandled = true;

            try
            {
                StockInfoResponse response = this.StockInfoRequestReceivedCallback?.Invoke( this, ( StockInfoRequest )( e.Message ) );

                this.DialogProvider.StockInfo.SendResponse( response );
            }catch( Exception ex )
            {
                ExecutionLogProvider.LogError( ex );
            }
        }

        private void StockLocationInfo_RequestReceived( Object sender, MessageReceivedEventArgs e )
        {
            e.IsHandled = true;

            try
            {
                StockLocationInfoResponse response = this.StockLocationInfoRequestReceivedCallback?.Invoke( this, ( StockLocationInfoRequest )( e.Message ) );

                this.DialogProvider.StockLocationInfo.SendResponse( response );
            }catch( Exception ex )
            {
                ExecutionLogProvider.LogError( ex );
            }
        }

        private void TaskCancelOutput_RequestReceived( Object sender, MessageReceivedEventArgs e )
        {
            e.IsHandled = true;

            try
            {
                TaskCancelOutputResponse response = this.TaskCancelOutputRequestReceivedCallback?.Invoke( this, ( TaskCancelOutputRequest )( e.Message ) );

                this.DialogProvider.TaskCancelOutput.SendResponse( response );
            }catch( Exception ex )
            {
                ExecutionLogProvider.LogError( ex );
            }
        }

        public ArticleInfoResponse GetArticleInfo( IEnumerable<ArticleInfoRequestArticle> articles )
        {
            this.VerifyCapability( DialogName.ArticleInfo );

            return this.DialogProvider.ArticleInfo.SendRequest( this.CreateArticleInfoRequest( articles ) );
        }

        public Task<ArticleInfoResponse> GetArticleInfoAsync( IEnumerable<ArticleInfoRequestArticle> articles )
        {
            return this.GetArticleInfoAsync( articles, CancellationToken.None );
        }

        public Task<ArticleInfoResponse> GetArticleInfoAsync( IEnumerable<ArticleInfoRequestArticle> articles, CancellationToken cancellationToken )
        {
            this.VerifyCapability( DialogName.ArticleInfo );

            return this.DialogProvider.ArticleInfo.SendRequestAsync(    this.CreateArticleInfoRequest( articles ),
                                                                        cancellationToken   );
        }

        public void SendOutputMessage(  OutputMessageDetails details,
                                        IEnumerable<OutputArticle> articles,
                                        IEnumerable<OutputBox> boxes )
        {
            this.VerifyCapability( DialogName.Output );

            this.DialogProvider.Output.SendMessage( this.CreateOutputMessage( details, articles, boxes ) );
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
            this.VerifyCapability( DialogName.Output );

            return this.DialogProvider.Output.SendMessageAsync( this.CreateOutputMessage( details, articles, boxes ),
                                                                cancellationToken   );
        }

        public void SendStockInfoMessage( IEnumerable<StockInfoArticle> articles )
        {
            this.VerifyCapability( DialogName.StockInfo );

            this.DialogProvider.StockInfo.SendMessage( this.CreateStockInfoMessage( articles ) );
        }

        public Task SendStockInfoMessageAsync( IEnumerable<StockInfoArticle> articles )
        {
            return this.SendStockInfoMessageAsync( articles, CancellationToken.None );
        }

        public Task SendStockInfoMessageAsync( IEnumerable<StockInfoArticle> articles, CancellationToken cancellationToken )
        {
            this.VerifyCapability( DialogName.StockInfo );

            return this.DialogProvider.StockInfo.SendMessageAsync( this.CreateStockInfoMessage( articles ), cancellationToken );
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
