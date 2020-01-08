using System;
using System.Collections.Generic;
using System.Xml;

using Reth.Itss2.StandardExtensions.Dialogs.ArticleData.ArticleMasterSet;
using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Serialization.Xml.DataContracts;
using Reth.Protocols;
using Reth.Protocols.Serialization;

namespace Reth.Itss2.StandardExtensions.Serialization.Xml.DataContracts.ArticleData.ArticleMasterSet
{
    internal class ArticleMasterSetRequestDataContract<TTypeMappings>:TraceableRequestDataContract<ArticleMasterSetRequest, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        private const String ArticleName = "Article";

        public ArticleMasterSetRequestDataContract()
        {
        }

        public ArticleMasterSetRequestDataContract( ArticleMasterSetRequest dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {
            IMessageId id = base.ReadId( reader );
            SubscriberId source = base.ReadSource( reader );
            SubscriberId destination = base.ReadDestination( reader );

            IEnumerable<ArticleMasterSetArticle> articles = base.Serializer.ReadOptionalElements<ArticleMasterSetArticle>( reader, ArticleMasterSetRequestDataContract<TTypeMappings>.ArticleName ); 

            this.DataObject = new ArticleMasterSetRequest( id, source, destination, articles );
        }

        public override void WriteXml( XmlWriter writer )
        {
            base.WriteXml( writer );

            ArticleMasterSetArticle[] articles = ( ArticleMasterSetArticle[] )( this.DataObject.GetArticles() );

            base.Serializer.WriteOptionalElements<ArticleMasterSetArticle>( writer, ArticleMasterSetRequestDataContract<TTypeMappings>.ArticleName, articles );
        }
    }
}