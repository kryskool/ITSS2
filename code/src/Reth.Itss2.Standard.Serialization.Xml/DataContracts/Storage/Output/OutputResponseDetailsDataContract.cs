using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs.Storage;
using Reth.Itss2.Standard.Dialogs.Storage.Output;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.Output
{
    internal class OutputResponseDetailsDataContract<TTypeMappings>:XmlSerializable<OutputResponseDetails, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public OutputResponseDetailsDataContract()
        {
        }

        public OutputResponseDetailsDataContract( OutputResponseDetails dataObject )
        :
            base( dataObject )
        {
        }
        
        public override void ReadXml( XmlReader reader )
        {      
            int outputDestination = base.Serializer.ReadMandatoryInteger( reader, nameof( this.DataObject.OutputDestination ) );
            OutputResponseStatus status = base.Serializer.ReadMandatoryEnum<OutputResponseStatus>( reader, nameof( this.DataObject.Status ) );
            Nullable<OutputPriority> priority = base.Serializer.ReadOptionalEnum<OutputPriority>( reader, nameof( this.DataObject.Priority ) );
            Nullable<int> outputPoint = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.OutputPoint ) );

            this.DataObject = new OutputResponseDetails(    outputDestination,
                                                            status,
                                                            priority,
                                                            outputPoint );
        }

        public override void WriteXml( XmlWriter writer )
        {
            OutputResponseDetails dataObject = this.DataObject;

            base.Serializer.WriteMandatoryInteger( writer, nameof( dataObject.OutputDestination ), dataObject.OutputDestination );
            base.Serializer.WriteMandatoryEnum<OutputResponseStatus>( writer, nameof( dataObject.Status ), dataObject.Status );
            base.Serializer.WriteOptionalEnum<OutputPriority>( writer, nameof( dataObject.Priority ), dataObject.Priority );
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.OutputPoint ), dataObject.OutputPoint );
        }
    }
}