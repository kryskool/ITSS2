using System;
using System.Collections.Generic;
using System.Xml;

using Reth.Itss2.Standard.Dialogs.ArticleData.StockDeliveryInfo;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.ArticleData.StockDeliveryInfo
{
    internal class StockDeliveryInfoResponseTaskDataContract<TTypeMappings>:XmlSerializable<StockDeliveryInfoResponseTask, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        private const String ArticleName = "Article";
        private const String StatusName = "Status";

        public StockDeliveryInfoResponseTaskDataContract()
        {
        }

        public StockDeliveryInfoResponseTaskDataContract( StockDeliveryInfoResponseTask dataObject )
        :
            base( dataObject )
        {
        }
        
        public override void ReadXml( XmlReader reader )
        {
            String id = base.Serializer.ReadMandatoryString( reader, nameof( this.DataObject.Id ) );
            StockDeliveryInfoStatus status = base.Serializer.ReadMandatoryEnum<StockDeliveryInfoStatus>( reader, StockDeliveryInfoResponseTaskDataContract<TTypeMappings>.StatusName );
            IEnumerable<StockDeliveryInfoArticle> articles = base.Serializer.ReadOptionalElements<StockDeliveryInfoArticle>( reader, StockDeliveryInfoResponseTaskDataContract<TTypeMappings>.ArticleName );

            this.DataObject = new StockDeliveryInfoResponseTask( id, status, articles );
        }

        public override void WriteXml( XmlWriter writer )
        {
            StockDeliveryInfoResponseTask dataObject = this.DataObject;

            base.Serializer.WriteMandatoryString( writer, nameof( dataObject.Id ), dataObject.Id );
            base.Serializer.WriteMandatoryEnum<StockDeliveryInfoStatus>( writer, StockDeliveryInfoResponseTaskDataContract<TTypeMappings>.StatusName, dataObject.Status );
            base.Serializer.WriteOptionalElements<StockDeliveryInfoArticle>( writer, StockDeliveryInfoResponseTaskDataContract<TTypeMappings>.ArticleName, dataObject.GetArticles() );
        }
    }
}