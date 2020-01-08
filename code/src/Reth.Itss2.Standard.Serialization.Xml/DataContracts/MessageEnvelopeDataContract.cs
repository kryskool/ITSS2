using System;
using System.Xml;
using System.Xml.Serialization;

using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts
{
    [XmlRoot( "WWKS" )]
    internal class MessageEnvelopeDataContract<TMessage, TTypeMappings>:XmlSerializable<MessageEnvelope<TMessage>, TTypeMappings>
        where TMessage:class, IMessage
        where TTypeMappings:ITypeMappings
    {
        private const String TimestampName = "TimeStamp";

        public MessageEnvelopeDataContract()
        {
        }

        public MessageEnvelopeDataContract( MessageEnvelope<TMessage> dataObject )
        {
            this.DataObject = dataObject;
        }

        public override void ReadXml( XmlReader reader )
        {
            String version = base.Serializer.ReadMandatoryString( reader, nameof( this.DataObject.Version ) );
            MessageEnvelopeTimestamp timestamp = base.Serializer.ReadMandatoryAttribute<MessageEnvelopeTimestamp>( reader, MessageEnvelopeDataContract<TMessage, TTypeMappings>.TimestampName );

            TMessage content = base.Serializer.ReadMandatoryElement<TMessage>( reader, typeof( TMessage ).Name );

            this.DataObject = new MessageEnvelope<TMessage>( content, timestamp, version );
        }

        public override void WriteXml( XmlWriter writer )
        {
            MessageEnvelope<TMessage> dataObject = this.DataObject;

            base.Serializer.WriteMandatoryString( writer, nameof( dataObject.Version ), dataObject.Version );
            base.Serializer.WriteMandatoryAttribute<MessageEnvelopeTimestamp>( writer, MessageEnvelopeDataContract<TMessage, TTypeMappings>.TimestampName, dataObject.Timestamp );

            base.Serializer.WriteMandatoryElement<TMessage>( writer, typeof( TMessage ).Name, dataObject.Message );
        }

        public override String ToString()
        {
            return base.Serializer.WriteToString<MessageEnvelope<TMessage>>( this );
        }
    }
}