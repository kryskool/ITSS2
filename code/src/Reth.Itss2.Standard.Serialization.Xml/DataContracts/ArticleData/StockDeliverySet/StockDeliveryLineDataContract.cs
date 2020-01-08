using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.ArticleData.StockDeliverySet;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.ArticleData.StockDeliverySet
{
    internal class StockDeliveryLineDataContract<TTypeMappings>:XmlSerializable<StockDeliveryLine, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public StockDeliveryLineDataContract()
        {
        }

        public StockDeliveryLineDataContract( StockDeliveryLine dataObject )
        :
            base( dataObject )
        {
        }
        
        public override void ReadXml( XmlReader reader )
        {
            ArticleId id = base.Serializer.ReadMandatoryAttribute<ArticleId>( reader, nameof( this.DataObject.Id ) );
            String batchNumber = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.BatchNumber ) );
            String externalId = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.ExternalId ) );
            String serialNumber = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.SerialNumber ) );
            String machineLocation = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.MachineLocation ) );
            StockLocationId stockLocationId = base.Serializer.ReadOptionalAttribute<StockLocationId>( reader, nameof( this.DataObject.StockLocationId ) );
            Nullable<int> quantity = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.Quantity ), 0 );
            PackDate expiryDate = base.Serializer.ReadOptionalAttribute<PackDate>( reader, nameof( this.DataObject.ExpiryDate ) );

            this.DataObject = new StockDeliveryLine(    id,
                                                        batchNumber,
                                                        externalId,
                                                        serialNumber,
                                                        machineLocation,
                                                        stockLocationId,
                                                        quantity,
                                                        expiryDate  );
        }

        public override void WriteXml( XmlWriter writer )
        {
            StockDeliveryLine dataObject = this.DataObject;

            base.Serializer.WriteMandatoryAttribute<ArticleId>( writer, nameof( dataObject.Id ), dataObject.Id );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.BatchNumber ), dataObject.BatchNumber );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.ExternalId ), dataObject.ExternalId );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.SerialNumber ), dataObject.SerialNumber );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.MachineLocation ), dataObject.MachineLocation );
            base.Serializer.WriteOptionalAttribute<StockLocationId>( writer, nameof( dataObject.StockLocationId ), dataObject.StockLocationId );
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.Quantity ), dataObject.Quantity );
            base.Serializer.WriteOptionalAttribute<PackDate>( writer, nameof( dataObject.ExpiryDate ), dataObject.ExpiryDate );
        }
    }
}