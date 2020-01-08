using System.Collections.Generic;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.ArticleData.StockDeliverySet;
using Reth.Protocols;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.ArticleData.StockDeliverySet
{
    internal class StockDeliverySetRequestDataContract<TTypeMappings>:TraceableRequestDataContract<StockDeliverySetRequest, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        static StockDeliverySetRequestDataContract()
        {
        }

        public StockDeliverySetRequestDataContract()
        {
        }

        public StockDeliverySetRequestDataContract( StockDeliverySetRequest dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {
            IMessageId id = base.ReadId( reader );
            SubscriberId source = base.ReadSource( reader );
            SubscriberId destination = base.ReadDestination( reader );

            IEnumerable<StockDelivery> deliveries = base.Serializer.ReadOptionalElements<StockDelivery>( reader, nameof( StockDelivery ) ); 

            this.DataObject = new StockDeliverySetRequest( id, source, destination, deliveries );
        }

        public override void WriteXml( XmlWriter writer )
        {
            base.WriteXml( writer );

            base.Serializer.WriteOptionalElements<StockDelivery>( writer, nameof( StockDelivery ), this.DataObject.GetDeliveries() );
        }
    }
}