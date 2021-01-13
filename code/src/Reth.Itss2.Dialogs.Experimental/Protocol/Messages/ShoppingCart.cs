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
using System.Linq;

namespace Reth.Itss2.Dialogs.Experimental.Protocol.Messages
{
    public class ShoppingCart:IEquatable<ShoppingCart>
    {
        public static bool operator==( ShoppingCart? left, ShoppingCart? right )
		{
            return ShoppingCart.Equals( left, right );
		}
		
		public static bool operator!=( ShoppingCart? left, ShoppingCart? right )
		{
			return !( ShoppingCart.Equals( left, right ) );
		}

        public static bool Equals( ShoppingCart? left, ShoppingCart? right )
		{
            bool result = ShoppingCartId.Equals( left?.Id, right?.Id );

            result &= ( result ? SalesPointId.Equals( left?.SalesPointId, right?.SalesPointId ) : false );
            result &= ( result ? ViewPointId.Equals( left?.ViewPointId, right?.ViewPointId ) : false );
            result &= ( result ? SalesPersonId.Equals( left?.SalesPersonId, right?.SalesPersonId ) : false );
            result &= ( result ? CustomerId.Equals( left?.CustomerId, right?.CustomerId ) : false );
            result &= ( result ? ShoppingCartStatus.Equals( left?.Status, right?.Status ) : false );
            result &= ( result ? ( left?.Items.SequenceEqual( right?.Items ) ).GetValueOrDefault() : false );

            return result;
		}

        public ShoppingCart(    ShoppingCartId id,
                                SalesPointId salesPointId,
                                ViewPointId viewPointId,
                                SalesPersonId salesPersonId,
                                CustomerId customerId,
                                ShoppingCartStatus status   )
        :
            this( id, salesPointId, viewPointId, salesPersonId, customerId, status, null )
        {
        }

        public ShoppingCart(    ShoppingCartId id,
                                SalesPointId salesPointId,
                                ViewPointId viewPointId,
                                SalesPersonId salesPersonId,
                                CustomerId customerId,
                                ShoppingCartStatus status,
                                IEnumerable<ShoppingCartItem>? items )
        {
            this.Id = id;
            this.SalesPointId = salesPointId;
            this.ViewPointId = viewPointId;
            this.SalesPersonId = salesPersonId;
            this.CustomerId = customerId;
            this.Status = status;

            if( items is not null )
            {
                this.Items.AddRange( items );
            }
        }

        public ShoppingCartId Id
        {
            get;
        }

        public SalesPointId SalesPointId
        {
            get;
        }

        public ViewPointId ViewPointId
        {
            get;
        }

        public SalesPersonId SalesPersonId
        {
            get;
        }

        public CustomerId CustomerId
        {
            get;
        }

        public ShoppingCartStatus Status
        {
            get;
        }

        private List<ShoppingCartItem> Items
        {
            get;
        } = new List<ShoppingCartItem>();
        
        public ShoppingCartItem[] GetItems()
        {
            return this.Items.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as ShoppingCart );
		}
		
        public bool Equals( ShoppingCart? other )
		{
            return ShoppingCart.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override String ToString()
        {
            return $"{ this.Id }, { this.Status }";
        }
    }
}
