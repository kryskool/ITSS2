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

using Reth.Itss2.Dialogs.Standard.Diagnostics;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages;

namespace Reth.Itss2.Dialogs.Standard.Protocol
{
    public abstract class DialogProvider:IDialogProvider
    {
        public event EventHandler<MessageProcessingErrorEventArgs>? MessageProcessingError;

        private bool isDisposed;
        
        private IMessageTransmitter? messageTransmitter;

        protected DialogProvider()
        {
        }

        ~DialogProvider()
        {
            this.Dispose( false );
        }

        protected Object SyncRoot
        {
            get;
        } = new Object();

        protected IMessageTransmitter? MessageTransmitter
        {
            get
            {
                lock( this.SyncRoot )
                {
                    return this.messageTransmitter;
                }
            }
            
            private set
            {
                lock( this.SyncRoot )
                {
                    this.messageTransmitter = value;
                }
            }
        }

        public abstract String[] GetSupportedDialogs();

        public void SendMessage( String messageEnvelope )
        {
            lock( this.SyncRoot )
            {
                _ = this.MessageTransmitter ?? throw Assert.Exception( new InvalidOperationException( "Dialog provider is not connected." ) );

                this.MessageTransmitter.SendMessage( messageEnvelope );
            }
        }

        public Task SendMessageAsync( String messageEnvelope, CancellationToken cancellationToken = default )
        {
            lock( this.SyncRoot )
            {
                _ = this.MessageTransmitter ?? throw Assert.Exception( new InvalidOperationException( "Dialog provider is not connected." ) );
            
                return this.MessageTransmitter.SendMessageAsync( messageEnvelope, cancellationToken );
            }
        }

        public void Connect( IMessageTransmitter messageTransmitter )
        {
            lock( this.SyncRoot )
            {
                if( this.MessageTransmitter is not null )
                {
                    throw Assert.Exception( new InvalidOperationException( "Dialog provider is already connected." ) );
                }

                this.ConnectDialogs( messageTransmitter );

                messageTransmitter.MessageProcessingError += this.OnMessageProcessingError;
                messageTransmitter.Subscribe( ( IMessage message ) => {}, ( Exception ex ) => {}, () => {} );
                messageTransmitter.Connect();

                this.MessageTransmitter = messageTransmitter;
            }
        }

        protected virtual void OnMessageProcessingError( Object sender, MessageProcessingErrorEventArgs e )
        {
            this.MessageProcessingError?.Invoke( this, e );
        }

        public Task ConnectAsync( IMessageTransmitter messageTransmitter )
        {
            return this.ConnectAsync( messageTransmitter, CancellationToken.None );
        }

        public Task ConnectAsync( IMessageTransmitter messageTransmitter, CancellationToken cancellationToken = default )
        {
            return Task.Run(    () =>
                                {
                                    this.Connect( messageTransmitter );
                                },
                                cancellationToken   );
        }

        protected abstract void ConnectDialogs( IMessageTransmitter messageTransmitter );

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
                    if( this.MessageTransmitter is not null )
                    {
                        this.MessageTransmitter.MessageProcessingError -= this.OnMessageProcessingError;
                        this.MessageTransmitter.Dispose();
                    }
                }

                this.isDisposed = true;
            }
        }
    }
}
