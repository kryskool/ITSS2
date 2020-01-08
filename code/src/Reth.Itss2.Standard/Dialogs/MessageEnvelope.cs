using System;
using System.Diagnostics;

using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs
{
    public sealed class MessageEnvelope<TMessage>:IEquatable<MessageEnvelope<TMessage>>
        where TMessage:IMessage
    {
        public const String DefaultVersion = "2.0";

        public static bool operator==( MessageEnvelope<TMessage> left, MessageEnvelope<TMessage> right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( MessageEnvelope<TMessage> left, MessageEnvelope<TMessage> right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        private TMessage message = default( TMessage );
        private MessageEnvelopeTimestamp timestamp = MessageEnvelopeTimestamp.UtcNow;
        private String version = MessageEnvelope<TMessage>.DefaultVersion;
        
        public MessageEnvelope( TMessage message )
        {
            this.Message = message;
        }

        public MessageEnvelope( TMessage message, MessageEnvelopeTimestamp timeStamp )
        {
            this.Message = message;
            this.Timestamp = timeStamp;
        }

        public MessageEnvelope( TMessage message, MessageEnvelopeTimestamp timestamp, String version )
        {
            this.Message = message;
            this.Timestamp = timestamp;
            this.Version = version;
        }

        public TMessage Message
        {
            get{ return this.message; }

            private set
            {
                ( value as Object ).ThrowIfNull();

                this.message = value;
            }
        }

        public MessageEnvelopeTimestamp Timestamp
        {
            get{ return this.timestamp; }
            
            private set
            {
                timestamp.ThrowIfNull();

                this.timestamp = value;
            }
        }
        
        public String Version
        {
            get{ return this.version; }

            private set
            {
                Debug.Assert(   value == MessageEnvelope<TMessage>.DefaultVersion,
                                $"{ nameof( value ) } == { nameof( MessageEnvelope<TMessage> ) }.{ nameof( MessageEnvelope<TMessage>.DefaultVersion ) }"  );

                if( value != MessageEnvelope<TMessage>.DefaultVersion )
                {
                    throw new FormatException( $"Invalid message version: { value }" );
                }

                this.version = value;
            }
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as MessageEnvelope<TMessage> );
		}
		
		public bool Equals( MessageEnvelope<TMessage> other )
		{
            return ObjectEqualityComparer.Equals(   this,
                                                    other,
                                                    () =>
                                                    {
                                                        bool result = false;

                                                        result = this.Version.Equals( other.Version, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= this.Timestamp.Equals( other.Timestamp );
                                                        result &= this.Message.Equals( other.Message );

                                                        return result;
                                                    } );
		}

        public override int GetHashCode()
        {
            return this.Timestamp.GetHashCode();
        }

        public override String ToString()
        {
            return this.Message.ToString();
        }
    }
}