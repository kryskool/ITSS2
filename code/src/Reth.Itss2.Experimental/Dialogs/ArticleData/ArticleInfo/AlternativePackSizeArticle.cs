using System;

using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols;

namespace Reth.Itss2.Experimental.Dialogs.ArticleData.ArticleInfo
{
    public class AlternativePackSizeArticle:Article, IEquatable<AlternativePackSizeArticle>
    {
        public static bool operator==( AlternativePackSizeArticle left, AlternativePackSizeArticle right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( AlternativePackSizeArticle left, AlternativePackSizeArticle right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( AlternativePackSizeArticle left, AlternativePackSizeArticle right )
		{
            return Article.Equals( left, right );
		}

        public AlternativePackSizeArticle( ArticleId id )
        :
            base( id )
        {
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as AlternativePackSizeArticle );
		}
		
        public bool Equals( AlternativePackSizeArticle other )
		{
            return AlternativePackSizeArticle.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}