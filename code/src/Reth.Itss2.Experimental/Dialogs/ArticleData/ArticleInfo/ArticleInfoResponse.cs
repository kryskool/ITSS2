using System;
using System.Collections.Generic;

using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Experimental.Dialogs.ArticleData.ArticleInfo
{
    public class ArticleInfoResponse:Reth.Itss2.Standard.Dialogs.ArticleData.ArticleInfo.ArticleInfoResponse, IEquatable<ArticleInfoResponse>
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
            return Reth.Itss2.Standard.Dialogs.ArticleData.ArticleInfo.ArticleInfoResponse.Equals( left, right );
		}

        public ArticleInfoResponse( IMessageId id,
                                    SubscriberId source,
                                    SubscriberId destination,
                                    IEnumerable<ArticleInfoResponseArticle> articles  )
        :
            base( id, source, destination, articles )
        {
        }

        public ArticleInfoResponse( ArticleInfoRequest request,
                                    IEnumerable<ArticleInfoResponseArticle> articles  )
        :
            base( request, articles )
        {
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