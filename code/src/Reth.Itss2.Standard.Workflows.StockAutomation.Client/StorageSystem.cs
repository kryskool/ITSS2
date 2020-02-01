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
using Reth.Itss2.Standard.Dialogs.Storage.Input;
using Reth.Itss2.Standard.Dialogs.Storage.OutputInfo;
using Reth.Itss2.Standard.Dialogs.Storage.Status;
using Reth.Itss2.Standard.Dialogs.Storage.StockInfo;
using Reth.Itss2.Standard.Dialogs.Storage.StockLocationInfo;
using Reth.Itss2.Standard.Dialogs.Storage.TaskCancelOutput;
using Reth.Itss2.Standard.Workflows.StockAutomation.Client.Processing.InitiateInput;
using Reth.Itss2.Standard.Workflows.StockAutomation.Client.Processing.Input;
using Reth.Itss2.Standard.Workflows.StockAutomation.Client.Processing.Output;
using Reth.Protocols;
using Reth.Protocols.Diagnostics;
using Reth.Protocols.Dialogs;
using Reth.Protocols.Extensions.EventArgsExtensions;
using Reth.Protocols.Extensions.ObjectExtensions;
using Reth.Protocols.Transfer;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Client
{
    public class StorageSystem:Reth.Itss2.Standard.Workflows.StorageSystem, IStorageSystem
    {
        public event EventHandler<InputRequestReceivedEventArgs> InputRequestReceived;

        public event EventHandler<MessageReceivedEventArgs> OutputMessageReceived;
        public event EventHandler<MessageReceivedEventArgs> StockInfoMessageReceived;

        private bool isConnected;
        private volatile bool isDisposed;

        public StorageSystem(   SubscriberInfo subscriberInfo,
                                ILocalMessageClient messageClient,
                                ILocalClientDialogProvider dialogProvider   )
        {
            this.Initialize(    subscriberInfo,
                                messageClient,
                                dialogProvider,
                                dialogProvider.GetSupportedDialogs()    );
        }

        public StorageSystem(   SubscriberInfo subscriberInfo,
                                ILocalMessageClient messageClient,
                                ILocalClientDialogProvider dialogProvider,
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

        public ILocalMessageClient MessageClient
        {
            get; private set;
        }
        
        public ILocalClientDialogProvider DialogProvider
        {
            get; private set;
        }

        private Object SyncRoot
        {
            get;
        } = new Object();

        private bool IsConnected
        {
            get
            {
                lock( this.SyncRoot )
                {
                    return this.isConnected;
                }
            }

            set
            {
                lock( this.SyncRoot )
                {
                    this.isConnected = value;
                }
            }
        }

        public Func<IStorageSystem, ArticleInfoRequest, ArticleInfoResponse> ArticleInfoRequestReceivedCallback
        {
            get; set;
        }

        public void Initialize( SubscriberInfo subscriberInfo,
                                ILocalMessageClient messageClient,
                                ILocalClientDialogProvider dialogProvider,
                                IEnumerable<IDialogName> supportedDialogs   )
        {
            subscriberInfo.ThrowIfNull();
            messageClient.ThrowIfNull();
            dialogProvider.ThrowIfNull();

            ExecutionLogProvider.LogInformation( "Initializing client storage system." );

            UnhandledMessageHandler.UseCallback( this.OnMessageUnhandled );

            this.MessageClient = messageClient;
            this.MessageClient.Disconnected += base.OnMessageClientDisconnected;

            this.DialogProvider = dialogProvider;
            this.DialogProvider.ArticleInfo.RequestReceived += this.ArticleInfo_RequestReceived;
            this.DialogProvider.Input.RequestReceived += this.Input_RequestReceived;
            this.DialogProvider.Output.MessageReceived += this.Output_MessageReceived;
            this.DialogProvider.StockInfo.MessageReceived += this.StockInfo_MessageReceived;
            this.DialogProvider.Unprocessed.MessageReceived += base.OnUnprocessedMessageReceived;

            this.LocalSubscriber = new Subscriber(  subscriberInfo,
                                                    supportedDialogs   );
        }

        private StatusRequest CreateStatusRequest( bool includeDetails )
        {
            return new StatusRequest( MessageId.NewId(),
                                      this.LocalSubscriber.Id,
                                      this.GetRemoteSubscriberId(),
                                      includeDetails    );
        }

        private StockLocationInfoRequest CreateStockLocationInfoRequest()
        {
            return new StockLocationInfoRequest(    MessageId.NewId(),
                                                    this.LocalSubscriber.Id,
                                                    this.GetRemoteSubscriberId()    );
        }

        private OutputInfoRequest CreateOutputInfoRequest( MessageId outputId, bool includeTaskDetails )
        {
            return new OutputInfoRequest(   MessageId.NewId(),
                                            this.LocalSubscriber.Id,
                                            this.GetRemoteSubscriberId(),
                                            new OutputInfoRequestTask( outputId ),
                                            includeTaskDetails  );
        }

        private StockDeliveryInfoRequest CreateStockDeliveryInfoRequest( String deliveryNumber, bool includeTaskDetails )
        {
            return new StockDeliveryInfoRequest(    MessageId.NewId(),
                                                    this.LocalSubscriber.Id,
                                                    this.GetRemoteSubscriberId(),
                                                    new StockDeliveryInfoRequestTask( deliveryNumber ),
                                                    includeTaskDetails  );
        }

        private StockInfoRequest CreateStockInfoRequest(    bool includePacks,
                                                            bool includeArticleDetails,
                                                            IEnumerable<StockInfoRequestCriteria> criterias )
        {
            return new StockInfoRequest(    MessageId.NewId(),
                                            this.LocalSubscriber.Id,
                                            this.GetRemoteSubscriberId(),
                                            includePacks,
                                            includeArticleDetails,
                                            criterias   );
        }

        private ArticleMasterSetRequest CreateArticleMasterSetRequest( IEnumerable<ArticleMasterSetArticle> articles )
        {
            return new ArticleMasterSetRequest( MessageId.NewId(),
                                                this.LocalSubscriber.Id,
                                                this.GetRemoteSubscriberId(),
                                                articles   );
        }

        private StockDeliverySetRequest CreateStockDeliverySetRequest( IEnumerable<StockDelivery> deliveries )
        {
            return new StockDeliverySetRequest( MessageId.NewId(),
                                                this.LocalSubscriber.Id,
                                                this.GetRemoteSubscriberId(),
                                                deliveries   );
        }

        private TaskCancelOutputRequest CreateTaskCancelOutputRequest( MessageId outputId )
        {
            return new TaskCancelOutputRequest( MessageId.NewId(),
                                                this.LocalSubscriber.Id,
                                                this.GetRemoteSubscriberId(),
                                                new TaskCancelOutputRequestTask( outputId )  );
        }
        
        private void ArticleInfo_RequestReceived( Object sender, MessageReceivedEventArgs e )
        {
            e.IsHandled = true;

            try
            {
                ArticleInfoResponse response = this.ArticleInfoRequestReceivedCallback?.Invoke( this, ( ArticleInfoRequest )( e.Message ) );

                this.DialogProvider.ArticleInfo.SendResponse( response );
            }catch( Exception ex )
            {
                ExecutionLogProvider.LogError( ex );
            }
        }

        private void Input_RequestReceived( Object sender, MessageReceivedEventArgs e )
        {
            e.IsHandled = true;

            try
            {
                InputRequest request = ( InputRequest )( e.Message );

                InputProcess process = new InputProcess( this, request );

                this.InputRequestReceived?.Invoke( this, new InputRequestReceivedEventArgs( request, process ) );
            }catch( Exception ex )
            {
                ExecutionLogProvider.LogError( ex );
            }
        }

        private void Output_MessageReceived( Object sender, MessageReceivedEventArgs e )
        {
            try
            {
                this.OutputMessageReceived?.Invoke( this, e );
            }catch( Exception ex )
            {
                ExecutionLogProvider.LogError( ex );
            }
        }

        private void StockInfo_MessageReceived( Object sender, MessageReceivedEventArgs e )
        {
            try
            {
                this.StockInfoMessageReceived?.SafeInvoke( this, e );
            }catch( Exception ex )
            {
                ExecutionLogProvider.LogError( ex );
            }
        }

        public void Connect()
        {
            if( this.IsConnected == false )
            {
                this.MessageClient.Connect();

                HelloRequest request = new HelloRequest( MessageId.NewId(), this.LocalSubscriber );

                HelloResponse response = this.DialogProvider.Hello.SendRequest( request );

                this.RemoteSubscriber = response.Subscriber;

                Capability[] remoteCapabilities = this.RemoteSubscriber.GetCapabilities();

                foreach( Capability remoteCapability in remoteCapabilities )
                {
                    this.RemoteCapabilities.Add( remoteCapability.Name );
                }

                this.IsConnected = true;
            }
        }

        public Task ConnectAsync()
        {
            return Task.Run(    () =>
                                {
                                    this.Connect();
                                }   );
        }

        public void Disconnect()
        {
            if( this.IsConnected == true )
            {
                this.MessageClient.Disconnect();

                this.IsConnected = false;
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

        public StatusResponse GetStatus( bool includeDetails )
        {
            this.VerifyCapability( DialogName.Status );

            return this.DialogProvider.Status.SendRequest( this.CreateStatusRequest( includeDetails ) );
        }

        public Task<StatusResponse> GetStatusAsync( bool includeDetails )
        {
            return this.GetStatusAsync( includeDetails, CancellationToken.None );
        }

        public Task<StatusResponse> GetStatusAsync( bool includeDetails, CancellationToken cancellationToken )
        {
            this.VerifyCapability( DialogName.Status );

            return this.DialogProvider.Status.SendRequestAsync( this.CreateStatusRequest( includeDetails ), cancellationToken );
        }

        public StockLocationInfoResponse GetStockLocationInfo()
        {
            this.VerifyCapability( DialogName.StockLocationInfo );
            
            return this.DialogProvider.StockLocationInfo.SendRequest( this.CreateStockLocationInfoRequest() );
        }

        public Task<StockLocationInfoResponse> GetStockLocationInfoAsync()
        {
            return this.GetStockLocationInfoAsync( CancellationToken.None );
        }

        public Task<StockLocationInfoResponse> GetStockLocationInfoAsync( CancellationToken cancellationToken )
        {
            this.VerifyCapability( DialogName.StockLocationInfo );

            return this.DialogProvider.StockLocationInfo.SendRequestAsync( this.CreateStockLocationInfoRequest(), cancellationToken );
        }

        public OutputInfoResponse GetOutputInfo( MessageId outputId, bool includeTaskDetails )
        {
            this.VerifyCapability( DialogName.OutputInfo );

            return this.DialogProvider.OutputInfo.SendRequest( this.CreateOutputInfoRequest( outputId, includeTaskDetails ) );
        }

        public Task<OutputInfoResponse> GetOutputInfoAsync( MessageId outputId, bool includeTaskDetails )
        {
            return this.GetOutputInfoAsync( outputId, includeTaskDetails, CancellationToken.None );
        }

        public Task<OutputInfoResponse> GetOutputInfoAsync( MessageId outputId, bool includeTaskDetails, CancellationToken cancellationToken )
        {
            this.VerifyCapability( DialogName.OutputInfo );

            return this.DialogProvider.OutputInfo.SendRequestAsync( this.CreateOutputInfoRequest( outputId, includeTaskDetails ),
                                                                    cancellationToken   );
        }

        public StockDeliveryInfoResponse GetStockDeliveryInfo( String deliveryNumber, bool includeTaskDetails )
        {
            this.VerifyCapability( DialogName.StockDeliveryInfo );

            return this.DialogProvider.StockDeliveryInfo.SendRequest( this.CreateStockDeliveryInfoRequest( deliveryNumber, includeTaskDetails ) );
        }

        public Task<StockDeliveryInfoResponse> GetStockDeliveryInfoAsync( String deliveryNumber, bool includeTaskDetails )
        {
            return this.GetStockDeliveryInfoAsync( deliveryNumber, includeTaskDetails, CancellationToken.None );
        }

        public Task<StockDeliveryInfoResponse> GetStockDeliveryInfoAsync( String deliveryNumber, bool includeTaskDetails, CancellationToken cancellationToken )
        {
            this.VerifyCapability( DialogName.StockDeliveryInfo );

            return this.DialogProvider.StockDeliveryInfo.SendRequestAsync(  this.CreateStockDeliveryInfoRequest( deliveryNumber, includeTaskDetails ),
                                                                            cancellationToken   );
        }

        public StockInfoResponse GetStockInfo(  bool includePacks,
                                                bool includeArticleDetails,
                                                IEnumerable<StockInfoRequestCriteria> criterias )
        {
            this.VerifyCapability( DialogName.StockInfo );

            return this.DialogProvider.StockInfo.SendRequest( this.CreateStockInfoRequest( includePacks, includeArticleDetails, criterias ) );
        }

        public Task<StockInfoResponse> GetStockInfoAsync(   bool includePacks,
                                                            bool includeArticleDetails,
                                                            IEnumerable<StockInfoRequestCriteria> criterias )
        {
            return this.GetStockInfoAsync( includePacks, includeArticleDetails, criterias, CancellationToken.None );
        }

        public Task<StockInfoResponse> GetStockInfoAsync(   bool includePacks,
                                                            bool includeArticleDetails,
                                                            IEnumerable<StockInfoRequestCriteria> criterias,
                                                            CancellationToken cancellationToken )
        {
            this.VerifyCapability( DialogName.StockInfo );

            return this.DialogProvider.StockInfo.SendRequestAsync(  this.CreateStockInfoRequest( includePacks, includeArticleDetails, criterias ),
                                                                    cancellationToken   );
        }

        public ArticleMasterSetResponse SetArticleMaster( IEnumerable<ArticleMasterSetArticle> articles )
        {
            this.VerifyCapability( DialogName.ArticleMasterSet );

            return this.DialogProvider.ArticleMasterSet.SendRequest( this.CreateArticleMasterSetRequest( articles ) );
        }

        public Task<ArticleMasterSetResponse> SetArticleMasterAsync( IEnumerable<ArticleMasterSetArticle> articles )
        {
            return this.SetArticleMasterAsync( articles, CancellationToken.None );
        }

        public Task<ArticleMasterSetResponse> SetArticleMasterAsync( IEnumerable<ArticleMasterSetArticle> articles, CancellationToken cancellationToken )
        {
            this.VerifyCapability( DialogName.ArticleMasterSet );

            return this.DialogProvider.ArticleMasterSet.SendRequestAsync( this.CreateArticleMasterSetRequest( articles ), cancellationToken );
        }

        public StockDeliverySetResponse SetStockDelivery( IEnumerable<StockDelivery> deliveries )
        {
            this.VerifyCapability( DialogName.StockDeliverySet );

            return this.DialogProvider.StockDeliverySet.SendRequest( this.CreateStockDeliverySetRequest( deliveries ) );
        }

        public Task<StockDeliverySetResponse> SetStockDeliveryAsync( IEnumerable<StockDelivery> deliveries )
        {
            return this.SetStockDeliveryAsync( deliveries, CancellationToken.None );
        }

        public Task<StockDeliverySetResponse> SetStockDeliveryAsync( IEnumerable<StockDelivery> deliveries, CancellationToken cancellationToken )
        {
            this.VerifyCapability( DialogName.StockDeliverySet );

            return this.DialogProvider.StockDeliverySet.SendRequestAsync(   this.CreateStockDeliverySetRequest( deliveries ),
                                                                            cancellationToken   );
        }

        public TaskCancelOutputResponse CancelOutput( MessageId outputId )
        {
            this.VerifyCapability( DialogName.TaskCancelOutput );

            return this.DialogProvider.TaskCancelOutput.SendRequest( this.CreateTaskCancelOutputRequest( outputId ) );
        }

        public Task<TaskCancelOutputResponse> CancelOutputAsync( MessageId outputId )
        {
            return this.CancelOutputAsync( outputId, CancellationToken.None );
        }

        public Task<TaskCancelOutputResponse> CancelOutputAsync( MessageId outputId, CancellationToken cancellationToken )
        {
            this.VerifyCapability( DialogName.TaskCancelOutput );

            return this.DialogProvider.TaskCancelOutput.SendRequestAsync(   this.CreateTaskCancelOutputRequest( outputId ),
                                                                            cancellationToken   );
        }

        public IInitiateInputProcess CreateInitiateInputProcess()
        {
            return new InitiateInputProcess( this );
        }

        public IOutputProcess CreateOutputProcess()
        {
            return new OutputProcess( this );
        }

        public override String ToString()
        {
            return this.LocalSubscriber.ToString();
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
                this.MessageClient.Disconnected -= base.OnMessageClientDisconnected;

                this.DialogProvider.ArticleInfo.RequestReceived -= this.ArticleInfo_RequestReceived;
                this.DialogProvider.Input.RequestReceived -= this.Input_RequestReceived;
                this.DialogProvider.Output.MessageReceived -= this.Output_MessageReceived;
                this.DialogProvider.StockInfo.MessageReceived -= this.StockInfo_MessageReceived;
                this.DialogProvider.Unprocessed.MessageReceived -= base.OnUnprocessedMessageReceived;

                this.RemoteCapabilities.Clear();

                if( disposing == true )
                {                    
                    this.MessageClient.Dispose();
                    this.DialogProvider.Dispose();
                }

                this.isDisposed = true;
            }
        }
    }
}
