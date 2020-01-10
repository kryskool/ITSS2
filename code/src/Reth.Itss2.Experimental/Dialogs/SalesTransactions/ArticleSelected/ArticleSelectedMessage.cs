using System;

using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols;
using Reth.Protocols.Extensions.ListExtensions;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Experimental.Dialogs.SalesTransactions.ArticleSelected
{
    public class ArticleSelectedMessage:TraceableMessage, IEquatable<ArticleSelectedMessage>
    {
        public static bool operator==( ArticleSelectedMessage left, ArticleSelectedMessage right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( ArticleSelectedMessage left, ArticleSelectedMessage right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( ArticleSelectedMessage left, ArticleSelectedMessage right )
		{
            bool result = TraceableMessage.Equals( left, right );

            if( result == true )
            {
                result &= ArticleSelectedArticle.Equals( left.Article, right.Article );
            }

            return result;
		}

        public ArticleSelectedMessage(  IMessageId id,
                                        SubscriberId source,
                                        SubscriberId destination,
                                        ArticleSelectedArticle article   )
        :
            base( DialogName.ArticleSelected, id, source, destination )
        {
            article.ThrowIfNull();

            this.Article = article;
        }

        public ArticleSelectedArticle Article
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as ArticleSelectedMessage );
		}
		
		public bool Equals( ArticleSelectedMessage other )
		{
            return ArticleSelectedMessage.Equals( this, other );
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