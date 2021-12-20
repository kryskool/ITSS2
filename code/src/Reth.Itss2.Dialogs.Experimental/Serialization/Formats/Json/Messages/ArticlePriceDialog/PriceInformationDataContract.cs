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

using Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ArticlePrice;
using Reth.Itss2.Dialogs.Experimental.Serialization.Conversion;
using Reth.Itss2.Serialization;

namespace Reth.Itss2.Dialogs.Experimental.Serialization.Formats.Json.Messages.ArticlePrice
{
    public class PriceInformationDataContract:IDataContract<PriceInformation>
    {
        public PriceInformationDataContract()
        {
        }

        public PriceInformationDataContract( PriceInformation dataObject )
        {
            this.Category = TypeConverter.PriceCategory.ConvertFrom( dataObject.Category );
            this.Price = TypeConverter.Decimal.ConvertFrom( dataObject.Price );
            this.Description = dataObject.Description;
            this.BasePriceUnit = dataObject.BasePriceUnit;
            this.BasePrice = TypeConverter.Int32.ConvertNullableFrom( dataObject.BasePrice );
            this.Quantity = TypeConverter.Int32.ConvertNullableFrom( dataObject.Quantity );
            this.Vat = dataObject.Vat;
        }

        public String Category{ get; set; } = String.Empty;

        public String Price{ get; set; } = String.Empty;

        public String? Description{ get; set; }

        public String? BasePriceUnit{ get; set; }

        public String? BasePrice{ get; set; }

        public String? Quantity{ get; set; }

        public decimal? Vat{ get; set; }
        
        public PriceInformation GetDataObject()
        {
            return new PriceInformation(    TypeConverter.PriceCategory.ConvertTo( this.Category ),
                                            TypeConverter.Decimal.ConvertTo( this.Price ),
                                            this.Description,
                                            this.BasePriceUnit,
                                            TypeConverter.Int32.ConvertNullableTo( this.BasePrice ),
                                            TypeConverter.Int32.ConvertNullableTo( this.Quantity ),
                                            this.Vat    );
        }
    }
}
