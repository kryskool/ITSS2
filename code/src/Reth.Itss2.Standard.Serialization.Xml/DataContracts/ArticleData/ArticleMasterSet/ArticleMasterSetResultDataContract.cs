using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs.ArticleData.ArticleMasterSet;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.ArticleData.ArticleMasterSet
{
    internal class ArticleMasterSetResultDataContract<TTypeMappings>:XmlSerializable<ArticleMasterSetResult, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public ArticleMasterSetResultDataContract()
        {
        }

        public ArticleMasterSetResultDataContract( ArticleMasterSetResult dataObject )
        :
            base( dataObject )
        {
        }
        
        public override void ReadXml( XmlReader reader )
        {
            ArticleMasterSetResultValue value = base.Serializer.ReadMandatoryEnum<ArticleMasterSetResultValue>( reader, nameof( this.DataObject.Value ) );
            String text = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.Text ) );

            this.DataObject = new ArticleMasterSetResult( value, text );
        }

        public override void WriteXml( XmlWriter writer )
        {
            ArticleMasterSetResult dataObject = this.DataObject;

            base.Serializer.WriteMandatoryEnum<ArticleMasterSetResultValue>( writer, nameof( dataObject.Value ), dataObject.Value );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.Text ), dataObject.Text );
        }
    }
}