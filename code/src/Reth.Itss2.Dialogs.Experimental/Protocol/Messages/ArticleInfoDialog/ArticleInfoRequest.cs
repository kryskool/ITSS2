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

using ArticleInfoRequestArticle = Reth.Itss2.Dialogs.Standard.Protocol.Messages.ArticleInfoDialog.ArticleInfoRequestArticle;

namespace Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ArticleInfoDialog
{
    public class ArticleInfoRequest:Standard.Protocol.Messages.ArticleInfoDialog.ArticleInfoRequest, IEquatable<ArticleInfoRequest>
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
            bool result = Standard.Protocol.Messages.ArticleInfoDialog.ArticleInfoRequest.Equals( left, right );

            result &= ( result ? EqualityComparer<bool?>.Default.Equals( left?.IncludeCrossSellingArticles, right?.IncludeCrossSellingArticles ) : false );
            result &= ( result ? EqualityComparer<bool?>.Default.Equals( left?.IncludeAlternativeArticles, right?.IncludeAlternativeArticles ) : false );
            result &= ( result ? EqualityComparer<bool?>.Default.Equals( left?.IncludeAlternativePackSizeArticles, right?.IncludeAlternativePackSizeArticles ) : false );
            
            return result;
		}

        public ArticleInfoRequest(  MessageId id,
                                    SubscriberId source,
                                    SubscriberId destination,
                                    IEnumerable<ArticleInfoRequestArticle> articles )
        :
            base( id, source, destination, articles )
        {
        }

        public ArticleInfoRequest(  MessageId id,
                                    SubscriberId source,
                                    SubscriberId destination,
                                    IEnumerable<ArticleInfoRequestArticle> articles,
                                    bool? includeCrossSellingArticles,
                                    bool? includeAlternativeArticles,
                                    bool? includeAlternativePackSizeArticles    )
        :
            base( id, source, destination, articles )
        {
            this.IncludeCrossSellingArticles = includeCrossSellingArticles;
            this.IncludeAlternativeArticles = includeAlternativeArticles;
            this.IncludeAlternativePackSizeArticles = includeAlternativePackSizeArticles;
        }

        public bool? IncludeCrossSellingArticles
        {
            get;
        }

        public bool? IncludeAlternativeArticles
        {
            get;
        }

        public bool? IncludeAlternativePackSizeArticles
        {
            get;
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
