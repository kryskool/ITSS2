using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs.Storage.InitiateInput;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.InitiateInput
{
    internal class InitiateInputRequestDetailsDataContract<TTypeMappings>:XmlSerializable<InitiateInputRequestDetails, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public InitiateInputRequestDetailsDataContract()
        {
        }

        public InitiateInputRequestDetailsDataContract( InitiateInputRequestDetails dataObject )
        :
            base( dataObject )
        {
        }
        
        public override void ReadXml( XmlReader reader )
        {      
            int inputSource = base.Serializer.ReadMandatoryInteger( reader, nameof( this.DataObject.InputSource ) );
            Nullable<int> inputPoint = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.InputPoint ) );

            this.DataObject = new InitiateInputRequestDetails(  inputSource,
                                                                inputPoint );
        }

        public override void WriteXml( XmlWriter writer )
        {
            InitiateInputRequestDetails dataObject = this.DataObject;

            base.Serializer.WriteMandatoryInteger( writer, nameof( dataObject.InputSource ), dataObject.InputSource );
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.InputPoint ), dataObject.InputPoint );
        }
    }
}