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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages;

namespace Reth.Itss2.Dialogs.Standard.Protocol
{
    public class MessageInterceptor<TMessage>:IDisposable
        where TMessage:IMessage
    {
        private bool isDisposed;

        public MessageInterceptor(  IDialog dialog,
                                    IMessageFilter filter,
                                    Action<MessageReceivedEventArgs<TMessage>> callback )
        {
            this.Dialog = dialog;
            this.Dialog.MessageDispatching += this.Dialog_MessageDispatching;

            this.Filter = filter;
            this.Callback = callback;
        }

        private IDialog Dialog
        {
            get;
        }

        private IMessageFilter Filter
        {
            get;
        }

        private Action<MessageReceivedEventArgs<TMessage>> Callback
        {
            get;
        }

        ~MessageInterceptor()
        {
            this.Dispose( false );
        }

        private void Dialog_MessageDispatching( Object? sender, MessageDispatchingEventArgs e )
        {
            if( this.Filter.Intercept( e.Message ) == true )
            {
                try
                {
                    this.Callback( new MessageReceivedEventArgs<TMessage>( ( TMessage )e.Message, e.DialogProvider ) );
                }finally
                {
                    e.Cancel = true;
                }
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
                this.Dialog.MessageDispatching -= this.Dialog_MessageDispatching;

                this.isDisposed = true;
            }
        }
    }
}
