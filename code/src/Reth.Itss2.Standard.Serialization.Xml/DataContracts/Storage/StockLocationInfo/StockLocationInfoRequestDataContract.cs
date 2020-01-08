using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage.StockLocationInfo;
using Reth.Protocols;
using Reth.Protocols.Serialization;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.StockLocationInfo
{
    internal class StockLocationInfoRequestDataContract<TTypeMappings>:TraceableRequestDataContract<StockLocationInfoRequest, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public StockLocationInfoRequestDataContract()
        {
        }

        public StockLocationInfoRequestDataContract( StockLocationInfoRequest dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {
            IMessageId id = base.ReadId( reader );
            SubscriberId source = base.ReadSource( reader );
            SubscriberId destination = base.ReadDestination( reader );

            this.DataObject = new StockLocationInfoRequest( id, source, destination );
        }

        public override void WriteXml( XmlWriter writer )
        {
            base.WriteXml( writer );
        }
    }
}