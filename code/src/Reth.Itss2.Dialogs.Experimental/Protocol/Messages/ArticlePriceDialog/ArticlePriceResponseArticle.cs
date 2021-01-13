// Implementation of the WWKS2 protocol.
// Copyright (C) 2020  Thomas Reth

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.

// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;

using Reth.Itss2.Dialogs.Standard.Protocol.Messages;

namespace Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ArticlePriceDialog
{
    public class ArticlePriceResponseArticle:IEquatable<ArticlePriceResponseArticle>
    {
        public static bool operator==( ArticlePriceResponseArticle? left, ArticlePriceResponseArticle? right )
		{
            return ArticlePriceResponseArticle.Equals( left, right );
		}
		
		public static bool operator!=( ArticlePriceResponseArticle? left, ArticlePriceResponseArticle? right )
		{
			return !( ArticlePriceResponseArticle.Equals( left, right ) );
		}

        public static bool Equals( ArticlePriceResponseArticle? left, ArticlePriceResponseArticle? right )
		{
            bool result = ArticleId.Equals( left?.Id, right?.Id );

            result &= ( result ? ( left?.PriceInformations.SequenceEqual( right?.PriceInformations ) ).GetValueOrDefault() : false );

            return result;
		}

        public ArticlePriceResponseArticle( ArticleId id )
        {
            this.Id = id;
        }

        public ArticlePriceResponseArticle( ArticleId id,
                                            IEnumerable<PriceInformation>? priceInformations    )
        {
            this.Id = id;

            if( priceInformations is not null )
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
		
        public bool Equals( ArticlePriceResponseArticle? other )
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
