using System;
using System.Collections.Generic;

using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols;
using Reth.Protocols.Extensions.Int32Extensions;
using Reth.Protocols.Extensions.ListExtensions;

namespace Reth.Itss2.Experimental.Dialogs.ArticleData.ArticleInfo
{
    public class ArticleInfoResponseArticle:Reth.Itss2.Standard.Dialogs.ArticleData.ArticleInfo.ArticleInfoResponseArticle, IEquatable<ArticleInfoResponseArticle>
    {
        public static bool operator==( ArticleInfoResponseArticle left, ArticleInfoResponseArticle right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( ArticleInfoResponseArticle left, ArticleInfoResponseArticle right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( ArticleInfoResponseArticle left, ArticleInfoResponseArticle right )
		{
            bool result = Reth.Itss2.Standard.Dialogs.ArticleData.ArticleInfo.ArticleInfoResponseArticle.Equals( left, right );

            if( result == true )
            {
                result &= Nullable.Equals( left.Quantity, right.Quantity );
                result &= left.Tags.ElementsEqual<ArticleTag>( right.Tags );
                result &= left.AlternativeArticles.ElementsEqual<AlternativeArticle>( right.AlternativeArticles );
                result &= left.AlternativePackSizeArticles.ElementsEqual<AlternativePackSizeArticle>( right.AlternativePackSizeArticles );
                result &= left.CrossSellingArticles.ElementsEqual<CrossSellingArticle>( right.CrossSellingArticles );
            }

            return result;
		}

        public ArticleInfoResponseArticle( ArticleId id )
        :
            base( id )
        {
        }

        public ArticleInfoResponseArticle(  ArticleId id,
                                            String name,
                                            String dosageForm,
                                            String packingUnit,
                                            Nullable<bool> requiresFridge,
                                            Nullable<int> maxSubItemQuantity,
                                            Nullable<int> quantity,
                                            PackDate serialNumberSinceExpiryDate,
                                            IEnumerable<ArticleTag> tags,
                                            IEnumerable<AlternativeArticle> alternativeArticles,
                                            IEnumerable<AlternativePackSizeArticle> alternativePackSizeArticles,
                                            IEnumerable<CrossSellingArticle> crossSellingArticles   )
        :
            base(   id,
                    name,
                    dosageForm,
                    packingUnit,
                    requiresFridge,
                    maxSubItemQuantity,
                    serialNumberSinceExpiryDate )
        {
            quantity?.ThrowIfNotPositive();

            this.Quantity = quantity;

            if( !( tags is null ) )
            {
                this.Tags.AddRange( tags );
            }

            if( !( alternativeArticles is null ) )
            {
                this.AlternativeArticles.AddRange( alternativeArticles );
            }

            if( !( alternativePackSizeArticles is null ) )
            {
                this.AlternativePackSizeArticles.AddRange( alternativePackSizeArticles );
            }

            if( !( crossSellingArticles is null ) )
            {
                this.CrossSellingArticles.AddRange( crossSellingArticles );
            }
        }

        public Nullable<int> Quantity
        {
            get;
        }

        private List<ArticleTag> Tags
        {
            get;
        } = new List<ArticleTag>();

        private List<AlternativeArticle> AlternativeArticles
        {
            get;
        } = new List<AlternativeArticle>();

        private List<AlternativePackSizeArticle> AlternativePackSizeArticles
        {
            get;
        } = new List<AlternativePackSizeArticle>();

        private List<CrossSellingArticle> CrossSellingArticles
        {
            get;
        } = new List<CrossSellingArticle>();
        
        public ArticleTag[] GetTags()
        {
            return this.Tags.ToArray();
        }

        public AlternativeArticle[] GetAlternativeArticles()
        {
            return this.AlternativeArticles.ToArray();
        }

        public AlternativePackSizeArticle[] GetAlternativePackSizeArticle()
        {
            return this.AlternativePackSizeArticles.ToArray();
        }

        public CrossSellingArticle[] GetCrossSellingArticles()
        {
            return this.CrossSellingArticles.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as ArticleInfoResponseArticle );
		}
		
        public bool Equals( ArticleInfoResponseArticle other )
		{
            return ArticleInfoResponseArticle.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}