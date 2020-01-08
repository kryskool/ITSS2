using System;
using System.Globalization;

using Reth.Protocols;
using Reth.Protocols.Extensions.StringExtensions;

namespace Reth.Itss2.Standard.Dialogs.ArticleData.StockDeliveryInfo
{
    public class StockDeliveryInfoRequestTask:IEquatable<StockDeliveryInfoRequestTask>
    {
        public static bool operator==( StockDeliveryInfoRequestTask left, StockDeliveryInfoRequestTask right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( StockDeliveryInfoRequestTask left, StockDeliveryInfoRequestTask right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( StockDeliveryInfoRequestTask left, StockDeliveryInfoRequestTask right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        return String.Equals( left.Id, right.Id, StringComparison.InvariantCultureIgnoreCase );
                                                    }   );
		}

        private String id = String.Empty;

        public StockDeliveryInfoRequestTask( String id )
        {
            this.Id = id;
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

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as StockDeliveryInfoRequestTask );
		}
		
        public bool Equals( StockDeliveryInfoRequestTask other )
		{
            return StockDeliveryInfoRequestTask.Equals( this, other );
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