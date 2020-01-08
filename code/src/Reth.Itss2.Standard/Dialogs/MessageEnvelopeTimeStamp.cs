using System;
using System.Globalization;

using Reth.Protocols;

namespace Reth.Itss2.Standard.Dialogs
{
    public sealed class MessageEnvelopeTimestamp:IEquatable<MessageEnvelopeTimestamp>
    {
        public static MessageEnvelopeTimestamp Parse( String value )
        {
            return new MessageEnvelopeTimestamp( DateTimeOffset.Parse( value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal ) );
        }

        public static bool TryParse( String value, out MessageEnvelopeTimestamp result )
        {
            DateTimeOffset timeStamp = DateTimeOffset.UtcNow;    

            bool success = DateTimeOffset.TryParse( value, out timeStamp );

            result = new MessageEnvelopeTimestamp( timeStamp );
            
            return success;
        }

        public static bool operator==( MessageEnvelopeTimestamp left, MessageEnvelopeTimestamp right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( MessageEnvelopeTimestamp left, MessageEnvelopeTimestamp right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( MessageEnvelopeTimestamp left, MessageEnvelopeTimestamp right )
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
                                                        result &= ( thisValue.Hour == otherValue.Hour );
                                                        result &= ( thisValue.Minute == otherValue.Minute );
                                                        result &= ( thisValue.Second == otherValue.Second );
                                                        result &= ( thisValue.Offset.Equals( otherValue.Offset ) );

                                                        return result;
                                                    }   );
		}

        public static MessageEnvelopeTimestamp UtcNow
        {
            get{ return new MessageEnvelopeTimestamp( DateTimeOffset.UtcNow ); }
        }

        public MessageEnvelopeTimestamp()
        :
            this( DateTimeOffset.UtcNow )
        {
        }

        public MessageEnvelopeTimestamp( DateTimeOffset value )
        {
            this.Value = value;
        }

        public DateTimeOffset Value
        {
            get;
        } = DateTimeOffset.UtcNow;

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as MessageEnvelopeTimestamp );
		}
		
		public bool Equals( MessageEnvelopeTimestamp other )
		{
            return MessageEnvelopeTimestamp.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

        public override String ToString()
        {
            return String.Format( CultureInfo.InvariantCulture, "{0:yyyy-MM-ddTHH:mm:ssZ}", this.Value );
        }
    }
}