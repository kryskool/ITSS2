using System;
using System.Collections.Generic;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage.Input;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.Input
{
    internal class InputMessageArticleDataContract<TTypeMappings>:XmlSerializable<InputMessageArticle, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        private const String PackName = "Pack";

        public InputMessageArticleDataContract()
        {
        }

        public InputMessageArticleDataContract( InputMessageArticle dataObject )
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
            IEnumerable<ProductCode> productCodes = base.Serializer.ReadOptionalElements<ProductCode>( reader, nameof( ProductCode ) );
            IEnumerable<InputMessagePack> packs = base.Serializer.ReadOptionalElements<InputMessagePack>( reader, InputMessageArticleDataContract<TTypeMappings>.PackName );

            this.DataObject = new InputMessageArticle(  id,
                                                        name,
                                                        dosageForm,
                                                        packingUnit,
                                                        maxSubItemQuantity,
                                                        productCodes,
                                                        packs   );
        }

        public override void WriteXml( XmlWriter writer )
        {
            InputMessageArticle dataObject = this.DataObject;

            base.Serializer.WriteMandatoryAttribute<ArticleId>( writer, nameof( dataObject.Id ), dataObject.Id );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.Name ), dataObject.Name );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.DosageForm ), dataObject.DosageForm );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.PackingUnit ), dataObject.PackingUnit );
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.MaxSubItemQuantity ), dataObject.MaxSubItemQuantity );
            base.Serializer.WriteOptionalElements<ProductCode>( writer, nameof( ProductCode ), dataObject.GetProductCodes() );
            base.Serializer.WriteOptionalElements<InputMessagePack>( writer, InputMessageArticleDataContract<TTypeMappings>.PackName, dataObject.GetPacks() );
        }
    }
}