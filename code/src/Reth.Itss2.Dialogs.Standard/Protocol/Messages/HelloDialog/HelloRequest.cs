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

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages.HelloDialog
{
    public class HelloRequest:Request, IEquatable<HelloRequest>
    {
        public static bool operator==( HelloRequest? left, HelloRequest? right )
		{
            return HelloRequest.Equals( left, right );
		}
		
		public static bool operator!=( HelloRequest? left, HelloRequest? right )
		{
			return !( HelloRequest.Equals( left, right ) );
		}

        public static bool Equals( HelloRequest? left, HelloRequest? right )
		{
            bool result = Request.Equals( left, right );

            result &= ( result ? EqualityComparer<Subscriber?>.Default.Equals( left?.Subscriber, right?.Subscriber ) : false );

            return result;
		}

        public HelloRequest( MessageId id, Subscriber subscriber )
        :
            base( id )
        {
            this.Subscriber = subscriber;
        }

        public Subscriber Subscriber
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as HelloRequest );
		}
		
        public bool Equals( HelloRequest? other )
		{
            return HelloRequest.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
