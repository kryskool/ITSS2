using System;
using System.Globalization;

using Reth.Protocols;
using Reth.Protocols.Extensions.DecimalExtensions;
using Reth.Protocols.Extensions.Int32Extensions;

namespace Reth.Itss2.Experimental.Dialogs.ArticleData.ArticlePrice
{
    public class PriceInformation:IEquatable<PriceInformation>
    {
        public static bool operator==( PriceInformation left, PriceInformation right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( PriceInformation left, PriceInformation right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( PriceInformation left, PriceInformation right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = false;

                                                        result = ( left.Category == right.Category );
                                                        result &= ( left.Price == right.Price );
                                                        result &= String.Equals( left.Description, right.Description, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.BasePriceUnit, right.BasePriceUnit, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= Nullable.Equals( left.BasePrice, right.BasePrice );
                                                        result &= Nullable.Equals( left.Quantity, right.Quantity );
                                                        result &= Nullable.Equals( left.VAT, right.VAT );

                                                        return result;
                                                    }   );
		}

        public PriceInformation( PriceCategory category, decimal price )
        {
            price.ThrowIfNotPositive();

            this.Category = category;
            this.Price = price;
        }

        public PriceInformation(    PriceCategory category,
                                    decimal price,
                                    String description,
                                    String basePriceUnit,
                                    Nullable<int> basePrice,
                                    Nullable<int> quantity,
                                    Nullable<decimal> vat   )
        {
            price.ThrowIfNotPositive();

            quantity?.ThrowIfNotPositive();
            basePrice?.ThrowIfNotPositive();

            this.Category = category;
            this.Price = price;
            this.Description = description;
            this.BasePriceUnit = basePriceUnit;
            this.BasePrice = basePrice;
            this.Quantity = quantity;
            this.VAT = vat;
        }

        public PriceCategory Category
        {
            get;
        }

        public decimal Price
        {
            get;
        }

        public String Description
        {
            get;
        }

        public String BasePriceUnit
        {
            get;
        }

        public Nullable<int> BasePrice
        {
            get;
        }

        public Nullable<int> Quantity
        {
            get;
        }

        public Nullable<decimal> VAT
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as PriceInformation );
		}
		
        public bool Equals( PriceInformation other )
		{
            return PriceInformation.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override String ToString()
        {
            return this.Price.ToString( CultureInfo.InvariantCulture );
        }
    }
}