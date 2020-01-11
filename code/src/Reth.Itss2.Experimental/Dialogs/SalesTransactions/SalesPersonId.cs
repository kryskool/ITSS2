using System;
using System.Diagnostics;

using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols;

namespace Reth.Itss2.Experimental.Dialogs
{
    public class SalesPersonId:String64, IComparable<SalesPersonId>, IEquatable<SalesPersonId>
    {
        public static bool operator==( SalesPersonId left, SalesPersonId right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( SalesPersonId left, SalesPersonId right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool operator<( SalesPersonId left, SalesPersonId right )
		{
			return ObjectComparer.LessThan( left, right );
		}
		
		public static bool operator<=( SalesPersonId left, SalesPersonId right )
		{
			return ObjectComparer.LessThanOrEqual( left, right );
		}
		
		public static bool operator>( SalesPersonId left, SalesPersonId right )
		{
            return ObjectComparer.GreaterThan( left, right );
		}
		
		public static bool operator>=( SalesPersonId left, SalesPersonId right )
		{
			return ObjectComparer.GreaterThanOrEqual( left, right );
		}

        public static bool Equals( SalesPersonId left, SalesPersonId right )
		{
			return String64.Equals( left, right );
		}

        public static int Compare( SalesPersonId left, SalesPersonId right )
        {
            return String64.Compare( left, right );
        }

		public SalesPersonId( String value )
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
			return this.Equals( obj as SalesPersonId );
		}
		
        public bool Equals( SalesPersonId other )
		{
            return SalesPersonId.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int CompareTo( SalesPersonId other )
		{
            return SalesPersonId.Compare( this, other );
		}

        public override String ToString()
        {
            return this.Value;
        }
    }
}