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

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockDeliveryInfo
{
    public class StockDeliveryInfoArticle:IEquatable<StockDeliveryInfoArticle>
    {
        public static bool operator==( StockDeliveryInfoArticle? left, StockDeliveryInfoArticle? right )
		{
            return StockDeliveryInfoArticle.Equals( left, right );
		}
		
		public static bool operator!=( StockDeliveryInfoArticle? left, StockDeliveryInfoArticle? right )
		{
			return !( StockDeliveryInfoArticle.Equals( left, right ) );
		}

        public static bool Equals( StockDeliveryInfoArticle? left, StockDeliveryInfoArticle? right )
		{
            bool result = ArticleId.Equals( left?.Id, right?.Id );

            result &= ( result ? ( EqualityComparer<int?>.Default.Equals( left?.Quantity, right?.Quantity ) ) : false );
            result &= ( result ? ( left?.Packs.SequenceEqual( right?.Packs ) ).GetValueOrDefault() : false );

            return result;
		}

        public StockDeliveryInfoArticle( ArticleId id )
        {
            this.Id = id;
        }

        public StockDeliveryInfoArticle(    ArticleId id,
                                            int? quantity,
                                            IEnumerable<StockDeliveryInfoPack>? packs    )
        {
            this.Id = id;
            this.Quantity = quantity;
            
            if( packs is not null )
            {
                this.Packs.AddRange( packs );
            }
        }

        public ArticleId Id
        {
            get;
        }

        public int? Quantity
        {
            get;
        }

        private List<StockDeliveryInfoPack> Packs
        {
            get;
        } = new List<StockDeliveryInfoPack>();

        public StockDeliveryInfoPack[] GetPacks()
        {
            return this.Packs.ToArray();
        }

        public override bool Equals( Object? obj )
		{
			return this.Equals( obj as StockDeliveryInfoArticle );
		}
		
        public bool Equals( StockDeliveryInfoArticle? other )
		{
            return StockDeliveryInfoArticle.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override String? ToString()
        {
            return this.Id.ToString();
        }
    }
}
