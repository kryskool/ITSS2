using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs.Storage.Status;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.Status
{
    internal class ComponentDataContract<TTypeMappings>:XmlSerializable<Component, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {     
        public ComponentDataContract()
        {
        }

        public ComponentDataContract( Component dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {
            ComponentType type = base.Serializer.ReadMandatoryEnum<ComponentType>( reader, nameof( this.DataObject.Type ) );
            ComponentState state = base.Serializer.ReadMandatoryEnum<ComponentState>( reader, nameof( this.DataObject.State ) );
            String description = base.Serializer.ReadMandatoryString( reader, nameof( this.DataObject.Description ) );
            String stateText = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.StateText ) );

            this.DataObject = new Component( type, state, description, stateText );
        }

        public override void WriteXml( XmlWriter writer )
        {
            Component dataObject = this.DataObject;

            base.Serializer.WriteMandatoryEnum<ComponentType>( writer, nameof( dataObject.Type ), dataObject.Type );
            base.Serializer.WriteMandatoryEnum<ComponentState>( writer, nameof( dataObject.State ), dataObject.State );
            base.Serializer.WriteMandatoryString( writer, nameof( dataObject.Description ), dataObject.Description );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.StateText ), dataObject.StateText );
        }
    }
}