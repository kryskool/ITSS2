using System;
using System.Collections.Generic;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage.InitiateInput;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.InitiateInput
{
    internal class InitiateInputRequestArticleDataContract<TTypeMappings>:XmlSerializable<InitiateInputRequestArticle, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        private const String PackName = "Pack";

        public InitiateInputRequestArticleDataContract()
        {
        }

        public InitiateInputRequestArticleDataContract( InitiateInputRequestArticle dataObject )
        :
            base( dataObject )
        {
        }
        
        public override void ReadXml( XmlReader reader )
        {
            ArticleId id = base.Serializer.ReadOptionalAttribute<ArticleId>( reader, nameof( this.DataObject.Id ) );
            String fmdId = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.FmdId ) );
            IEnumerable<InitiateInputRequestPack> packs = base.Serializer.ReadOptionalElements<InitiateInputRequestPack>( reader, InitiateInputRequestArticleDataContract<TTypeMappings>.PackName );

            this.DataObject = new InitiateInputRequestArticle(  id,
                                                                fmdId,
                                                                packs   );
        }

        public override void WriteXml( XmlWriter writer )
        {
            InitiateInputRequestArticle dataObject = this.DataObject;

            base.Serializer.WriteMandatoryAttribute<ArticleId>( writer, nameof( dataObject.Id ), dataObject.Id );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.FmdId ), dataObject.FmdId );
            base.Serializer.WriteOptionalElements<InitiateInputRequestPack>( writer, InitiateInputRequestArticleDataContract<TTypeMappings>.PackName, dataObject.GetPacks() );
        }
    }
}