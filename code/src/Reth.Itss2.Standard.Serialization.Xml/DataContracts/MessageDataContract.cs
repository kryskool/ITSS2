using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;

using Reth.Protocols;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts
{
    public abstract class MessageDataContract<TMessage, TTypeMappings>:XmlSerializable<TMessage, TTypeMappings>
        where TMessage:class, IMessage
        where TTypeMappings:ITypeMappings
    {
        protected MessageDataContract()
        {
        }

        protected MessageDataContract( TMessage dataObject )
        :
            base( dataObject )
        {
        }

        protected IMessageId ReadId( XmlReader reader )
        {
            return base.Serializer.ReadMandatoryAttribute<IMessageId>( reader, nameof( this.DataObject.Id ) );
        }

        protected void WriteId( XmlWriter writer )
        {
            base.Serializer.WriteMandatoryAttribute<IMessageId>( writer, nameof( this.DataObject.Id ), this.DataObject.Id );
        }

        public override void WriteXml( XmlWriter writer )
        {
            this.WriteId( writer );
        }

        public override String ToString()
        {
            return this.DataObject.Id.ToString();
        }
    }
}