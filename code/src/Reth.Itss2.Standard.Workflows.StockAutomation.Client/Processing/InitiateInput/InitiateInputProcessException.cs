using System;
using System.Runtime.Serialization;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Client.Processing.InitiateInput
{
    [Serializable]
    public class InitiateInputProcessException:Exception
    {
        public InitiateInputProcessException()
        {
        }

        public InitiateInputProcessException( string message )
        :
            base( message )
        {
        }

        public InitiateInputProcessException( string message, Exception innerException )
        :
            base( message, innerException )
        {
        }

        protected InitiateInputProcessException( SerializationInfo info, StreamingContext context )
        :
            base( info, context )
        {
        }
    }
}
