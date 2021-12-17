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

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages.ArticleInfo
{
    public class ArticleInfoResponse:SubscribedResponse, IEquatable<ArticleInfoResponse>
    {
        public static bool operator==( ArticleInfoResponse? left, ArticleInfoResponse? right )
		{
            return ArticleInfoResponse.Equals( left, right );
		}
		
		public static bool operator!=( ArticleInfoResponse? left, ArticleInfoResponse? right )
		{
			return !( ArticleInfoResponse.Equals( left, right ) );
		}

        public static bool Equals( ArticleInfoResponse? left, ArticleInfoResponse? right )
		{
            bool result = SubscribedResponse.Equals( left, right );

            result &= ( result ? ( left?.Articles.SequenceEqual( right?.Articles ) ).GetValueOrDefault() : false );

            return result;
		}

        public ArticleInfoResponse( MessageId id,
                                    SubscriberId source,
                                    SubscriberId destination   )
        :
            base( id, StandardDialogs.ArticleInfo, source, destination )
        {
        }

        public ArticleInfoResponse( MessageId id,
                                    SubscriberId source,
                                    SubscriberId destination,
                                    IEnumerable<ArticleInfoResponseArticle> articles   )
        :
            base( id, StandardDialogs.ArticleInfo, source, destination )
        {
            this.Articles.AddRange( articles );
        }

        public ArticleInfoResponse( ArticleInfoRequest request )
        :
            base( request )
        {
        }

        public ArticleInfoResponse( ArticleInfoRequest request,
                                    IEnumerable<ArticleInfoResponseArticle> articles   )
        :
            base( request )
        {
            this.Articles.AddRange( articles );
        }

        private List<ArticleInfoResponseArticle> Articles
        {
            get;
        } = new List<ArticleInfoResponseArticle>();

        public ArticleInfoResponseArticle[] GetArticles()
        {
            return this.Articles.ToArray();
        }

        public override bool Equals( Object? obj )
		{
			return this.Equals( obj as ArticleInfoResponse );
		}
		
        public bool Equals( ArticleInfoResponse? other )
		{
            return ArticleInfoResponse.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
