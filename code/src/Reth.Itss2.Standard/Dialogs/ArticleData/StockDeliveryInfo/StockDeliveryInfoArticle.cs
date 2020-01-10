using System;
using System.Collections.Generic;

using Reth.Protocols;
using Reth.Protocols.Extensions.Int32Extensions;
using Reth.Protocols.Extensions.ListExtensions;

namespace Reth.Itss2.Standard.Dialogs.ArticleData.StockDeliveryInfo
{
    public class StockDeliveryInfoArticle:Article, IEquatable<StockDeliveryInfoArticle>
    {
        public static bool operator==( StockDeliveryInfoArticle left, StockDeliveryInfoArticle right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( StockDeliveryInfoArticle left, StockDeliveryInfoArticle right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( StockDeliveryInfoArticle left, StockDeliveryInfoArticle right )
		{
            bool result = Article.Equals( left, right );

            if( result == true )
            {
                result &= Nullable.Equals( left.Quantity, right.Quantity );
                result &= left.Packs.ElementsEqual( right.Packs );
            }

            return result;
		}

        private Nullable<int> quantity;

        public StockDeliveryInfoArticle(    ArticleId id,
                                            Nullable<int> quantity,
                                            IEnumerable<StockDeliveryInfoPack> packs    )
        :
            base( id )
        {
            this.Quantity = quantity;
            
            if( !( packs is null ) )
            {
                this.Packs.AddRange( packs );
            }
        }

        public Nullable<int> Quantity
        {
            get{ return this.quantity; }

            private set
            {
                value?.ThrowIfNegative();

                this.quantity = value;
            }
        }

        private List<StockDeliveryInfoPack> Packs
        {
            get;
        } = new List<StockDeliveryInfoPack>();
        
        public StockDeliveryInfoPack[] GetPacks()
        {
            return this.Packs.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as StockDeliveryInfoArticle );
		}
		
        public bool Equals( StockDeliveryInfoArticle other )
		{
            return StockDeliveryInfoArticle.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}