using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts
{
    internal class MessageIdDataContract<TTypeMappings>:XmlSerializable<IMessageId, TypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public MessageIdDataContract()
        {
        }

        public MessageIdDataContract( IMessageId dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {         
            this.DataObject = new MessageId( reader.ReadContentAsString() );
        }

        public override void WriteXml( XmlWriter writer )
        {
            writer.WriteString( this.DataObject.Value );
        }
    }
}