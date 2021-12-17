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
using System.Collections.Generic;

using Reth.Itss2.Diagnostics;

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages
{
    public sealed class MessageEnvelope:IMessageEnvelope, IEquatable<MessageEnvelope>
    {
        public const String DefaultVersion = "2.0";
        
        public static bool operator==( MessageEnvelope? left, MessageEnvelope? right )
		{
            return MessageEnvelope.Equals( left, right );
		}
		
		public static bool operator!=( MessageEnvelope? left, MessageEnvelope? right )
		{
			return !( MessageEnvelope.Equals( left, right ) );
		}

        public static bool Equals( MessageEnvelope? left, MessageEnvelope? right )
		{
            bool result = EqualityComparer<IMessage?>.Default.Equals( left?.Message, right?.Message );

            result &= ( result ? ( EqualityComparer<MessageEnvelopeTimestamp?>.Default.Equals( left?.Timestamp, right?.Timestamp ) ) : false );
            result &= ( result ? ( String.Equals( left?.Version, right?.Version, StringComparison.OrdinalIgnoreCase ) ) : false );

            return result;
		}

        public MessageEnvelope( IMessage message )
        {
            this.Message = message;
        }

        public MessageEnvelope( IMessage message, MessageEnvelopeTimestamp timestamp )
        {
            this.Message = message;
            this.Timestamp = timestamp;
        }

        public MessageEnvelope( IMessage message, MessageEnvelopeTimestamp timestamp, String version )
        {
            if( version != MessageEnvelope.DefaultVersion )
            {
                throw Assert.Exception( new FormatException( $"Invalid message version: { version }" ) );
            }

            this.Message = message;
            this.Timestamp = timestamp;
            this.Version = version;
        }

        public IMessage Message
        {
            get;
        }

        public MessageEnvelopeTimestamp Timestamp
        {
            get;
        } = MessageEnvelopeTimestamp.UtcNow;
        
        public String Version
        {
            get;
        } = MessageEnvelope.DefaultVersion;

        public override bool Equals( Object? obj )
		{
			return this.Equals( obj as MessageEnvelope );
		}
		
        public bool Equals( MessageEnvelope? other )
		{
            return MessageEnvelope.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
            return HashCode.Combine( this.Message, this.Timestamp, this.Version );
		}

        public override String? ToString()
        {
            return this.Message.ToString();
        }
    }
}
