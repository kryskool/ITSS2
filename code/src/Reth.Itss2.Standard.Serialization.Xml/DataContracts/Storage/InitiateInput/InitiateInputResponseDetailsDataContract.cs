using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs.Storage.InitiateInput;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.InitiateInput
{
    internal class InitiateInputResponseDetailsDataContract<TTypeMappings>:XmlSerializable<InitiateInputResponseDetails, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public InitiateInputResponseDetailsDataContract()
        {
        }

        public InitiateInputResponseDetailsDataContract( InitiateInputResponseDetails dataObject )
        :
            base( dataObject )
        {
        }
        
        public override void ReadXml( XmlReader reader )
        {      
            int inputSource = base.Serializer.ReadMandatoryInteger( reader, nameof( this.DataObject.InputSource ) );
            InitiateInputResponseStatus status = base.Serializer.ReadMandatoryEnum<InitiateInputResponseStatus>( reader, nameof( this.DataObject.Status ) );
            Nullable<int> inputPoint = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.InputPoint ) );

            this.DataObject = new InitiateInputResponseDetails( inputSource,
                                                                status,
                                                                inputPoint );
        }

        public override void WriteXml( XmlWriter writer )
        {
            InitiateInputResponseDetails dataObject = this.DataObject;

            base.Serializer.WriteMandatoryInteger( writer, nameof( dataObject.InputSource ), dataObject.InputSource );
            base.Serializer.WriteMandatoryEnum<InitiateInputResponseStatus>( writer, nameof( dataObject.Status ), dataObject.Status );
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.InputPoint ), dataObject.InputPoint );
        }
    }
}