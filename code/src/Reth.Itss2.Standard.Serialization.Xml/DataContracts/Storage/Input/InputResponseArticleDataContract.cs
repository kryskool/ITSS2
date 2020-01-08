using System;
using System.Collections.Generic;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage.Input;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.Input
{
    internal class InputResponseArticleDataContract<TTypeMappings>:XmlSerializable<InputResponseArticle, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        private const String PackName = "Pack";

        public InputResponseArticleDataContract()
        {
        }

        public InputResponseArticleDataContract( InputResponseArticle dataObject )
        :
            base( dataObject )
        {
        }
        
        public override void ReadXml( XmlReader reader )
        {
            ArticleId id = base.Serializer.ReadOptionalAttribute<ArticleId>( reader, nameof( this.DataObject.Id ) );
            String name = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.Name ) );
            String dosageForm = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.DosageForm ) );
            String packingUnit = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.PackingUnit ) );
            Nullable<bool> requiresFridge = base.Serializer.ReadOptionalBool( reader, nameof( this.DataObject.RequiresFridge ), false );
            Nullable<int> maxSubItemQuantity = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.MaxSubItemQuantity ) );
            PackDate serialNumberSinceExpiryDate = base.Serializer.ReadOptionalAttribute<PackDate>( reader, nameof( this.DataObject.SerialNumberSinceExpiryDate ) );
            IEnumerable<ProductCode> productCodes = base.Serializer.ReadOptionalElements<ProductCode>( reader, nameof( ProductCode ) );
            IEnumerable<InputResponsePack> packs = base.Serializer.ReadOptionalElements<InputResponsePack>( reader, InputResponseArticleDataContract<TTypeMappings>.PackName );

            this.DataObject = new InputResponseArticle( id,
                                                        name,
                                                        dosageForm,
                                                        packingUnit,
                                                        requiresFridge,
                                                        maxSubItemQuantity,
                                                        serialNumberSinceExpiryDate,
                                                        productCodes,
                                                        packs );
        }

        public override void WriteXml( XmlWriter writer )
        {
            InputResponseArticle dataObject = this.DataObject;

            base.Serializer.WriteOptionalAttribute<ArticleId>( writer, nameof( dataObject.Id ), dataObject.Id );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.Name ), dataObject.Name );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.DosageForm ), dataObject.DosageForm );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.PackingUnit ), dataObject.PackingUnit );
            base.Serializer.WriteOptionalBool( writer, nameof( dataObject.RequiresFridge ), dataObject.RequiresFridge );
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.MaxSubItemQuantity ), dataObject.MaxSubItemQuantity );
            base.Serializer.WriteOptionalAttribute<PackDate>( writer, nameof( dataObject.SerialNumberSinceExpiryDate ), dataObject.SerialNumberSinceExpiryDate );
            base.Serializer.WriteOptionalElements<ProductCode>( writer, nameof( ProductCode ), dataObject.GetProductCodes() );
            base.Serializer.WriteOptionalElements<InputResponsePack>( writer, InputResponseArticleDataContract<TTypeMappings>.PackName, dataObject.GetPacks() );
        }
    }
}