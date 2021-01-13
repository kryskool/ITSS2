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

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages.ArticleInfoDialog
{
    public class ArticleInfoRequest:SubscribedRequest, IEquatable<ArticleInfoRequest>
    {
        public static bool operator==( ArticleInfoRequest? left, ArticleInfoRequest? right )
		{
            return ArticleInfoRequest.Equals( left, right );
		}
		
		public static bool operator!=( ArticleInfoRequest? left, ArticleInfoRequest? right )
		{
			return !( ArticleInfoRequest.Equals( left, right ) );
		}

        public static bool Equals( ArticleInfoRequest? left, ArticleInfoRequest? right )
		{
            bool result = SubscribedRequest.Equals( left, right );

            result &= ( result ? ( left?.Articles.SequenceEqual( right?.Articles ) ).GetValueOrDefault() : false );

            return result;
		}

        public ArticleInfoRequest(  MessageId id,
									SubscriberId source,
                                    SubscriberId destination    )
        :
            base( id, source, destination )
        {
        }

        public ArticleInfoRequest(  MessageId id,
									SubscriberId source,
                                    SubscriberId destination,
                                    IEnumerable<ArticleInfoRequestArticle>? articles   )
        :
            base( id, source, destination )
        {
            if( articles is not null )
            {
                this.Articles.AddRange( articles );
            }
        }

        private List<ArticleInfoRequestArticle> Articles
        {
            get;
        } = new List<ArticleInfoRequestArticle>();

        public ArticleInfoRequestArticle[] GetArticles()
        {
            return this.Articles.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as ArticleInfoRequest );
		}
		
        public bool Equals( ArticleInfoRequest? other )
		{
            return ArticleInfoRequest.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
