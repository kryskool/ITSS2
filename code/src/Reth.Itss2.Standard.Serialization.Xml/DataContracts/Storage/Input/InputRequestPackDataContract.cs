using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage.Input;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.Input
{
    internal class InputRequestPackDataContract<TTypeMappings>:XmlSerializable<InputRequestPack, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public InputRequestPackDataContract()
        {
        }

        public InputRequestPackDataContract( InputRequestPack dataObject )
        :
            base( dataObject )
        {
        }
        
        public override void ReadXml( XmlReader reader )
        {
            String scanCode = base.Serializer.ReadMandatoryString( reader, nameof( this.DataObject.ScanCode ) );
            String deliveryNumber = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.DeliveryNumber ) );
            String batchNumber = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.BatchNumber ) );
            String externalId = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.ExternalId ) );
            String serialNumber = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.SerialNumber ) );                   
            String machineLocation = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.MachineLocation ) );
            StockLocationId stockLocationId = base.Serializer.ReadOptionalAttribute<StockLocationId>( reader, nameof( this.DataObject.StockLocationId ) );
            PackId id = base.Serializer.ReadOptionalAttribute<PackId>( reader, nameof( this.DataObject.Id ) );              
            PackDate expiryDate = base.Serializer.ReadOptionalAttribute<PackDate>( reader, nameof( this.DataObject.ExpiryDate ) );
            Nullable<int> index = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.Index ) );
            Nullable<int> subItemQuantity = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.SubItemQuantity ) );

            this.DataObject = new InputRequestPack( scanCode,
                                                    deliveryNumber,
                                                    batchNumber,
                                                    externalId,
                                                    serialNumber,
                                                    machineLocation,
                                                    stockLocationId,
                                                    id,
                                                    expiryDate,
                                                    index,
                                                    subItemQuantity );
        }

        public override void WriteXml( XmlWriter writer )
        {
            InputRequestPack dataObject = this.DataObject;

            base.Serializer.WriteMandatoryString( writer, nameof( dataObject.ScanCode ), dataObject.ScanCode );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.DeliveryNumber ), dataObject.DeliveryNumber );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.BatchNumber ), dataObject.BatchNumber );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.ExternalId ), dataObject.ExternalId );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.SerialNumber ), dataObject.SerialNumber );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.MachineLocation ), dataObject.MachineLocation );
            base.Serializer.WriteOptionalAttribute<StockLocationId>( writer, nameof( dataObject.StockLocationId ), dataObject.StockLocationId );
            base.Serializer.WriteOptionalAttribute<PackId>( writer, nameof( dataObject.Id ), dataObject.Id );
            base.Serializer.WriteOptionalAttribute<PackDate>( writer, nameof( dataObject.ExpiryDate ), dataObject.ExpiryDate );
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.Index ), dataObject.Index );
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.SubItemQuantity ), dataObject.SubItemQuantity );
        }
    }
}