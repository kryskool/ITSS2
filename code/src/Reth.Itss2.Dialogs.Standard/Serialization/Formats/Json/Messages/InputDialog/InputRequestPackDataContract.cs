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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Input;
using Reth.Itss2.Dialogs.Standard.Serialization.Conversion;
using Reth.Itss2.Serialization;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Formats.Json.Messages.Input
{
    public class InputRequestPackDataContract:IDataContract<InputRequestPack>
    {
        public InputRequestPackDataContract()
        {
        }

        public InputRequestPackDataContract( InputRequestPack dataObject )
        {
            this.ScanCode = dataObject.ScanCode;
            this.DeliveryNumber = dataObject.DeliveryNumber;
            this.BatchNumber = dataObject.BatchNumber;
            this.ExternalId = dataObject.ExternalId;
            this.SerialNumber = dataObject.SerialNumber;
            this.MachineLocation = dataObject.MachineLocation;
            this.StockLocationId = TypeConverter.StockLocationId.ConvertNullableFrom( dataObject.StockLocationId );
            this.ExpiryDate = TypeConverter.PackDate.ConvertNullableFrom( dataObject.ExpiryDate );
            this.Index = TypeConverter.Int32.ConvertNullableFrom( dataObject.Index );
            this.SubItemQuantity = TypeConverter.Int32.ConvertNullableFrom( dataObject.SubItemQuantity );
        }

        public String ScanCode{ get; set; } = String.Empty;

        public String? DeliveryNumber{ get; set; }

        public String? BatchNumber{ get; set; }

        public String? ExternalId{ get; set; }

        public String? SerialNumber{ get; set; }

        public String? MachineLocation{ get; set; }

        public String? StockLocationId{ get; set; }

        public String? ExpiryDate{ get; set; }

        public String? Index{ get; set; }

        public String? SubItemQuantity{ get; set; }
        
        public InputRequestPack GetDataObject()
        {
            return new InputRequestPack(    this.ScanCode,
                                            this.DeliveryNumber,
                                            this.BatchNumber,
                                            this.ExternalId,
                                            this.SerialNumber,
                                            this.MachineLocation,
                                            TypeConverter.StockLocationId.ConvertNullableTo( this.StockLocationId ),
                                            TypeConverter.PackDate.ConvertNullableTo( this.ExpiryDate ),
                                            TypeConverter.Int32.ConvertNullableTo( this.Index ),
                                            TypeConverter.Int32.ConvertNullableTo( this.SubItemQuantity )    );
        }
    }
}
