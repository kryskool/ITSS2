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
using Reth.Itss2.Serialization;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Formats.Json.Messages.Input
{
    public class InputMessageArticleDataContract:IDataContract<InputMessageArticle>
    {
        public InputMessageArticleDataContract()
        {
        }

        public InputMessageArticleDataContract( InputMessageArticle dataObject )
        {
            this.Id = TypeConverter.ArticleId.ConvertFrom( dataObject.Id );
            this.Name = dataObject.Name;
            this.DosageForm = dataObject.DosageForm;
            this.PackagingUnit = dataObject.PackagingUnit;
            this.MaxSubItemQuantity = TypeConverter.Int32.ConvertNullableFrom( dataObject.MaxSubItemQuantity );
            this.ProductCodes = TypeConverter.ConvertFromDataObjects<ProductCode, ProductCodeDataContract>( dataObject.GetProductCodes() );
            this.Packs = TypeConverter.ConvertFromDataObjects<InputMessagePack, InputMessagePackDataContract>( dataObject.GetPacks() );
        }

        public String Id{ get; set; } = String.Empty;

        public String? Name{ get; set; }

        public String? DosageForm{ get; set; }

        public String? PackagingUnit{ get; set; }
        
        public String? MaxSubItemQuantity{ get; set; }

        [JsonPropertyName( nameof( ProductCode ) )]
        public ProductCodeDataContract[]? ProductCodes{ get; set; }

        [JsonPropertyName( "Pack" )]
        public InputMessagePackDataContract[]? Packs{ get; set; }

        public virtual InputMessageArticle GetDataObject()
        {
            return new InputMessageArticle( TypeConverter.ArticleId.ConvertTo( this.Id ),
                                            this.Name,
                                            this.DosageForm,
                                            this.PackagingUnit,
                                            TypeConverter.Int32.ConvertNullableTo( this.MaxSubItemQuantity ),
                                            TypeConverter.ConvertToDataObjects<ProductCode, ProductCodeDataContract>( this.ProductCodes ),
                                            TypeConverter.ConvertToDataObjects<InputMessagePack, InputMessagePackDataContract>( this.Packs )    );
        }
    }
}
