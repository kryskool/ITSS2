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
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.HelloDialog;
using Reth.Itss2.Dialogs.Standard.Protocol.Roles.StorageSystem;
using Reth.Itss2.Dialogs.Standard.Serialization;
using Reth.Itss2.Workflows.Standard.StorageSystem.ArticleInfoDialog;
using Reth.Itss2.Workflows.Standard.StorageSystem.ArticleMasterSetDialog;
using Reth.Itss2.Workflows.Standard.StorageSystem.HelloDialog;
using Reth.Itss2.Workflows.Standard.StorageSystem.InitiateInputDialog;
using Reth.Itss2.Workflows.Standard.StorageSystem.InputDialog;
using Reth.Itss2.Workflows.Standard.StorageSystem.KeepAliveDialog;
using Reth.Itss2.Workflows.Standard.StorageSystem.OutputDialog;
using Reth.Itss2.Workflows.Standard.StorageSystem.OutputInfoDialog;
using Reth.Itss2.Workflows.Standard.StorageSystem.StatusDialog;
using Reth.Itss2.Workflows.Standard.StorageSystem.StockDeliveryInfoDialog;
using Reth.Itss2.Workflows.Standard.StorageSystem.StockDeliverySetDialog;
using Reth.Itss2.Workflows.Standard.StorageSystem.StockInfoDialog;
using Reth.Itss2.Workflows.Standard.StorageSystem.StockLocationInfoDialog;
using Reth.Itss2.Workflows.Standard.StorageSystem.TaskCancelOutputDialog;
using Reth.Itss2.Workflows.Standard.StorageSystem.UnprocessedDialog;

namespace Reth.Itss2.Workflows.Standard.StorageSystem
{
    public class StorageSystemWorkflowProvider:IStorageSystemWorkflowProvider
    {
        public event EventHandler<MessageProcessingErrorEventArgs>? MessageProcessingError;

        protected bool isDisposed;

        private SubscriberInfo subscriberInfo;

        public StorageSystemWorkflowProvider(   ISerializationProvider serializationProvider,
                                                Subscriber localSubscriber )
        :
            this( serializationProvider, localSubscriber, new StorageSystemDialogProvider() )
        {
        }

        protected StorageSystemWorkflowProvider(    ISerializationProvider serializationProvider,
                                                    Subscriber localSubscriber,
                                                    IStorageSystemDialogProvider dialogProvider )
        {
            this.SerializationProvider = serializationProvider;
            this.DialogProvider = dialogProvider;

            this.subscriberInfo = new SubscriberInfo( localSubscriber );

            dialogProvider.MessageProcessingError += this.OnMessageProcessingError;

            this.ArticleInfoWorkflow = new ArticleInfoWorkflow( this );
            this.ArticleMasterSetWorkflow = new ArticleMasterSetWorkflow( this );
            this.HelloWorkflow = new HelloWorkflow( this );
            this.InitiateInputWorkflow = new InitiateInputWorkflow( this );
            this.InputWorkflow = new InputWorkflow( this );
            this.KeepAliveWorkflow = new KeepAliveWorkflow( this );
            this.OutputWorkflow = new OutputWorkflow( this );
            this.OutputInfoWorkflow = new OutputInfoWorkflow( this );
            this.StatusWorkflow = new StatusWorkflow( this );
            this.StockDeliveryInfoWorkflow = new StockDeliveryInfoWorkflow( this );
            this.StockDeliverySetWorkflow = new StockDeliverySetWorkflow( this );
            this.StockInfoWorkflow = new StockInfoWorkflow( this );
            this.StockLocationInfoWorkflow = new StockLocationInfoWorkflow( this );
            this.TaskCancelOutputWorkflow = new TaskCancelOutputWorkflow( this );
            this.UnprocessedWorkflow = new UnprocessedWorkflow( this );

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

        ~StorageSystemWorkflowProvider()
        {
            this.Dispose( false );
        }

        private Object SyncRoot
        {
            get;
        } = new Object();

        public IStorageSystemDialogProvider DialogProvider
        {
            get;
        }

        public ISerializationProvider SerializationProvider
        {
            get;
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

        private void OnHelloRequestAccepted( Object sender, MessageReceivedEventArgs e )
        {
            lock( this.SyncRoot )
            {
                Subscriber localSubscriber = this.subscriberInfo.LocalSubscriber;
                Subscriber remoteSubscriber = ( ( HelloRequest )e.Message ).Subscriber;

                this.subscriberInfo = new SubscriberInfo( localSubscriber, remoteSubscriber );
            }
        }

        protected virtual void OnMessageProcessingError( Object sender, MessageProcessingErrorEventArgs e )
        {
            this.MessageProcessingError?.Invoke( this, e );
        }

        public SubscriberInfo GetSubscriberInfo()
        {
            lock( this.SyncRoot )
            {
                return this.subscriberInfo;
            }
        }

        public void Dispose()
        {
            this.Dispose( true );

            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
            if( this.isDisposed == false )
            {
                if( disposing == true )
                {
                    this.DialogProvider.Dispose();
                    this.SerializationProvider.Dispose();

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

                this.isDisposed = true;
            }
        }
    }
}
