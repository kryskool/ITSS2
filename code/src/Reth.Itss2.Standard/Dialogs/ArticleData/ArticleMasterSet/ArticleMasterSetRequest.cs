using System;
using System.Collections.Generic;

using Reth.Protocols;
using Reth.Protocols.Extensions.ListExtensions;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.ArticleData.ArticleMasterSet
{
    public class ArticleMasterSetRequest:TraceableRequest, IEquatable<ArticleMasterSetRequest>
    {
        public static bool operator==( ArticleMasterSetRequest left, ArticleMasterSetRequest right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( ArticleMasterSetRequest left, ArticleMasterSetRequest right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( ArticleMasterSetRequest left, ArticleMasterSetRequest right )
		{
            bool result = TraceableRequest.Equals( left, right );

            if( result == true )
            {
                result &= left.Articles.ElementsEqual( right.Articles );
            }

            return result;
		}        

        public ArticleMasterSetRequest( IMessageId id,
                                        SubscriberId source,
                                        SubscriberId destination,
                                        IEnumerable<ArticleMasterSetArticle> articles    )
        :
            base( DialogName.ArticleMasterSet, id, source, destination )
        {
            if( !( articles is null ) )
            {
                this.Articles.AddRange( articles );
            }
        }

        private List<ArticleMasterSetArticle> Articles
        {
            get;
        } = new List<ArticleMasterSetArticle>();
        
        public ArticleMasterSetArticle[] GetArticles()
        {
            return this.Articles.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as ArticleMasterSetRequest );
		}
		
		public bool Equals( ArticleMasterSetRequest other )
		{
            return ArticleMasterSetRequest.Equals( this, other );
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