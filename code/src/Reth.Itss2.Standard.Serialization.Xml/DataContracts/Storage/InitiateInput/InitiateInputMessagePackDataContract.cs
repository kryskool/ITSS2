using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage;
using Reth.Itss2.Standard.Dialogs.Storage.InitiateInput;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.InitiateInput
{
    internal class InitiateInputMessagePackDataContract<TTypeMappings>:XmlSerializable<InitiateInputMessagePack, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public InitiateInputMessagePackDataContract()
        {
        }

        public InitiateInputMessagePackDataContract( InitiateInputMessagePack dataObject )
        :
            base( dataObject )
        {
        }
        
        public override void ReadXml( XmlReader reader )
        {
            String scanCode = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.ScanCode ) );
            String deliveryNumber = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.DeliveryNumber ) );
            String batchNumber = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.BatchNumber ) );
            String externalId = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.ExternalId ) );
            String serialNumber = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.SerialNumber ) );                   
            String machineLocation = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.MachineLocation ) );
            StockLocationId stockLocationId = base.Serializer.ReadOptionalAttribute<StockLocationId>( reader, nameof( this.DataObject.StockLocationId ) );
            PackId id = base.Serializer.ReadOptionalAttribute<PackId>( reader, nameof( this.DataObject.Id ) );              
            PackDate expiryDate = base.Serializer.ReadOptionalAttribute<PackDate>( reader, nameof( this.DataObject.ExpiryDate ) );
            PackDate stockInDate = base.Serializer.ReadOptionalAttribute<PackDate>( reader, nameof( this.DataObject.StockInDate ) );         
            Nullable<int> index = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.Index ) );
            Nullable<int> subItemQuantity = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.SubItemQuantity ) );
            Nullable<int> depth = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.Depth ) );
            Nullable<int> width = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.Width ) );
            Nullable<int> height = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.Height ) );
            Nullable<int> weight = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.Weight ) );
            Nullable<PackShape> shape = base.Serializer.ReadOptionalEnum<PackShape>( reader, nameof( this.DataObject.Shape ) );
            Nullable<PackState> state = base.Serializer.ReadOptionalEnum<PackState>( reader, nameof( this.DataObject.State ) );
            Nullable<bool> isInFridge = base.Serializer.ReadOptionalBool( reader, nameof( this.DataObject.IsInFridge ), false );
            InitiateInputError error = base.Serializer.ReadOptionalElement<InitiateInputError>( reader, nameof( this.DataObject.Error ) );

            this.DataObject = new InitiateInputMessagePack( scanCode,
                                                            deliveryNumber,
                                                            batchNumber,
                                                            externalId,
                                                            serialNumber,
                                                            machineLocation,
                                                            stockLocationId,
                                                            id,
                                                            expiryDate,
                                                            stockInDate,
                                                            index,
                                                            subItemQuantity,
                                                            depth,
                                                            width,
                                                            height,
                                                            weight,
                                                            shape,
                                                            state,
                                                            isInFridge,
                                                            error   );
                }

        public override void WriteXml( XmlWriter writer )
        {
            InitiateInputMessagePack dataObject = this.DataObject;

            base.Serializer.WriteOptionalString( writer, nameof( dataObject.ScanCode ), dataObject.ScanCode );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.DeliveryNumber ), dataObject.DeliveryNumber );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.BatchNumber ), dataObject.BatchNumber );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.ExternalId ), dataObject.ExternalId );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.SerialNumber ), dataObject.SerialNumber );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.MachineLocation ), dataObject.MachineLocation );
            base.Serializer.WriteOptionalAttribute<StockLocationId>( writer, nameof( dataObject.StockLocationId ), dataObject.StockLocationId );
            base.Serializer.WriteOptionalAttribute<PackId>( writer, nameof( dataObject.Id ), dataObject.Id );
            base.Serializer.WriteOptionalAttribute<PackDate>( writer, nameof( dataObject.ExpiryDate ), dataObject.ExpiryDate );
            base.Serializer.WriteOptionalAttribute<PackDate>( writer, nameof( dataObject.StockInDate ), dataObject.StockInDate );
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.Index ), dataObject.Index );
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.SubItemQuantity ), dataObject.SubItemQuantity );
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.Depth ), dataObject.Depth );            
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.Width ), dataObject.Width );
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.Height ), dataObject.Height );
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.Weight ), dataObject.Weight );
            base.Serializer.WriteOptionalEnum<PackShape>( writer, nameof( dataObject.Shape ), dataObject.Shape );
            base.Serializer.WriteOptionalEnum<PackState>( writer, nameof( dataObject.State ), dataObject.State );
            base.Serializer.WriteOptionalBool( writer, nameof( dataObject.IsInFridge ), dataObject.IsInFridge );
            base.Serializer.WriteOptionalElement<InitiateInputError>( writer, nameof( dataObject.Error ), dataObject.Error );
        }
    }
}