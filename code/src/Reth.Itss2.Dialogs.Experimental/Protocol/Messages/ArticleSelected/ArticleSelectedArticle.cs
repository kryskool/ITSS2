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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages;

namespace Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ArticleSelected
{
    public class ArticleSelectedArticle:IEquatable<ArticleSelectedArticle>
    {
        public static bool operator==( ArticleSelectedArticle? left, ArticleSelectedArticle? right )
		{
            return ArticleSelectedArticle.Equals( left, right );
		}
		
		public static bool operator!=( ArticleSelectedArticle? left, ArticleSelectedArticle? right )
		{
			return !( ArticleSelectedArticle.Equals( left, right ) );
		}

        public static bool Equals( ArticleSelectedArticle? left, ArticleSelectedArticle? right )
		{
            return ArticleId.Equals( left?.Id, right?.Id );
		}

        public ArticleSelectedArticle( ArticleId id )
        {
            this.Id = id;
        }

        public ArticleId Id
        {
            get;
        }

        public override bool Equals( Object? obj )
		{
			return this.Equals( obj as ArticleSelectedArticle );
		}
		
        public bool Equals( ArticleSelectedArticle? other )
		{
            return ArticleSelectedArticle.Equals( this, other );
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
