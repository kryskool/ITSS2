using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.General.Unprocessed;
using Reth.Protocols;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;
using Reth.Protocols.Serialization.Xml.Parsing;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.General.Unprocessed
{
    internal class UnprocessedContentDataContract<TTypeMappings>:XmlSerializable<UnprocessedContent, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public UnprocessedContentDataContract()
        {
            this.MessageParser = new MessageParser( this.TypeMappings );
        }

        public UnprocessedContentDataContract( UnprocessedContent dataObject )
        :
            base( dataObject )
        {
            this.MessageParser = new MessageParser( this.TypeMappings );
        }
        
        private MessageParser MessageParser
        {
            get;
        }

        public override void ReadXml( XmlReader reader )
        {
            MessageId id = base.Serializer.ReadOptionalAttribute<MessageId>( reader, nameof( this.DataObject.Id ) );
            String value = base.Serializer.ReadMandatoryCData( reader );

            IMessage content = this.MessageParser.Parse( value );

            if( content is null )
            {
                content = new RawMessage( value );
            }

            this.DataObject = new UnprocessedContent( content, id );
        }

        public override void WriteXml( XmlWriter writer )
        {
            UnprocessedContent dataObject = this.DataObject;

            base.Serializer.WriteOptionalAttribute<MessageId>( writer, nameof( dataObject.Id ), dataObject.Id );
            
            String content = String.Empty;
            
            IMessage message = dataObject.Data;

            RawMessage rawMessage = message as RawMessage;

            if( !( rawMessage is null ) )
            {
                content = rawMessage.Value;
            }else
            {
                content = this.MessageParser.Parse( message );
            }
            
            if( content is null )
            {
                content = $"Unprocessed message: { message.Id }";
            }

            base.Serializer.WriteMandatoryCData( writer, content );
        }
    }
}