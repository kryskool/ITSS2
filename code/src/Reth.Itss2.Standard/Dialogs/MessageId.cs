using System;
using System.Diagnostics;
using System.Globalization;

using Reth.Protocols;

namespace Reth.Itss2.Standard.Dialogs
{
    public class MessageId:String64, IComparable<MessageId>, IEquatable<MessageId>, IMessageId
    {
        private static Object SyncRoot
        {
            get;
        } = new Object();
                
        private static ulong LastGeneratedId
        {
            get; set;
        }

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
                                                        return String64.Equals( ( MessageId )left, ( MessageId )right );
                                                    }   );			
		}

        public static bool Equals( MessageId left, MessageId right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        return String64.Equals( left, right );
                                                    }   );			
		}

        public static int Compare( IMessageId left, IMessageId right )
        {
            return ObjectComparer.Compare(  left,
                                            right,
                                            () =>
                                            {
                                                return String64.Compare( ( MessageId )left, ( MessageId )right );
                                            }   );
        }

        public static int Compare( MessageId left, MessageId right )
        {
            return ObjectComparer.Compare(  left,
                                            right,
                                            () =>
                                            {
                                                return String64.Compare( left, right );
                                            }   );
        }

        public static MessageId NewId()
        {
            MessageId result = null;

            lock( MessageId.SyncRoot )
            {
                unchecked
                {
                    result = new MessageId( ( ++MessageId.LastGeneratedId ).ToString( CultureInfo.InvariantCulture ) );
                }
            }

            return result;
        }

		public MessageId( String value )
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