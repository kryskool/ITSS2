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

using Reth.Itss2.Dialogs;
using Reth.Itss2.Messaging;
using Reth.Itss2.Serialization;

namespace Reth.Itss2.Workflows
{
    public abstract class Workflow<TDialog>:IWorkflow
        where TDialog:IDialog
    {
        public event EventHandler<MessageProcessingErrorEventArgs>? MessageProcessingError;
        public event EventHandler<MessageDispatchingEventArgs>? MessageDispatching;

        private bool isDisposed;

        protected Workflow( TDialog dialog, ISubscription subscription )
        {
            this.Dialog = dialog;
            this.Subscription = subscription;

            this.Dialog.MessageDispatching += this.Dialog_MessageDispatching;
        }

        ~Workflow()
        {
            this.Dispose( false );
        }

        public TDialog Dialog
        {
            get;
        }

        public ISubscription Subscription
        {
            get;
        }

        protected virtual void OnMessageProcessingError( MessageProcessingErrorEventArgs e )
        {
            this.MessageProcessingError?.Invoke( this, e );
        }

        protected virtual void OnMessageDispatching( MessageDispatchingEventArgs e )
        {
            this.MessageDispatching?.Invoke( this, e );
        }

        private void Dialog_MessageDispatching( Object? sender, MessageDispatchingEventArgs e )
        {
            this.OnMessageDispatching( e );
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
                this.Dialog.MessageDispatching += this.Dialog_MessageDispatching;

                this.isDisposed = true;
            }
        }
    }
}
