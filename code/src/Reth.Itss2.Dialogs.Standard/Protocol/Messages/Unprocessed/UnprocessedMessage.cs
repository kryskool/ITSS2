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

using Reth.Itss2.Messaging;

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages.Unprocessed
{
    public class UnprocessedMessage:SubscribedMessage, IEquatable<UnprocessedMessage>
    {
        public static bool operator==( UnprocessedMessage? left, UnprocessedMessage? right )
		{
            return UnprocessedMessage.Equals( left, right );
		}
		
		public static bool operator!=( UnprocessedMessage? left, UnprocessedMessage? right )
		{
			return !( UnprocessedMessage.Equals( left, right ) );
		}

        public static bool Equals( UnprocessedMessage? left, UnprocessedMessage? right )
		{
            bool result = SubscribedMessage.Equals( left, right );

            result &= ( result ? EqualityComparer<String?>.Default.Equals( left?.Message, right?.Message ) : false );
            result &= ( result ? EqualityComparer<String?>.Default.Equals( left?.Text, right?.Text ) : false );
            result &= ( result ? EqualityComparer<UnprocessedReason?>.Default.Equals( left?.Reason, right?.Reason ) : false );

            return result;
		}

        public UnprocessedMessage(  MessageId id,
                                    SubscriberId source,
                                    SubscriberId destination,
                                    String message  )
        :
            base( id, StandardDialogs.Unprocessed, source, destination )
        {
            this.Message = message;
        }

        public UnprocessedMessage(  MessageId id,
                                    SubscriberId source,
                                    SubscriberId destination,
                                    String message,
                                    String? text,
                                    UnprocessedReason? reason    )
        :
            base( id, StandardDialogs.Unprocessed, source, destination )
        {
            this.Message = message;
            this.Text = text;
            this.Reason = reason;
        }

        public String Message
        {
            get;
        }

        public String? Text
        {
            get;
        }

        public UnprocessedReason? Reason
        {
            get;
        }

        public override bool Equals( Object? obj )
		{
			return this.Equals( obj as UnprocessedMessage );
		}
		
        public bool Equals( UnprocessedMessage? other )
		{
            return UnprocessedMessage.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
