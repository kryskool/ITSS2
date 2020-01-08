using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml
{
    public class ProtocolProvider:IProtocolProvider
    {
        public ProtocolProvider()
        {
            this.MessageSerializer = MessageSerializerFactory.Create( this.TypeMappings ); 
        }

        public ITypeMappings TypeMappings
        {
            get;
        } = new TypeMappings();

        public IMessageSerializer MessageSerializer
        {
            get;
        }
    }
}
