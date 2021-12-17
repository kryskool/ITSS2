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

using Reth.Itss2.Diagnostics;
using Reth.Itss2.Dialogs.Standard.Protocol;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages;

namespace Reth.Itss2.Dialogs.Standard.Serialization
{
    public abstract class SerializationProvider:ISerializationProvider
    {
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

        public abstract IMessageEnvelope DeserializeMessageEnvelope( String messageEnvelope );
        public abstract Task<IMessageEnvelope> DeserializeMessageEnvelopeAsync( String messageEnvelope, CancellationToken cancellationToken = default );

        public abstract IMessage DeserializeMessage( String message );
        public abstract Task<IMessage> DeserializeMessageAsync( String message, CancellationToken cancellationToken = default );
        
        public abstract String SerializeMessageEnvelope( IMessageEnvelope messageEnvelope );
        public abstract Task<String> SerializeMessageEnvelopeAsync( IMessageEnvelope messageEnvelope, CancellationToken cancellationToken = default );

        public abstract String SerializeMessage( IMessage message );
        public abstract Task<String> SerializeMessageAsync( IMessage message, CancellationToken cancellationToken = default );

        protected abstract IMessageStreamReader CreateMessageStreamReader( Stream baseStream );
        protected abstract IMessageStreamWriter CreateMessageStreamWriter( Stream baseStream );

        public IMessageTransmitter CreateMessageTransmitter( Stream baseStream )
        {
            IMessageStreamReader messageStreamReader = this.CreateMessageStreamReader( baseStream );
            IMessageStreamWriter messageStreamWriter = this.CreateMessageStreamWriter( baseStream );

            return new MessageTransmitter(  messageStreamReader,
                                            messageStreamWriter,
                                            baseStream,
                                            this.MessageRoundTripTimeout    );
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
