using System;

using Reth.Protocols;
using Reth.Protocols.Extensions.Int32Extensions;

namespace Reth.Itss2.Standard.Dialogs.ArticleData.ArticleInfo
{
    public class ArticleInfoRequestArticle:Article, IEquatable<ArticleInfoRequestArticle>
    {
        public static bool operator==( ArticleInfoRequestArticle left, ArticleInfoRequestArticle right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( ArticleInfoRequestArticle left, ArticleInfoRequestArticle right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( ArticleInfoRequestArticle left, ArticleInfoRequestArticle right )
		{
            bool result = Article.Equals( left, right );

            if( result == true )
            {
                result &= Nullable.Equals( left.Depth, right.Depth );
                result &= Nullable.Equals( left.Width, right.Width );
                result &= Nullable.Equals( left.Height, right.Height );
                result &= Nullable.Equals( left.Weight, right.Weight );
            }

            return result;
		}

        private Nullable<int> depth;
        private Nullable<int> width;
        private Nullable<int> height;
        private Nullable<int> weight;

        public ArticleInfoRequestArticle( ArticleId id )
        :
            base( id )
        {
        }

        public ArticleInfoRequestArticle(   ArticleId id,
                                            Nullable<int> depth,
                                            Nullable<int> width,
                                            Nullable<int> height,
                                            Nullable<int> weight   )
        :
            base( id )
        {
            this.Depth = depth;
            this.Width = width;
            this.Height = height;
            this.Weight = weight;            
        }

        public Nullable<int> Depth
        {
            get{ return this.depth; }
            
            private set
            {
                value?.ThrowIfNegative();

                this.depth = value;
            }
        }

        public Nullable<int> Width
        {
            get{ return this.width; }
            
            private set
            {
                value?.ThrowIfNegative();

                this.width = value;
            }
        }

        public Nullable<int> Height
        {
            get{ return this.height; }
            
            private set
            {
                value?.ThrowIfNegative();

                this.height = value;
            }
        }

        public Nullable<int> Weight
        {
            get{ return this.weight; }
            
            private set
            {
                value?.ThrowIfNegative();

                this.weight = value;
            }
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as ArticleInfoRequestArticle );
		}
		
        public bool Equals( ArticleInfoRequestArticle other )
		{
            return ArticleInfoRequestArticle.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}