using System;
using System.Collections.Generic;

using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols;

namespace Reth.Itss2.Experimental.Dialogs.ArticleData.ArticleInfo
{
    public class ArticleInfoRequest:Reth.Itss2.Standard.Dialogs.ArticleData.ArticleInfo.ArticleInfoRequest, IEquatable<ArticleInfoRequest>
    {
        public static bool operator==( ArticleInfoRequest left, ArticleInfoRequest right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( ArticleInfoRequest left, ArticleInfoRequest right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( ArticleInfoRequest left, ArticleInfoRequest right )
		{
            bool result = Reth.Itss2.Standard.Dialogs.ArticleData.ArticleInfo.ArticleInfoRequest.Equals( left, right );

            if( result == true )
            {
                result &= Nullable.Equals( left.IncludeAlternativeArticles, right.IncludeAlternativeArticles );
                result &= Nullable.Equals( left.IncludeAlternativePackSizeArticles, right.IncludeAlternativePackSizeArticles );
                result &= Nullable.Equals( left.IncludeCrossSellingArticles, right.IncludeCrossSellingArticles );
            }

            return result;
		}        

        public ArticleInfoRequest(  IMessageId id,
                                    SubscriberId source,
                                    SubscriberId destination,
                                    IEnumerable<Reth.Itss2.Standard.Dialogs.ArticleData.ArticleInfo.ArticleInfoRequestArticle> articles,
                                    Nullable<bool> includeAlternativeArticles,
                                    Nullable<bool> includeAlternativePackSizeArticles,
                                    Nullable<bool> includeCrossSellingArticles  )
        :
            base( id, source, destination, articles )
        {
            this.IncludeAlternativeArticles = includeAlternativeArticles;
            this.IncludeAlternativePackSizeArticles = includeAlternativePackSizeArticles;
            this.IncludeCrossSellingArticles = includeCrossSellingArticles;
        }

        public Nullable<bool> IncludeAlternativeArticles
        {
            get;
        }

        public Nullable<bool> IncludeAlternativePackSizeArticles
        {
            get;
        }

        public Nullable<bool> IncludeCrossSellingArticles
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as ArticleInfoRequest );
		}
		
		public bool Equals( ArticleInfoRequest other )
		{
            return ArticleInfoRequest.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}