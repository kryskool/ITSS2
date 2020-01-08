using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.ArticleData.ArticleInfo;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.ArticleData.ArticleInfo
{
    internal class ArticleInfoResponseArticleDataContract<TTypeMappings>:XmlSerializable<ArticleInfoResponseArticle, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public ArticleInfoResponseArticleDataContract()
        {
        }

        public ArticleInfoResponseArticleDataContract( ArticleInfoResponseArticle dataObject )
        :
            base( dataObject )
        {
        }
        
        public override void ReadXml( XmlReader reader )
        {
            ArticleId id = base.Serializer.ReadMandatoryAttribute<ArticleId>( reader, nameof( this.DataObject.Id ) );
            String name = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.Name ) );
            String dosageForm = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.DosageForm ) );
            String packingUnit = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.PackingUnit ) );
            Nullable<bool> requiresFridge = base.Serializer.ReadOptionalBool( reader, nameof( this.DataObject.RequiresFridge ), false );
            Nullable<int> maxSubItemQuantity = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.MaxSubItemQuantity ) );
            PackDate serialNumberSinceExpiryDate = base.Serializer.ReadOptionalAttribute<PackDate>( reader, nameof( this.DataObject.SerialNumberSinceExpiryDate ) );

            this.DataObject = new ArticleInfoResponseArticle(   id,
                                                                name,
                                                                dosageForm,
                                                                packingUnit,
                                                                requiresFridge,
                                                                maxSubItemQuantity,
                                                                serialNumberSinceExpiryDate );
        }

        public override void WriteXml( XmlWriter writer )
        {
            ArticleInfoResponseArticle dataObject = this.DataObject;

            base.Serializer.WriteMandatoryAttribute<ArticleId>( writer, nameof( dataObject.Id ), dataObject.Id );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.Name ), dataObject.Name );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.DosageForm ), dataObject.DosageForm );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.PackingUnit ), dataObject.PackingUnit );
            base.Serializer.WriteOptionalBool( writer, nameof( dataObject.RequiresFridge ), dataObject.RequiresFridge );
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.MaxSubItemQuantity ), dataObject.MaxSubItemQuantity );
            base.Serializer.WriteOptionalAttribute<PackDate>( writer, nameof( dataObject.SerialNumberSinceExpiryDate ), dataObject.SerialNumberSinceExpiryDate );
        }
    }
}