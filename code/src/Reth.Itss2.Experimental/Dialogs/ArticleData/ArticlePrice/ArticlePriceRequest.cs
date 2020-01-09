using System;

using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Experimental.Dialogs.ArticleData.ArticlePrice
{
    public class ArticlePriceRequest:TraceableRequest, IEquatable<ArticlePriceRequest>
    {
        public static bool operator==( ArticlePriceRequest left, ArticlePriceRequest right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( ArticlePriceRequest left, ArticlePriceRequest right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( ArticlePriceRequest left, ArticlePriceRequest right )
		{
            bool result = TraceableRequest.Equals( left, right );

            if( result == true )
            {
                result &= ArticlePriceRequestArticle.Equals( left.Article, right.Article );
                result &= Iso4217Code.Equals( left.Currency, right.Currency );
            }

            return result;
		}

        public ArticlePriceRequest( IMessageId id,
                                    SubscriberId source,
                                    SubscriberId destination,
                                    ArticlePriceRequestArticle article   )
        :
            base( DialogName.ArticlePrice, id, source, destination )
        {
            article.ThrowIfNull();

            this.Article = article;
        }

        public ArticlePriceRequest( IMessageId id,
                                    SubscriberId source,
                                    SubscriberId destination,
                                    ArticlePriceRequestArticle article,
                                    Iso4217Code currency   )
        :
            base( DialogName.ArticlePrice, id, source, destination )
        {
            article.ThrowIfNull();

            this.Article = article;
            this.Currency = currency;
        }

        public ArticlePriceRequestArticle Article
        {
            get;
        }

        public Iso4217Code Currency
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as ArticlePriceRequest );
		}
		
		public bool Equals( ArticlePriceRequest other )
		{
            return ArticlePriceRequest.Equals( this, other );
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