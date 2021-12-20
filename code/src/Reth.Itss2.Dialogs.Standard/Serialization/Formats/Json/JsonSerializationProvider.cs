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
using Reth.Itss2.Messaging;
using Reth.Itss2.Serialization;
using Reth.Itss2.Serialization.Formats.Json;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Formats.Json
{
    public class JsonSerializationProvider:SerializationProvider
    {
        public JsonSerializationProvider()
        :
            base( NullInteractionLog.Instance )
        {
            this.MessageParser = new JsonMessageParser( this );
        }

        public JsonSerializationProvider( IInteractionLog interactionLog )
        :
            base( interactionLog )
        {
            this.MessageParser = new JsonMessageParser( this );
        }

        public JsonSerializationProvider( IInteractionLog interactionLog, TimeSpan messageRoundTripTimeout )
        :
            base( interactionLog, messageRoundTripTimeout )
        {
            this.MessageParser = new JsonMessageParser( this );
        }

        private JsonMessageParser MessageParser
        {
            get;
        }

        public override IMessageEnvelope DeserializeMessageEnvelope( String messageEnvelope )
        {
            return this.MessageParser.DeserializeMessageEnvelope( messageEnvelope );
        }
        
        public override Task<IMessageEnvelope> DeserializeMessageEnvelopeAsync( String messageEnvelope, CancellationToken cancellationToken = default )
        {
            return Task.Run(    () =>
                                {
                                    return this.MessageParser.DeserializeMessageEnvelope( messageEnvelope );
                                },
                                cancellationToken   );
        }

        public override IMessage DeserializeMessage( String message )
        {
            return this.MessageParser.DeserializeMessage( message );
        }
        
        public override Task<IMessage> DeserializeMessageAsync( String message, CancellationToken cancellationToken = default )
        {
            return Task.Run(    () =>
                                {
                                    return this.MessageParser.DeserializeMessage( message );
                                },
                                cancellationToken   );
        }

        public override String SerializeMessageEnvelope( IMessageEnvelope messageEnvelope )
        {
            return this.MessageParser.SerializeMessageEnvelope( messageEnvelope );
        }

        public override Task<String> SerializeMessageEnvelopeAsync( IMessageEnvelope messageEnvelope, CancellationToken cancellationToken = default )
        {
            return Task.Run(    () =>
                                {
                                    return this.MessageParser.SerializeMessageEnvelope( messageEnvelope );
                                },
                                cancellationToken   );
        }

        public override String SerializeMessage( IMessage message )
        {
            return this.MessageParser.SerializeMessage( message );
        }

        public override Task<String> SerializeMessageAsync( IMessage message, CancellationToken cancellationToken = default )
        {
            return Task.Run(    () =>
                                {
                                    return this.MessageParser.SerializeMessage( message );
                                },
                                cancellationToken   );
        }

        protected override IMessageStreamReader CreateMessageStreamReader( Stream baseStream )
        {
            return new JsonMessageStreamReader( baseStream, this );
        }

        protected override IMessageStreamWriter CreateMessageStreamWriter( Stream baseStream )
        {
            return new JsonMessageStreamWriter( baseStream, this );
        }
    }
}
