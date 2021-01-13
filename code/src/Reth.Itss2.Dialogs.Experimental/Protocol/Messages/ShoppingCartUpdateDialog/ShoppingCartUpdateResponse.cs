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

namespace Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ShoppingCartUpdateDialog
{
    public class ShoppingCartUpdateResponse:SubscribedResponse, IEquatable<ShoppingCartUpdateResponse>
    {
        public static bool operator==( ShoppingCartUpdateResponse? left, ShoppingCartUpdateResponse? right )
		{
            return ShoppingCartUpdateResponse.Equals( left, right );
		}
		
		public static bool operator!=( ShoppingCartUpdateResponse? left, ShoppingCartUpdateResponse? right )
		{
			return !( ShoppingCartUpdateResponse.Equals( left, right ) );
		}

        public static bool Equals( ShoppingCartUpdateResponse? left, ShoppingCartUpdateResponse? right )
		{
            bool result = SubscribedResponse.Equals( left, right );

            result &= ( result ? ShoppingCart.Equals( left?.ShoppingCart, right?.ShoppingCart ) : false );

            return result;
		}

        public ShoppingCartUpdateResponse(  MessageId id,
									        SubscriberId source,
                                            SubscriberId destination,
                                            ShoppingCart shoppingCart,
                                            ShoppingCartUpdateResult result )
        :
            base( id, source, destination )
        {
            this.ShoppingCart = shoppingCart;
            this.Result = result;
        }

        public ShoppingCartUpdateResponse(  ShoppingCartUpdateRequest request,
                                            ShoppingCart shoppingCart,
                                            ShoppingCartUpdateResult result )
        :
            base( request )
        {
            this.ShoppingCart = shoppingCart;
            this.Result = result;
        }

        public ShoppingCart ShoppingCart
        {
            get;
        }

        public ShoppingCartUpdateResult Result
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as ShoppingCartUpdateResponse );
		}
		
        public bool Equals( ShoppingCartUpdateResponse? other )
		{
            return ShoppingCartUpdateResponse.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
