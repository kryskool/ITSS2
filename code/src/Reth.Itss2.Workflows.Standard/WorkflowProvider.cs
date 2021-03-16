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
using Reth.Itss2.Dialogs.Standard.Serialization;

namespace Reth.Itss2.Workflows.Standard
{
    public abstract class WorkflowProvider:IDisposable
    {
        public event EventHandler<MessageProcessingErrorEventArgs>? MessageProcessingError;

        private SubscriberInfo subscriberInfo;

        protected bool isDisposed;

        protected WorkflowProvider( ISerializationProvider serializationProvider,
                                    Subscriber localSubscriber,
                                    IDialogProvider dialogProvider  )
        {
            this.SerializationProvider = serializationProvider;
            this.DialogProvider = dialogProvider;

            this.subscriberInfo = new SubscriberInfo( localSubscriber );

            dialogProvider.MessageProcessingError += this.OnMessageProcessingError;
        }

        ~WorkflowProvider()
        {
            this.Dispose( false );
        }

        private Object SyncRoot
        {
            get;
        } = new Object();

        public IDialogProvider DialogProvider
        {
            get;
        }

        public ISerializationProvider SerializationProvider
        {
            get;
        }

        protected virtual void OnMessageProcessingError( Object sender, MessageProcessingErrorEventArgs e )
        {
            this.MessageProcessingError?.Invoke( this, e );
        }

        protected void SetRemoteSubscriber( Subscriber subscriber )
        {
            lock( this.SyncRoot )
            {
                Subscriber localSubscriber = this.subscriberInfo.LocalSubscriber;
                Subscriber remoteSubscriber = subscriber;

                this.subscriberInfo = new SubscriberInfo( localSubscriber, remoteSubscriber );
            }
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
                }

                this.isDisposed = true;
            }
        }
    }
}
