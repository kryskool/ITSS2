using System;
using System.Collections.Generic;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.ArticleData.ArticleInfo;
using Reth.Protocols;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.ArticleData.ArticleInfo
{
    internal class ArticleInfoResponseDataContract<TTypeMappings>:TraceableResponseDataContract<ArticleInfoResponse, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        private const String ArticleName = "Article";

        public ArticleInfoResponseDataContract()
        {
        }

        public ArticleInfoResponseDataContract( ArticleInfoResponse dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {
            IMessageId id = base.ReadId( reader );
            SubscriberId source = base.ReadSource( reader );
            SubscriberId destination = base.ReadDestination( reader );

            IEnumerable<ArticleInfoResponseArticle> articles = base.Serializer.ReadOptionalElements<ArticleInfoResponseArticle>( reader, ArticleInfoResponseDataContract<TTypeMappings>.ArticleName );

            this.DataObject = new ArticleInfoResponse( id, source, destination, articles );
        }

        public override void WriteXml( XmlWriter writer )
        {
            base.WriteXml( writer );

            base.Serializer.WriteOptionalElements<ArticleInfoResponseArticle>( writer, ArticleInfoResponseDataContract<TTypeMappings>.ArticleName, this.DataObject.GetArticles() );
        }
    }
}