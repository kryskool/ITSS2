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

namespace Reth.Itss2.Serialization
{
    public interface ISerializationProvider:IDisposable
    {
        IInteractionLog InteractionLog{ get; }

        TimeSpan MessageRoundTripTimeout{ get; }
        
        IMessageEnvelope DeserializeMessageEnvelope( String messageEnvelope );
        Task<IMessageEnvelope> DeserializeMessageEnvelopeAsync( String messageEnvelope, CancellationToken cancellationToken = default );

        IMessage DeserializeMessage( String message );
        Task<IMessage> DeserializeMessageAsync( String message, CancellationToken cancellationToken = default );

        String SerializeMessageEnvelope( IMessageEnvelope messageEnvelope );
        Task<String> SerializeMessageEnvelopeAsync( IMessageEnvelope messageEnvelope, CancellationToken cancellationToken = default );

        String SerializeMessage( IMessage message );
        Task<String> SerializeMessageAsync( IMessage message, CancellationToken cancellationToken = default );

        IMessageTransmitter CreateMessageTransmitter( Stream baseStream );
    }
}
