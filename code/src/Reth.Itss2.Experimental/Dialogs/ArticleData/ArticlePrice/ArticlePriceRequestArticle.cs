using System;

using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Experimental.Dialogs.ArticleData.ArticlePrice
{
    public class ArticlePriceRequestArticle:IEquatable<ArticlePriceRequestArticle>
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
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        return left.Id.Equals( right.Id );
                                                    }   );
		}

        public ArticlePriceRequestArticle( ArticleId id )
        {
            id.ThrowIfNull();

            this.Id = id;
        }

        public ArticleId Id
        {
            get;
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
            return this.Id.GetHashCode();
        }

        public override String ToString()
        {
            return this.Id.ToString();
        }
    }
}