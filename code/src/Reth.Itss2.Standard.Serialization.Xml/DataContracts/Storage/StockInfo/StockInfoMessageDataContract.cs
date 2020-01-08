using System;
using System.Collections.Generic;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage.StockInfo;
using Reth.Protocols;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.StockInfo
{
    internal class StockInfoMessageDataContract<TTypeMappings>:TraceableMessageDataContract<StockInfoMessage, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        private const String ArticleName = "Article";

        public StockInfoMessageDataContract()
        {
        }

        public StockInfoMessageDataContract( StockInfoMessage dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {
            IMessageId id = base.ReadId( reader );
            SubscriberId source = base.ReadSource( reader );
            SubscriberId destination = base.ReadDestination( reader );

            IEnumerable<StockInfoArticle> articles = base.Serializer.ReadOptionalElements<StockInfoArticle>( reader, StockInfoMessageDataContract<TTypeMappings>.ArticleName );

            this.DataObject = new StockInfoMessage( id, source, destination, articles );
        }

        public override void WriteXml( XmlWriter writer )
        {
            base.WriteXml( writer );

            base.Serializer.WriteOptionalElements<StockInfoArticle>( writer, StockInfoMessageDataContract<TTypeMappings>.ArticleName, this.DataObject.GetArticles() );
        }
    }
}