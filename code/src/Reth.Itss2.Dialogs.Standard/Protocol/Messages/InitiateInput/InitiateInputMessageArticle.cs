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
    public class InitiateInputMessageArticle:IEquatable<InitiateInputMessageArticle>
    {
        public static bool operator==( InitiateInputMessageArticle? left, InitiateInputMessageArticle? right )
		{
            return InitiateInputMessageArticle.Equals( left, right );
		}
		
		public static bool operator!=( InitiateInputMessageArticle? left, InitiateInputMessageArticle? right )
		{
			return !( InitiateInputMessageArticle.Equals( left, right ) );
		}

        public static bool Equals( InitiateInputMessageArticle? left, InitiateInputMessageArticle? right )
		{
            bool result = ArticleId.Equals( left?.Id, right?.Id );
            
            result &= ( result ? String.Equals( left?.Name, right?.Name, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? String.Equals( left?.DosageForm, right?.DosageForm, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? String.Equals( left?.PackagingUnit, right?.PackagingUnit, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.MaxSubItemQuantity, right?.MaxSubItemQuantity ) : false );
            result &= ( result ? ( left?.Packs.SequenceEqual( right?.Packs ) ).GetValueOrDefault() : false );
            
            return result;
		}

        public InitiateInputMessageArticle( ArticleId id )
        {
            this.Id = id;
        }

        public InitiateInputMessageArticle( ArticleId id,
                                            String? name,
                                            String? dosageForm,
                                            String? packagingUnit,
                                            int? maxSubItemQuantity,
                                            IEnumerable<InitiateInputMessagePack>? packs    )
        {
            maxSubItemQuantity?.ThrowIfNegative();

            this.Id = id;
            this.Name = name;
            this.DosageForm = dosageForm;
            this.PackagingUnit = packagingUnit;
            this.MaxSubItemQuantity = maxSubItemQuantity;

            if( packs is not null )
            {
                this.Packs.AddRange( packs );
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

        public int? MaxSubItemQuantity
        {
            get;
        }

        private List<InitiateInputMessagePack> Packs
        {
            get;
        } = new List<InitiateInputMessagePack>();
        
        public InitiateInputMessagePack[] GetPacks()
        {
            return this.Packs.ToArray();
        }

        public override bool Equals( Object? obj )
		{
			return this.Equals( obj as InitiateInputMessageArticle );
		}
		
        public bool Equals( InitiateInputMessageArticle? other )
		{
            return InitiateInputMessageArticle.Equals( this, other );
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
