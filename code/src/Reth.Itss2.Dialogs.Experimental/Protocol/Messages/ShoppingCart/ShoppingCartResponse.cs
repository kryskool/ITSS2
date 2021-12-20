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
using Reth.Itss2.Messaging;

namespace Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ShoppingCart
{
    public class ShoppingCartResponse:SubscribedResponse, IEquatable<ShoppingCartResponse>
    {
        public static bool operator==( ShoppingCartResponse? left, ShoppingCartResponse? right )
		{
            return ShoppingCartResponse.Equals( left, right );
		}
		
		public static bool operator!=( ShoppingCartResponse? left, ShoppingCartResponse? right )
		{
			return !( ShoppingCartResponse.Equals( left, right ) );
		}

        public static bool Equals( ShoppingCartResponse? left, ShoppingCartResponse? right )
		{
            bool result = SubscribedResponse.Equals( left, right );

            result &= ( result ? ShoppingCartContent.Equals( left?.ShoppingCart, right?.ShoppingCart ) : false );

            return result;
		}

        public ShoppingCartResponse(    MessageId id,
									    SubscriberId source,
                                        SubscriberId destination,
                                        ShoppingCartContent shoppingCart )
        :
            base( id, ExperimentalDialogs.ShoppingCart, source, destination )
        {
            this.ShoppingCart = shoppingCart;
        }

        public ShoppingCartResponse(    ShoppingCartRequest request,
                                        ShoppingCartContent shoppingCart )
        :
            base( request )
        {
            this.ShoppingCart = shoppingCart;
        }

        public ShoppingCartContent ShoppingCart
        {
            get;
        }

        public override bool Equals( Object? obj )
		{
			return this.Equals( obj as ShoppingCartResponse );
		}
		
        public bool Equals( ShoppingCartResponse? other )
		{
            return ShoppingCartResponse.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
