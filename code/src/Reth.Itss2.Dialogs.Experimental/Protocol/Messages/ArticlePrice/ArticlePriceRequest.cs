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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages;

namespace Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ArticlePrice
{
    public class ArticlePriceRequest:SubscribedRequest, IEquatable<ArticlePriceRequest>
    {
        public static bool operator==( ArticlePriceRequest? left, ArticlePriceRequest? right )
		{
            return ArticlePriceRequest.Equals( left, right );
		}
		
		public static bool operator!=( ArticlePriceRequest? left, ArticlePriceRequest? right )
		{
			return !( ArticlePriceRequest.Equals( left, right ) );
		}

        public static bool Equals( ArticlePriceRequest? left, ArticlePriceRequest? right )
		{
            bool result = SubscribedRequest.Equals( left, right );

            result &= ( result ? ( left?.Articles.SequenceEqual( right?.Articles ) ).GetValueOrDefault() : false );
            result &= ( result ? Iso4217Code.Equals( left?.Currency, right?.Currency ) : false );

            return result;
		}

        public ArticlePriceRequest( MessageId id,
									SubscriberId source,
                                    SubscriberId destination,
                                    IEnumerable<ArticlePriceRequestArticle> articles   )
        :
            this( id, source, destination, articles, null )
        {
        }

        public ArticlePriceRequest( MessageId id,
									SubscriberId source,
                                    SubscriberId destination,
                                    IEnumerable<ArticlePriceRequestArticle> articles,
                                    Iso4217Code? currency   )
        :
            base( id, Dialogs.ArticlePrice, source, destination )
        {
            this.Articles.AddRange( articles );

            this.Currency = currency;
        }

        public Iso4217Code? Currency
        {
            get;
        }

        private List<ArticlePriceRequestArticle> Articles
        {
            get;
        } = new List<ArticlePriceRequestArticle>();

        public ArticlePriceRequestArticle[] GetArticles()
        {
            return this.Articles.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as ArticlePriceRequest );
		}
		
        public bool Equals( ArticlePriceRequest? other )
		{
            return ArticlePriceRequest.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
