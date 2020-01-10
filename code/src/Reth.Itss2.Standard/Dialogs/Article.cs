using System;

using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs
{
    public abstract class Article:IEquatable<Article>
    {
        public static bool operator==( Article left, Article right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( Article left, Article right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( Article left, Article right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        return left.Id.Equals( right.Id );
                                                    }   );
		}

        protected Article( ArticleId id )
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
			return this.Equals( obj as Article );
		}
		
        public bool Equals( Article other )
		{
            return Article.Equals( this, other );
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