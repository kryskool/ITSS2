using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs.Storage.Input;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.Input
{
    internal class InputMessagePackHandlingDataContract<TTypeMappings>:XmlSerializable<InputMessagePackHandling, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public InputMessagePackHandlingDataContract()
        {
        }

        public InputMessagePackHandlingDataContract( InputMessagePackHandling dataObject )
        :
            base( dataObject )
        {
        }
        
        public override void ReadXml( XmlReader reader )
        {
            InputMessagePackHandlingInput input = base.Serializer.ReadMandatoryEnum<InputMessagePackHandlingInput>( reader, nameof( this.DataObject.Input ) );
            String text = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.Text ) );

            this.DataObject = new InputMessagePackHandling( input, text );
        }

        public override void WriteXml( XmlWriter writer )
        {
            InputMessagePackHandling dataObject = this.DataObject;

            base.Serializer.WriteMandatoryEnum<InputMessagePackHandlingInput>( writer, nameof( dataObject.Input ), dataObject.Input );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.Text ), dataObject.Text );
        }
    }
}