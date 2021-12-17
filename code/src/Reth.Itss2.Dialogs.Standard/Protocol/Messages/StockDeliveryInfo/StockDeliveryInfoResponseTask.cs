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
    public class StockDeliveryInfoResponseTask:IEquatable<StockDeliveryInfoResponseTask>
    {
        public static bool operator==( StockDeliveryInfoResponseTask? left, StockDeliveryInfoResponseTask? right )
		{
            return StockDeliveryInfoResponseTask.Equals( left, right );
		}
		
		public static bool operator!=( StockDeliveryInfoResponseTask? left, StockDeliveryInfoResponseTask? right )
		{
			return !( StockDeliveryInfoResponseTask.Equals( left, right ) );
		}

        public static bool Equals( StockDeliveryInfoResponseTask? left, StockDeliveryInfoResponseTask? right )
		{
            bool result = String.Equals( left?.Id, right?.Id, StringComparison.OrdinalIgnoreCase );

            result &= ( result ? EqualityComparer<StockDeliveryInfoStatus?>.Default.Equals( left?.Status, right?.Status ) : false );
            result &= ( result ? ( left?.Articles.SequenceEqual( right?.Articles ) ).GetValueOrDefault() : false );

            return result;
		}

        public StockDeliveryInfoResponseTask(   String id,
                                                StockDeliveryInfoStatus status  )
        :
            this( id, status, null )
        {
        }

        public StockDeliveryInfoResponseTask(   String id,
                                                StockDeliveryInfoStatus status,
                                                IEnumerable<StockDeliveryInfoArticle>? articles )
        {
            id.ThrowIfEmpty();

            this.Id = id;
            this.Status = status;

            if( articles is not null )
            {
                this.Articles.AddRange( articles );
            }
        }

        public String Id
        {
            get;
        }

        public StockDeliveryInfoStatus Status
        {
            get;
        }

        private List<StockDeliveryInfoArticle> Articles
        {
            get;
        } = new List<StockDeliveryInfoArticle>();
        
        public StockDeliveryInfoArticle[] GetArticles()
        {
            return this.Articles.ToArray();
        }

        public override bool Equals( Object? obj )
		{
			return this.Equals( obj as StockDeliveryInfoResponseTask );
		}
		
        public bool Equals( StockDeliveryInfoResponseTask? other )
		{
            return StockDeliveryInfoResponseTask.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override String? ToString()
        {
            return $"{ this.Id }, { this.Status }";
        }
    }
}
