using System;

using Reth.Protocols;

namespace Reth.Itss2.Experimental.Dialogs.ArticleData.ArticleInfo
{
    public class ArticleTag:IEquatable<ArticleTag>
    {
        public static bool operator==( ArticleTag left, ArticleTag right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( ArticleTag left, ArticleTag right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( ArticleTag left, ArticleTag right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        return ( left.Tag == right.Tag );
                                                    }   );
		}

        public ArticleTag( ArticleTagValue tag )
        {
            this.Tag = tag;
        }

        public ArticleTagValue Tag
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as ArticleTag );
		}
		
        public bool Equals( ArticleTag other )
		{
            return ArticleTag.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return this.Tag.GetHashCode();
        }

        public override String ToString()
        {
            return this.Tag.ToString();
        }
    }
}