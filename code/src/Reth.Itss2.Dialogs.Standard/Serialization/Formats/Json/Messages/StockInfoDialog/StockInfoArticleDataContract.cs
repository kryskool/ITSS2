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
using System.Text.Json.Serialization;

using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockInfo;
using Reth.Itss2.Dialogs.Standard.Serialization.Conversion;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Formats.Json.Messages.StockInfo
{
    public class StockInfoArticleDataContract:IDataContract<StockInfoArticle>
    {
        public StockInfoArticleDataContract()
        {
        }

        public StockInfoArticleDataContract( StockInfoArticle dataObject )
        {
            this.Id = TypeConverter.ArticleId.ConvertFrom( dataObject.Id );
            this.Quantity = TypeConverter.Int32.ConvertFrom( dataObject.Quantity );
            this.Name = dataObject.Name;
            this.DosageForm = dataObject.DosageForm;
            this.PackagingUnit = dataObject.PackagingUnit;
            this.MaxSubItemQuantity = TypeConverter.Int32.ConvertNullableFrom( dataObject.MaxSubItemQuantity );
            this.ProductCodes = TypeConverter.ConvertFromDataObjects<ProductCode, ProductCodeDataContract>( dataObject.GetProductCodes() );
            this.Packs = TypeConverter.ConvertFromDataObjects<StockInfoPack, StockInfoPackDataContract>( dataObject.GetPacks() );
        }

        public String Id{ get; set; } = String.Empty;

        public String Quantity{ get; set; } = String.Empty;

        public String? Name{ get; set; }

        public String? DosageForm{ get; set; }

        public String? PackagingUnit{ get; set; }

        public String? MaxSubItemQuantity{ get; set; }

        [JsonPropertyName( nameof( ProductCode ) )]
        public ProductCodeDataContract[]? ProductCodes{ get; set; }

        [JsonPropertyName( "Pack" )]
        public StockInfoPackDataContract[]? Packs{ get; set; }
        
        public StockInfoArticle GetDataObject()
        {
            return new StockInfoArticle(    TypeConverter.ArticleId.ConvertTo( this.Id ),
                                            TypeConverter.Int32.ConvertTo( this.Quantity ),
                                            this.Name,
                                            this.DosageForm,
                                            this.PackagingUnit,
                                            TypeConverter.Int32.ConvertNullableTo( this.MaxSubItemQuantity ),
                                            TypeConverter.ConvertToDataObjects<ProductCode, ProductCodeDataContract>( this.ProductCodes ),
                                            TypeConverter.ConvertToDataObjects<StockInfoPack, StockInfoPackDataContract>( this.Packs )   );
        }
    }
}
