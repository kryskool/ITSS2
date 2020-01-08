using System;
using System.Collections.Generic;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage;
using Reth.Itss2.Standard.Dialogs.Storage.Output;
using Reth.Protocols;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.Output
{
    internal class OutputMessageDataContract<TTypeMappings>:TraceableMessageDataContract<OutputMessage, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        private const String ArticleName = "Article";
        private const String BoxName = "Box";
        private const String DetailsName = "Details";
        
        public OutputMessageDataContract()
        {
        }

        public OutputMessageDataContract( OutputMessage dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {
            IMessageId id = base.ReadId( reader );
            SubscriberId source = base.ReadSource( reader );
            SubscriberId destination = base.ReadDestination( reader );

            OutputMessageDetails details = base.Serializer.ReadMandatoryElement<OutputMessageDetails>( reader, OutputMessageDataContract<TTypeMappings>.DetailsName );
            IEnumerable<OutputArticle> articles = base.Serializer.ReadOptionalElements<OutputArticle>( reader, OutputMessageDataContract<TTypeMappings>.ArticleName );
            IEnumerable<OutputBox> boxes = base.Serializer.ReadOptionalElements<OutputBox>( reader, OutputMessageDataContract<TTypeMappings>.BoxName );
                        
            this.DataObject = new OutputMessage(    id,
                                                    source,
                                                    destination,
                                                    details,
                                                    articles,
                                                    boxes   );
        }

        public override void WriteXml( XmlWriter writer )
        {
            base.WriteXml( writer );

            OutputMessage dataObject = this.DataObject;

            base.Serializer.WriteMandatoryElement<OutputMessageDetails>( writer, OutputMessageDataContract<TTypeMappings>.DetailsName, dataObject.Details );
            base.Serializer.WriteOptionalElements<OutputArticle>( writer, OutputMessageDataContract<TTypeMappings>.ArticleName, dataObject.GetArticles() );
            base.Serializer.WriteOptionalElements<OutputBox>( writer, OutputMessageDataContract<TTypeMappings>.ArticleName, dataObject.GetBoxes() );
        }
    }
}