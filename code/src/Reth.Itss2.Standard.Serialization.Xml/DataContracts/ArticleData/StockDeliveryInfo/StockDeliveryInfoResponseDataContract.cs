using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.ArticleData.StockDeliveryInfo;
using Reth.Protocols;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.ArticleData.StockDeliveryInfo
{
    internal class StockDeliveryInfoResponseDataContract<TTypeMappings>:TraceableResponseDataContract<StockDeliveryInfoResponse, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        private const String TaskName = "Task";

        public StockDeliveryInfoResponseDataContract()
        {
        }

        public StockDeliveryInfoResponseDataContract( StockDeliveryInfoResponse dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {
            IMessageId id = base.ReadId( reader );
            SubscriberId source = base.ReadSource( reader );
            SubscriberId destination = base.ReadDestination( reader );

            StockDeliveryInfoResponseTask task = base.Serializer.ReadMandatoryElement<StockDeliveryInfoResponseTask>( reader, StockDeliveryInfoResponseDataContract<TTypeMappings>.TaskName );
            
            this.DataObject = new StockDeliveryInfoResponse( id, source, destination, task );
        }

        public override void WriteXml( XmlWriter writer )
        {
            base.WriteXml( writer );

            base.Serializer.WriteMandatoryElement<StockDeliveryInfoResponseTask>( writer, StockDeliveryInfoResponseDataContract<TTypeMappings>.TaskName, this.DataObject.Task );
        }
    }
}