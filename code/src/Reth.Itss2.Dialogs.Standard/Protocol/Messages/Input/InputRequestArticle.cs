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

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages.Input
{
    public class InputRequestArticle:IEquatable<InputRequestArticle>
    {
        public static bool operator==( InputRequestArticle? left, InputRequestArticle? right )
		{
            return InputRequestArticle.Equals( left, right );
		}
		
		public static bool operator!=( InputRequestArticle? left, InputRequestArticle? right )
		{
			return !( InputRequestArticle.Equals( left, right ) );
		}

        public static bool Equals( InputRequestArticle? left, InputRequestArticle? right )
		{
            bool result = ArticleId.Equals( left?.Id, right?.Id );
            
            result &= ( result ? String.Equals( left?.FmdId, right?.FmdId, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? ( left?.Packs.SequenceEqual( right?.Packs ) ).GetValueOrDefault() : false );
            
            return result;
		}

        public InputRequestArticle()
        {
        }

        public InputRequestArticle( ArticleId? id,
                                    String? fmdId,
                                    IEnumerable<InputRequestPack>? packs  )
        {
            this.Id = id;
            this.FmdId = fmdId;

            if( packs is not null )
            {
                this.Packs.AddRange( packs );
            }
        }

        public ArticleId? Id
        {
            get;
        }

        public String? FmdId
        {
            get;
        }

        private List<InputRequestPack> Packs
        {
            get;
        } = new List<InputRequestPack>();
        
        public InputRequestPack[] GetPacks()
        {
            return this.Packs.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as InputRequestArticle );
		}
		
        public bool Equals( InputRequestArticle? other )
		{
            return InputRequestArticle.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return HashCode.Combine( this.Id, this.FmdId );
        }
    }
}
