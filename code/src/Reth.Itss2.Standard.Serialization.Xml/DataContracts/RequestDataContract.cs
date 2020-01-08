using Reth.Protocols;
using Reth.Protocols.Serialization;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts
{
    internal abstract class RequestDataContract<TRequest, TTypeMappings>:MessageDataContract<TRequest, TTypeMappings>
        where TRequest:Request
        where TTypeMappings:ITypeMappings
    {
        public RequestDataContract()
        {
        }

        public RequestDataContract( TRequest dataObject )
        :
            base( dataObject )
        {
        }
    }
}