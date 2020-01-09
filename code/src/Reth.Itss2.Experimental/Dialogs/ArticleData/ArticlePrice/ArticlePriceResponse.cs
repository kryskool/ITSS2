using System;

using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Experimental.Dialogs.ArticleData.ArticlePrice
{
    public class ArticlePriceResponse:TraceableResponse, IEquatable<ArticlePriceResponse>
    {
        public static bool operator==( ArticlePriceResponse left, ArticlePriceResponse right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( ArticlePriceResponse left, ArticlePriceResponse right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( ArticlePriceResponse left, ArticlePriceResponse right )
		{
            bool result = TraceableResponse.Equals( left, right );

            if( result == true )
            {
                result &= ArticlePriceResponseArticle.Equals( left.Article, right.Article );
                result &= Iso4217Code.Equals( left.Currency, right.Currency );
            }

            return result;
		}

        public ArticlePriceResponse(    IMessageId id,
                                        SubscriberId source,
                                        SubscriberId destination,
                                        ArticlePriceResponseArticle article  )
        :
            base( DialogName.ArticleMasterSet, id, source, destination )
        {
            article.ThrowIfNull();

            this.Article = article;
        }

        public ArticlePriceResponse(    IMessageId id,
                                        SubscriberId source,
                                        SubscriberId destination,
                                        ArticlePriceResponseArticle article,
                                        Iso4217Code currency    )
        :
            base( DialogName.ArticleMasterSet, id, source, destination )
        {
            article.ThrowIfNull();

            this.Article = article;
            this.Currency = currency;
        }

        public ArticlePriceResponse(    ArticlePriceRequest request,
                                        ArticlePriceResponseArticle article  )
        :
            base( request )
        {
            article.ThrowIfNull();

            this.Article = article;
            this.Currency = request.Currency;
        }

        public ArticlePriceResponseArticle Article
        {
            get;
        }

        public Iso4217Code Currency
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as ArticlePriceResponse );
		}
		
		public bool Equals( ArticlePriceResponse other )
		{
            return ArticlePriceResponse.Equals( this, other );
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