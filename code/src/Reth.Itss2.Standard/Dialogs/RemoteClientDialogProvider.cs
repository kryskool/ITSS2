using System.Collections.Generic;

using Reth.Itss2.Standard.Dialogs.ArticleData.ArticleInfo;
using Reth.Itss2.Standard.Dialogs.ArticleData.ArticleMasterSet;
using Reth.Itss2.Standard.Dialogs.ArticleData.StockDeliveryInfo;
using Reth.Itss2.Standard.Dialogs.ArticleData.StockDeliverySet;
using Reth.Itss2.Standard.Dialogs.General.Hello;
using Reth.Itss2.Standard.Dialogs.General.KeepAlive;
using Reth.Itss2.Standard.Dialogs.General.Unprocessed;
using Reth.Itss2.Standard.Dialogs.Storage.InitiateInput;
using Reth.Itss2.Standard.Dialogs.Storage.Input;
using Reth.Itss2.Standard.Dialogs.Storage.Output;
using Reth.Itss2.Standard.Dialogs.Storage.OutputInfo;
using Reth.Itss2.Standard.Dialogs.Storage.Status;
using Reth.Itss2.Standard.Dialogs.Storage.StockInfo;
using Reth.Itss2.Standard.Dialogs.Storage.StockLocationInfo;
using Reth.Itss2.Standard.Dialogs.Storage.TaskCancelOutput;
using Reth.Itss2.Standard.Serialization;
using Reth.Protocols;
using Reth.Protocols.Diagnostics;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs
{
    public class RemoteClientDialogProvider:DialogProviderBase, IRemoteClientDialogProvider
    {
        private volatile bool isDisposed;

        public RemoteClientDialogProvider(  IProtocolProvider protocolProvider,
                                            IInteractionLog interactionLog  )
        :
            this(   protocolProvider,
                    interactionLog,
                    DialogName.GetAvailableNames()    )
        {
        }

        public RemoteClientDialogProvider(  IProtocolProvider protocolProvider,
                                            IInteractionLog interactionLog,
                                            IEnumerable<IDialogName> supportedDialogs   )
        :
            base(   new MessageInitializer( typeof( HelloResponse ) ),
                    new MessageInitializer( typeof( HelloRequest ) ),
                    protocolProvider,
                    interactionLog,
                    supportedDialogs    )
        {
            this.ArticleInfo = new ArticleInfoServerDialog( this );
            this.ArticleMasterSet = new ArticleMasterSetServerDialog( this );
            this.Hello = new HelloServerDialog( this );
            this.InitiateInput = new InitiateInputServerDialog( this );
            this.Input = new InputServerDialog( this );
            this.KeepAlive = new KeepAliveDialog( this );
            this.OutputInfo = new OutputInfoServerDialog( this );
            this.Output = new OutputServerDialog( this );
            this.Status = new StatusServerDialog( this );
            this.StockDeliveryInfo = new StockDeliveryInfoServerDialog( this );
            this.StockDeliverySet = new StockDeliverySetServerDialog( this );
            this.StockInfo = new StockInfoServerDialog( this );
            this.StockLocationInfo = new StockLocationInfoServerDialog( this );
            this.TaskCancelOutput = new TaskCancelOutputServerDialog( this );
            this.Unprocessed = new UnprocessedDialog( this );
        }

        public IArticleInfoServerDialog ArticleInfo{ get; }
        public IArticleMasterSetServerDialog ArticleMasterSet{ get; }
        public IHelloServerDialog Hello{ get; }
        public IInitiateInputServerDialog InitiateInput{ get; }
        public IInputServerDialog Input{ get; }
        public IKeepAliveServerDialog KeepAlive{ get; }
        public IOutputInfoServerDialog OutputInfo{ get; }
        public IOutputServerDialog Output{ get; }
        public IStatusServerDialog Status{ get; }
        public IStockDeliveryInfoServerDialog StockDeliveryInfo{ get; }
        public IStockDeliverySetServerDialog StockDeliverySet{ get; }
        public IStockInfoServerDialog StockInfo{ get; }
        public IStockLocationInfoServerDialog StockLocationInfo{ get; }
        public ITaskCancelOutputServerDialog TaskCancelOutput{ get; }
        public override IUnprocessedDialog Unprocessed{ get; }

        protected override void Dispose( bool disposing )
        {
            ExecutionLogProvider.LogInformation( "Disposing remote client dialog provider." );

            base.Dispose( disposing );

            if( this.isDisposed == false )
            {
                if( disposing == true )
                {
                    this.ArticleInfo.Dispose();
                    this.ArticleMasterSet.Dispose();
                    this.Hello.Dispose();
                    this.InitiateInput.Dispose();
                    this.Input.Dispose();
                    this.KeepAlive.Dispose();
                    this.OutputInfo.Dispose();
                    this.Output.Dispose();
                    this.Status.Dispose();
                    this.StockDeliveryInfo.Dispose();
                    this.StockDeliverySet.Dispose();
                    this.StockInfo.Dispose();
                    this.StockLocationInfo.Dispose();
                    this.TaskCancelOutput.Dispose();
                    this.Unprocessed.Dispose();
                }

                this.isDisposed = true;
            }
        }
    }
}
