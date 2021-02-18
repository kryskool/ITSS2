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
    public class ShoppingCartUpdateMessage:SubscribedMessage, IEquatable<ShoppingCartUpdateMessage>
    {
        public static bool operator==( ShoppingCartUpdateMessage? left, ShoppingCartUpdateMessage? right )
		{
            return ShoppingCartUpdateMessage.Equals( left, right );
		}
		
		public static bool operator!=( ShoppingCartUpdateMessage? left, ShoppingCartUpdateMessage? right )
		{
			return !( ShoppingCartUpdateMessage.Equals( left, right ) );
		}

        public static bool Equals( ShoppingCartUpdateMessage? left, ShoppingCartUpdateMessage? right )
		{
            bool result = SubscribedMessage.Equals( left, right );

            result &= ( result ? ShoppingCart.Equals( left?.ShoppingCart, right?.ShoppingCart ) : false );

            return result;
		}

        public ShoppingCartUpdateMessage(   MessageId id,
									        SubscriberId source,
                                            SubscriberId destination,
                                            ShoppingCart shoppingCart )
        :
            base( id, Dialogs.ShoppingCartUpdate, source, destination )
        {
            this.ShoppingCart = shoppingCart;
        }

        public ShoppingCart ShoppingCart
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as ShoppingCartUpdateMessage );
		}
		
        public bool Equals( ShoppingCartUpdateMessage? other )
		{
            return ShoppingCartUpdateMessage.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
