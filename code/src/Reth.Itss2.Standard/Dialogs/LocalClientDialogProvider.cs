using System.Collections.Generic;

using Reth.Itss2.Standard.Serialization;
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
using Reth.Protocols;
using Reth.Protocols.Diagnostics;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs
{
    public class LocalClientDialogProvider:DialogProviderBase, ILocalClientDialogProvider
    {
        private volatile bool isDisposed;

        public LocalClientDialogProvider(   IProtocolProvider protocolProvider,
                                            IInteractionLog interactionLog  )
        :
            this(   protocolProvider,
                    interactionLog,
                    DialogName.GetAvailableNames()  )
        {
        }

        public LocalClientDialogProvider(   IProtocolProvider protocolProvider,
                                            IInteractionLog interactionLog,
                                            IEnumerable<IDialogName> supportedDialogs   )
        :
            base(   new MessageInitializer( typeof( HelloRequest ) ),
                    new MessageInitializer( typeof( HelloResponse ) ),
                    protocolProvider,
                    interactionLog,
                    supportedDialogs    )
        {
            this.ArticleInfo = new ArticleInfoClientDialog( this );
            this.ArticleMasterSet = new ArticleMasterSetClientDialog( this );
            this.Hello = new HelloClientDialog( this );
            this.InitiateInput = new InitiateInputClientDialog( this );
            this.Input = new InputClientDialog( this );
            this.KeepAlive = new KeepAliveDialog( this );
            this.OutputInfo = new OutputInfoClientDialog( this );
            this.Output = new OutputClientDialog( this );
            this.Status = new StatusClientDialog( this );
            this.StockDeliveryInfo = new StockDeliveryInfoClientDialog( this );
            this.StockDeliverySet = new StockDeliverySetClientDialog( this );
            this.StockInfo = new StockInfoClientDialog( this );
            this.StockLocationInfo = new StockLocationInfoClientDialog( this );
            this.TaskCancelOutput = new TaskCancelOutputClientDialog( this );
            this.Unprocessed = new UnprocessedDialog( this );
        }

        public IArticleInfoClientDialog ArticleInfo{ get; }
        public IArticleMasterSetClientDialog ArticleMasterSet{ get; }
        public IHelloClientDialog Hello{ get; }
        public IInitiateInputClientDialog InitiateInput{ get; }
        public IInputClientDialog Input{ get; }
        public IKeepAliveClientDialog KeepAlive{ get; }
        public IOutputInfoClientDialog OutputInfo{ get; }
        public IOutputClientDialog Output{ get; }
        public IStatusClientDialog Status{ get; }
        public IStockDeliveryInfoClientDialog StockDeliveryInfo{ get; }
        public IStockDeliverySetClientDialog StockDeliverySet{ get; }
        public IStockInfoClientDialog StockInfo{ get; }
        public IStockLocationInfoClientDialog StockLocationInfo{ get; }
        public ITaskCancelOutputClientDialog TaskCancelOutput{ get; }
        public IUnprocessedClientDialog Unprocessed{ get; }

        protected override void Dispose( bool disposing )
        {
            ExecutionLogProvider.LogInformation( "Disposing local client dialog provider." );

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
