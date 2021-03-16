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

namespace Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ArticlePrice
{
    public class PriceInformation:IEquatable<PriceInformation>
    {
        public static bool operator==( PriceInformation? left, PriceInformation? right )
		{
            return PriceInformation.Equals( left, right );
		}
		
		public static bool operator!=( PriceInformation? left, PriceInformation? right )
		{
			return !( PriceInformation.Equals( left, right ) );
		}

        public static bool Equals( PriceInformation? left, PriceInformation? right )
		{
            bool result = EqualityComparer<PriceCategory?>.Default.Equals( left?.Category, right?.Category );

            result &= ( result ? EqualityComparer<decimal?>.Default.Equals( left?.Price, right?.Price ) : false );
            result &= ( result ? String.Equals( left?.Description, right?.Description, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? String.Equals( left?.BasePriceUnit, right?.BasePriceUnit, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.BasePrice, right?.BasePrice ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.Quantity, right?.Quantity ) : false );
            result &= ( result ? EqualityComparer<decimal?>.Default.Equals( left?.Vat, right?.Vat ) : false );

            return result;
		}

        public PriceInformation( PriceCategory category, decimal price )
        :
            this( category, price, null, null, null, null, null )
        {
        }

        public PriceInformation(    PriceCategory category,
                                    decimal price,
                                    String? description,
                                    String? basePriceUnit,
                                    int? basePrice,
                                    int? quantity,
                                    decimal? vat   )
        {
            price.ThrowIfNotPositive();

            basePrice?.ThrowIfNotPositive();
            quantity?.ThrowIfNotPositive();

            this.Category = category;
            this.Price = price;
            this.Description = description;
            this.BasePriceUnit = basePriceUnit;
            this.BasePrice = basePrice;
            this.Quantity = quantity;
            this.Vat = vat;
        }

        public PriceCategory Category
        {
            get;
        }

        public decimal Price
        {
            get;
        }

        public String? Description
        {
            get;
        }

        public String? BasePriceUnit
        {
            get;
        }

        public int? BasePrice
        {
            get;
        }

        public int? Quantity
        {
            get;
        }

        public decimal? Vat
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as PriceInformation );
		}
		
        public bool Equals( PriceInformation? other )
		{
            return PriceInformation.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return HashCode.Combine( this.Category, this.Price );
        }

        public override String ToString()
        {
            return $"{ this.Category }, { this.Price }";
        }
    }
}
