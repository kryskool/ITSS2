using System;
using System.Collections.Generic;
using System.Globalization;

using Reth.Protocols;
using Reth.Protocols.Extensions.ListExtensions;
using Reth.Protocols.Extensions.StringExtensions;

namespace Reth.Itss2.Standard.Dialogs.ArticleData.StockDeliveryInfo
{
    public class StockDeliveryInfoResponseTask:IEquatable<StockDeliveryInfoResponseTask>
    {
        public static bool operator==( StockDeliveryInfoResponseTask left, StockDeliveryInfoResponseTask right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( StockDeliveryInfoResponseTask left, StockDeliveryInfoResponseTask right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( StockDeliveryInfoResponseTask left, StockDeliveryInfoResponseTask right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = false;

                                                        result = String.Equals( left.Id, right.Id, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= ( left.Status == right.Status );
                                                        result &= left.Articles.ElementsEqual( right.Articles );

                                                        return result;
                                                    }   );
		}

        private String id = String.Empty;

        public StockDeliveryInfoResponseTask(   String id,
                                                StockDeliveryInfoStatus status,
                                                IEnumerable<StockDeliveryInfoArticle> articles    )
        {
            this.Id = id;
            this.Status = status;

            if( !( articles is null ) )
            {
                this.Articles.AddRange( articles );
            }
        }

        public String Id
        {
            get{ return this.id; }

            private set
            {
                value.ThrowIfNullOrEmpty();

                this.id = value;
            }
        }

        public StockDeliveryInfoStatus Status
        {
            get;
        }

        private List<StockDeliveryInfoArticle> Articles
        {
            get;
        } = new List<StockDeliveryInfoArticle>();
        
        public StockDeliveryInfoArticle[] GetArticles()
        {
            return this.Articles.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as StockDeliveryInfoResponseTask );
		}
		
        public bool Equals( StockDeliveryInfoResponseTask other )
		{
            return StockDeliveryInfoResponseTask.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override String ToString()
        {
            return this.Id.ToString( CultureInfo.InvariantCulture );
        }
    }
}