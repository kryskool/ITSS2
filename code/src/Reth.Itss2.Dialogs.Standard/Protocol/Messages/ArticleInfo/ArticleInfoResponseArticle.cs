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

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages.ArticleInfo
{
    public class ArticleInfoResponseArticle:IEquatable<ArticleInfoResponseArticle>
    {
        public static bool operator==( ArticleInfoResponseArticle? left, ArticleInfoResponseArticle? right )
		{
            return ArticleInfoResponseArticle.Equals( left, right );
		}
		
		public static bool operator!=( ArticleInfoResponseArticle? left, ArticleInfoResponseArticle? right )
		{
			return !( ArticleInfoResponseArticle.Equals( left, right ) );
		}

        public static bool Equals( ArticleInfoResponseArticle? left, ArticleInfoResponseArticle? right )
		{
            bool result = ArticleId.Equals( left?.Id, right?.Id );
            
            result &= ( result ? String.Equals( left?.Name, right?.Name, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? String.Equals( left?.DosageForm, right?.DosageForm, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? String.Equals( left?.PackagingUnit, right?.PackagingUnit, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? EqualityComparer<bool?>.Default.Equals( left?.RequiresFridge, right?.RequiresFridge ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.MaxSubItemQuantity, right?.MaxSubItemQuantity ) : false );
            result &= ( result ? PackDate.Equals( left?.SerialNumberSinceExpiryDate, right?.SerialNumberSinceExpiryDate ) : false );
            
            return result;
		}
        
        public ArticleInfoResponseArticle( ArticleId id )
        {
            this.Id = id;
        }

        public ArticleInfoResponseArticle(  ArticleId id,
                                            String? name,
                                            String? dosageForm,
                                            String? packagingUnit,
                                            bool? requiresFridge,
                                            int? maxSubItemQuantity,
                                            PackDate? serialNumberSinceExpiryDate   )
        {
            maxSubItemQuantity?.ThrowIfNegative();

            this.Id = id;
            this.Name = name;
            this.DosageForm = dosageForm;
            this.PackagingUnit = packagingUnit;
            this.RequiresFridge = requiresFridge;
            this.MaxSubItemQuantity = maxSubItemQuantity;
            this.SerialNumberSinceExpiryDate = serialNumberSinceExpiryDate;
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

        public bool? RequiresFridge
        {
            get;
        }

        public int? MaxSubItemQuantity
        {
            get;
        }

        public PackDate? SerialNumberSinceExpiryDate
        {
            get;
        }
        
        public override bool Equals( Object obj )
		{
			return this.Equals( obj as ArticleInfoResponseArticle );
		}
		
        public bool Equals( ArticleInfoResponseArticle? other )
		{
            return ArticleInfoResponseArticle.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override String ToString()
        {
            return this.Id.ToString();
        }
    }
}
