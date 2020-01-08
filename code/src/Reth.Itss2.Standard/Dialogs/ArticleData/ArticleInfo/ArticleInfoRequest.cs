using System;
using System.Collections.Generic;

using Reth.Protocols;
using Reth.Protocols.Extensions.ListExtensions;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.ArticleData.ArticleInfo
{
    public class ArticleInfoRequest:TraceableRequest, IEquatable<ArticleInfoRequest>
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
            bool result = TraceableRequest.Equals( left, right );

            if( result == true )
            {
                result &= left.Articles.ElementsEqual( right.Articles );
            }

            return result;
		}

        public ArticleInfoRequest(  IMessageId id,
                                    SubscriberId source,
                                    SubscriberId destination,
                                    IEnumerable<ArticleInfoRequestArticle> articles    )
        :
            base( DialogName.ArticleInfo, id, source, destination )
        {
            if( !( articles is null ) )
            {
                this.Articles.AddRange( articles );
            }
        }

        private List<ArticleInfoRequestArticle> Articles
        {
            get;
        } = new List<ArticleInfoRequestArticle>();
        
        public ArticleInfoRequestArticle[] GetArticles()
        {
            return this.Articles.ToArray();
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

        public override void Dispatch( IMessageDispatcher dispatcher )
        {
            dispatcher.ThrowIfNull();
            dispatcher.Dispatch( this );
        }
    }
}