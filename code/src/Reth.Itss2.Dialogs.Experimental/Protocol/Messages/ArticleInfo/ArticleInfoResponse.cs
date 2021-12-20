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
using Reth.Itss2.Messaging;

namespace Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ArticleInfo
{
    public class ArticleInfoResponse:Standard.Protocol.Messages.ArticleInfo.ArticleInfoResponse, IEquatable<ArticleInfoResponse>
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
            return Standard.Protocol.Messages.ArticleInfo.ArticleInfoResponse.Equals( left, right );
		}

        public ArticleInfoResponse( MessageId id,
									SubscriberId source,
                                    SubscriberId destination    )
        :
            base( id, source, destination )
        {
        }

        public ArticleInfoResponse( MessageId id,
									SubscriberId source,
                                    SubscriberId destination,
                                    IEnumerable<ArticleInfoResponseArticle> articles  )
        :
            base( id, source, destination, articles )
        {
        }

        public ArticleInfoResponse( ArticleInfoRequest request )
        :
            base( request )
        {
        }

        public ArticleInfoResponse( ArticleInfoRequest request,
                                    IEnumerable<ArticleInfoResponseArticle> articles   )
        :
            base( request, articles )
        {
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
