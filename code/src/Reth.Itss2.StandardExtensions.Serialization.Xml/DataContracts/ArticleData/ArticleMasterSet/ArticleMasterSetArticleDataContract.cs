using System;
using System.Collections.Generic;
using System.Xml;

using Reth.Itss2.StandardExtensions.Dialogs.ArticleData.ArticleMasterSet;
using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.StandardExtensions.Serialization.Xml.DataContracts.ArticleData.ArticleMasterSet
{
    internal class ArticleMasterSetArticleDataContract<TTypeMappings>:XmlSerializable<ArticleMasterSetArticle, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public ArticleMasterSetArticleDataContract()
        {
        }

        public ArticleMasterSetArticleDataContract( ArticleMasterSetArticle dataObject )
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
            String machineLocation = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.MachineLocation ) );
            StockLocationId stockLocationId = base.Serializer.ReadOptionalAttribute<StockLocationId>( reader, nameof( this.DataObject.StockLocationId ) );
            Nullable<bool> requiresFridge = base.Serializer.ReadOptionalBool( reader, nameof( this.DataObject.RequiresFridge ), false );
            Nullable<bool> batchTracking = base.Serializer.ReadOptionalBool( reader, nameof( this.DataObject.BatchTracking ), false );
            Nullable<bool> expiryTracking = base.Serializer.ReadOptionalBool( reader, nameof( this.DataObject.ExpiryTracking ), false );
            Nullable<bool> serialTracking = base.Serializer.ReadOptionalBool( reader, nameof( this.DataObject.SerialTracking ), false );
            Nullable<int> maxSubItemQuantity = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.MaxSubItemQuantity ) );
            Nullable<int> depth = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.Depth ) );
            Nullable<int> width = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.Width ) );
            Nullable<int> height = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.Height ) );
            Nullable<int> weight = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.Weight ) );
            PackDate serialNumberSinceExpiryDate = base.Serializer.ReadOptionalAttribute<PackDate>( reader, nameof( this.DataObject.SerialNumberSinceExpiryDate ) );
            
            IEnumerable<ProductCode> productCodes = base.Serializer.ReadOptionalElements<ProductCode>( reader, nameof( ProductCode ) );

            this.DataObject = new ArticleMasterSetArticle(  id,
                                                            name,
                                                            dosageForm,
                                                            packingUnit,
                                                            machineLocation,
                                                            stockLocationId,
                                                            requiresFridge,
                                                            batchTracking,
                                                            expiryTracking,
                                                            serialTracking,
                                                            maxSubItemQuantity,
                                                            depth,
                                                            width,
                                                            height,
                                                            weight,
                                                            serialNumberSinceExpiryDate,
                                                            productCodes    );
        }

        public override void WriteXml( XmlWriter writer )
        {
            ArticleMasterSetArticle dataObject = this.DataObject;

            base.Serializer.WriteMandatoryAttribute<ArticleId>( writer, nameof( dataObject.Id ), dataObject.Id );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.Name ), dataObject.Name );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.DosageForm ), dataObject.DosageForm );            
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.PackingUnit ), dataObject.PackingUnit );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.MachineLocation ), dataObject.MachineLocation );
            base.Serializer.WriteOptionalAttribute<StockLocationId>( writer, nameof( dataObject.StockLocationId ), dataObject.StockLocationId );
            base.Serializer.WriteOptionalBool( writer, nameof( dataObject.RequiresFridge ), dataObject.RequiresFridge );
            base.Serializer.WriteOptionalBool( writer, nameof( dataObject.BatchTracking ), dataObject.BatchTracking );
            base.Serializer.WriteOptionalBool( writer, nameof( dataObject.ExpiryTracking ), dataObject.ExpiryTracking );
            base.Serializer.WriteOptionalBool( writer, nameof( dataObject.SerialTracking ), dataObject.SerialTracking );
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.MaxSubItemQuantity ), dataObject.MaxSubItemQuantity );
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.Depth ), dataObject.Depth );
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.Width ), dataObject.Width );
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.Height ), dataObject.Height );
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.Weight ), dataObject.Weight );
            base.Serializer.WriteOptionalAttribute<PackDate>( writer, nameof( dataObject.SerialNumberSinceExpiryDate ), dataObject.SerialNumberSinceExpiryDate );
            base.Serializer.WriteOptionalElements<ProductCode>( writer, nameof( ProductCode ), dataObject.GetProductCodes() );
        }
    }
}