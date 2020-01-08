using System;
using System.Collections.Generic;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage.InitiateInput;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.InitiateInput
{
    internal class InitiateInputResponseArticleDataContract<TTypeMappings>:XmlSerializable<InitiateInputResponseArticle, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        private const String PackName = "Pack";

        public InitiateInputResponseArticleDataContract()
        {
        }

        public InitiateInputResponseArticleDataContract( InitiateInputResponseArticle dataObject )
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
            Nullable<int> maxSubItemQuantity = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.MaxSubItemQuantity ) );
            PackDate serialNumberSinceExpiryDate = base.Serializer.ReadOptionalAttribute<PackDate>( reader, nameof( this.DataObject.SerialNumberSinceExpiryDate ) );
            IEnumerable<ProductCode> productCodes = base.Serializer.ReadOptionalElements<ProductCode>( reader, nameof( ProductCode ) );
            IEnumerable<InitiateInputResponsePack> packs = base.Serializer.ReadOptionalElements<InitiateInputResponsePack>( reader, InitiateInputResponseArticleDataContract<TTypeMappings>.PackName );

            this.DataObject = new InitiateInputResponseArticle( id,
                                                                name,
                                                                dosageForm,
                                                                packingUnit,
                                                                maxSubItemQuantity,
                                                                serialNumberSinceExpiryDate,
                                                                productCodes,
                                                                packs   );
        }

        public override void WriteXml( XmlWriter writer )
        {
            InitiateInputResponseArticle dataObject = this.DataObject;

            base.Serializer.WriteMandatoryAttribute<ArticleId>( writer, nameof( dataObject.Id ), dataObject.Id );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.Name ), dataObject.Name );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.DosageForm ), dataObject.DosageForm );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.PackingUnit ), dataObject.PackingUnit );
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.MaxSubItemQuantity ), dataObject.MaxSubItemQuantity );
            base.Serializer.WriteOptionalAttribute<PackDate>( writer, nameof( dataObject.SerialNumberSinceExpiryDate ), dataObject.SerialNumberSinceExpiryDate );
            base.Serializer.WriteOptionalElements<ProductCode>( writer, nameof( ProductCode ), dataObject.GetProductCodes() );
            base.Serializer.WriteOptionalElements<InitiateInputResponsePack>( writer, InitiateInputResponseArticleDataContract<TTypeMappings>.PackName, dataObject.GetPacks() );
        }
    }
}