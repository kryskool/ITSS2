using System;
using System.Collections.Generic;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage;
using Reth.Itss2.Standard.Dialogs.Storage.OutputInfo;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.OutputInfo
{
    internal class OutputInfoResponseTaskDataContract<TTypeMappings>:XmlSerializable<OutputInfoResponseTask, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        private const String ArticleName = "Article";
        private const String BoxName = "Box";
        private const String StatusName = "Status";

        public OutputInfoResponseTaskDataContract()
        {
        }

        public OutputInfoResponseTaskDataContract( OutputInfoResponseTask dataObject )
        :
            base( dataObject )
        {
        }
        
        public override void ReadXml( XmlReader reader )
        {
            MessageId id = base.Serializer.ReadMandatoryAttribute<MessageId>( reader, nameof( this.DataObject.Id ) );
            OutputInfoStatus status = base.Serializer.ReadMandatoryEnum<OutputInfoStatus>( reader, OutputInfoResponseTaskDataContract<TTypeMappings>.StatusName );
            IEnumerable<OutputInfoArticle> articles = base.Serializer.ReadOptionalElements<OutputInfoArticle>( reader, OutputInfoResponseTaskDataContract<TTypeMappings>.ArticleName );
            IEnumerable<OutputBox> boxes = base.Serializer.ReadOptionalElements<OutputBox>( reader, OutputInfoResponseTaskDataContract<TTypeMappings>.BoxName );

            this.DataObject = new OutputInfoResponseTask( id, status, articles, boxes );
        }

        public override void WriteXml( XmlWriter writer )
        {
            OutputInfoResponseTask dataObject = this.DataObject;

            base.Serializer.WriteMandatoryAttribute<MessageId>( writer, nameof( dataObject.Id ), dataObject.Id );
            base.Serializer.WriteMandatoryEnum<OutputInfoStatus>( writer, OutputInfoResponseTaskDataContract<TTypeMappings>.StatusName, dataObject.Status );
            base.Serializer.WriteOptionalElements<OutputInfoArticle>( writer, OutputInfoResponseTaskDataContract<TTypeMappings>.ArticleName, dataObject.GetArticles() );
            base.Serializer.WriteOptionalElements<OutputBox>( writer, OutputInfoResponseTaskDataContract<TTypeMappings>.BoxName, dataObject.GetBoxes() );
        }
    }
}