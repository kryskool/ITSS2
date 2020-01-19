using System;
using System.Runtime.Serialization;

namespace Reth.Protocols.Serialization
{
    [Serializable]
    public class MessageSerializationException:Exception
    {
        public MessageSerializationException()
        {
        }

        public MessageSerializationException( String message )
        :
            base( message )
        {
        }

        public MessageSerializationException( String message, Exception innerException )
        :
            base( message, innerException )
        {
        }

        protected MessageSerializationException( SerializationInfo info, StreamingContext context )
        :
            base( info, context )
        {
        }
    }
}
