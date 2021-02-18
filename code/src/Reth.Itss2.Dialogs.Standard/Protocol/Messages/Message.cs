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
using System.Threading;

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages
{
    public abstract class Message:IMessage, IEquatable<Message>
    {
        public static bool operator==( Message? left, Message? right )
		{
            return Message.Equals( left, right );
		}
		
		public static bool operator!=( Message? left, Message? right )
		{
			return !( Message.Equals( left, right ) );
		}

        public static bool Equals( Message? left, Message? right )
		{
            return EqualityComparer<MessageId?>.Default.Equals( left?.Id, right?.Id );
		}

        protected Message( Message other )
        :
            this( other.Id, other.DialogName )
        {
        }

        protected Message( MessageId id, String dialogName )
        {
            this.Id = id;
            this.Name = this.GetType().Name;
            this.DialogName = dialogName;
        }

        public MessageId Id
        {
            get;
        }

        public String Name
        {
            get;
        }

        public String DialogName
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as Message );
		}
		
        public bool Equals( Message? other )
		{
            return Message.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}

        public override String ToString()
        {
            return $"{ this.Name } ({ this.Id })'";
        }
    }
}
