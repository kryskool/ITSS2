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
        private bool isDisposed;

        protected DialogProvider()
        {
        }

        ~DialogProvider()
        {
            this.Dispose( false );
        }

        private ManualResetEventSlim DisconnectedEvent
        {
            get;
        } = new ManualResetEventSlim( false );

        protected IMessageTransmitter? MessageTransmitter
        {
            get; private set;
        }

        public abstract String[] GetSupportedDialogs();

        public void SendMessage( String messageEnvelope )
        {
            _ = this.MessageTransmitter ?? throw Assert.Exception( new InvalidOperationException( "Dialog provider is not connected." ) );

            this.MessageTransmitter.SendMessage( messageEnvelope );
        }

        public Task SendMessageAsync( String messageEnvelope )
        {
            _ = this.MessageTransmitter ?? throw Assert.Exception( new InvalidOperationException( "Dialog provider is not connected." ) );
            
            return this.MessageTransmitter.SendMessageAsync( messageEnvelope );
        }

        public void Connect( IMessageTransmitter messageTransmitter, bool blocking )
        {
            this.ConnectDialogs( messageTransmitter );

            messageTransmitter.Subscribe( ( IMessage message ) => {}, this.OnError, this.OnCompleted );
            messageTransmitter.Connect();

            this.MessageTransmitter = messageTransmitter;

            if( blocking == true )
            {
                this.DisconnectedEvent.Wait();
            }
        }

        public Task ConnectAsync( IMessageTransmitter messageTransmitter, bool blocking )
        {
            return this.ConnectAsync( messageTransmitter, blocking, CancellationToken.None );
        }

        public Task ConnectAsync( IMessageTransmitter messageTransmitter, bool blocking, CancellationToken cancellationToken )
        {
            cancellationToken.Register( () =>
                                        {
                                            this.DisconnectedEvent.Set();
                                        }   );

            return Task.Factory.StartNew(   () =>
                                            {
                                                this.Connect( messageTransmitter, blocking );
                                            },
                                            CancellationToken.None,
                                            TaskCreationOptions.LongRunning,
                                            TaskScheduler.Current   );
        }

        protected abstract void ConnectDialogs( IMessageTransmitter messageTransmitter );

        protected void OnCompleted()
        {
            this.DisconnectedEvent.Set();
        }

        protected void OnError( Exception ex )
        {
            this.DisconnectedEvent.Set();
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
                    this.MessageTransmitter?.Dispose();
                }

                this.isDisposed = true;
            }
        }
    }
}
