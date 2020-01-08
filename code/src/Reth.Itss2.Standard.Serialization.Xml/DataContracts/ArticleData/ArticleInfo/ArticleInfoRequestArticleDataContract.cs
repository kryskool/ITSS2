using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.ArticleData.ArticleInfo;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.ArticleData.ArticleInfo
{
    internal class ArticleInfoRequestArticleDataContract<TTypeMappings>:XmlSerializable<ArticleInfoRequestArticle, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public ArticleInfoRequestArticleDataContract()
        {
        }

        public ArticleInfoRequestArticleDataContract( ArticleInfoRequestArticle dataObject )
        :
            base( dataObject )
        {
        }
        
        public override void ReadXml( XmlReader reader )
        {
            ArticleId id = base.Serializer.ReadMandatoryAttribute<ArticleId>( reader, nameof( this.DataObject.Id ) );
            Nullable<int> depth = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.Depth ) );
            Nullable<int> width = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.Width ) );
            Nullable<int> height = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.Height ) );
            Nullable<int> weight = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.Weight ) );

            this.DataObject = new ArticleInfoRequestArticle(    id,
                                                                depth,
                                                                width,
                                                                height,
                                                                weight  );
        }

        public override void WriteXml( XmlWriter writer )
        {
            ArticleInfoRequestArticle dataObject = this.DataObject;

            base.Serializer.WriteMandatoryAttribute<ArticleId>( writer, nameof( dataObject.Id ), dataObject.Id );
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.Depth ), dataObject.Depth );
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.Width ), dataObject.Width );
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.Height ), dataObject.Height );
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.Weight ), dataObject.Weight );
        }
    }
}