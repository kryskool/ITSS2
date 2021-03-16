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
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Input;
using Reth.Itss2.Dialogs.Standard.Serialization.Conversion;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Formats.Json.Messages.Input
{
    public class InputResponseArticleDataContract:IDataContract<InputResponseArticle>
    {
        public InputResponseArticleDataContract()
        {
        }

        public InputResponseArticleDataContract( InputResponseArticle dataObject )
        {
            this.Id = TypeConverter.ArticleId.ConvertNullableFrom( dataObject.Id );
            this.Name = dataObject.Name;
            this.DosageForm = dataObject.DosageForm;
            this.PackagingUnit = dataObject.PackagingUnit;
            this.RequiresFridge = TypeConverter.Boolean.ConvertNullableFrom( dataObject.RequiresFridge );
            this.MaxSubItemQuantity = TypeConverter.Int32.ConvertNullableFrom( dataObject.MaxSubItemQuantity );
            this.SerialNumberSinceExpiryDate = TypeConverter.PackDate.ConvertNullableFrom( dataObject.SerialNumberSinceExpiryDate );
            this.ProductCodes = TypeConverter.ConvertFromDataObjects<ProductCode, ProductCodeDataContract>( dataObject.GetProductCodes() );
            this.Packs = TypeConverter.ConvertFromDataObjects<InputResponsePack, InputResponsePackDataContract>( dataObject.GetPacks() );
        }

        public String? Id{ get; set; }

        public String? Name{ get; set; }

        public String? DosageForm{ get; set; }

        public String? PackagingUnit{ get; set; }

        public String? RequiresFridge{ get; set; }
        
        public String? MaxSubItemQuantity{ get; set; }

        public String? SerialNumberSinceExpiryDate{ get; set; }

        [JsonPropertyName( nameof( ProductCode ) )]
        public ProductCodeDataContract[]? ProductCodes{ get; set; }

        [JsonPropertyName( "Pack" )]
        public InputResponsePackDataContract[]? Packs{ get; set; }

        public virtual InputResponseArticle GetDataObject()
        {
            return new InputResponseArticle(    TypeConverter.ArticleId.ConvertNullableTo( this.Id ),
                                                this.Name,
                                                this.DosageForm,
                                                this.PackagingUnit,
                                                TypeConverter.Boolean.ConvertNullableTo( this.RequiresFridge ),
                                                TypeConverter.Int32.ConvertNullableTo( this.MaxSubItemQuantity ),
                                                TypeConverter.PackDate.ConvertNullableTo( this.SerialNumberSinceExpiryDate ),
                                                TypeConverter.ConvertToDataObjects<ProductCode, ProductCodeDataContract>( this.ProductCodes ),
                                                TypeConverter.ConvertToDataObjects<InputResponsePack, InputResponsePackDataContract>( this.Packs )   );
        }
    }
}
