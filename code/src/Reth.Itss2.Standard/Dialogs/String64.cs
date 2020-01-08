using System;
using System.Diagnostics;

using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs
{
    public abstract class String64:IComparable<String64>, IEquatable<String64>
    {
        public const int MaxLength = 64;

        public static bool operator==( String64 left, String64 right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( String64 left, String64 right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool operator<( String64 left, String64 right )
		{
			return ObjectComparer.LessThan( left, right );
		}
		
		public static bool operator<=( String64 left, String64 right )
		{
			return ObjectComparer.LessThanOrEqual( left, right );
		}
		
		public static bool operator>( String64 left, String64 right )
		{
            return ObjectComparer.GreaterThan( left, right );
		}
		
		public static bool operator>=( String64 left, String64 right )
		{
			return ObjectComparer.GreaterThanOrEqual( left, right );
		}

        public static bool Equals( String64 left, String64 right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        return left.Value.Equals( right.Value, StringComparison.InvariantCultureIgnoreCase );
                                                    }   );
		}

        public static int Compare( String64 left, String64 right )
		{
            return ObjectComparer.Compare(  left,
                                            right,
                                            () =>
                                            {
                                                return String.CompareOrdinal( left.Value, right.Value );
                                            }   );
		}
        
        private String value = String.Empty;

		protected String64( String value )
		{
            this.Value = value;
		}

        public String Value
        {
            get{ return this.value; }

            private set
            {
                value.ThrowIfNull();

                Debug.Assert( value.Length <= String64.MaxLength, $"{ nameof( value ) }.{ nameof( value.Length ) } <= { String64.MaxLength }" );

                if( value.Length > String64.MaxLength )
                {
                    throw new ArgumentOutOfRangeException( nameof( value ), $"Value is less than minimum of { String64.MaxLength }." );
                }

                this.value = value;
            }
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as String64 );
		}
		
        public bool Equals( String64 other )
		{
            return String64.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public int CompareTo( String64 other )
		{
            return String64.Compare( this, other );
		}

        public override String ToString()
        {
            return this.Value;
        }
    }
}