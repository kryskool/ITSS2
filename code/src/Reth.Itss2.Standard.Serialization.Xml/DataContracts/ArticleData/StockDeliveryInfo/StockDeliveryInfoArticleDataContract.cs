using System;
using System.Collections.Generic;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.ArticleData.StockDeliveryInfo;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.ArticleData.StockDeliveryInfo
{
    internal class StockDeliveryInfoArticleDataContract<TTypeMappings>:XmlSerializable<StockDeliveryInfoArticle, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        private const String PackName = "Pack";

        public StockDeliveryInfoArticleDataContract()
        {
        }

        public StockDeliveryInfoArticleDataContract( StockDeliveryInfoArticle dataObject )
        :
            base( dataObject )
        {
        }
        
        public override void ReadXml( XmlReader reader )
        {
            ArticleId id = base.Serializer.ReadMandatoryAttribute<ArticleId>( reader, nameof( this.DataObject.Id ) );
            Nullable<int> quantity = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.Quantity ), 0 );
            IEnumerable<StockDeliveryInfoPack> packs = base.Serializer.ReadOptionalElements<StockDeliveryInfoPack>( reader, StockDeliveryInfoArticleDataContract<TTypeMappings>.PackName );

            this.DataObject = new StockDeliveryInfoArticle( id,
                                                            quantity,
                                                            packs   );
        }

        public override void WriteXml( XmlWriter writer )
        {
            StockDeliveryInfoArticle dataObject = this.DataObject;

            base.Serializer.WriteMandatoryAttribute<ArticleId>( writer, nameof( dataObject.Id ), dataObject.Id );
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.Quantity ), dataObject.Quantity );
            base.Serializer.WriteOptionalElements<StockDeliveryInfoPack>( writer, StockDeliveryInfoArticleDataContract<TTypeMappings>.PackName, dataObject.GetPacks() );
        }
    }
}