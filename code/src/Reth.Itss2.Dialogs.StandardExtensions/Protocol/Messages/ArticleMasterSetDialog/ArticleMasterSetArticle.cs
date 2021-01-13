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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.ArticleMasterSetDialog;

namespace Reth.Itss2.Dialogs.StandardExtensions.Protocol.Messages.ArticleMasterSetDialog
{
    public class ArticleMasterSetArticle:Standard.Protocol.Messages.ArticleMasterSetDialog.ArticleMasterSetArticle, IEquatable<ArticleMasterSetArticle>
    {
        public static bool operator==( ArticleMasterSetArticle? left, ArticleMasterSetArticle? right )
		{
            return ArticleMasterSetResult.Equals( left, right );
		}
		
		public static bool operator!=( ArticleMasterSetArticle? left, ArticleMasterSetArticle? right )
		{
			return !( ArticleMasterSetResult.Equals( left, right ) );
		}

        public static bool Equals( ArticleMasterSetArticle? left, ArticleMasterSetArticle? right )
		{
            bool result = Standard.Protocol.Messages.ArticleMasterSetDialog.ArticleMasterSetArticle.Equals( left, right );
            
            result &= ( result ? EqualityComparer<bool?>.Default.Equals( left?.BatchTracking, right?.BatchTracking ) : false );
            result &= ( result ? EqualityComparer<bool?>.Default.Equals( left?.ExpiryTracking, right?.ExpiryTracking ) : false );
            result &= ( result ? EqualityComparer<bool?>.Default.Equals( left?.SerialTracking, right?.SerialTracking ) : false );
            
            return result;
		}

        public ArticleMasterSetArticle( ArticleId id )
        :
            base( id )
        {
        }

        public ArticleMasterSetArticle( ArticleId id,
                                        String? name,
                                        String? dosageForm,
                                        String? packagingUnit,
                                        String? machineLocation,
                                        StockLocationId? stockLocationId,
                                        bool? requiresFridge,
                                        bool? batchTracking,
                                        bool? expiryTracking,
                                        bool? serialTracking,
                                        int? maxSubItemQuantity,
                                        int? depth,
                                        int? width,
                                        int? height,
                                        int? weight,
                                        PackDate? serialNumberSinceExpiryDate,
                                        IEnumerable<ProductCode>? productCodes   )
        :
            base(   id,
                    name,
                    dosageForm,
                    packagingUnit,
                    machineLocation,
                    stockLocationId,
                    requiresFridge,
                    maxSubItemQuantity,
                    depth,
                    width,
                    height,
                    weight,
                    serialNumberSinceExpiryDate,
                    productCodes    )
        {
            this.BatchTracking = batchTracking;
            this.ExpiryTracking = expiryTracking;
            this.SerialTracking = serialTracking;
        }

        public bool? BatchTracking
        {
            get;
        }

        public bool? ExpiryTracking
        {
            get;
        }

        public bool? SerialTracking
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as ArticleMasterSetArticle );
		}
		
        public bool Equals( ArticleMasterSetArticle? other )
		{
            return ArticleMasterSetArticle.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override String ToString()
        {
            return this.Id.ToString();
        }
    }
}
