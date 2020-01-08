using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs.Storage.Output;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.Output
{
    internal class OutputRequestDetailsDataContract<TTypeMappings>:XmlSerializable<OutputRequestDetails, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public OutputRequestDetailsDataContract()
        {
        }

        public OutputRequestDetailsDataContract( OutputRequestDetails dataObject )
        :
            base( dataObject )
        {
        }
        
        public override void ReadXml( XmlReader reader )
        {      
            int outputDestination = base.Serializer.ReadMandatoryInteger( reader, nameof( this.DataObject.OutputDestination ) );
            Nullable<OutputPriority> priority = base.Serializer.ReadOptionalEnum<OutputPriority>( reader, nameof( this.DataObject.Priority ), OutputPriority.Normal );
            Nullable<int> outputPoint = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.OutputPoint ) );

            this.DataObject = new OutputRequestDetails( outputDestination,
                                                        priority,
                                                        outputPoint );
        }

        public override void WriteXml( XmlWriter writer )
        {
            OutputRequestDetails dataObject = this.DataObject;

            base.Serializer.WriteMandatoryInteger( writer, nameof( dataObject.OutputDestination ), dataObject.OutputDestination );
            base.Serializer.WriteOptionalEnum<OutputPriority>( writer, nameof( dataObject.Priority ), dataObject.Priority );
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.OutputPoint ), dataObject.OutputPoint );
        }
    }
}