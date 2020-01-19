using System;
using System.Runtime.Serialization;

namespace Reth.Protocols.Serialization
{
    [Serializable]
    public class MessageTypeException:Exception
    {
        public MessageTypeException()
        {
        }

        public MessageTypeException( String message )
        :
            base( message )
        {
        }

        public MessageTypeException( String message, Exception innerException )
        :
            base( message, innerException )
        {
        }

        protected MessageTypeException( SerializationInfo info, StreamingContext context )
        :
            base( info, context )
        {
        }
    }
}
