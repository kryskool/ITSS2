using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage.InitiateInput;
using Reth.Protocols;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.InitiateInput
{
    internal class InitiateInputRequestDataContract<TTypeMappings>:TraceableRequestDataContract<InitiateInputRequest, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        private const String ArticleName = "Article";
        private const String DetailsName = "Details";
                
        public InitiateInputRequestDataContract()
        {
        }

        public InitiateInputRequestDataContract( InitiateInputRequest dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {
            IMessageId id = base.ReadId( reader );
            SubscriberId source = base.ReadSource( reader );
            SubscriberId destination = base.ReadDestination( reader );

            InitiateInputRequestDetails details = base.Serializer.ReadMandatoryElement<InitiateInputRequestDetails>( reader, InitiateInputRequestDataContract<TTypeMappings>.DetailsName );
            InitiateInputRequestArticle article = base.Serializer.ReadMandatoryElement<InitiateInputRequestArticle>( reader, InitiateInputRequestDataContract<TTypeMappings>.ArticleName );
            Nullable<bool> isNewDelivery = base.Serializer.ReadOptionalBool( reader, nameof( this.DataObject.IsNewDelivery ), false );
            Nullable<bool> setPickingIndicator = base.Serializer.ReadOptionalBool( reader, nameof( this.DataObject.SetPickingIndicator ), false );

            this.DataObject = new InitiateInputRequest( id,
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

            InitiateInputRequest dataObject = this.DataObject;

            base.Serializer.WriteMandatoryElement<InitiateInputRequestDetails>( writer, InitiateInputRequestDataContract<TTypeMappings>.DetailsName, dataObject.Details );
            base.Serializer.WriteMandatoryElement<InitiateInputRequestArticle>( writer, InitiateInputRequestDataContract<TTypeMappings>.ArticleName, dataObject.Article );
            base.Serializer.WriteOptionalBool( writer, nameof( this.DataObject.IsNewDelivery ), dataObject.IsNewDelivery );
            base.Serializer.WriteOptionalBool( writer, nameof( this.DataObject.SetPickingIndicator ), dataObject.SetPickingIndicator );
        }
    }
}