using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.ArticleData.StockDeliverySet;
using Reth.Protocols;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.ArticleData.StockDeliverySet
{
    internal class StockDeliverySetResponseDataContract<TTypeMappings>:TraceableResponseDataContract<StockDeliverySetResponse, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        private const String SetResultName = "SetResult";

        public StockDeliverySetResponseDataContract()
        {
        }

        public StockDeliverySetResponseDataContract( StockDeliverySetResponse dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {
            IMessageId id = base.ReadId( reader );
            SubscriberId source = base.ReadSource( reader );
            SubscriberId destination = base.ReadDestination( reader );

            StockDeliverySetResult result = base.Serializer.ReadMandatoryElement<StockDeliverySetResult>( reader, StockDeliverySetResponseDataContract<TTypeMappings>.SetResultName );

            this.DataObject = new StockDeliverySetResponse( id, source, destination, result );
        }

        public override void WriteXml( XmlWriter writer )
        {
            base.WriteXml( writer );

            base.Serializer.WriteMandatoryElement<StockDeliverySetResult>( writer, StockDeliverySetResponseDataContract<TTypeMappings>.SetResultName, this.DataObject.Result );
        }
    }
}