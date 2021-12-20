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

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages
{
    public abstract class SubscribedMessage:Message, IEquatable<SubscribedMessage>
    {
        public static bool operator==( SubscribedMessage? left, SubscribedMessage? right )
		{
            return SubscribedMessage.Equals( left, right );
		}
		
		public static bool operator!=( SubscribedMessage? left, SubscribedMessage? right )
		{
			return !( SubscribedMessage.Equals( left, right ) );
		}

        public static bool Equals( SubscribedMessage? left, SubscribedMessage? right )
		{
            bool result = Message.Equals( left, right );

            result &= ( result ? EqualityComparer<SubscriberId?>.Default.Equals( left?.Source, right?.Source ) : false );
            result &= ( result ? EqualityComparer<SubscriberId?>.Default.Equals( left?.Destination, right?.Destination ) : false );

            return result;
		}

        protected SubscribedMessage(    MessageId id,
                                        String dialogName,
                                        SubscriberId source,
                                        SubscriberId destination )
        :
            base( id, dialogName )
        {
            this.Source = source;
            this.Destination = destination;
        }

        public SubscriberId Source
        {
            get; protected set;
        }

        public SubscriberId Destination
        {
            get; protected set;
        }

        public override bool Equals( Object? obj )
		{
			return this.Equals( obj as SubscribedMessage );
		}
		
        public bool Equals( SubscribedMessage? other )
		{
            return SubscribedMessage.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

        public override String? ToString()
        {
            return this.Id.ToString();
        }
    }
}
