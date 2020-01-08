using System;
using System.Globalization;
using System.Text;

using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Protocols.Serialization.Xml.Parsing
{
    internal class MessageBlockPatternMatch:IEquatable<MessageBlockPatternMatch>
    {
        public static readonly MessageBlockPatternMatch None = new MessageBlockPatternMatch( MessageBlockPattern.Empty, -1 );

        public static bool operator==( MessageBlockPatternMatch left, MessageBlockPatternMatch right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( MessageBlockPatternMatch left, MessageBlockPatternMatch right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( MessageBlockPatternMatch left, MessageBlockPatternMatch right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = false;

                                                        result = MessageBlockPattern.Equals( left.Pattern, right.Pattern );
                                                        result &= ( left.StartIndex == right.StartIndex );
                                                        
                                                        return result;
                                                    }   );
		}

        public MessageBlockPatternMatch( MessageBlockPattern pattern, int startIndex )
        {
            pattern.ThrowIfNull();

            this.Pattern = pattern;
            this.StartIndex = startIndex;
        }

        public MessageBlockPattern Pattern
        {
            get;
        }

        public int StartIndex
        {
            get;
        }

        public int Length
        {
            get{ return this.Pattern.Count; }
        }

        public bool Success
        {
            get
            {
                return ( this.StartIndex >= 0 );
            }
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as MessageBlockPatternMatch );
		}
		
		public bool Equals( MessageBlockPatternMatch other )
		{
            return MessageBlockPatternMatch.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return this.Pattern.GetHashCode();
		}

        public override String ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append( "'" );
            result.Append( this.Pattern.ToString() );
            result.Append( "'(" );
            result.Append( this.StartIndex.ToString( CultureInfo.InvariantCulture ) );
            result.Append( ")" );

            return result.ToString();
        }
    }
}
