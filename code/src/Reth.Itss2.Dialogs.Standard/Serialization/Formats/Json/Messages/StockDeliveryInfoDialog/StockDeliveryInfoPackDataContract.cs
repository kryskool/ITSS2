// Implementation of the WWKS2 protocol.
// Copyright (C) 2020  Thomas Reth

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.

// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System;

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockDeliveryInfoDialog;
using Reth.Itss2.Dialogs.Standard.Serialization.Conversion;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Formats.Json.Messages.StockDeliveryInfoDialog
{
    public class StockDeliveryInfoPackDataContract:IDataContract<StockDeliveryInfoPack>
    {
        public StockDeliveryInfoPackDataContract()
        {
        }

        public StockDeliveryInfoPackDataContract( StockDeliveryInfoPack dataObject )
        {
            this.Id = TypeConverter.PackId.ConvertFrom( dataObject.Id );
            this.DeliveryNumber = dataObject.DeliveryNumber;
            this.BatchNumber = dataObject.BatchNumber;
            this.ExternalId = dataObject.ExternalId;
            this.SerialNumber = dataObject.SerialNumber;
            this.ScanCode = dataObject.ScanCode;
            this.MachineLocation = dataObject.MachineLocation;
            this.StockLocationId = TypeConverter.StockLocationId.ConvertNullableFrom( dataObject.StockLocationId );
            this.ExpiryDate = TypeConverter.PackDate.ConvertNullableFrom( dataObject.ExpiryDate );
            this.StockInDate = TypeConverter.PackDate.ConvertNullableFrom( dataObject.StockInDate );
            this.SubItemQuantity = TypeConverter.Int32.ConvertNullableFrom( dataObject.SubItemQuantity );
            this.Depth = TypeConverter.Int32.ConvertNullableFrom( dataObject.Depth );
            this.Width = TypeConverter.Int32.ConvertNullableFrom( dataObject.Width );
            this.Height = TypeConverter.Int32.ConvertNullableFrom( dataObject.Height );
            this.Weight = TypeConverter.Int32.ConvertNullableFrom( dataObject.Weight );
            this.Shape = TypeConverter.PackShape.ConvertNullableFrom( dataObject.Shape );
            this.IsInFridge = TypeConverter.Boolean.ConvertNullableFrom( dataObject.IsInFridge );
        }

        public String Id{ get; set; } = String.Empty;

        public String? DeliveryNumber{ get; set; }

        public String? BatchNumber{ get; set; }

        public String? ExternalId{ get; set; }

        public String? SerialNumber{ get; set; }

        public String? ScanCode{ get; set; }

        public String? MachineLocation{ get; set; }

        public String? StockLocationId{ get; set; }

        public String? ExpiryDate{ get; set; }

        public String? StockInDate{ get; set; }

        public String? SubItemQuantity{ get; set; }

        public String? Depth{ get; set; }

        public String? Width{ get; set; }

        public String? Height{ get; set; }

        public String? Weight{ get; set; }

        public String? Shape{ get; set; }

        public String? IsInFridge{ get; set; }
        
        public StockDeliveryInfoPack GetDataObject()
        {
            return new StockDeliveryInfoPack(   TypeConverter.PackId.ConvertTo( this.Id ),
                                                this.DeliveryNumber,
                                                this.BatchNumber,
                                                this.ExternalId,
                                                this.SerialNumber,
                                                this.ScanCode,
                                                this.MachineLocation,
                                                TypeConverter.StockLocationId.ConvertNullableTo( this.StockLocationId ),
                                                TypeConverter.PackDate.ConvertNullableTo( this.ExpiryDate ),
                                                TypeConverter.PackDate.ConvertNullableTo( this.StockInDate ),
                                                TypeConverter.Int32.ConvertNullableTo( this.SubItemQuantity ),
                                                TypeConverter.Int32.ConvertNullableTo( this.Depth ),
                                                TypeConverter.Int32.ConvertNullableTo( this.Width ),
                                                TypeConverter.Int32.ConvertNullableTo( this.Height ),
                                                TypeConverter.Int32.ConvertNullableTo( this.Weight ),
                                                TypeConverter.PackShape.ConvertNullableTo( this.Shape ),
                                                TypeConverter.Boolean.ConvertNullableTo( this.IsInFridge )  );
        }
    }
}
