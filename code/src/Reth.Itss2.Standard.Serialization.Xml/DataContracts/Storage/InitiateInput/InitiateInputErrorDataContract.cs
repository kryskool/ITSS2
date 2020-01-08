using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs.Storage.InitiateInput;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.InitiateInput
{
    internal class InitiateInputErrorDataContract<TTypeMappings>:XmlSerializable<InitiateInputError, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public InitiateInputErrorDataContract()
        {
        }

        public InitiateInputErrorDataContract( InitiateInputError dataObject )
        :
            base( dataObject )
        {
        }
        
        public override void ReadXml( XmlReader reader )
        {
            InitiateInputErrorType type = base.Serializer.ReadMandatoryEnum<InitiateInputErrorType>( reader, nameof( this.DataObject.Type ) );
            String text = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.Text ) );

            this.DataObject = new InitiateInputError( type, text );
        }

        public override void WriteXml( XmlWriter writer )
        {
            InitiateInputError dataObject = this.DataObject;

            base.Serializer.WriteMandatoryEnum<InitiateInputErrorType>( writer, nameof( dataObject.Type ), dataObject.Type );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.Text ), dataObject.Text );
        }
    }
}