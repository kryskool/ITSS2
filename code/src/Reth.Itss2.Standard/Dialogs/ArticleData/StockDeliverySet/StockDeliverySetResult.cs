using System;

using Reth.Protocols;

namespace Reth.Itss2.Standard.Dialogs.ArticleData.StockDeliverySet
{
    public class StockDeliverySetResult:IEquatable<StockDeliverySetResult>
    {
        public static bool operator==( StockDeliverySetResult left, StockDeliverySetResult right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( StockDeliverySetResult left, StockDeliverySetResult right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( StockDeliverySetResult left, StockDeliverySetResult right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = false;

                                                        result = ( left.Value == right.Value );
			                                            result &= String.Equals( left.Text, right.Text, StringComparison.InvariantCultureIgnoreCase );

                                                        return result;
                                                    }   );
		}

        public StockDeliverySetResult( StockDeliverySetResultValue value, String text )
        {
            this.Value = value;
            this.Text = text;
        }

        public StockDeliverySetResultValue Value
        {
            get;
        }

        public String Text
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as StockDeliverySetResult );
		}
		
		public bool Equals( StockDeliverySetResult other )
		{
            return StockDeliverySetResult.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

        public override String ToString()
        {
            return this.Value.ToString();
        }
    }
}