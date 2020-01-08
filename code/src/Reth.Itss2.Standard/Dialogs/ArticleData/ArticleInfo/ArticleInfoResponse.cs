using System;
using System.Collections.Generic;

using Reth.Protocols;
using Reth.Protocols.Extensions.ListExtensions;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.ArticleData.ArticleInfo
{
    public class ArticleInfoResponse:TraceableResponse, IEquatable<ArticleInfoResponse>
    {
        public static bool operator==( ArticleInfoResponse left, ArticleInfoResponse right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( ArticleInfoResponse left, ArticleInfoResponse right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( ArticleInfoResponse left, ArticleInfoResponse right )
		{
            bool result = TraceableResponse.Equals( left, right );

            if( result == true )
            {
                result &= left.Articles.ElementsEqual( right.Articles );
            }

            return result;
		}

        public ArticleInfoResponse( IMessageId id,
                                    SubscriberId source,
                                    SubscriberId destination,
                                    IEnumerable<ArticleInfoResponseArticle> articles )
        :
            base( DialogName.ArticleInfo, id, source, destination )
        {
            if( !( articles is null ) )
            {
                this.Articles.AddRange( articles );
            }
        }

        public ArticleInfoResponse( ArticleInfoRequest request,
                                    IEnumerable<ArticleInfoResponseArticle> articles )
        :
            base( request )
        {
            if( !( articles is null ) )
            {
                this.Articles.AddRange( articles );
            }
        }

        private List<ArticleInfoResponseArticle> Articles
        {
            get;
        } = new List<ArticleInfoResponseArticle>();

        public ArticleInfoResponseArticle[] GetArticles()
        {
            return this.Articles.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as ArticleInfoResponse );
		}
		
		public bool Equals( ArticleInfoResponse other )
		{
            return ArticleInfoResponse.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override void Dispatch( IMessageDispatcher dispatcher )
        {
            dispatcher.ThrowIfNull();
            dispatcher.Dispatch( this );
        }
    }
}