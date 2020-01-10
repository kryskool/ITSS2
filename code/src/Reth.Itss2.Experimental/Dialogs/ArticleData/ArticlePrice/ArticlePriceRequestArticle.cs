using System;

using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols;

namespace Reth.Itss2.Experimental.Dialogs.ArticleData.ArticlePrice
{
    public class ArticlePriceRequestArticle:Article, IEquatable<ArticlePriceRequestArticle>
    {
        public static bool operator==( ArticlePriceRequestArticle left, ArticlePriceRequestArticle right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( ArticlePriceRequestArticle left, ArticlePriceRequestArticle right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( ArticlePriceRequestArticle left, ArticlePriceRequestArticle right )
		{
            return Article.Equals( left, right );
		}

        public ArticlePriceRequestArticle( ArticleId id )
        :
            base( id )
        {
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as ArticlePriceRequestArticle );
		}
		
        public bool Equals( ArticlePriceRequestArticle other )
		{
            return ArticlePriceRequestArticle.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}