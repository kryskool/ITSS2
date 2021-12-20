// Implementation of the WWKS2 protocol.
// Copyright (C) 2020  Thomas Reth

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.

// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.Standard.Protocol.Roles.PharmacyInventorySystem;
using Reth.Itss2.Workflows.Standard.Messages.ArticleInfo.Reactive;
using Reth.Itss2.Workflows.Standard.Messages.ArticleMasterSet.Active;
using Reth.Itss2.Workflows.Standard.Messages.Hello.Active;
using Reth.Itss2.Workflows.Standard.Messages.InitiateInput.Active;
using Reth.Itss2.Workflows.Standard.Messages.Input.Reactive;
using Reth.Itss2.Workflows.Standard.Messages.KeepAlive;
using Reth.Itss2.Workflows.Standard.Messages.Output.Active;
using Reth.Itss2.Workflows.Standard.Messages.OutputInfo.Active;
using Reth.Itss2.Workflows.Standard.Messages.Status.Active;
using Reth.Itss2.Workflows.Standard.Messages.StockDeliveryInfo.Active;
using Reth.Itss2.Workflows.Standard.Messages.StockDeliverySet.Active;
using Reth.Itss2.Workflows.Standard.Messages.StockInfo.Active;
using Reth.Itss2.Workflows.Standard.Messages.StockLocationInfo.Active;
using Reth.Itss2.Workflows.Standard.Messages.TaskCancelOutput.Active;
using Reth.Itss2.Workflows.Standard.Messages.Unprocessed;
using Reth.Itss2.Serialization;

namespace Reth.Itss2.Workflows.Standard.Roles.PharmacyInventorySystem
{
    public class PharmacyInventorySystemWorkflowProvider:WorkflowProvider<IPharmacyInventorySystemDialogProvider>, IPharmacyInventorySystemWorkflowProvider
    {
        public PharmacyInventorySystemWorkflowProvider( ISerializationProvider serializationProvider,
                                                        Subscriber localSubscriber )
        :
            this( serializationProvider, localSubscriber, new PharmacyInventorySystemDialogProvider() )
        {
        }

        protected PharmacyInventorySystemWorkflowProvider(  ISerializationProvider serializationProvider,
                                                            Subscriber localSubscriber,
                                                            IPharmacyInventorySystemDialogProvider dialogProvider )
        :
            base( dialogProvider )
        {
            Subscription subscription = new Subscription( localSubscriber );

            this.ArticleInfoWorkflow = new ArticleInfoWorkflow( dialogProvider.ArticleInfoDialog, subscription );
            this.ArticleMasterSetWorkflow = new ArticleMasterSetWorkflow( dialogProvider.ArticleMasterSetDialog, subscription );
            this.HelloWorkflow = new HelloWorkflow( dialogProvider.HelloDialog, subscription, dialogProvider, serializationProvider );
            this.InitiateInputWorkflow = new InitiateInputWorkflow( dialogProvider.InitiateInputDialog, subscription );
            this.InputWorkflow = new InputWorkflow( dialogProvider.InputDialog, subscription );
            this.KeepAliveWorkflow = new KeepAliveWorkflow( dialogProvider.KeepAliveDialog, subscription );
            this.OutputWorkflow = new OutputWorkflow( dialogProvider.OutputDialog, subscription );
            this.OutputInfoWorkflow = new OutputInfoWorkflow( dialogProvider.OutputInfoDialog, subscription );
            this.StatusWorkflow = new StatusWorkflow( dialogProvider.StatusDialog, subscription );
            this.StockDeliveryInfoWorkflow = new StockDeliveryInfoWorkflow( dialogProvider.StockDeliveryInfoDialog, subscription );
            this.StockDeliverySetWorkflow = new StockDeliverySetWorkflow( dialogProvider.StockDeliverySetDialog, subscription );
            this.StockInfoWorkflow = new StockInfoWorkflow( dialogProvider.StockInfoDialog, subscription );
            this.StockLocationInfoWorkflow = new StockLocationInfoWorkflow( dialogProvider.StockLocationInfoDialog, subscription );
            this.TaskCancelOutputWorkflow = new TaskCancelOutputWorkflow( dialogProvider.TaskCancelOutputDialog, subscription );
            this.UnprocessedWorkflow = new UnprocessedWorkflow( dialogProvider.UnprocessedDialog, subscription, serializationProvider );

            this.ArticleInfoWorkflow.MessageProcessingError += this.OnMessageProcessingError;
            this.ArticleMasterSetWorkflow.MessageProcessingError += this.OnMessageProcessingError;
            this.HelloWorkflow.MessageProcessingError += this.OnMessageProcessingError;
            this.InitiateInputWorkflow.MessageProcessingError += this.OnMessageProcessingError;
            this.InputWorkflow.MessageProcessingError += this.OnMessageProcessingError;
            this.KeepAliveWorkflow.MessageProcessingError += this.OnMessageProcessingError;
            this.OutputWorkflow.MessageProcessingError += this.OnMessageProcessingError;
            this.OutputInfoWorkflow.MessageProcessingError += this.OnMessageProcessingError;
            this.StatusWorkflow.MessageProcessingError += this.OnMessageProcessingError;
            this.StockDeliveryInfoWorkflow.MessageProcessingError += this.OnMessageProcessingError;
            this.StockDeliverySetWorkflow.MessageProcessingError += this.OnMessageProcessingError;
            this.StockInfoWorkflow.MessageProcessingError += this.OnMessageProcessingError;
            this.StockLocationInfoWorkflow.MessageProcessingError += this.OnMessageProcessingError;
            this.TaskCancelOutputWorkflow.MessageProcessingError += this.OnMessageProcessingError;
            this.UnprocessedWorkflow.MessageProcessingError += this.OnMessageProcessingError;
        }

        public IArticleInfoWorkflow ArticleInfoWorkflow{ get; }
        public IArticleMasterSetWorkflow ArticleMasterSetWorkflow{ get; }
        public IHelloWorkflow HelloWorkflow{ get; }
        public IInitiateInputWorkflow InitiateInputWorkflow{ get; }
        public IInputWorkflow InputWorkflow{ get; }
        public IKeepAliveWorkflow KeepAliveWorkflow{ get; }
        public IOutputWorkflow OutputWorkflow{ get; }
        public IOutputInfoWorkflow OutputInfoWorkflow{ get; }
        public IStatusWorkflow StatusWorkflow{ get; }
        public IStockDeliveryInfoWorkflow StockDeliveryInfoWorkflow{ get; }
        public IStockDeliverySetWorkflow StockDeliverySetWorkflow{ get; }
        public IStockInfoWorkflow StockInfoWorkflow{ get; }
        public IStockLocationInfoWorkflow StockLocationInfoWorkflow{ get; }
        public ITaskCancelOutputWorkflow TaskCancelOutputWorkflow{ get; }
        public IUnprocessedWorkflow UnprocessedWorkflow{ get; }

        protected override void Dispose( bool disposing )
        {
            if( this.isDisposed == false )
            {
                if( disposing == true )
                {
                    this.ArticleInfoWorkflow.Dispose();
                    this.ArticleMasterSetWorkflow.Dispose();
                    this.HelloWorkflow.Dispose();
                    this.InitiateInputWorkflow.Dispose();
                    this.InputWorkflow.Dispose();
                    this.KeepAliveWorkflow.Dispose();
                    this.OutputWorkflow.Dispose();
                    this.OutputInfoWorkflow.Dispose();
                    this.StatusWorkflow.Dispose();
                    this.StockDeliveryInfoWorkflow.Dispose();
                    this.StockDeliverySetWorkflow.Dispose();
                    this.StockInfoWorkflow.Dispose();
                    this.StockLocationInfoWorkflow.Dispose();
                    this.TaskCancelOutputWorkflow.Dispose();
                    this.UnprocessedWorkflow.Dispose();
                }

                base.Dispose( disposing );

                this.isDisposed = true;
            }
        }
    }
}
