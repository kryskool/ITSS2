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

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockDeliveryInfoDialog
{
    public class StockDeliveryInfoRequestTask:IEquatable<StockDeliveryInfoRequestTask>
    {
        public static bool operator==( StockDeliveryInfoRequestTask? left, StockDeliveryInfoRequestTask? right )
		{
            return StockDeliveryInfoRequestTask.Equals( left, right );
		}
		
		public static bool operator!=( StockDeliveryInfoRequestTask? left, StockDeliveryInfoRequestTask? right )
		{
			return !( StockDeliveryInfoRequestTask.Equals( left, right ) );
		}

        public static bool Equals( StockDeliveryInfoRequestTask? left, StockDeliveryInfoRequestTask? right )
		{
            return String.Equals( left?.Id, right?.Id, StringComparison.OrdinalIgnoreCase );
		}

        public StockDeliveryInfoRequestTask( String id )
        {
            id.ThrowIfEmpty();

            this.Id = id;
        }

        public String Id
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as StockDeliveryInfoRequestTask );
		}
		
        public bool Equals( StockDeliveryInfoRequestTask? other )
		{
            return StockDeliveryInfoRequestTask.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override String ToString()
        {
            return this.Id;
        }
    }
}
