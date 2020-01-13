using System;

using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols;

namespace Reth.Itss2.Experimental.Dialogs.ArticleData.ArticleInfo
{
    public class AlternativeArticle:Article, IEquatable<AlternativeArticle>
    {
        public static bool operator==( AlternativeArticle left, AlternativeArticle right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( AlternativeArticle left, AlternativeArticle right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( AlternativeArticle left, AlternativeArticle right )
		{
            return Article.Equals( left, right );
		}

        public AlternativeArticle( ArticleId id )
        :
            base( id )
        {
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as AlternativeArticle );
		}
		
        public bool Equals( AlternativeArticle other )
		{
            return AlternativeArticle.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}