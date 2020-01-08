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
    internal class ArticleInfoRequestDataContract<TTypeMappings>:TraceableRequestDataContract<ArticleInfoRequest, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        private const String ArticleName = "Article";

        public ArticleInfoRequestDataContract()
        {
        }

        public ArticleInfoRequestDataContract( ArticleInfoRequest dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {
            IMessageId id = base.ReadId( reader );
            SubscriberId source = base.ReadSource( reader );
            SubscriberId destination = base.ReadDestination( reader );

            IEnumerable<ArticleInfoRequestArticle> articles = base.Serializer.ReadOptionalElements<ArticleInfoRequestArticle>( reader, ArticleInfoRequestDataContract<TTypeMappings>.ArticleName );

            this.DataObject = new ArticleInfoRequest( id, source, destination, articles );
        }

        public override void WriteXml( XmlWriter writer )
        {
            base.WriteXml( writer );

            base.Serializer.WriteOptionalElements<ArticleInfoRequestArticle>( writer, ArticleInfoRequestDataContract<TTypeMappings>.ArticleName, this.DataObject.GetArticles() );
        }
    }
}