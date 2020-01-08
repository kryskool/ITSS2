using System;
using System.Collections.Generic;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage.StockInfo;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.StockInfo
{
    internal class StockInfoArticleDataContract<TTypeMappings>:XmlSerializable<StockInfoArticle, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        private const String PackName = "Pack";

        public StockInfoArticleDataContract()
        {
        }

        public StockInfoArticleDataContract( StockInfoArticle dataObject )
        :
            base( dataObject )
        {
        }
        
        public override void ReadXml( XmlReader reader )
        {
            ArticleId id = base.Serializer.ReadMandatoryAttribute<ArticleId>( reader, nameof( this.DataObject.Id ) );
            int quantity = base.Serializer.ReadMandatoryInteger( reader, nameof( this.DataObject.Quantity ) );
            String name = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.Name ) );
            String dosageForm = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.DosageForm ) );
            String packingUnit = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.PackagingUnit ) );
            Nullable<int> maxSubItemQuantity = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.MaxSubItemQuantity ) );

            IEnumerable<ProductCode> productCodes = base.Serializer.ReadOptionalElements<ProductCode>( reader, nameof( ProductCode ) );
            IEnumerable<StockInfoPack> packs = base.Serializer.ReadOptionalElements<StockInfoPack>( reader, StockInfoArticleDataContract<TTypeMappings>.PackName );

            this.DataObject = new StockInfoArticle( id,
                                                    quantity,
                                                    name,
                                                    dosageForm,
                                                    packingUnit,
                                                    maxSubItemQuantity,
                                                    productCodes,
                                                    packs   );
        }

        public override void WriteXml( XmlWriter writer )
        {
            StockInfoArticle dataObject = this.DataObject;

            base.Serializer.WriteMandatoryAttribute<ArticleId>( writer, nameof( dataObject.Id ), dataObject.Id );
            base.Serializer.WriteMandatoryInteger( writer, nameof( dataObject.Quantity ), dataObject.Quantity );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.Name ), dataObject.Name );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.DosageForm ), dataObject.DosageForm );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.PackagingUnit ), dataObject.PackagingUnit );
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.MaxSubItemQuantity ), dataObject.MaxSubItemQuantity );
            base.Serializer.WriteOptionalElements<ProductCode>( writer, nameof( ProductCode ), dataObject.GetProductCodes() );
            base.Serializer.WriteOptionalElements<StockInfoPack>( writer, StockInfoArticleDataContract<TTypeMappings>.PackName, dataObject.GetPacks() );
        }
    }
}