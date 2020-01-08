using System;
using System.Runtime.Serialization;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Client.Processing.Output
{
    [Serializable]
    public class OutputProcessException:Exception
    {
        public OutputProcessException()
        {
        }

        public OutputProcessException( string message )
        :
            base( message )
        {
        }

        public OutputProcessException( string message, Exception innerException )
        :
            base( message, innerException )
        {
        }

        protected OutputProcessException( SerializationInfo info, StreamingContext context )
        :
            base( info, context )
        {
        }
    }
}
