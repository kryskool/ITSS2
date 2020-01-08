using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs.ArticleData.StockDeliveryInfo;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.ArticleData.StockDeliveryInfo
{
    internal class StockDeliveryInfoRequestTaskDataContract<TTypeMappings>:XmlSerializable<StockDeliveryInfoRequestTask, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public StockDeliveryInfoRequestTaskDataContract()
        {
        }

        public StockDeliveryInfoRequestTaskDataContract( StockDeliveryInfoRequestTask dataObject )
        :
            base( dataObject )
        {
        }
        
        public override void ReadXml( XmlReader reader )
        {
            String id = base.Serializer.ReadMandatoryString( reader, nameof( this.DataObject.Id ) );
            
            this.DataObject = new StockDeliveryInfoRequestTask( id );
        }

        public override void WriteXml( XmlWriter writer )
        {
            StockDeliveryInfoRequestTask dataObject = this.DataObject;

            base.Serializer.WriteMandatoryString( writer, nameof( dataObject.Id ), dataObject.Id );
        }
    }
}