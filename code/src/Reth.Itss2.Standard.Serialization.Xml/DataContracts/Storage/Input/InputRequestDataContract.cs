using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage.Input;
using Reth.Protocols;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.Input
{
    internal class InputRequestDataContract<TTypeMappings>:TraceableRequestDataContract<InputRequest, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        private const String ArticleName = "Article";
                
        public InputRequestDataContract()
        {
        }

        public InputRequestDataContract( InputRequest dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {
            IMessageId id = base.ReadId( reader );
            SubscriberId source = base.ReadSource( reader );
            SubscriberId destination = base.ReadDestination( reader );

            InputRequestArticle article = base.Serializer.ReadMandatoryElement<InputRequestArticle>( reader, InputRequestDataContract<TTypeMappings>.ArticleName );
            Nullable<bool> isNewDelivery = base.Serializer.ReadOptionalBool( reader, nameof( this.DataObject.IsNewDelivery ), false );
            Nullable<bool> setPickingIndicator = base.Serializer.ReadOptionalBool( reader, nameof( this.DataObject.SetPickingIndicator ), false );

            this.DataObject = new InputRequest( id,
                                                source,
                                                destination,
                                                article,
                                                isNewDelivery,
                                                setPickingIndicator );
        }

        public override void WriteXml( XmlWriter writer )
        {
            base.WriteXml( writer );

            InputRequest dataObject = this.DataObject;

            base.Serializer.WriteMandatoryElement<InputRequestArticle>( writer, InputRequestDataContract<TTypeMappings>.ArticleName, dataObject.Article );
            base.Serializer.WriteOptionalBool( writer, nameof( this.DataObject.IsNewDelivery ), dataObject.IsNewDelivery );
            base.Serializer.WriteOptionalBool( writer, nameof( this.DataObject.SetPickingIndicator ), dataObject.SetPickingIndicator );
        }
    }
}