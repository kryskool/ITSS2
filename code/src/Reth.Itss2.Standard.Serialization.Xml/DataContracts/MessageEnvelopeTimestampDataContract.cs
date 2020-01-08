using System.Globalization;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts
{
    internal class MessageEnvelopeTimestampDataContract<TTypeMappings>:XmlSerializable<MessageEnvelopeTimestamp, TypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public MessageEnvelopeTimestampDataContract()
        {
        }

        public MessageEnvelopeTimestampDataContract( MessageEnvelopeTimestamp dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {            
            this.DataObject = MessageEnvelopeTimestamp.Parse( reader.ReadContentAsString() );
        }

        public override void WriteXml( XmlWriter writer )
        {
            writer.WriteString( this.DataObject.Value.ToString( CultureInfo.InvariantCulture ) );
        }
    }
}