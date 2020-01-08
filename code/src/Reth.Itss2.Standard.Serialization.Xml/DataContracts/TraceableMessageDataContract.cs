using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts
{
    public abstract class TraceableMessageDataContract<TMessage, TTypeMappings>:MessageDataContract<TMessage, TTypeMappings>
        where TMessage:TraceableMessage
        where TTypeMappings:ITypeMappings
    {
        public TraceableMessageDataContract()
        {
        }

        public TraceableMessageDataContract( TMessage dataObject )
        :
            base( dataObject )
        {
        }

        protected SubscriberId ReadSource( XmlReader reader )
        {
            return base.Serializer.ReadMandatoryAttribute<SubscriberId>( reader, nameof( this.DataObject.Source ) );
        }

        protected SubscriberId ReadDestination( XmlReader reader )
        {
            return base.Serializer.ReadMandatoryAttribute<SubscriberId>( reader, nameof( this.DataObject.Destination ) );
        }

        protected void WriteSource( XmlWriter writer )
        {
            base.Serializer.WriteMandatoryAttribute<SubscriberId>( writer, nameof( this.DataObject.Source ), this.DataObject.Source );
        }

        protected void WriteDestination( XmlWriter writer )
        {
            base.Serializer.WriteMandatoryAttribute<SubscriberId>( writer, nameof( this.DataObject.Destination ), this.DataObject.Destination );
        }

        public override void WriteXml( XmlWriter writer )
        {
            base.WriteId( writer );
            this.WriteSource( writer );
            this.WriteDestination( writer );
        }
    }
}