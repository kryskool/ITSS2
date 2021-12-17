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
using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Diagnostics;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages;

namespace Reth.Itss2.Dialogs.Standard.Protocol
{
    public abstract class Dialog:IDialog
    {
        public event EventHandler<MessageDispatchingEventArgs>? MessageDispatching;

        protected Dialog( String name, IDialogProvider dialogProvider )
        {
            this.Name = name;
            this.DialogProvider = dialogProvider;
        }

        public String Name
        {
            get;
        }

        public IDialogProvider DialogProvider
        {
            get;
        }

        private IMessageTransmitter? MessageTransmitter
        {
            get; set;
        }

        protected void SendMessage( IMessage message )
        {
            _ = this.MessageTransmitter ?? throw Assert.Exception( new InvalidOperationException( "Dialog is not connected." ) );
            
            this.MessageTransmitter.SendMessage( message );
        }

        protected Task SendMessageAsync( IMessage message, CancellationToken cancellationToken = default )
        {
            _ = this.MessageTransmitter ?? throw Assert.Exception( new InvalidOperationException( "Dialog is not connected." ) );

            return this.MessageTransmitter.SendMessageAsync( message, cancellationToken );
        }

        protected void SendResponse( IResponse response )
        {
            this.SendMessage( response );
        }

        protected Task SendResponseAsync( IResponse response, CancellationToken cancellationToken = default )
        {
            return this.SendMessageAsync( response, cancellationToken );
        }

        public TResponse SendRequest<TRequest, TResponse>( TRequest request )
            where TRequest:IRequest
            where TResponse:IResponse
        {
            _ = this.MessageTransmitter ?? throw Assert.Exception( new InvalidOperationException( "Dialog is not connected." ) );

            return this.MessageTransmitter.SendRequest<TRequest, TResponse>( request );
        }

        public Task<TResponse> SendRequestAsync<TRequest, TResponse>( TRequest request, CancellationToken cancellationToken = default )
            where TRequest:IRequest
            where TResponse:IResponse
        {
            _ = this.MessageTransmitter ?? throw Assert.Exception( new InvalidOperationException( "Dialog is not connected." ) );

            return this.MessageTransmitter.SendRequestAsync<TRequest, TResponse>( request, cancellationToken );  
        }

        public virtual void Connect( IMessageTransmitter messageTransmitter )
        {
            this.MessageTransmitter = messageTransmitter;
        }

        protected virtual void Connect( IMessageTransmitter messageTransmitter, Action<IMessage> onNext )
        {
            this.MessageTransmitter = messageTransmitter;

            messageTransmitter.Subscribe( onNext );
        }

        protected void Dispatch<TMessage>( IMessage message, Action<TMessage> callback )
            where TMessage:class, IMessage
        {
            TMessage? dispatchee = message as TMessage;

            if( dispatchee is not null )
            {
                MessageDispatchingEventArgs eventArgs = new MessageDispatchingEventArgs( dispatchee, this.DialogProvider );

                this.MessageDispatching?.Invoke( this, eventArgs );

                if( eventArgs.Cancel == false )
                {
                    callback( dispatchee );
                }
            }
        }
    }
}
