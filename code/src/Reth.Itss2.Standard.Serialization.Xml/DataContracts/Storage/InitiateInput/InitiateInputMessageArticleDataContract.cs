using System;
using System.Collections.Generic;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage.InitiateInput;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.InitiateInput
{
    internal class InitiateInputMessageArticleDataContract<TTypeMappings>:XmlSerializable<InitiateInputMessageArticle, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        private const String PackName = "Pack";

        public InitiateInputMessageArticleDataContract()
        {
        }

        public InitiateInputMessageArticleDataContract( InitiateInputMessageArticle dataObject )
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
            IEnumerable<InitiateInputMessagePack> packs = base.Serializer.ReadOptionalElements<InitiateInputMessagePack>( reader, InitiateInputMessageArticleDataContract<TTypeMappings>.PackName );

            this.DataObject = new InitiateInputMessageArticle(  id,
                                                                name,
                                                                dosageForm,
                                                                packingUnit,
                                                                maxSubItemQuantity,
                                                                packs   );
        }

        public override void WriteXml( XmlWriter writer )
        {
            InitiateInputMessageArticle dataObject = this.DataObject;

            base.Serializer.WriteMandatoryAttribute<ArticleId>( writer, nameof( dataObject.Id ), dataObject.Id );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.Name ), dataObject.Name );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.DosageForm ), dataObject.DosageForm );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.PackingUnit ), dataObject.PackingUnit );
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.MaxSubItemQuantity ), dataObject.MaxSubItemQuantity );
            base.Serializer.WriteOptionalElements<InitiateInputMessagePack>( writer, InitiateInputMessageArticleDataContract<TTypeMappings>.PackName, dataObject.GetPacks() );
        }
    }
}