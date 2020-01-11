using System;
using System.Diagnostics;

using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols;

namespace Reth.Itss2.Experimental.Dialogs
{
    public class SalesPointId:String64, IComparable<SalesPointId>, IEquatable<SalesPointId>
    {
        public static bool operator==( SalesPointId left, SalesPointId right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( SalesPointId left, SalesPointId right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool operator<( SalesPointId left, SalesPointId right )
		{
			return ObjectComparer.LessThan( left, right );
		}
		
		public static bool operator<=( SalesPointId left, SalesPointId right )
		{
			return ObjectComparer.LessThanOrEqual( left, right );
		}
		
		public static bool operator>( SalesPointId left, SalesPointId right )
		{
            return ObjectComparer.GreaterThan( left, right );
		}
		
		public static bool operator>=( SalesPointId left, SalesPointId right )
		{
			return ObjectComparer.GreaterThanOrEqual( left, right );
		}

        public static bool Equals( SalesPointId left, SalesPointId right )
		{
			return String64.Equals( left, right );
		}

        public static int Compare( SalesPointId left, SalesPointId right )
        {
            return String64.Compare( left, right );
        }

		public SalesPointId( String value )
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
			return this.Equals( obj as SalesPointId );
		}
		
        public bool Equals( SalesPointId other )
		{
            return SalesPointId.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int CompareTo( SalesPointId other )
		{
            return SalesPointId.Compare( this, other );
		}

        public override String ToString()
        {
            return this.Value;
        }
    }
}