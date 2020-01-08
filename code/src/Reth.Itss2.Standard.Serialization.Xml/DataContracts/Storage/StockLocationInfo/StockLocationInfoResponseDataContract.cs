using System.Collections.Generic;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage.StockLocationInfo;
using Reth.Protocols;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.StockLocationInfo
{
    internal class StockLocationInfoResponseDataContract<TTypeMappings>:TraceableResponseDataContract<StockLocationInfoResponse, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public StockLocationInfoResponseDataContract()
        {
        }

        public StockLocationInfoResponseDataContract( StockLocationInfoResponse dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {
            IMessageId id = base.ReadId( reader );
            SubscriberId source = base.ReadSource( reader );
            SubscriberId destination = base.ReadDestination( reader );

            IEnumerable<StockLocation> stockLocations = base.Serializer.ReadOptionalElements<StockLocation>( reader, nameof( StockLocation ) );

            this.DataObject = new StockLocationInfoResponse( id, source, destination, stockLocations );
        }

        public override void WriteXml( XmlWriter writer )
        {
            base.WriteXml( writer );

            base.Serializer.WriteOptionalElements<StockLocation>( writer, nameof( StockLocation ), this.DataObject.GetStockLocations() );
        }
    }
}