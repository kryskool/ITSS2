using System;
using System.Diagnostics;

using Reth.Protocols;

namespace Reth.Itss2.Standard.Dialogs
{
    public class StockLocationId:String64, IEquatable<StockLocationId>
    {
        public static bool operator==( StockLocationId left, StockLocationId right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( StockLocationId left, StockLocationId right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( StockLocationId left, StockLocationId right )
		{
			return String64.Equals( left, right );
		}

		public StockLocationId( String value )
        :
            base( value )
		{
            Debug.Assert( value.Length > 0, $"{ nameof( value ) }.{ nameof( value.Length ) } > 0" );

            if( value.Length <= 0 )
            {
                throw new ArgumentOutOfRangeException( nameof( value ), $"Stock location id must not be empty." );
            }
		}

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as StockLocationId );
		}
		
        public bool Equals( StockLocationId other )
		{
            return StockLocationId.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override String ToString()
        {
            return this.Value;
        }
    }
}