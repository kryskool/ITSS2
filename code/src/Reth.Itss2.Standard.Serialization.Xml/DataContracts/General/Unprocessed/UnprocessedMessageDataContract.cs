using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.General.Unprocessed;
using Reth.Protocols;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.General.Unprocessed
{
    internal class UnprocessedMessageDataContract<TTypeMappings>:TraceableMessageDataContract<UnprocessedMessage, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        private const String MessageName = "Message";

        public UnprocessedMessageDataContract()
        {
        }

        public UnprocessedMessageDataContract( UnprocessedMessage dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {
            IMessageId id = base.ReadId( reader );
            SubscriberId source = base.ReadSource( reader );
            SubscriberId destination = base.ReadDestination( reader );

            String text = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.Text ) );
            Nullable<UnprocessedReason> reason = base.Serializer.ReadOptionalEnum<UnprocessedReason>( reader, nameof( this.DataObject.Reason ) );
            UnprocessedContent content = base.Serializer.ReadOptionalElement<UnprocessedContent>( reader, UnprocessedMessageDataContract<TTypeMappings>.MessageName );

            this.DataObject = new UnprocessedMessage( id, source, destination, content, reason, text );
        }

        public override void WriteXml( XmlWriter writer )
        {
            base.WriteXml( writer );

            UnprocessedMessage dataObject = this.DataObject;

            base.Serializer.WriteOptionalString( writer, nameof( dataObject.Text ), this.DataObject.Text );
            base.Serializer.WriteOptionalEnum<UnprocessedReason>( writer, nameof( dataObject.Reason ), this.DataObject.Reason );
            base.Serializer.WriteOptionalElement<UnprocessedContent>( writer, UnprocessedMessageDataContract<TTypeMappings>.MessageName, this.DataObject.Message );
        }
    }
}