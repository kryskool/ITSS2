using System;
using System.Collections.Generic;

using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols;
using Reth.Protocols.Extensions.ListExtensions;

namespace Reth.Itss2.Experimental.Dialogs.ArticleData.ArticlePrice
{
    public class ArticlePriceResponseArticle:Article, IEquatable<ArticlePriceResponseArticle>
    {
        public static bool operator==( ArticlePriceResponseArticle left, ArticlePriceResponseArticle right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( ArticlePriceResponseArticle left, ArticlePriceResponseArticle right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( ArticlePriceResponseArticle left, ArticlePriceResponseArticle right )
		{
            bool result = Article.Equals( left, right );

            if( result == true )
            {
                result &= left.PriceInformations.ElementsEqual<PriceInformation>( right.PriceInformations );
            }

            return result;
		}

        public ArticlePriceResponseArticle( ArticleId id, IEnumerable<PriceInformation> priceInformations )
        :
            base( id )
        {
            if( !( priceInformations is null ) )
            {
                this.PriceInformations.AddRange( priceInformations );
            }
        }

        private List<PriceInformation> PriceInformations
        {
            get;
        } = new List<PriceInformation>();
        
        public PriceInformation[] GetPriceInformations()
        {
            return this.PriceInformations.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as ArticlePriceResponseArticle );
		}
		
        public bool Equals( ArticlePriceResponseArticle other )
		{
            return ArticlePriceResponseArticle.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}