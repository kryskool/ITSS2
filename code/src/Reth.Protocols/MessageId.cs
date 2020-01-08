using System;

using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Protocols
{
    internal class MessageId:IComparable<MessageId>, IEquatable<MessageId>, IMessageId
    {
        public static MessageId DefaultId
        {
            get;
        } = new MessageId( "0" );

        public static bool operator==( MessageId left, MessageId right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( MessageId left, MessageId right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool operator<( MessageId left, MessageId right )
		{
			return ObjectComparer.LessThan( left, right );
		}
		
		public static bool operator<=( MessageId left, MessageId right )
		{
			return ObjectComparer.LessThanOrEqual( left, right );
		}
		
		public static bool operator>( MessageId left, MessageId right )
		{
            return ObjectComparer.GreaterThan( left, right );
		}
		
		public static bool operator>=( MessageId left, MessageId right )
		{
			return ObjectComparer.GreaterThanOrEqual( left, right );
		}

        public static bool Equals( IMessageId left, IMessageId right )
		{
			return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        return left.Value.Equals( right.Value, StringComparison.InvariantCultureIgnoreCase );
                                                    }   );
		}

        public static bool Equals( MessageId left, MessageId right )
		{
			return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        return left.Value.Equals( right.Value, StringComparison.InvariantCultureIgnoreCase );
                                                    }   );
		}

        public static int Compare( IMessageId left, IMessageId right )
        {
            return ObjectComparer.Compare(  left,
                                            right,
                                            () =>
                                            {
                                                return String.CompareOrdinal( left.Value, right.Value );
                                            }   );
        }

        public static int Compare( MessageId left, MessageId right )
        {
            return ObjectComparer.Compare(  left,
                                            right,
                                            () =>
                                            {
                                                return String.CompareOrdinal( left.Value, right.Value );
                                            }   );
        }

		private MessageId( String value )
		{
            value.ThrowIfNull();

            this.Value = value;
		}

        public String Value
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as MessageId );
		}
		
        public bool Equals( IMessageId other )
		{
            return MessageId.Equals( this, other );
		}

        public bool Equals( MessageId other )
		{
            return MessageId.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int CompareTo( IMessageId other )
		{
            return MessageId.Compare( this, other );
		}

        public int CompareTo( MessageId other )
		{
            return MessageId.Compare( this, other );
		}

        public override String ToString()
        {
            return this.Value;
        }
    }
}