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

namespace Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ShoppingCartDialog
{
    public class ShoppingCartCriteria:IEquatable<ShoppingCartCriteria>
    {
        public static bool operator==( ShoppingCartCriteria? left, ShoppingCartCriteria? right )
		{
            return ShoppingCartCriteria.Equals( left, right );
		}
		
		public static bool operator!=( ShoppingCartCriteria? left, ShoppingCartCriteria? right )
		{
			return !( ShoppingCartCriteria.Equals( left, right ) );
		}

        public static bool Equals( ShoppingCartCriteria? left, ShoppingCartCriteria? right )
		{
            bool result = ShoppingCartId.Equals( left?.ShoppingCartId, right?.ShoppingCartId );

            result &= ( result ? SalesPointId.Equals( left?.SalesPointId, right?.SalesPointId ) : false );
            result &= ( result ? ViewPointId.Equals( left?.ViewPointId, right?.ViewPointId ) : false );
            result &= ( result ? SalesPersonId.Equals( left?.SalesPersonId, right?.SalesPersonId ) : false );
            result &= ( result ? CustomerId.Equals( left?.CustomerId, right?.CustomerId ) : false );
            
            return result;
		}

        public ShoppingCartCriteria(    ShoppingCartId? shoppingCartId,
                                        SalesPointId? salesPointId,
                                        ViewPointId? viewPointId,
                                        SalesPersonId? salesPersonId,
                                        CustomerId? customerId   )
        {
            this.ShoppingCartId = shoppingCartId;
            this.SalesPointId = salesPointId;
            this.ViewPointId = viewPointId;
            this.SalesPersonId = salesPersonId;
            this.CustomerId = customerId;
        }

        public ShoppingCartId? ShoppingCartId
        {
            get;
        }

        public SalesPointId? SalesPointId
        {
            get;
        }

        public ViewPointId? ViewPointId
        {
            get;
        }

        public SalesPersonId? SalesPersonId
        {
            get;
        }

        public CustomerId? CustomerId
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as ShoppingCartCriteria );
		}
		
        public bool Equals( ShoppingCartCriteria? other )
		{
            return ShoppingCartCriteria.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return HashCode.Combine( this.ShoppingCartId, this.SalesPointId, this.ViewPointId, this.SalesPersonId, this.CustomerId );
        }
    }
}
