using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols.Serialization;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts
{
    public abstract class TraceableRequestDataContract<TRequest, TTypeMappings>:TraceableMessageDataContract<TRequest, TTypeMappings>
        where TRequest:TraceableRequest
        where TTypeMappings:ITypeMappings
    {
        public TraceableRequestDataContract()
        {
        }

        public TraceableRequestDataContract( TRequest dataObject )
        :
            base( dataObject )
        {
        }
    }
}