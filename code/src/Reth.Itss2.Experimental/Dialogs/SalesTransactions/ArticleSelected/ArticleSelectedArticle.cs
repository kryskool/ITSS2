using System;

using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols;

namespace Reth.Itss2.Experimental.Dialogs.SalesTransactions.ArticleSelected
{
    public class ArticleSelectedArticle:Article, IEquatable<ArticleSelectedArticle>
    {
        public static bool operator==( ArticleSelectedArticle left, ArticleSelectedArticle right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( ArticleSelectedArticle left, ArticleSelectedArticle right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( ArticleSelectedArticle left, ArticleSelectedArticle right )
		{
            return Article.Equals( left, right );
		}

        public ArticleSelectedArticle( ArticleId id )
        :
            base( id )
        {
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as ArticleSelectedArticle );
		}
		
        public bool Equals( ArticleSelectedArticle other )
		{
            return ArticleSelectedArticle.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}