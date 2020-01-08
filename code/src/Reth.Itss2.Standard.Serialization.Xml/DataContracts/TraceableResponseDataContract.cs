using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols.Serialization;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts
{
    public abstract class TraceableResponseDataContract<TResponse, TTypeMappings>:TraceableMessageDataContract<TResponse, TTypeMappings>
        where TResponse:TraceableResponse
        where TTypeMappings:ITypeMappings
    {
        public TraceableResponseDataContract()
        {
        }

        public TraceableResponseDataContract( TResponse dataObject )
        :
            base( dataObject )
        {
        }
    }
}