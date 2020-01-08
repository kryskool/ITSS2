using System;
using System.Diagnostics;

using Reth.Protocols;

namespace Reth.Itss2.Standard.Dialogs
{
    public class PackId:String64, IComparable<PackId>, IEquatable<PackId>
    {
        public static bool operator==( PackId left, PackId right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( PackId left, PackId right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool operator<( PackId left, PackId right )
		{
			return ObjectComparer.LessThan( left, right );
		}
		
		public static bool operator<=( PackId left, PackId right )
		{
			return ObjectComparer.LessThanOrEqual( left, right );
		}
		
		public static bool operator>( PackId left, PackId right )
		{
            return ObjectComparer.GreaterThan( left, right );
		}
		
		public static bool operator>=( PackId left, PackId right )
		{
			return ObjectComparer.GreaterThanOrEqual( left, right );
		}

        public static bool Equals( PackId left, PackId right )
		{
			return String64.Equals( left, right );
		}

        public static int Compare( PackId left, PackId right )
        {
            return String64.Compare( left, right );
        }

		public PackId( String value )
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
			return this.Equals( obj as PackId );
		}
		
        public bool Equals( PackId other )
		{
            return PackId.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int CompareTo( PackId other )
		{
            return PackId.Compare( this, other );
		}

        public override String ToString()
        {
            return this.Value;
        }
    }
}