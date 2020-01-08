using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage.InitiateInput;
using Reth.Protocols;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.InitiateInput
{
    internal class InitiateInputMessageDataContract<TTypeMappings>:TraceableMessageDataContract<InitiateInputMessage, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        private const String ArticleName = "Article";
        private const String DetailsName = "Details";
                
        public InitiateInputMessageDataContract()
        {
        }

        public InitiateInputMessageDataContract( InitiateInputMessage dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {
            IMessageId id = base.ReadId( reader );
            SubscriberId source = base.ReadSource( reader );
            SubscriberId destination = base.ReadDestination( reader );

            InitiateInputMessageDetails details = base.Serializer.ReadMandatoryElement<InitiateInputMessageDetails>( reader, InitiateInputMessageDataContract<TTypeMappings>.DetailsName );
            InitiateInputMessageArticle article = base.Serializer.ReadMandatoryElement<InitiateInputMessageArticle>( reader, InitiateInputMessageDataContract<TTypeMappings>.ArticleName );
            
            this.DataObject = new InitiateInputMessage( id,
                                                        source,
                                                        destination,
                                                        details,
                                                        article );
        }

        public override void WriteXml( XmlWriter writer )
        {
            base.WriteXml( writer );

            InitiateInputMessage dataObject = this.DataObject;

            base.Serializer.WriteMandatoryElement<InitiateInputMessageDetails>( writer, InitiateInputMessageDataContract<TTypeMappings>.DetailsName, dataObject.Details );
            base.Serializer.WriteMandatoryElement<InitiateInputMessageArticle>( writer, InitiateInputMessageDataContract<TTypeMappings>.ArticleName, dataObject.Article );
        }
    }
}