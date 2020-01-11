using System;
using System.Diagnostics;

using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols;

namespace Reth.Itss2.Experimental.Dialogs
{
    public class ShoppingCartId:String64, IComparable<ShoppingCartId>, IEquatable<ShoppingCartId>
    {
        public static bool operator==( ShoppingCartId left, ShoppingCartId right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( ShoppingCartId left, ShoppingCartId right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool operator<( ShoppingCartId left, ShoppingCartId right )
		{
			return ObjectComparer.LessThan( left, right );
		}
		
		public static bool operator<=( ShoppingCartId left, ShoppingCartId right )
		{
			return ObjectComparer.LessThanOrEqual( left, right );
		}
		
		public static bool operator>( ShoppingCartId left, ShoppingCartId right )
		{
            return ObjectComparer.GreaterThan( left, right );
		}
		
		public static bool operator>=( ShoppingCartId left, ShoppingCartId right )
		{
			return ObjectComparer.GreaterThanOrEqual( left, right );
		}

        public static bool Equals( ShoppingCartId left, ShoppingCartId right )
		{
			return String64.Equals( left, right );
		}

        public static int Compare( ShoppingCartId left, ShoppingCartId right )
        {
            return String64.Compare( left, right );
        }

		public ShoppingCartId( String value )
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
			return this.Equals( obj as ShoppingCartId );
		}
		
        public bool Equals( ShoppingCartId other )
		{
            return ShoppingCartId.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int CompareTo( ShoppingCartId other )
		{
            return ShoppingCartId.Compare( this, other );
		}

        public override String ToString()
        {
            return this.Value;
        }
    }
}