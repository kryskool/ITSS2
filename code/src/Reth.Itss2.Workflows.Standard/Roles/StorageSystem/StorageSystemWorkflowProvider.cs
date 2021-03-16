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

using System;

using Reth.Itss2.Dialogs.Standard.Protocol;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Hello;
using Reth.Itss2.Dialogs.Standard.Protocol.Roles.StorageSystem;
using Reth.Itss2.Dialogs.Standard.Serialization;
using Reth.Itss2.Workflows.Standard.Messages.ArticleInfo.Active;
using Reth.Itss2.Workflows.Standard.Messages.ArticleMasterSet.Reactive;
using Reth.Itss2.Workflows.Standard.Messages.Hello.Reactive;
using Reth.Itss2.Workflows.Standard.Messages.InitiateInput.Reactive;
using Reth.Itss2.Workflows.Standard.Messages.Input.Active;
using Reth.Itss2.Workflows.Standard.Messages.KeepAlive;
using Reth.Itss2.Workflows.Standard.Messages.Output.Reactive;
using Reth.Itss2.Workflows.Standard.Messages.OutputInfo.Reactive;
using Reth.Itss2.Workflows.Standard.Messages.Status.Reactive;
using Reth.Itss2.Workflows.Standard.Messages.StockDeliveryInfo.Reactive;
using Reth.Itss2.Workflows.Standard.Messages.StockDeliverySet.Reactive;
using Reth.Itss2.Workflows.Standard.Messages.StockInfo.Reactive;
using Reth.Itss2.Workflows.Standard.Messages.StockLocationInfo.Reactive;
using Reth.Itss2.Workflows.Standard.Messages.TaskCancelOutput.Reactive;
using Reth.Itss2.Workflows.Standard.Messages.Unprocessed;

namespace Reth.Itss2.Workflows.Standard.Roles.StorageSystem
{
    public class StorageSystemWorkflowProvider:WorkflowProvider, IStorageSystemWorkflowProvider
    {
        public StorageSystemWorkflowProvider(   ISerializationProvider serializationProvider,
                                                Subscriber localSubscriber )
        :
            this( serializationProvider, localSubscriber, new StorageSystemDialogProvider() )
        {
        }

        protected StorageSystemWorkflowProvider(    ISerializationProvider serializationProvider,
                                                    Subscriber localSubscriber,
                                                    IStorageSystemDialogProvider dialogProvider )
        :
            base( serializationProvider, localSubscriber, dialogProvider )
        {
            this.ArticleInfoWorkflow = new ArticleInfoWorkflow( this, dialogProvider.ArticleInfoDialog );
            this.ArticleMasterSetWorkflow = new ArticleMasterSetWorkflow( this, dialogProvider.ArticleMasterSetDialog );
            this.HelloWorkflow = new HelloWorkflow( this, dialogProvider.HelloDialog );
            this.InitiateInputWorkflow = new InitiateInputWorkflow( this, dialogProvider.InitiateInputDialog );
            this.InputWorkflow = new InputWorkflow( this, dialogProvider.InputDialog );
            this.KeepAliveWorkflow = new KeepAliveWorkflow( this, dialogProvider.KeepAliveDialog );
            this.OutputWorkflow = new OutputWorkflow( this, dialogProvider.OutputDialog );
            this.OutputInfoWorkflow = new OutputInfoWorkflow( this, dialogProvider.OutputInfoDialog );
            this.StatusWorkflow = new StatusWorkflow( this, dialogProvider.StatusDialog );
            this.StockDeliveryInfoWorkflow = new StockDeliveryInfoWorkflow( this, dialogProvider.StockDeliveryInfoDialog );
            this.StockDeliverySetWorkflow = new StockDeliverySetWorkflow( this, dialogProvider.StockDeliverySetDialog );
            this.StockInfoWorkflow = new StockInfoWorkflow( this, dialogProvider.StockInfoDialog );
            this.StockLocationInfoWorkflow = new StockLocationInfoWorkflow( this, dialogProvider.StockLocationInfoDialog );
            this.TaskCancelOutputWorkflow = new TaskCancelOutputWorkflow( this, dialogProvider.TaskCancelOutputDialog );
            this.UnprocessedWorkflow = new UnprocessedWorkflow( this, dialogProvider.UnprocessedDialog );

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

            this.HelloWorkflow.RequestAccepted += this.OnHelloRequestAccepted;
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

        private void OnHelloRequestAccepted( Object sender, MessageReceivedEventArgs<HelloRequest> e )
        {
            this.SetRemoteSubscriber( e.Message.Subscriber );
        }

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
