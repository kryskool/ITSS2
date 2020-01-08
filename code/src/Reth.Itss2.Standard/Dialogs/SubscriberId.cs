using System;
using System.Diagnostics;
using System.Globalization;

using Reth.Protocols;

namespace Reth.Itss2.Standard.Dialogs
{
    public class SubscriberId:IComparable<SubscriberId>, IEquatable<SubscriberId>
    {
        public const uint MinReserved = 900;
        public const uint MaxReserved = 999;

        public static bool operator==( SubscriberId left, SubscriberId right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( SubscriberId left, SubscriberId right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool operator<( SubscriberId left, SubscriberId right )
		{
			return ObjectComparer.LessThan( left, right );
		}
		
		public static bool operator<=( SubscriberId left, SubscriberId right )
		{
			return ObjectComparer.LessThanOrEqual( left, right );
		}
		
		public static bool operator>( SubscriberId left, SubscriberId right )
		{
            return ObjectComparer.GreaterThan( left, right );
		}
		
		public static bool operator>=( SubscriberId left, SubscriberId right )
		{
			return ObjectComparer.GreaterThanOrEqual( left, right );
		}

        public static bool Equals( SubscriberId left, SubscriberId right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        return ( left.Value == right.Value );
                                                    }   );
		}

        public static int Compare( SubscriberId left, SubscriberId right )
        {
            return ObjectComparer.Compare(  left,
                                            right,
                                            () =>
                                            {
                                                return left.Value.CompareTo( right.Value );
                                            }   );
        }

        public static SubscriberId Invalid
        {
            get;
        } = new SubscriberId( 0, false );

        public static SubscriberId DefaultIMS
        {
            get;
        } = new SubscriberId( 100 );

        public static SubscriberId DefaultRobot
        {
            get;
        } = new SubscriberId( 999 );

        public static bool IsReserved( uint value )
        {
            bool result = false;

            if( value >= SubscriberId.MinReserved && value <= SubscriberId.MaxReserved )
            {
                result = true;
            }

            return result;
        }

        public SubscriberId( uint value )
        :
            this( value, true )
        {
        }

        private SubscriberId( uint value, bool validate )
        {
            if( validate == true )
            {
                Debug.Assert( value > 0, $"{ nameof( value ) } > 0" );

                if( value == 0 )
                {
                    throw new ArgumentOutOfRangeException( nameof( value ), "Subscriber id of zero is not allowed." );
                }
            }

            this.Value = value;
        }

        public uint Value
        {
            get; private set;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as SubscriberId );
		}
		
		public bool Equals( SubscriberId other )
		{
            return SubscriberId.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

        public int CompareTo( SubscriberId other )
		{
            return SubscriberId.Compare( this, other );
		}

        public bool IsReserved()
        {
            return SubscriberId.IsReserved( this.Value );
        }

        public override String ToString()
        {
            return this.Value.ToString( CultureInfo.InvariantCulture );
        }
    }
}