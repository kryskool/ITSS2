using System;
using System.Collections.Generic;

using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols;
using Reth.Protocols.Extensions.ListExtensions;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Experimental.Dialogs.ArticleData.ArticlePrice
{
    public class ArticlePriceResponseArticle:IEquatable<ArticlePriceResponseArticle>
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
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = false;

                                                        result = left.Id.Equals( right.Id );
                                                        result &= left.PriceInformations.ElementsEqual<PriceInformation>( right.PriceInformations );

                                                        return result;
                                                    }   );
		}

        public ArticlePriceResponseArticle( ArticleId id, IEnumerable<PriceInformation> priceInformations )
        {
            id.ThrowIfNull();

            this.Id = id;

            if( !( priceInformations is null ) )
            {
                this.PriceInformations.AddRange( priceInformations );
            }
        }

        public ArticleId Id
        {
            get;
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
            return this.Id.GetHashCode();
        }

        public override String ToString()
        {
            return this.Id.ToString();
        }
    }
}