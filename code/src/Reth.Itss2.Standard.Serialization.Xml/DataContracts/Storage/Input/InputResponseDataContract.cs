using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage.Input;
using Reth.Protocols;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.Input
{
    internal class InputResponseDataContract<TTypeMappings>:TraceableResponseDataContract<InputResponse, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        private const String ArticleName = "Article";
                
        public InputResponseDataContract()
        {
        }

        public InputResponseDataContract( InputResponse dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {
            IMessageId id = base.ReadId( reader );
            SubscriberId source = base.ReadSource( reader );
            SubscriberId destination = base.ReadDestination( reader );

            InputResponseArticle article = base.Serializer.ReadMandatoryElement<InputResponseArticle>( reader, InputResponseDataContract<TTypeMappings>.ArticleName );
            Nullable<bool> isNewDelivery = base.Serializer.ReadOptionalBool( reader, nameof( this.DataObject.IsNewDelivery ) );

            this.DataObject = new InputResponse(    id,
                                                    source,
                                                    destination,
                                                    article,
                                                    isNewDelivery );
        }

        public override void WriteXml( XmlWriter writer )
        {
            base.WriteXml( writer );

            InputResponse dataObject = this.DataObject;

            base.Serializer.WriteMandatoryElement<InputResponseArticle>( writer, InputResponseDataContract<TTypeMappings>.ArticleName, dataObject.Article );
            base.Serializer.WriteOptionalBool( writer, nameof( this.DataObject.IsNewDelivery ), dataObject.IsNewDelivery );
        }
    }
}