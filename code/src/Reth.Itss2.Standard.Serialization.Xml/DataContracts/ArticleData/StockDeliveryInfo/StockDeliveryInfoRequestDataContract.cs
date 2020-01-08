using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.ArticleData.StockDeliveryInfo;
using Reth.Protocols;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.ArticleData.StockDeliveryInfo
{
    internal class StockDeliveryInfoRequestDataContract<TTypeMappings>:TraceableRequestDataContract<StockDeliveryInfoRequest, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        private const String TaskName = "Task";

        public StockDeliveryInfoRequestDataContract()
        {
        }

        public StockDeliveryInfoRequestDataContract( StockDeliveryInfoRequest dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {
            IMessageId id = base.ReadId( reader );
            SubscriberId source = base.ReadSource( reader );
            SubscriberId destination = base.ReadDestination( reader );

            Nullable<bool> includeTaskDetails = base.Serializer.ReadOptionalBool( reader, nameof( this.DataObject.IncludeTaskDetails ), false );
            StockDeliveryInfoRequestTask task = base.Serializer.ReadMandatoryElement<StockDeliveryInfoRequestTask>( reader, StockDeliveryInfoRequestDataContract<TTypeMappings>.TaskName );
            
            this.DataObject = new StockDeliveryInfoRequest( id, source, destination, task, includeTaskDetails );
        }

        public override void WriteXml( XmlWriter writer )
        {
            base.WriteXml( writer );

            StockDeliveryInfoRequest dataObject = this.DataObject;

            base.Serializer.WriteOptionalBool( writer, nameof( dataObject.IncludeTaskDetails ), dataObject.IncludeTaskDetails );
            base.Serializer.WriteMandatoryElement<StockDeliveryInfoRequestTask>( writer, StockDeliveryInfoRequestDataContract<TTypeMappings>.TaskName, dataObject.Task );
        }
    }
}