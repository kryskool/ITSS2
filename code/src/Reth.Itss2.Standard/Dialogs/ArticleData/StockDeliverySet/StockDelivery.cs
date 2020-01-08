using System;
using System.Collections.Generic;
using System.Globalization;

using Reth.Protocols;
using Reth.Protocols.Extensions.ListExtensions;
using Reth.Protocols.Extensions.StringExtensions;

namespace Reth.Itss2.Standard.Dialogs.ArticleData.StockDeliverySet
{
    public class StockDelivery:IEquatable<StockDelivery>
    {
        public static bool operator==( StockDelivery left, StockDelivery right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( StockDelivery left, StockDelivery right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( StockDelivery left, StockDelivery right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = false;

                                                        result = String.Equals( left.DeliveryNumber, right.DeliveryNumber, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= left.Lines.ElementsEqual( right.Lines );

                                                        return result;
                                                    }   );
		}

        private String deliveryNumber = String.Empty;

        public StockDelivery(   String deliveryNumber,
                                IEnumerable<StockDeliveryLine> lines   )
        {
            this.DeliveryNumber = deliveryNumber;

            if( !( lines is null ) )
            {
                this.Lines.AddRange( lines );
            }
        }

        public String DeliveryNumber
        {
            get{ return this.deliveryNumber; }

            private set
            {
                value.ThrowIfNullOrEmpty();

                this.deliveryNumber = value;
            }
        }

        private List<StockDeliveryLine> Lines
        {
            get;
        } = new List<StockDeliveryLine>();
        
        public StockDeliveryLine[] GetLines()
        {
            return this.Lines.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as StockDelivery );
		}
		
        public bool Equals( StockDelivery other )
		{
            return StockDelivery.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return this.DeliveryNumber.GetHashCode();
        }

        public override String ToString()
        {
            return this.DeliveryNumber.ToString( CultureInfo.InvariantCulture );
        }
    }
}