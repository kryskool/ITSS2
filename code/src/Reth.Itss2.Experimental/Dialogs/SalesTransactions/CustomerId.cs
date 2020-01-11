using System;
using System.Diagnostics;

using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols;

namespace Reth.Itss2.Experimental.Dialogs
{
    public class CustomerId:String64, IComparable<CustomerId>, IEquatable<CustomerId>
    {
        public static bool operator==( CustomerId left, CustomerId right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( CustomerId left, CustomerId right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool operator<( CustomerId left, CustomerId right )
		{
			return ObjectComparer.LessThan( left, right );
		}
		
		public static bool operator<=( CustomerId left, CustomerId right )
		{
			return ObjectComparer.LessThanOrEqual( left, right );
		}
		
		public static bool operator>( CustomerId left, CustomerId right )
		{
            return ObjectComparer.GreaterThan( left, right );
		}
		
		public static bool operator>=( CustomerId left, CustomerId right )
		{
			return ObjectComparer.GreaterThanOrEqual( left, right );
		}

        public static bool Equals( CustomerId left, CustomerId right )
		{
			return String64.Equals( left, right );
		}

        public static int Compare( CustomerId left, CustomerId right )
        {
            return String64.Compare( left, right );
        }

		public CustomerId( String value )
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
			return this.Equals( obj as CustomerId );
		}
		
        public bool Equals( CustomerId other )
		{
            return CustomerId.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int CompareTo( CustomerId other )
		{
            return CustomerId.Compare( this, other );
		}

        public override String ToString()
        {
            return this.Value;
        }
    }
}