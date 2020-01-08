using System;
using System.Runtime.Serialization;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Client.Processing.Input
{
    [Serializable]
    public class InputProcessException:Exception
    {
        public InputProcessException()
        {
        }

        public InputProcessException( string message )
        :
            base( message )
        {
        }

        public InputProcessException( string message, Exception innerException )
        :
            base( message, innerException )
        {
        }

        protected InputProcessException( SerializationInfo info, StreamingContext context )
        :
            base( info, context )
        {
        }
    }
}
