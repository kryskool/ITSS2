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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages;

namespace Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ShoppingCart
{
    public class ShoppingCartRequest:SubscribedRequest, IEquatable<ShoppingCartRequest>
    {
        public static bool operator==( ShoppingCartRequest? left, ShoppingCartRequest? right )
		{
            return ShoppingCartRequest.Equals( left, right );
		}
		
		public static bool operator!=( ShoppingCartRequest? left, ShoppingCartRequest? right )
		{
			return !( ShoppingCartRequest.Equals( left, right ) );
		}

        public static bool Equals( ShoppingCartRequest? left, ShoppingCartRequest? right )
		{
            bool result = SubscribedRequest.Equals( left, right );

            result &= ( result ? ShoppingCartCriteria.Equals( left?.Criteria, right?.Criteria ) : false );

            return result;
		}

        public ShoppingCartRequest( MessageId id,
									SubscriberId source,
                                    SubscriberId destination,
                                    ShoppingCartCriteria criteria   )
        :
            base( id, Dialogs.ShoppingCart, source, destination )
        {
            this.Criteria = criteria;
        }

        public ShoppingCartCriteria Criteria
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as ShoppingCartRequest );
		}
		
        public bool Equals( ShoppingCartRequest? other )
		{
            return ShoppingCartRequest.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
