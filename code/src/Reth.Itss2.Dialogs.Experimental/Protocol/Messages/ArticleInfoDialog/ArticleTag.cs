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

namespace Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ArticleInfoDialog
{
    public class ArticleTag:IEquatable<ArticleTag>
    {
        public static bool operator==( ArticleTag? left, ArticleTag? right )
		{
            return ArticleTag.Equals( left, right );
		}
		
		public static bool operator!=( ArticleTag? left, ArticleTag? right )
		{
			return !( ArticleTag.Equals( left, right ) );
		}

        public static bool Equals( ArticleTag? left, ArticleTag? right )
		{
            return EqualityComparer<ArticleTagValue?>.Default.Equals( left?.Value, right?.Value );
		}

        public ArticleTag( ArticleTagValue value )
        {
            this.Value = value;
        }

        public ArticleTagValue Value
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as ArticleTag );
		}
		
        public bool Equals( ArticleTag? other )
		{
            return ArticleTag.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public override String ToString()
        {
            return this.Value.ToString();
        }
    }
}
