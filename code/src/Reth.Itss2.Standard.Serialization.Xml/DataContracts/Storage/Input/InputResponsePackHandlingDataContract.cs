using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs.Storage.Input;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.Input
{
    internal class InputResponsePackHandlingDataContract<TTypeMappings>:XmlSerializable<InputResponsePackHandling, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public InputResponsePackHandlingDataContract()
        {
        }

        public InputResponsePackHandlingDataContract( InputResponsePackHandling dataObject )
        :
            base( dataObject )
        {
        }
        
        public override void ReadXml( XmlReader reader )
        {
            InputResponsePackHandlingInput input = base.Serializer.ReadMandatoryEnum<InputResponsePackHandlingInput>( reader, nameof( this.DataObject.Input ) );
            String text = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.Text ) );

            this.DataObject = new InputResponsePackHandling( input, text );
        }

        public override void WriteXml( XmlWriter writer )
        {
            InputResponsePackHandling dataObject = this.DataObject;

            base.Serializer.WriteMandatoryEnum<InputResponsePackHandlingInput>( writer, nameof( dataObject.Input ), dataObject.Input );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.Text ), dataObject.Text );
        }
    }
}