using System;
using System.Globalization;

using Reth.Protocols;

namespace Reth.Itss2.Standard.Dialogs
{
    public class PackDate:IComparable<PackDate>, IEquatable<PackDate>
    {
        public static PackDate Parse( String value )
        {
            return new PackDate( DateTimeOffset.Parse( value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal ) );
        }

        public static bool TryParse( String value, out PackDate result )
        {
            DateTimeOffset timeStamp = DateTimeOffset.UtcNow;    

            bool success = DateTimeOffset.TryParse( value, out timeStamp );

            result = new PackDate( timeStamp );
            
            return success;
        }

        public static bool operator==( PackDate left, PackDate right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( PackDate left, PackDate right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool operator<( PackDate left, PackDate right )
		{
			return ObjectComparer.LessThan( left, right );
		}
		
		public static bool operator<=( PackDate left, PackDate right )
		{
			return ObjectComparer.LessThanOrEqual( left, right );
		}
		
		public static bool operator>( PackDate left, PackDate right )
		{
            return ObjectComparer.GreaterThan( left, right );
		}
		
		public static bool operator>=( PackDate left, PackDate right )
		{
			return ObjectComparer.GreaterThanOrEqual( left, right );
		}

        public static bool Equals( PackDate left, PackDate right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = false;

                                                        DateTimeOffset thisValue = left.Value;
                                                        DateTimeOffset otherValue = right.Value;

			                                            result = ( thisValue.Year == otherValue.Year );
                                                        result &= ( thisValue.Month == otherValue.Month );
                                                        result &= ( thisValue.Day == otherValue.Day );
                                                        result &= ( thisValue.Offset.Equals( otherValue.Offset ) );

                                                        return result;
                                                    }   );
		}

        public static int Compare( PackDate left, PackDate right )
		{
            return ObjectComparer.Compare(  left,
                                            right,
                                            () =>
                                            {
                                                DateTimeOffset leftValue = left.Value;
                                                DateTimeOffset rightValue = right.Value;

                                                int result = leftValue.Year.CompareTo( rightValue.Year );

                                                if( result == 0 )
                                                {
                                                    result = leftValue.Month.CompareTo( rightValue.Month );

                                                    if( result == 0 )
                                                    {
                                                        result = leftValue.Day.CompareTo( rightValue.Day );

                                                        if( result == 0 )
                                                        {
                                                            result = leftValue.Offset.CompareTo( rightValue.Offset );
                                                        }
                                                    }
                                                }

                                                return result;
                                            }   );
		}

        public static PackDate UtcNow
        {
            get{ return new PackDate( DateTimeOffset.UtcNow ); }
        }

        public PackDate()
        :
            this( DateTimeOffset.UtcNow )
        {
        }

        public PackDate( DateTimeOffset value )
        {
            this.Value = value;
        }

        public PackDate( int year, int month, int day )
        {
            this.Value = new DateTimeOffset( year, month, day, 0, 0, 0, TimeSpan.Zero );
        }

        public DateTimeOffset Value
        {
            get;
        } = DateTimeOffset.UtcNow;

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as PackDate );
		}
		
		public bool Equals( PackDate other )
		{
            return PackDate.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

        public int CompareTo( PackDate other )
		{
            return PackDate.Compare( this, other );
		}

        public override String ToString()
        {
            return String.Format( CultureInfo.InvariantCulture, "{0:yyyy-MM-dd}", this.Value );
        }
    }
}