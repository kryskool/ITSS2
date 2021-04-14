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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Input;
using Reth.Itss2.Dialogs.Standard.Serialization.Conversion;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Formats.Xml.Messages.Input
{
    public class InputResponsePackDataContract:IDataContract<InputResponsePack>
    {
        public InputResponsePackDataContract()
        {
            this.Handling = new InputResponsePackHandlingDataContract();
        }

        public InputResponsePackDataContract( InputResponsePack dataObject )
        {
            this.Handling = TypeConverter.ConvertFromDataObject<InputResponsePackHandling, InputResponsePackHandlingDataContract>( dataObject.Handling );
            this.DeliveryNumber = dataObject.DeliveryNumber;
            this.BatchNumber = dataObject.BatchNumber;
            this.ExternalId = dataObject.ExternalId;
            this.SerialNumber = dataObject.SerialNumber;
            this.StockLocationId = TypeConverter.StockLocationId.ConvertNullableFrom( dataObject.StockLocationId );
            this.ExpiryDate = TypeConverter.PackDate.ConvertNullableFrom( dataObject.ExpiryDate );
            this.Index = TypeConverter.Int32.ConvertNullableFrom( dataObject.Index );
            this.SubItemQuantity = TypeConverter.Int32.ConvertNullableFrom( dataObject.SubItemQuantity );
            this.Depth = TypeConverter.Int32.ConvertNullableFrom( dataObject.Depth );
            this.Width = TypeConverter.Int32.ConvertNullableFrom( dataObject.Width );
            this.Height = TypeConverter.Int32.ConvertNullableFrom( dataObject.Height );
            this.Weight = TypeConverter.Int32.ConvertNullableFrom( dataObject.Weight );
        }

        [XmlElement]
        public InputResponsePackHandlingDataContract Handling{ get; set; }

        [XmlAttribute]
        public String? DeliveryNumber{ get; set; }

        [XmlAttribute]
        public String? BatchNumber{ get; set; }

        [XmlAttribute]
        public String? ExternalId{ get; set; }

        [XmlAttribute]
        public String? SerialNumber{ get; set; }

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

        public InputResponsePack GetDataObject()
        {
            return new InputResponsePack(   TypeConverter.ConvertToDataObject<InputResponsePackHandling, InputResponsePackHandlingDataContract>( this.Handling ),
                                            this.DeliveryNumber,
                                            this.BatchNumber,
                                            this.ExternalId,
                                            this.SerialNumber,
                                            TypeConverter.StockLocationId.ConvertNullableTo( this.StockLocationId ),
                                            TypeConverter.PackDate.ConvertNullableTo( this.ExpiryDate ),
                                            TypeConverter.Int32.ConvertNullableTo( this.Index ),
                                            TypeConverter.Int32.ConvertNullableTo( this.SubItemQuantity ),
                                            TypeConverter.Int32.ConvertNullableTo( this.Depth ),
                                            TypeConverter.Int32.ConvertNullableTo( this.Width ),
                                            TypeConverter.Int32.ConvertNullableTo( this.Height ),
                                            TypeConverter.Int32.ConvertNullableTo( this.Weight )   );
        }
    }
}
