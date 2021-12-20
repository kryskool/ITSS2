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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.OutputInfo;
using Reth.Itss2.Dialogs.Standard.Serialization.Conversion;
using Reth.Itss2.Serialization;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Formats.Xml.Messages.OutputInfo
{
    public class OutputInfoPackDataContract:IDataContract<OutputInfoPack>
    {
        public OutputInfoPackDataContract()
        {
        }

        public OutputInfoPackDataContract( OutputInfoPack dataObject )
        {
            this.Id = TypeConverter.PackId.ConvertFrom( dataObject.Id );
            this.OutputDestination = TypeConverter.Int32.ConvertFrom( dataObject.OutputDestination );
            this.OutputPoint = TypeConverter.Int32.ConvertNullableFrom( dataObject.OutputPoint );
            this.DeliveryNumber = dataObject.DeliveryNumber;
            this.BatchNumber = dataObject.BatchNumber;
            this.ExternalId = dataObject.ExternalId;
            this.SerialNumber = dataObject.SerialNumber;
            this.ScanCode = dataObject.ScanCode;
            this.BoxNumber = dataObject.BoxNumber;
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
            this.LabelStatus = TypeConverter.LabelStatus.ConvertNullableFrom( dataObject.LabelStatus );
        }

        [XmlAttribute]
        public String Id{ get; set; } = String.Empty;

        [XmlAttribute]
        public String OutputDestination{ get; set; } = String.Empty;

        [XmlAttribute]
        public String? OutputPoint{ get; set; }

        [XmlAttribute]
        public String? DeliveryNumber{ get; set; }

        [XmlAttribute]
        public String? BatchNumber{ get; set; }

        [XmlAttribute]
        public String? ExternalId{ get; set; }

        [XmlAttribute]
        public String? SerialNumber{ get; set; }

        [XmlAttribute]
        public String? ScanCode{ get; set; }

        [XmlAttribute]
        public String? BoxNumber{ get; set; }

        [XmlAttribute]
        public String? MachineLocation{ get; set; }

        [XmlAttribute]
        public String? StockLocationId{ get; set; }

        [XmlAttribute]
        public String? ExpiryDate{ get; set; }

        [XmlAttribute]
        public String? StockInDate{ get; set; }

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

        [XmlAttribute]
        public String? IsInFridge{ get; set; }

        [XmlAttribute]
        public String? LabelStatus{ get; set; }
        
        public OutputInfoPack GetDataObject()
        {
            return new OutputInfoPack(  TypeConverter.PackId.ConvertTo( this.Id ),
                                        TypeConverter.Int32.ConvertTo( this.OutputDestination ),
                                        TypeConverter.Int32.ConvertNullableTo( this.OutputPoint ),
                                        this.DeliveryNumber,
                                        this.BatchNumber,
                                        this.ExternalId,
                                        this.SerialNumber,
                                        this.ScanCode,
                                        this.BoxNumber,
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
                                        TypeConverter.Boolean.ConvertNullableTo( this.IsInFridge ),
                                        TypeConverter.LabelStatus.ConvertNullableTo( this.LabelStatus ) );
        }
    }
}
