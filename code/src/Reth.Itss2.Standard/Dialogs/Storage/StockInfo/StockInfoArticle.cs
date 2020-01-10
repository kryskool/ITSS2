using System;
using System.Collections.Generic;
using System.Diagnostics;

using Reth.Protocols;
using Reth.Protocols.Extensions.Int32Extensions;
using Reth.Protocols.Extensions.ListExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.StockInfo
{
    public class StockInfoArticle:Article, IEquatable<StockInfoArticle>
    {
        public static bool operator==( StockInfoArticle left, StockInfoArticle right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( StockInfoArticle left, StockInfoArticle right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( StockInfoArticle left, StockInfoArticle right )
		{
            bool result = Article.Equals( left, right );

            if( result == true )
            {
                result &= ( left.Quantity == right.Quantity );
                result &= String.Equals( left.Name, right.Name, StringComparison.InvariantCultureIgnoreCase );
                result &= String.Equals( left.DosageForm, right.DosageForm, StringComparison.InvariantCultureIgnoreCase );
                result &= String.Equals( left.PackagingUnit, right.PackagingUnit, StringComparison.InvariantCultureIgnoreCase );
                result &= Nullable.Equals( left.MaxSubItemQuantity, right.MaxSubItemQuantity );
                result &= left.ProductCodes.ElementsEqual( right.ProductCodes );
                result &= left.Packs.ElementsEqual( right.Packs );
            }

            return result;
		}

        private int quantity;

        private Nullable<int> maxSubItemQuantity;

        public StockInfoArticle( ArticleId id, int quantity )
        :
            base( id )
        {
            this.Quantity = quantity;
        }

        public StockInfoArticle(    ArticleId id,
                                    int quantity,
                                    String name,
                                    String dosageForm,
                                    String packagingUnit,
                                    Nullable<int> maxSubItemQuantity,
                                    IEnumerable<ProductCode> productCodes,
                                    IEnumerable<StockInfoPack> packs    )
        :
            base( id )
        {
            this.Quantity = quantity;
            this.Name = name;
            this.DosageForm = dosageForm;
            this.PackagingUnit = packagingUnit;
            this.MaxSubItemQuantity = maxSubItemQuantity;

            if( !( productCodes is null ) )
            {
                this.ProductCodes.AddRange( productCodes );                
            }

            if( !( packs is null ) )
            {
                this.Packs.AddRange( packs );
            }
        }

        public int Quantity
        {
            get{ return this.quantity; }

            private set
            {
                Debug.Assert( value > 0, $"{ nameof( value ) } > 0" );

                if( value == 0 )
                {
                    throw new ArgumentOutOfRangeException( nameof( value ), "Quantity of stock info article must not be zero." );
                }

                this.quantity = value;
            }
        }

        public String Name
        {
            get;
        }

        public String DosageForm
        {
            get;
        }

        public String PackagingUnit
        {
            get;
        }

        public Nullable<int> MaxSubItemQuantity
        {
            get{ return this.maxSubItemQuantity; }
            
            private set
            {
                value?.ThrowIfNegative();

                this.maxSubItemQuantity = value;
            }
        }

        private List<ProductCode> ProductCodes
        {
            get;
        } = new List<ProductCode>();

        private List<StockInfoPack> Packs
        {
            get;
        } = new List<StockInfoPack>();
        
        public ProductCode[] GetProductCodes()
        {
            return this.ProductCodes.ToArray();
        }

        public StockInfoPack[] GetPacks()
        {
            return this.Packs.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as StockInfoArticle );
		}
		
        public bool Equals( StockInfoArticle other )
		{
            return StockInfoArticle.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}