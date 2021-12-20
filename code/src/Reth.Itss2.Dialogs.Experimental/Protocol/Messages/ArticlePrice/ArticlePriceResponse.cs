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

namespace Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ArticlePrice
{
    public class ArticlePriceResponse:SubscribedResponse, IEquatable<ArticlePriceResponse>
    {
        public static bool operator==( ArticlePriceResponse? left, ArticlePriceResponse? right )
		{
            return ArticlePriceResponse.Equals( left, right );
		}
		
		public static bool operator!=( ArticlePriceResponse? left, ArticlePriceResponse? right )
		{
			return !( ArticlePriceResponse.Equals( left, right ) );
		}

        public static bool Equals( ArticlePriceResponse? left, ArticlePriceResponse? right )
		{
            bool result = SubscribedResponse.Equals( left, right );

            result &= ( result ? ( left?.Articles.SequenceEqual( right?.Articles ) ).GetValueOrDefault() : false );
            result &= ( result ? Iso4217Code.Equals( left?.Currency, right?.Currency ) : false );

            return result;
		}

        public ArticlePriceResponse(    MessageId id,
									    SubscriberId source,
                                        SubscriberId destination,
                                        IEnumerable<ArticlePriceResponseArticle> articles  )
        :
            this( id, source, destination, articles, currency:null )
        {
        }

        public ArticlePriceResponse(    MessageId id,
									    SubscriberId source,
                                        SubscriberId destination,
                                        IEnumerable<ArticlePriceResponseArticle> articles,
                                        Iso4217Code? currency    )
        :
            base( id, ExperimentalDialogs.ArticlePrice, source, destination )
        {
            this.Articles.AddRange( articles );

            this.Currency = currency;
        }

        public ArticlePriceResponse(    ArticlePriceRequest request,
                                        IEnumerable<ArticlePriceResponseArticle> articles )
        :
            this( request, articles, currency:null )
        {
        }

        public ArticlePriceResponse(    ArticlePriceRequest request,
                                        IEnumerable<ArticlePriceResponseArticle> articles,
                                        Iso4217Code? currency    )
        :
            base( request )
        {
            this.Articles.AddRange( articles );
            
            this.Currency = currency;
        }

        public Iso4217Code? Currency
        {
            get;
        }

        private List<ArticlePriceResponseArticle> Articles
        {
            get;
        } = new List<ArticlePriceResponseArticle>();

        public ArticlePriceResponseArticle[] GetArticles()
        {
            return this.Articles.ToArray();
        }

        public override bool Equals( Object? obj )
		{
			return this.Equals( obj as ArticlePriceResponse );
		}
		
        public bool Equals( ArticlePriceResponse? other )
		{
            return ArticlePriceResponse.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
