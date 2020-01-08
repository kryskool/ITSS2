using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage.Input;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.Input
{
    internal class InputResponsePackDataContract<TTypeMappings>:XmlSerializable<InputResponsePack, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public InputResponsePackDataContract()
        {
        }

        public InputResponsePackDataContract( InputResponsePack dataObject )
        :
            base( dataObject )
        {
        }
        
        public override void ReadXml( XmlReader reader )
        {
            InputResponsePackHandling handling = base.Serializer.ReadMandatoryElement<InputResponsePackHandling>( reader, nameof( this.DataObject.Handling ) );
            String deliveryNumber = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.DeliveryNumber ) );
            String batchNumber = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.BatchNumber ) );
            String externalId = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.ExternalId ) );
            String serialNumber = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.SerialNumber ) );
            StockLocationId stockLocationId = base.Serializer.ReadOptionalAttribute<StockLocationId>( reader, nameof( this.DataObject.StockLocationId ) );
            PackDate expiryDate = base.Serializer.ReadOptionalAttribute<PackDate>( reader, nameof( this.DataObject.ExpiryDate ) );
            Nullable<int> index = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.Index ) );
            Nullable<int> subItemQuantity = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.SubItemQuantity ) );
            Nullable<int> depth = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.Depth ) );
            Nullable<int> width = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.Width ) );
            Nullable<int> height = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.Height ) );
            Nullable<int> weight = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.Weight ) );

            this.DataObject = new InputResponsePack(    handling,
                                                        deliveryNumber,
                                                        batchNumber,
                                                        externalId,
                                                        serialNumber,
                                                        stockLocationId,
                                                        expiryDate,
                                                        index,
                                                        subItemQuantity,
                                                        depth,
                                                        width,
                                                        height,
                                                        weight  );
        }

        public override void WriteXml( XmlWriter writer )
        {
            InputResponsePack dataObject = this.DataObject;

            base.Serializer.WriteMandatoryElement<InputResponsePackHandling>( writer, nameof( dataObject.Handling ), dataObject.Handling );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.DeliveryNumber ), dataObject.DeliveryNumber );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.BatchNumber ), dataObject.BatchNumber );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.ExternalId ), dataObject.ExternalId );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.SerialNumber ), dataObject.SerialNumber );
            base.Serializer.WriteOptionalAttribute<StockLocationId>( writer, nameof( dataObject.StockLocationId ), dataObject.StockLocationId );
            base.Serializer.WriteOptionalAttribute<PackDate>( writer, nameof( dataObject.ExpiryDate ), dataObject.ExpiryDate );
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.Index ), dataObject.Index );
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.SubItemQuantity ), dataObject.SubItemQuantity );
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.Depth ), dataObject.Depth );            
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.Width ), dataObject.Width );
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.Height ), dataObject.Height );
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.Weight ), dataObject.Weight );
        }
    }
}