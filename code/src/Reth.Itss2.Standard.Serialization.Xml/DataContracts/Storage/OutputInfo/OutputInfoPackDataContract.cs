using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage;
using Reth.Itss2.Standard.Dialogs.Storage.OutputInfo;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.OutputInfo
{
    internal class OutputInfoPackDataContract<TTypeMappings>:XmlSerializable<OutputInfoPack, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public OutputInfoPackDataContract()
        {
        }

        public OutputInfoPackDataContract( OutputInfoPack dataObject )
        :
            base( dataObject )
        {
        }
        
        public override void ReadXml( XmlReader reader )
        {
            PackId id = base.Serializer.ReadMandatoryAttribute<PackId>( reader, nameof( this.DataObject.Id ) );
            int outputDestination = base.Serializer.ReadMandatoryInteger( reader, nameof( this.DataObject.OutputDestination ) );
            Nullable<int> outputPoint = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.OutputPoint ) );
            String deliveryNumber = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.DeliveryNumber ) );
            String batchNumber = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.BatchNumber ) );
            String externalId = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.ExternalId ) );
            String serialNumber = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.SerialNumber ) );
            String scanCode = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.ScanCode ) );
            String boxNumber = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.BoxNumber ) );
            String machineLocation = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.MachineLocation ) );
            StockLocationId stockLocationId = base.Serializer.ReadOptionalAttribute<StockLocationId>( reader, nameof( this.DataObject.StockLocationId ) );
            PackDate expiryDate = base.Serializer.ReadOptionalAttribute<PackDate>( reader, nameof( this.DataObject.ExpiryDate ) );
            PackDate stockInDate = base.Serializer.ReadOptionalAttribute<PackDate>( reader, nameof( this.DataObject.StockInDate ) );
            Nullable<int> subItemQuantity = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.SubItemQuantity ) );
            Nullable<int> depth = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.Depth ) );
            Nullable<int> width = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.Width ) );
            Nullable<int> height = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.Height ) );
            Nullable<int> weight = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.Weight ) );
            Nullable<PackShape> shape = base.Serializer.ReadOptionalEnum<PackShape>( reader, nameof( this.DataObject.Shape ), PackShape.Cuboid );
            Nullable<bool> isInFridge = base.Serializer.ReadOptionalBool( reader, nameof( this.DataObject.IsInFridge ), false );
            Nullable<LabelStatus> labelStatus = base.Serializer.ReadOptionalEnum<LabelStatus>( reader, nameof( this.DataObject.LabelStatus ) );

            this.DataObject = new OutputInfoPack(   id,
                                                    outputDestination,
                                                    outputPoint,
                                                    deliveryNumber,
                                                    batchNumber,
                                                    externalId,
                                                    serialNumber,
                                                    scanCode,
                                                    boxNumber,
                                                    machineLocation,
                                                    stockLocationId,
                                                    expiryDate,
                                                    stockInDate,
                                                    subItemQuantity,
                                                    depth,
                                                    width,
                                                    height,
                                                    weight,
                                                    shape,
                                                    isInFridge,
                                                    labelStatus );
        }

        public override void WriteXml( XmlWriter writer )
        {
            OutputInfoPack dataObject = this.DataObject;

            base.Serializer.WriteMandatoryAttribute<PackId>( writer, nameof( dataObject.Id ), dataObject.Id );
            base.Serializer.WriteMandatoryInteger( writer, nameof( dataObject.OutputDestination ), dataObject.OutputDestination );
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.OutputPoint ), dataObject.OutputPoint );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.DeliveryNumber ), dataObject.DeliveryNumber );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.BatchNumber ), dataObject.BatchNumber );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.ExternalId ), dataObject.ExternalId );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.SerialNumber ), dataObject.SerialNumber );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.ScanCode ), dataObject.ScanCode );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.BoxNumber ), dataObject.BoxNumber );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.MachineLocation ), dataObject.MachineLocation );
            base.Serializer.WriteOptionalAttribute<StockLocationId>( writer, nameof( dataObject.StockLocationId ), dataObject.StockLocationId );
            base.Serializer.WriteOptionalAttribute<PackDate>( writer, nameof( dataObject.ExpiryDate ), dataObject.ExpiryDate );
            base.Serializer.WriteOptionalAttribute<PackDate>( writer, nameof( dataObject.StockInDate ), dataObject.StockInDate );
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.SubItemQuantity ), dataObject.SubItemQuantity );
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.Depth ), dataObject.Depth );            
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.Width ), dataObject.Width );
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.Height ), dataObject.Height );
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.Weight ), dataObject.Weight );
            base.Serializer.WriteOptionalEnum<PackShape>( writer, nameof( dataObject.Shape ), dataObject.Shape );
            base.Serializer.WriteOptionalBool( writer, nameof( dataObject.IsInFridge ), dataObject.IsInFridge );
            base.Serializer.WriteOptionalEnum<LabelStatus>( writer, nameof( dataObject.LabelStatus ), dataObject.LabelStatus );
        }
    }
}