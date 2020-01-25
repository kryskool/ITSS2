using System;
using System.Runtime.Serialization;

namespace Reth.Protocols
{
    [Serializable]
    public class RequestException:Exception
    {
        public RequestException()
        {
        }

        public RequestException( String message )
        :
            base( message )
        {
        }

        public RequestException( String message, Exception innerException )
        :
            base( message, innerException )
        {
        }

        protected RequestException( SerializationInfo info, StreamingContext context )
        :
            base( info, context )
        {
        }
    }
}
