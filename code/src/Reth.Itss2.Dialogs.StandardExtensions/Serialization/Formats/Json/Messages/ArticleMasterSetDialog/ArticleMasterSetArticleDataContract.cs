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
using Reth.Itss2.Dialogs.Standard.Serialization;
using Reth.Itss2.Dialogs.Standard.Serialization.Conversion;
using Reth.Itss2.Dialogs.Standard.Serialization.Formats.Json.Messages;
using Reth.Itss2.Dialogs.StandardExtensions.Protocol.Messages.ArticleMasterSetDialog;

namespace Reth.Itss2.Dialogs.StandardExtensions.Serialization.Formats.Json.Messages.ArticleMasterSetDialog
{
    public class ArticleMasterSetArticleDataContract:IDataContract<ArticleMasterSetArticle>
    {
        public ArticleMasterSetArticleDataContract()
        {
        }

        public ArticleMasterSetArticleDataContract( ArticleMasterSetArticle dataObject )
        {
            this.Id = TypeConverter.ArticleId.ConvertFrom( dataObject.Id );
            this.Name = dataObject.Name;
            this.DosageForm = dataObject.DosageForm;
            this.PackagingUnit = dataObject.PackagingUnit;
            this.MachineLocation = dataObject.MachineLocation;
            this.StockLocationId = TypeConverter.StockLocationId.ConvertNullableFrom( dataObject.StockLocationId );
            this.RequiresFridge = TypeConverter.Boolean.ConvertNullableFrom( dataObject.RequiresFridge );
            this.BatchTracking = TypeConverter.Boolean.ConvertNullableFrom( dataObject.BatchTracking );
            this.ExpiryTracking = TypeConverter.Boolean.ConvertNullableFrom( dataObject.ExpiryTracking );
            this.SerialTracking = TypeConverter.Boolean.ConvertNullableFrom( dataObject.SerialTracking );
            this.MaxSubItemQuantity = TypeConverter.Int32.ConvertNullableFrom( dataObject.MaxSubItemQuantity );
            this.Depth = TypeConverter.Int32.ConvertNullableFrom( dataObject.Depth );
            this.Width = TypeConverter.Int32.ConvertNullableFrom( dataObject.Width );
            this.Height = TypeConverter.Int32.ConvertNullableFrom( dataObject.Height );
            this.Weight = TypeConverter.Int32.ConvertNullableFrom( dataObject.Weight );
            this.SerialNumberSinceExpiryDate = TypeConverter.PackDate.ConvertNullableFrom( dataObject.SerialNumberSinceExpiryDate );
            this.ProductCodes = TypeConverter.ConvertFromDataObjects<ProductCode, ProductCodeDataContract>( dataObject.GetProductCodes() );
        }

        public String Id { get; set; } = String.Empty;

        public String? Name{ get; set; }
        
        public String? DosageForm{ get; set; }
        
        public String? PackagingUnit{ get; set; }
        
        public String? MachineLocation{ get; set; }
        
        public String? StockLocationId{ get; set; }
        
        public String? RequiresFridge{ get; set; }
        
        public String? BatchTracking{ get; set; }
        
        public String? ExpiryTracking{ get; set; }
        
        public String? SerialTracking{ get; set; }
        
        public String? MaxSubItemQuantity{ get; set; }
        
        public String? Depth{ get; set; }
        
        public String? Width{ get; set; }
        
        public String? Height{ get; set; }
        
        public String? Weight{ get; set; }
        
        public String? SerialNumberSinceExpiryDate{ get; set; }
        
        [JsonPropertyName( nameof( ProductCode ) )]
        public ProductCodeDataContract[]? ProductCodes{ get; set; }
        
        public ArticleMasterSetArticle GetDataObject()
        {
            return new ArticleMasterSetArticle( TypeConverter.ArticleId.ConvertTo( this.Id ),
                                                this.Name,
                                                this.DosageForm,
                                                this.PackagingUnit,
                                                this.MachineLocation,
                                                TypeConverter.StockLocationId.ConvertNullableTo( this.StockLocationId ),
                                                TypeConverter.Boolean.ConvertNullableTo( this.RequiresFridge ),
                                                TypeConverter.Boolean.ConvertNullableTo( this.BatchTracking ),
                                                TypeConverter.Boolean.ConvertNullableTo( this.ExpiryTracking ),
                                                TypeConverter.Boolean.ConvertNullableTo( this.SerialTracking ),
                                                TypeConverter.Int32.ConvertNullableTo( this.MaxSubItemQuantity ),
                                                TypeConverter.Int32.ConvertNullableTo( this.Depth ),
                                                TypeConverter.Int32.ConvertNullableTo( this.Width ),
                                                TypeConverter.Int32.ConvertNullableTo( this.Height ),
                                                TypeConverter.Int32.ConvertNullableTo( this.Weight ),
                                                TypeConverter.PackDate.ConvertNullableTo( this.SerialNumberSinceExpiryDate ),
                                                TypeConverter.ConvertToDataObjects<ProductCode, ProductCodeDataContract>( this.ProductCodes ) );
        }
    }
}
