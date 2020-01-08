using System;
using System.Diagnostics;

using Reth.Protocols;

namespace Reth.Itss2.Standard.Dialogs
{
    public class ProductCodeId:String64, IComparable<ProductCodeId>, IEquatable<ProductCodeId>
    {
        public static bool operator==( ProductCodeId left, ProductCodeId right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( ProductCodeId left, ProductCodeId right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool operator<( ProductCodeId left, ProductCodeId right )
		{
			return ObjectComparer.LessThan( left, right );
		}
		
		public static bool operator<=( ProductCodeId left, ProductCodeId right )
		{
			return ObjectComparer.LessThanOrEqual( left, right );
		}
		
		public static bool operator>( ProductCodeId left, ProductCodeId right )
		{
            return ObjectComparer.GreaterThan( left, right );
		}
		
		public static bool operator>=( ProductCodeId left, ProductCodeId right )
		{
			return ObjectComparer.GreaterThanOrEqual( left, right );
		}

        public static bool Equals( ProductCodeId left, ProductCodeId right )
		{
			return String64.Equals( left, right );
		}

        public static int Compare( ProductCodeId left, ProductCodeId right )
        {
            return String64.Compare( left, right );
        }

		public ProductCodeId( String value )
        :
            base( value )
		{
            Debug.Assert( value.Length > 0, $"{ nameof( value ) }.{ nameof( value.Length ) } > 0" );

            if( value.Length <= 0 )
            {
                throw new ArgumentOutOfRangeException( nameof( value ), $"Id must not be empty." );
            }
		}

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as ProductCodeId );
		}
		
        public bool Equals( ProductCodeId other )
		{
            return ProductCodeId.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int CompareTo( ProductCodeId other )
		{
            return ProductCodeId.Compare( this, other );
		}

        public override String ToString()
        {
            return this.Value;
        }
    }
}