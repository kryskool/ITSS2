using System;
using System.Globalization;

using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols;
using Reth.Protocols.Extensions.DecimalExtensions;
using Reth.Protocols.Extensions.Int32Extensions;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Experimental.Dialogs.SalesTransactions
{
    public class ShoppingCartItem:IEquatable<ShoppingCartItem>
    {
        public static bool operator==( ShoppingCartItem left, ShoppingCartItem right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( ShoppingCartItem left, ShoppingCartItem right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( ShoppingCartItem left, ShoppingCartItem right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = false;

                                                        result = ArticleId.Equals( left.ArticleId, right.ArticleId );
                                                        result &= ( left.OrderedQuantity == right.OrderedQuantity );
                                                        result &= ( left.DispensedQuantity == right.DispensedQuantity );
                                                        result &= ( left.PaidQuantity == right.PaidQuantity );
                                                        result &= ( left.Price == right.Price );
                                                        result &= Iso4217Code.Equals( left.Currency, right.Currency );

                                                        return result;
                                                    }   );
		}

        public ShoppingCartItem(    ArticleId articleId,
                                    int orderedQuantity,
                                    int dispensedQuantity,
                                    int paidQuantity,
                                    decimal price,
                                    Iso4217Code currency    )
        {
            articleId.ThrowIfNull();
            orderedQuantity.ThrowIfNegative();
            dispensedQuantity.ThrowIfNegative();
            paidQuantity.ThrowIfNegative();
            price.ThrowIfNotPositive();
            currency.ThrowIfNull();

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
		
        public bool Equals( ShoppingCartItem other )
		{
            return ShoppingCartItem.Equals( this, other );
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