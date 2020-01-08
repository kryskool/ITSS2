using System;
using System.Collections.Generic;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage.Output;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.Output
{
    internal class OutputArticleDataContract<TTypeMappings>:XmlSerializable<OutputArticle, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        private const String PackName = "Pack";

        public OutputArticleDataContract()
        {
        }

        public OutputArticleDataContract( OutputArticle dataObject )
        :
            base( dataObject )
        {
        }
        
        public override void ReadXml( XmlReader reader )
        {
            ArticleId id = base.Serializer.ReadMandatoryAttribute<ArticleId>( reader, nameof( this.DataObject.Id ) );
            IEnumerable<OutputPack> packs = base.Serializer.ReadOptionalElements<OutputPack>( reader, OutputArticleDataContract<TTypeMappings>.PackName );

            this.DataObject = new OutputArticle( id, packs );
        }

        public override void WriteXml( XmlWriter writer )
        {
            OutputArticle dataObject = this.DataObject;

            base.Serializer.WriteMandatoryAttribute<ArticleId>( writer, nameof( dataObject.Id ), dataObject.Id );
            base.Serializer.WriteOptionalElements<OutputPack>( writer, OutputArticleDataContract<TTypeMappings>.PackName, dataObject.GetPacks() );
        }
    }
}