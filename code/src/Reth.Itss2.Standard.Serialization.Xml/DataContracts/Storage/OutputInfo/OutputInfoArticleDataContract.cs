using System;
using System.Collections.Generic;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage.OutputInfo;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.OutputInfo
{
    internal class OutputInfoArticleDataContract<TTypeMappings>:XmlSerializable<OutputInfoArticle, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        private const String PackName = "Pack";

        public OutputInfoArticleDataContract()
        {
        }

        public OutputInfoArticleDataContract( OutputInfoArticle dataObject )
        :
            base( dataObject )
        {
        }
        
        public override void ReadXml( XmlReader reader )
        {
            ArticleId id = base.Serializer.ReadMandatoryAttribute<ArticleId>( reader, nameof( this.DataObject.Id ) );
            IEnumerable<OutputInfoPack> packs = base.Serializer.ReadOptionalElements<OutputInfoPack>( reader, OutputInfoArticleDataContract<TTypeMappings>.PackName );

            this.DataObject = new OutputInfoArticle( id, packs );
        }

        public override void WriteXml( XmlWriter writer )
        {
            OutputInfoArticle dataObject = this.DataObject;

            base.Serializer.WriteMandatoryAttribute<ArticleId>( writer, nameof( dataObject.Id ), dataObject.Id );
            base.Serializer.WriteOptionalElements<OutputInfoPack>( writer, OutputInfoArticleDataContract<TTypeMappings>.PackName, dataObject.GetPacks() );
        }
    }
}