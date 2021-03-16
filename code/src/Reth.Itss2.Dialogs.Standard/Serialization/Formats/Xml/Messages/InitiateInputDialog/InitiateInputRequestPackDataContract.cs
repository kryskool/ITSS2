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
using System.Xml;
using System.Xml.Serialization;

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.InitiateInput;
using Reth.Itss2.Dialogs.Standard.Serialization.Conversion;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Formats.Xml.Messages.InitiateInput
{
    public class InitiateInputRequestPackDataContract:IDataContract<InitiateInputRequestPack>
    {
        public InitiateInputRequestPackDataContract()
        {
        }

        public InitiateInputRequestPackDataContract( InitiateInputRequestPack dataObject )
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
            this.Depth = TypeConverter.Int32.ConvertNullableFrom( dataObject.Depth );
            this.Width = TypeConverter.Int32.ConvertNullableFrom( dataObject.Width );
            this.Height = TypeConverter.Int32.ConvertNullableFrom( dataObject.Height );
            this.Weight = TypeConverter.Int32.ConvertNullableFrom( dataObject.Weight );
            this.Shape = TypeConverter.PackShape.ConvertNullableFrom( dataObject.Shape );
        }

        [XmlAttribute]
        public String ScanCode{ get; set; } = String.Empty;

        [XmlAttribute]
        public String? DeliveryNumber{ get; set; }

        [XmlAttribute]
        public String? BatchNumber{ get; set; }

        [XmlAttribute]
        public String? ExternalId{ get; set; }

        [XmlAttribute]
        public String? SerialNumber{ get; set; }

        [XmlAttribute]
        public String? MachineLocation{ get; set; }

        [XmlAttribute]
        public String? StockLocationId{ get; set; }

        [XmlAttribute]
        public String? ExpiryDate{ get; set; }

        [XmlAttribute]
        public String? Index{ get; set; }

        [XmlAttribute]
        public String? SubItemQuantity{ get; set; }

        [XmlAttribute]
        public String? Depth{ get; set; }

        [XmlAttribute]
        public String? Width{ get; set; }

        [XmlAttribute]
        public String? Height{ get; set; }

        [XmlAttribute]
        public String? Weight{ get; set; }

        [XmlAttribute]
        public String? Shape{ get; set; }
        
        public InitiateInputRequestPack GetDataObject()
        {
            return new InitiateInputRequestPack(    this.ScanCode,
                                                    this.DeliveryNumber,
                                                    this.BatchNumber,
                                                    this.ExternalId,
                                                    this.SerialNumber,
                                                    this.MachineLocation,
                                                    TypeConverter.StockLocationId.ConvertNullableTo( this.StockLocationId ),
                                                    TypeConverter.PackDate.ConvertNullableTo( this.ExpiryDate ),
                                                    TypeConverter.Int32.ConvertNullableTo( this.Index ),
                                                    TypeConverter.Int32.ConvertNullableTo( this.SubItemQuantity ),
                                                    TypeConverter.Int32.ConvertNullableTo( this.Depth ),
                                                    TypeConverter.Int32.ConvertNullableTo( this.Width ),
                                                    TypeConverter.Int32.ConvertNullableTo( this.Height ),
                                                    TypeConverter.Int32.ConvertNullableTo( this.Weight ),
                                                    TypeConverter.PackShape.ConvertNullableTo( this.Shape ) );
        }
    }
}
