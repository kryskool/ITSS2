using Reth.Protocols;
using Reth.Protocols.Serialization;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts
{
    internal abstract class ResponseDataContract<TResponse, TTypeMappings>:MessageDataContract<TResponse, TTypeMappings>
        where TResponse:Response
        where TTypeMappings:ITypeMappings
    {
        public ResponseDataContract()
        {
        }

        public ResponseDataContract( TResponse dataObject )
        :
            base( dataObject )
        {
        }
    }
}