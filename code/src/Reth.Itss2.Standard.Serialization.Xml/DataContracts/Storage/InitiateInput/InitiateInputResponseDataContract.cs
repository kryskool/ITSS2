using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage.InitiateInput;
using Reth.Protocols;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.InitiateInput
{
    internal class InitiateInputResponseDataContract<TTypeMappings>:TraceableResponseDataContract<InitiateInputResponse, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        private const String ArticleName = "Article";
        private const String DetailsName = "Details";
                
        public InitiateInputResponseDataContract()
        {
        }

        public InitiateInputResponseDataContract( InitiateInputResponse dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {
            IMessageId id = base.ReadId( reader );
            SubscriberId source = base.ReadSource( reader );
            SubscriberId destination = base.ReadDestination( reader );

            InitiateInputResponseDetails details = base.Serializer.ReadMandatoryElement<InitiateInputResponseDetails>( reader, InitiateInputResponseDataContract<TTypeMappings>.DetailsName );
            InitiateInputResponseArticle article = base.Serializer.ReadMandatoryElement<InitiateInputResponseArticle>( reader, InitiateInputResponseDataContract<TTypeMappings>.ArticleName );
            Nullable<bool> isNewDelivery = base.Serializer.ReadOptionalBool( reader, nameof( this.DataObject.IsNewDelivery ), false );
            Nullable<bool> setPickingIndicator = base.Serializer.ReadOptionalBool( reader, nameof( this.DataObject.SetPickingIndicator ), false );

            this.DataObject = new InitiateInputResponse(    id,
                                                            source,
                                                            destination,
                                                            details,
                                                            article,
                                                            isNewDelivery,
                                                            setPickingIndicator );
        }

        public override void WriteXml( XmlWriter writer )
        {
            base.WriteXml( writer );

            InitiateInputResponse dataObject = this.DataObject;

            base.Serializer.WriteMandatoryElement<InitiateInputResponseDetails>( writer, InitiateInputResponseDataContract<TTypeMappings>.DetailsName, dataObject.Details );
            base.Serializer.WriteMandatoryElement<InitiateInputResponseArticle>( writer, InitiateInputResponseDataContract<TTypeMappings>.ArticleName, dataObject.Article );
            base.Serializer.WriteOptionalBool( writer, nameof( this.DataObject.IsNewDelivery ), dataObject.IsNewDelivery );
            base.Serializer.WriteOptionalBool( writer, nameof( this.DataObject.SetPickingIndicator ), dataObject.SetPickingIndicator );
        }
    }
}