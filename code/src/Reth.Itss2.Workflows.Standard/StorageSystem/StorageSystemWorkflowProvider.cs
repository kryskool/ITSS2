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
using Reth.Itss2.Workflows.Standard.StorageSystem.ArticleMasterSetDialog;
using Reth.Itss2.Workflows.Standard.StorageSystem.HelloDialog;
using Reth.Itss2.Workflows.Standard.StorageSystem.StatusDialog;
using Reth.Itss2.Workflows.Standard.StorageSystem.StockInfoDialog;
using Reth.Itss2.Workflows.Standard.StorageSystem.UnprocessedDialog;

namespace Reth.Itss2.Workflows.Standard.StorageSystem
{
    public class StorageSystemWorkflowProvider:IStorageSystemWorkflowProvider
    {
        public event EventHandler<MessageProcessingErrorEventArgs>? MessageProcessingError;

        protected bool isDisposed;

        private Subscriber? remoteSubscriber;

        public StorageSystemWorkflowProvider( ISerializationProvider serializationProvider, Subscriber localSubscriber )
        {
            this.SerializationProvider = serializationProvider;
            this.LocalSubscriber = localSubscriber;
            this.DialogProvider = new StorageSystemDialogProvider();

            this.ArticleMasterSetWorkflow = new ArticleMasterSetWorkflow( this, this.DialogProvider, serializationProvider );
            this.HelloWorkflow = new HelloWorkflow( this, this.DialogProvider, serializationProvider );
            this.StatusWorkflow = new StatusWorkflow( this, this.DialogProvider, serializationProvider );
            this.StockInfoWorkflow = new StockInfoWorkflow( this, this.DialogProvider, serializationProvider );
            this.UnprocessedWorkflow = new UnprocessedWorkflow( this, this.DialogProvider, serializationProvider );

            this.ArticleMasterSetWorkflow.MessageProcessingError += this.OnMessageProcessingError;
            this.HelloWorkflow.MessageProcessingError += this.OnMessageProcessingError;
            this.StatusWorkflow.MessageProcessingError += this.OnMessageProcessingError;
            this.StockInfoWorkflow.MessageProcessingError += this.OnMessageProcessingError;
            this.UnprocessedWorkflow.MessageProcessingError += this.OnMessageProcessingError;

            this.HelloWorkflow.RequestAccepted += this.OnHelloRequestAccepted;

            this.DialogProvider.MessageProcessingError += this.OnMessageProcessingError;
        }

        protected StorageSystemWorkflowProvider(    ISerializationProvider serializationProvider,
                                                    Subscriber localSubscriber,
                                                    IStorageSystemDialogProvider dialogProvider )
        :
            this( serializationProvider, localSubscriber )
        {
            this.DialogProvider = dialogProvider;
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

        public Subscriber LocalSubscriber
        {
            get;
        }

        public Subscriber? RemoteSubscriber
        {
            get
            {
                lock( this.SyncRoot )
                {
                    return this.remoteSubscriber;
                }
            }

            private set
            {
                lock( this.SyncRoot )
                {
                    this.remoteSubscriber = value;
                }
            }
        }

        public IArticleMasterSetWorkflow ArticleMasterSetWorkflow{ get; set; }
        public IHelloWorkflow HelloWorkflow{ get; }
        public IStatusWorkflow StatusWorkflow{ get; }
        public IStockInfoWorkflow StockInfoWorkflow{ get; }
        public IUnprocessedWorkflow UnprocessedWorkflow{ get; }

        private void OnHelloRequestAccepted( Object sender, MessageReceivedEventArgs e )
        {
            this.RemoteSubscriber = ( ( HelloRequest )e.Message ).Subscriber;
        }

        protected virtual void OnMessageProcessingError( Object sender, MessageProcessingErrorEventArgs e )
        {
            this.MessageProcessingError?.Invoke( this, e );
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
                    this.ArticleMasterSetWorkflow.Dispose();
                    this.HelloWorkflow.Dispose();
                    this.StatusWorkflow.Dispose();
                    this.StockInfoWorkflow.Dispose();
                    this.UnprocessedWorkflow.Dispose();

                    this.DialogProvider.Dispose();
                    this.SerializationProvider.Dispose();
                }

                this.isDisposed = true;
            }
        }
    }
}
