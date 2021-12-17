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

namespace Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ArticleInfo
{
    public class ArticleInfoResponseArticle:Standard.Protocol.Messages.ArticleInfo.ArticleInfoResponseArticle, IEquatable<ArticleInfoResponseArticle>
    {
        public static bool operator==( ArticleInfoResponseArticle? left, ArticleInfoResponseArticle? right )
		{
            return ArticleInfoResponseArticle.Equals( left, right );
		}
		
		public static bool operator!=( ArticleInfoResponseArticle? left, ArticleInfoResponseArticle? right )
		{
			return !( ArticleInfoResponseArticle.Equals( left, right ) );
		}

        public static bool Equals( ArticleInfoResponseArticle? left, ArticleInfoResponseArticle? right )
		{
            bool result = Standard.Protocol.Messages.ArticleInfo.ArticleInfoResponseArticle.Equals( left, right );
            
            result &= ( result ? ( left?.ArticleTags.SequenceEqual( right?.ArticleTags ) ).GetValueOrDefault() : false );
            result &= ( result ? ( left?.CrossSellingArticles.SequenceEqual( right?.CrossSellingArticles ) ).GetValueOrDefault() : false );
            result &= ( result ? ( left?.AlternativeArticles.SequenceEqual( right?.AlternativeArticles ) ).GetValueOrDefault() : false );
            result &= ( result ? ( left?.AlternativePackSizeArticles.SequenceEqual( right?.AlternativePackSizeArticles ) ).GetValueOrDefault() : false );
                        
            return result;
		}

        public ArticleInfoResponseArticle( ArticleId id )
        :
            base( id )
        {
        }

        public ArticleInfoResponseArticle(  ArticleId id,
                                            String? name,
                                            String? dosageForm,
                                            String? packagingUnit,
                                            bool? requiresFridge,
                                            int? maxSubItemQuantity,
                                            PackDate? serialNumberSinceExpiryDate,
                                            IEnumerable<ArticleTag>? articleTags,
                                            IEnumerable<CrossSellingArticle>? crossSellingArticles,
                                            IEnumerable<AlternativeArticle>? alternativeArticles,
                                            IEnumerable<AlternativePackSizeArticle>? alternativePackSizeArticles    )
        :
            base(   id,
                    name,
                    dosageForm,
                    packagingUnit,
                    requiresFridge,
                    maxSubItemQuantity,
                    serialNumberSinceExpiryDate )
        {
            if( articleTags is not null )
            {
                this.ArticleTags.AddRange( articleTags );
            }

            if( crossSellingArticles is not null )
            {
                this.CrossSellingArticles.AddRange( crossSellingArticles );
            }

            if( alternativeArticles is not null )
            {
                this.AlternativeArticles.AddRange( alternativeArticles );
            }

            if( alternativePackSizeArticles is not null )
            {
                this.AlternativePackSizeArticles.AddRange( alternativePackSizeArticles );
            }
        }

        private List<ArticleTag> ArticleTags
        {
            get;
        } = new List<ArticleTag>();

        private List<CrossSellingArticle> CrossSellingArticles
        {
            get;
        } = new List<CrossSellingArticle>();

        private List<AlternativeArticle> AlternativeArticles
        {
            get;
        } = new List<AlternativeArticle>();

        private List<AlternativePackSizeArticle> AlternativePackSizeArticles
        {
            get;
        } = new List<AlternativePackSizeArticle>();

        public ArticleTag[] GetArticleTags()
        {
            return this.ArticleTags.ToArray();
        }

        public CrossSellingArticle[] GetCrossSellingArticles()
        {
            return this.CrossSellingArticles.ToArray();
        }

        public AlternativeArticle[] GetAlternativeArticles()
        {
            return this.AlternativeArticles.ToArray();
        }

        public AlternativePackSizeArticle[] GetAlternativePackSizeArticles()
        {
            return this.AlternativePackSizeArticles.ToArray();
        }

        public override bool Equals( Object? obj )
		{
			return this.Equals( obj as ArticleInfoResponseArticle );
		}
		
        public bool Equals( ArticleInfoResponseArticle? other )
		{
            return ArticleInfoResponseArticle.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override String? ToString()
        {
            return this.Id.ToString();
        }
    }
}
