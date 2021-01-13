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

using Reth.Itss2.Dialogs.Standard;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages;

namespace Reth.Itss2.Dialogs.Experimental.Protocol.Messages
{
    public class ShoppingCartItem:IEquatable<ShoppingCartItem>
    {
        public static bool operator==( ShoppingCartItem? left, ShoppingCartItem? right )
		{
            return ShoppingCartItem.Equals( left, right );
		}
		
		public static bool operator!=( ShoppingCartItem? left, ShoppingCartItem? right )
		{
			return !( ShoppingCartItem.Equals( left, right ) );
		}

        public static bool Equals( ShoppingCartItem? left, ShoppingCartItem? right )
		{
            bool result = ArticleId.Equals( left?.ArticleId, right?.ArticleId );

            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.OrderedQuantity, right?.OrderedQuantity ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.DispensedQuantity, right?.DispensedQuantity ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.PaidQuantity, right?.PaidQuantity ) : false );
            result &= ( result ? EqualityComparer<decimal?>.Default.Equals( left?.Price, right?.Price ) : false );
            result &= ( result ? Iso4217Code.Equals( left?.Currency, right?.Currency ) : false );            

            return result;
		}

        public ShoppingCartItem(    ArticleId articleId,
                                    int orderedQuantity,
                                    int dispensedQuantity,
                                    int paidQuantity,
                                    decimal price,
                                    Iso4217Code currency    )
        {
            orderedQuantity.ThrowIfNegative();
            dispensedQuantity.ThrowIfNegative();
            paidQuantity.ThrowIfNegative();
            price.ThrowIfNotPositive();

            this.ArticleId = articleId;
            this.OrderedQuantity = orderedQuantity;
            this.DispensedQuantity = dispensedQuantity;
            this.PaidQuantity = paidQuantity;
            this.Price = price;
            this.Currency = currency;
        }

        public ArticleId ArticleId
        {
            get;
        }

        public int OrderedQuantity
        {
            get;
        }

        public int DispensedQuantity
        {
            get;
        }

        public int PaidQuantity
        {
            get;
        }

        public decimal Price
        {
            get;
        }

        public Iso4217Code Currency
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as ShoppingCartItem );
		}
		
        public bool Equals( ShoppingCartItem? other )
		{
            return ShoppingCartItem.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return this.ArticleId.GetHashCode();
        }

        public override String ToString()
        {
            return this.ArticleId.ToString();
        }
    }
}
