using System;
using System.Diagnostics;

using Reth.Protocols;
using Reth.Protocols.Dialogs;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs
{
    public sealed class MessageEnvelope:IEquatable<MessageEnvelope>, IMessageEnvelope
    {
        public const String DefaultVersion = "2.0";

        public static bool operator==( MessageEnvelope left, MessageEnvelope right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( MessageEnvelope left, MessageEnvelope right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}
        
        public MessageEnvelope( IMessage message )
        {
            message.ThrowIfNull();

            this.Message = message;
        }

        public MessageEnvelope( IMessage message, MessageEnvelopeTimestamp timestamp )
        {
            message.ThrowIfNull();
            timestamp.ThrowIfNull();

            this.Message = message;
            this.Timestamp = timestamp;
        }

        public MessageEnvelope( IMessage message, MessageEnvelopeTimestamp timestamp, String version )
        {
            message.ThrowIfNull();
            timestamp.ThrowIfNull();

            Debug.Assert(   version == MessageEnvelope.DefaultVersion,
                            $"{ nameof( version ) } == { nameof( MessageEnvelope ) }.{ nameof( MessageEnvelope.DefaultVersion ) }"  );

            if( version != MessageEnvelope.DefaultVersion )
            {
                throw new FormatException( $"Invalid message version: { version }" );
            }

            this.Message = message;
            this.Timestamp = timestamp;
            this.Version = version;
        }

        public IMessage Message
        {
            get;
        }

        public MessageEnvelopeTimestamp Timestamp
        {
            get;
        } = MessageEnvelopeTimestamp.UtcNow;
        
        public String Version
        {
            get;
        } = MessageEnvelope.DefaultVersion;

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as MessageEnvelope );
		}
		
		public bool Equals( MessageEnvelope other )
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