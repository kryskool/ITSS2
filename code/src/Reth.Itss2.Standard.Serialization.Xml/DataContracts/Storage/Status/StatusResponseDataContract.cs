using System;
using System.Collections.Generic;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage.Status;
using Reth.Protocols;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.Status
{
    internal class StatusResponseDataContract<TTypeMappings>:TraceableResponseDataContract<StatusResponse, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public StatusResponseDataContract()
        {
        }

        public StatusResponseDataContract( StatusResponse dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {
            IMessageId id = base.ReadId( reader );
            SubscriberId source = base.ReadSource( reader );
            SubscriberId destination = base.ReadDestination( reader );

            ComponentState state = base.Serializer.ReadMandatoryEnum<ComponentState>( reader, nameof( this.DataObject.State ) );
            String stateText = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.StateText ) );

            IEnumerable<Component> components = base.Serializer.ReadOptionalElements<Component>( reader, nameof( Component ) );

            this.DataObject = new StatusResponse( id, source, destination, state, stateText, components );
        }

        public override void WriteXml( XmlWriter writer )
        {
            base.WriteXml( writer );

            StatusResponse dataObject = this.DataObject;

            base.Serializer.WriteMandatoryEnum<ComponentState>( writer, nameof( dataObject.State ), dataObject.State );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.StateText ), dataObject.StateText );
            base.Serializer.WriteOptionalElements<Component>( writer, nameof( Component ), dataObject.GetComponents() );
        }
    }
}