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

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages.OutputInfoDialog
{
    public class OutputInfoArticle:IEquatable<OutputInfoArticle>
    {
        public static bool operator==( OutputInfoArticle? left, OutputInfoArticle? right )
		{
            return OutputInfoArticle.Equals( left, right );
		}
		
		public static bool operator!=( OutputInfoArticle? left, OutputInfoArticle? right )
		{
			return !( OutputInfoArticle.Equals( left, right ) );
		}

        public static bool Equals( OutputInfoArticle? left, OutputInfoArticle? right )
		{
            bool result = ArticleId.Equals( left?.Id, right?.Id );
            
            result &= ( result ? ( left?.Packs.SequenceEqual( right?.Packs ) ).GetValueOrDefault() : false );
            
            return result;
		}

        public OutputInfoArticle( ArticleId id )
        {
            this.Id = id;
        }

        public OutputInfoArticle(   ArticleId id,
                                    IEnumerable<OutputInfoPack>? packs  )
        {
            this.Id = id;

            if( packs is not null )
            {
                this.Packs.AddRange( packs );
            }
        }

        public ArticleId Id
        {
            get;
        }

        private List<OutputInfoPack> Packs
        {
            get;
        } = new List<OutputInfoPack>();
        
        public OutputInfoPack[] GetPacks()
        {
            return this.Packs.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as OutputInfoArticle );
		}
		
        public bool Equals( OutputInfoArticle? other )
		{
            return OutputInfoArticle.Equals( this, other );
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
