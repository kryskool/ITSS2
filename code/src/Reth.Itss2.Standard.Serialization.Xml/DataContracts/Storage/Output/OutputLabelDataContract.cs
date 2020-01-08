using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs.Storage.Output;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.Output
{
    internal class OutputLabelDataContract<TTypeMappings>:XmlSerializable<OutputLabel, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        private const String ContentName = "Content";

        public OutputLabelDataContract()
        {
        }

        public OutputLabelDataContract( OutputLabel dataObject )
        :
            base( dataObject )
        {
        }
        
        public override void ReadXml( XmlReader reader )
        {
            String templateId = base.Serializer.ReadMandatoryString( reader, nameof( this.DataObject.TemplateId ) );
            OutputLabelContent content = base.Serializer.ReadMandatoryElement<OutputLabelContent>( reader, OutputLabelDataContract<TTypeMappings>.ContentName );

            this.DataObject = new OutputLabel( templateId, content );
        }

        public override void WriteXml( XmlWriter writer )
        {
            OutputLabel dataObject = this.DataObject;

            base.Serializer.WriteMandatoryString( writer, nameof( dataObject.TemplateId ), dataObject.TemplateId );
            base.Serializer.WriteMandatoryElement<OutputLabelContent>( writer, OutputLabelDataContract<TTypeMappings>.ContentName, dataObject.Content );
        }
    }
}