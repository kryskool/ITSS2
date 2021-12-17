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

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockDeliverySet
{
    public class StockDeliverySetRequest:SubscribedRequest, IEquatable<StockDeliverySetRequest>
    {
        public static bool operator==( StockDeliverySetRequest? left, StockDeliverySetRequest? right )
		{
            return StockDeliverySetRequest.Equals( left, right );
		}
		
		public static bool operator!=( StockDeliverySetRequest? left, StockDeliverySetRequest? right )
		{
			return !( StockDeliverySetRequest.Equals( left, right ) );
		}

        public static bool Equals( StockDeliverySetRequest? left, StockDeliverySetRequest? right )
		{
            bool result = SubscribedRequest.Equals( left, right );

            result &= ( result ? ( left?.Deliveries.SequenceEqual( right?.Deliveries ) ).GetValueOrDefault() : false );

            return result;
		}

        public StockDeliverySetRequest( MessageId id,
									    SubscriberId source,
                                        SubscriberId destination,
                                        IEnumerable<StockDelivery> deliveries  )
        :
            base( id, StandardDialogs.StockDeliverySet, source, destination )
        {
            this.Deliveries.AddRange( deliveries );
        }

        private List<StockDelivery> Deliveries
        {
            get;
        } = new List<StockDelivery>();

        public StockDelivery[] GetDeliveries()
        {
            return this.Deliveries.ToArray();
        }

        public override bool Equals( Object? obj )
		{
			return this.Equals( obj as StockDeliverySetRequest );
		}
		
        public bool Equals( StockDeliverySetRequest? other )
		{
            return StockDeliverySetRequest.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
