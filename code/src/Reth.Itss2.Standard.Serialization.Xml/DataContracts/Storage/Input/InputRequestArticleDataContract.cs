using System;
using System.Collections.Generic;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage.Input;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.Input
{
    internal class InputRequestArticleDataContract<TTypeMappings>:XmlSerializable<InputRequestArticle, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        private const String PackName = "Pack";

        public InputRequestArticleDataContract()
        {
        }

        public InputRequestArticleDataContract( InputRequestArticle dataObject )
        :
            base( dataObject )
        {
        }
        
        public override void ReadXml( XmlReader reader )
        {
            ArticleId id = base.Serializer.ReadOptionalAttribute<ArticleId>( reader, nameof( this.DataObject.Id ) );
            String fmdId = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.FmdId ) );
            IEnumerable<InputRequestPack> packs = base.Serializer.ReadOptionalElements<InputRequestPack>( reader, InputRequestArticleDataContract<TTypeMappings>.PackName );

            this.DataObject = new InputRequestArticle(  id,
                                                        fmdId,
                                                        packs   );
        }

        public override void WriteXml( XmlWriter writer )
        {
            InputRequestArticle dataObject = this.DataObject;

            base.Serializer.WriteMandatoryAttribute<ArticleId>( writer, nameof( dataObject.Id ), dataObject.Id );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.FmdId ), dataObject.FmdId );
            base.Serializer.WriteOptionalElements<InputRequestPack>( writer, InputRequestArticleDataContract<TTypeMappings>.PackName, dataObject.GetPacks() );
        }
    }
}