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
using System.Collections.Generic;
using System.Linq;

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages.ArticleMasterSet
{
    public class ArticleMasterSetArticle:IEquatable<ArticleMasterSetArticle>
    {
        public static bool operator==( ArticleMasterSetArticle? left, ArticleMasterSetArticle? right )
		{
            return ArticleMasterSetArticle.Equals( left, right );
		}
		
		public static bool operator!=( ArticleMasterSetArticle? left, ArticleMasterSetArticle? right )
		{
			return !( ArticleMasterSetArticle.Equals( left, right ) );
		}

        public static bool Equals( ArticleMasterSetArticle? left, ArticleMasterSetArticle? right )
		{
            bool result = ArticleId.Equals( left?.Id, right?.Id );
            
            result &= ( result ? String.Equals( left?.Name, right?.Name, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? String.Equals( left?.DosageForm, right?.DosageForm, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? String.Equals( left?.PackagingUnit, right?.PackagingUnit, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? String.Equals( left?.MachineLocation, right?.MachineLocation, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? StockLocationId.Equals( left?.StockLocationId, right?.StockLocationId ) : false );
            result &= ( result ? EqualityComparer<bool?>.Default.Equals( left?.RequiresFridge, right?.RequiresFridge ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.MaxSubItemQuantity, right?.MaxSubItemQuantity ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.Depth, right?.Depth ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.Width, right?.Width ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.Height, right?.Height ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.Weight, right?.Weight ) : false );
            result &= ( result ? PackDate.Equals( left?.SerialNumberSinceExpiryDate, right?.SerialNumberSinceExpiryDate ) : false );
            result &= ( result ? ( left?.ProductCodes.SequenceEqual( right?.ProductCodes ) ).GetValueOrDefault() : false );
            
            return result;
		}

        public ArticleMasterSetArticle( ArticleId id )
        {
            this.Id = id;
        }

        public ArticleMasterSetArticle( ArticleId id,
                                        String? name,
                                        String? dosageForm,
                                        String? packagingUnit,
                                        String? machineLocation,
                                        StockLocationId? stockLocationId,
                                        bool? requiresFridge,
                                        int? maxSubItemQuantity,
                                        int? depth,
                                        int? width,
                                        int? height,
                                        int? weight,
                                        PackDate? serialNumberSinceExpiryDate,
                                        IEnumerable<ProductCode>? productCodes   )
        {
            maxSubItemQuantity?.ThrowIfNegative();
            depth?.ThrowIfNegative();
            width?.ThrowIfNegative();
            height?.ThrowIfNegative();
            weight?.ThrowIfNegative();

            this.Id = id;
            this.Name = name;
            this.DosageForm = dosageForm;
            this.PackagingUnit = packagingUnit;
            this.MachineLocation = machineLocation;
            this.StockLocationId = stockLocationId;
            this.RequiresFridge = requiresFridge;
            this.MaxSubItemQuantity = maxSubItemQuantity;
            this.Depth = depth;
            this.Width = width;
            this.Height = height;
            this.Weight = weight;
            this.SerialNumberSinceExpiryDate = serialNumberSinceExpiryDate;

            if( productCodes is not null )
            {
                this.ProductCodes.AddRange( productCodes );
            }
        }

        public ArticleId Id
        {
            get;
        }

        public String? Name
        {
            get;
        }
        
        public String? DosageForm
        {
            get;
        }
        
        public String? PackagingUnit
        {
            get;
        }
        
        public String? MachineLocation
        {
            get;
        }
        
        public StockLocationId? StockLocationId
        {
            get;
        }
        
        public bool? RequiresFridge
        {
            get;
        }
        
        public int? MaxSubItemQuantity
        {
            get;
        }

        public int? Depth
        {
            get;
        }
        
        public int? Width
        {
            get;
        }
        
        public int? Height
        {
            get;
        }
        
        public int? Weight
        {
            get;
        }
        
        public PackDate? SerialNumberSinceExpiryDate{ get; }
        
        private List<ProductCode> ProductCodes
        {
            get;
        } = new List<ProductCode>();
        
        public ProductCode[] GetProductCodes()
        {
            return this.ProductCodes.ToArray();
        }

        public override bool Equals( Object? obj )
		{
			return this.Equals( obj as ArticleMasterSetArticle );
		}
		
        public bool Equals( ArticleMasterSetArticle? other )
		{
            return ArticleMasterSetArticle.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override String? ToString()
        {
            return this.Id.ToString();
        }
    }
}
