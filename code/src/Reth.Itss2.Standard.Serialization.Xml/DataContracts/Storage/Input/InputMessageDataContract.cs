using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage.Input;
using Reth.Protocols;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.Input
{
    internal class InputMessageDataContract<TTypeMappings>:TraceableMessageDataContract<InputMessage, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        private const String ArticleName = "Article";
                
        public InputMessageDataContract()
        {
        }

        public InputMessageDataContract( InputMessage dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {
            IMessageId id = base.ReadId( reader );
            SubscriberId source = base.ReadSource( reader );
            SubscriberId destination = base.ReadDestination( reader );

            InputMessageArticle article = base.Serializer.ReadMandatoryElement<InputMessageArticle>( reader, InputMessageDataContract<TTypeMappings>.ArticleName );
            Nullable<bool> isNewDelivery = base.Serializer.ReadOptionalBool( reader, nameof( this.DataObject.IsNewDelivery ) );
                        
            this.DataObject = new InputMessage( id,
                                                source,
                                                destination,
                                                article,
                                                isNewDelivery   );
        }

        public override void WriteXml( XmlWriter writer )
        {
            base.WriteXml( writer );

            InputMessage dataObject = this.DataObject;

            base.Serializer.WriteMandatoryElement<InputMessageArticle>( writer, InputMessageDataContract<TTypeMappings>.ArticleName, dataObject.Article );
            base.Serializer.WriteOptionalBool( writer, nameof( this.DataObject.IsNewDelivery ), dataObject.IsNewDelivery );
        }
    }
}