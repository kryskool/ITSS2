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

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages.InitiateInput
{
    public class InitiateInputResponseArticle:IEquatable<InitiateInputResponseArticle>
    {
        public static bool operator==( InitiateInputResponseArticle? left, InitiateInputResponseArticle? right )
		{
            return InitiateInputResponseArticle.Equals( left, right );
		}
		
		public static bool operator!=( InitiateInputResponseArticle? left, InitiateInputResponseArticle? right )
		{
			return !( InitiateInputResponseArticle.Equals( left, right ) );
		}

        public static bool Equals( InitiateInputResponseArticle? left, InitiateInputResponseArticle? right )
		{
            bool result = ArticleId.Equals( left?.Id, right?.Id );
            
            result &= ( result ? String.Equals( left?.Name, right?.Name, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? String.Equals( left?.DosageForm, right?.DosageForm, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? String.Equals( left?.PackagingUnit, right?.PackagingUnit, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.MaxSubItemQuantity, right?.MaxSubItemQuantity ) : false );
            result &= ( result ? PackDate.Equals( left?.SerialNumberSinceExpiryDate, right?.SerialNumberSinceExpiryDate ) : false );
            result &= ( result ? ( left?.ProductCodes.SequenceEqual( right?.ProductCodes ) ).GetValueOrDefault() : false );
            result &= ( result ? ( left?.Packs.SequenceEqual( right?.Packs ) ).GetValueOrDefault() : false );
            
            return result;
		}

        public InitiateInputResponseArticle(    ArticleId? id,
                                                String? name,
                                                String? dosageForm,
                                                String? packagingUnit,
                                                int? maxSubItemQuantity,
                                                PackDate? serialNumberSinceExpiryDate,
                                                IEnumerable<ProductCode>? productCodes,
                                                IEnumerable<InitiateInputResponsePack>? packs    )
        {
            maxSubItemQuantity?.ThrowIfNegative();

            this.Id = id;
            this.Name = name;
            this.DosageForm = dosageForm;
            this.PackagingUnit = packagingUnit;
            this.MaxSubItemQuantity = maxSubItemQuantity;
            this.SerialNumberSinceExpiryDate = serialNumberSinceExpiryDate;

            if( productCodes is not null )
            {
                this.ProductCodes.AddRange( productCodes );
            }

            if( packs is not null )
            {
                this.Packs.AddRange( packs );
            }
        }

        public ArticleId? Id
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

        public int? MaxSubItemQuantity
        {
            get;
        }

        public PackDate? SerialNumberSinceExpiryDate
        {
            get;
        }

        private List<ProductCode> ProductCodes
        {
            get;
        } = new List<ProductCode>();

        private List<InitiateInputResponsePack> Packs
        {
            get;
        } = new List<InitiateInputResponsePack>();
        
        public ProductCode[] GetProductCodes()
        {
            return this.ProductCodes.ToArray();

        }
        public InitiateInputResponsePack[] GetPacks()
        {
            return this.Packs.ToArray();
        }

        public override bool Equals( Object? obj )
		{
			return this.Equals( obj as InitiateInputResponseArticle );
		}
		
        public bool Equals( InitiateInputResponseArticle? other )
		{
            return InitiateInputResponseArticle.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return HashCode.Combine( this.Id, this.Name );
        }
    }
}
