using System;

using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols;

namespace Reth.Itss2.Experimental.Dialogs.ArticleData.ArticleInfo
{
    public class CrossSellingArticle:Article, IEquatable<CrossSellingArticle>
    {
        public static bool operator==( CrossSellingArticle left, CrossSellingArticle right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( CrossSellingArticle left, CrossSellingArticle right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( CrossSellingArticle left, CrossSellingArticle right )
		{
            return Article.Equals( left, right );
		}

        public CrossSellingArticle( ArticleId id )
        :
            base( id )
        {
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as CrossSellingArticle );
		}
		
        public bool Equals( CrossSellingArticle other )
		{
            return CrossSellingArticle.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}