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
using System.Globalization;

namespace Reth.Itss2.Messaging
{
    public sealed class MessageEnvelopeTimestamp:IEquatable<MessageEnvelopeTimestamp>
    {
        public static bool operator==( MessageEnvelopeTimestamp? left, MessageEnvelopeTimestamp? right )
		{
            return MessageEnvelopeTimestamp.Equals( left, right );
		}
		
		public static bool operator!=( MessageEnvelopeTimestamp? left, MessageEnvelopeTimestamp? right )
		{
			return !( MessageEnvelopeTimestamp.Equals( left, right ) );
		}

        public static bool Equals( MessageEnvelopeTimestamp? left, MessageEnvelopeTimestamp? right )
		{
            return EqualityComparer<DateTimeOffset?>.Default.Equals( left?.Value, right?.Value );
		}

        public static MessageEnvelopeTimestamp Parse( String value )
        {
            return new MessageEnvelopeTimestamp( DateTimeOffset.Parse( value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal ) );
        }

        public static bool TryParse( String value, out MessageEnvelopeTimestamp? result )
        {
            result = default( MessageEnvelopeTimestamp );

            bool success = DateTimeOffset.TryParse( value, out DateTimeOffset timeStamp );

            if( success == true )
            {
                result = new MessageEnvelopeTimestamp( timeStamp );
            }
            
            return success;
        }

        public static MessageEnvelopeTimestamp UtcNow
        {
            get{ return new MessageEnvelopeTimestamp( DateTimeOffset.UtcNow ); }
        }

        public MessageEnvelopeTimestamp()
        :
            this( DateTimeOffset.UtcNow )
        {
        }

        public MessageEnvelopeTimestamp( DateTimeOffset value )
        {
            this.Value = value;
        }

        public DateTimeOffset Value
        {
            get;
        }

        public override bool Equals( Object? obj )
		{
			return this.Equals( obj as MessageEnvelopeTimestamp );
		}
		
        public bool Equals( MessageEnvelopeTimestamp? other )
		{
            return MessageEnvelopeTimestamp.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

        public override String? ToString()
        {
            return String.Format( CultureInfo.InvariantCulture, "{0:yyyy-MM-ddTHH:mm:ssZ}", this.Value );
        }
    }
}
