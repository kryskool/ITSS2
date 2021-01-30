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
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Dialogs.Standard.Diagnostics;
using Reth.Itss2.Dialogs.Standard.Protocol;

namespace Reth.Itss2.Dialogs.Standard.Serialization
{
    public abstract class SerializationProvider:ISerializationProvider
    {
        public event EventHandler<Protocol.ErrorEventArgs>? MessageProcessingError;

        private bool isDisposed;

        protected SerializationProvider( IInteractionLog interactionLog )
        :
            this( interactionLog, MessageTransmitter.DefaultMessageRoundTripTimeout )
        {
        }

        protected SerializationProvider( IInteractionLog interactionLog, TimeSpan messageRoundTripTimeout )
        {
            this.InteractionLog = interactionLog;
            this.MessageRoundTripTimeout = messageRoundTripTimeout;
        }

        ~SerializationProvider()
        {
            this.Dispose( false );
        }

        public IInteractionLog InteractionLog
        {
            get;
        }

        public TimeSpan MessageRoundTripTimeout
        {
            get;
        }

        protected abstract IMessageStreamReader CreateMessageStreamReader( Stream baseStream, IInteractionLog interactionLog );
        protected abstract IMessageStreamWriter CreateMessageStreamWriter( Stream baseStream, IInteractionLog interactionLog );

        private IMessageTransmitter CreateMessageTransmitter( Stream stream )
        {
            IMessageStreamReader messageStreamReader = this.CreateMessageStreamReader( stream, this.InteractionLog );
            IMessageStreamWriter messageStreamWriter = this.CreateMessageStreamWriter( stream, this.InteractionLog );

            messageStreamReader.MessageProcessingError += this.OnMessageProcessingError;

            return new MessageTransmitter(  messageStreamReader,
                                            messageStreamWriter,
                                            this.MessageRoundTripTimeout    );
        }

        public void Connect( Stream stream, IDialogProvider dialogProvider, bool blocking )
        {
            IMessageTransmitter messageTransmitter = this.CreateMessageTransmitter( stream );
                                                
            dialogProvider.Connect( messageTransmitter, blocking );
        }

        public Task ConnectAsync( Stream stream, IDialogProvider dialogProvider, bool blocking )
        {
            return this.ConnectAsync( stream, dialogProvider, blocking, CancellationToken.None );
        }

        public Task ConnectAsync( Stream stream, IDialogProvider dialogProvider, bool blocking, CancellationToken cancellationToken )
        {
            IMessageTransmitter messageTransmitter = this.CreateMessageTransmitter( stream );
                                                
            return dialogProvider.ConnectAsync( messageTransmitter, blocking, cancellationToken );
        }

        protected void OnMessageProcessingError( Object sender, Protocol.ErrorEventArgs e )
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
                    this.InteractionLog.Dispose();
                }

                this.isDisposed = true;
            }
        }
    }
}
