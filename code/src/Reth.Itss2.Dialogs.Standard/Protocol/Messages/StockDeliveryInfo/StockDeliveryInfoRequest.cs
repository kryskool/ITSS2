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

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockDeliveryInfo
{
    public class StockDeliveryInfoRequest:SubscribedRequest, IEquatable<StockDeliveryInfoRequest>
    {
        public static bool operator==( StockDeliveryInfoRequest? left, StockDeliveryInfoRequest? right )
		{
            return StockDeliveryInfoRequest.Equals( left, right );
		}
		
		public static bool operator!=( StockDeliveryInfoRequest? left, StockDeliveryInfoRequest? right )
		{
			return !( StockDeliveryInfoRequest.Equals( left, right ) );
		}

        public static bool Equals( StockDeliveryInfoRequest? left, StockDeliveryInfoRequest? right )
		{
            bool result = SubscribedRequest.Equals( left, right );

            result &= ( result ? StockDeliveryInfoRequestTask.Equals( left?.Task, right?.Task ) : false );
            result &= ( result ? EqualityComparer<bool?>.Default.Equals( left?.IncludeTaskDetails, right?.IncludeTaskDetails ) : false );

            return result;
		}

        public StockDeliveryInfoRequest(    MessageId id,
									        SubscriberId source,
                                            SubscriberId destination,
                                            StockDeliveryInfoRequestTask task    )
        :
            this( id, source, destination, task, includeTaskDetails:null )
        {
        }

        public StockDeliveryInfoRequest(    MessageId id,
									        SubscriberId source,
                                            SubscriberId destination,
                                            StockDeliveryInfoRequestTask task,
                                            bool? includeTaskDetails    )
        :
            base( id, StandardDialogs.StockDeliveryInfo, source, destination )
        {
            this.Task = task;
            this.IncludeTaskDetails = includeTaskDetails;
        }

        public StockDeliveryInfoRequestTask Task
        {
            get;
        }

        public bool? IncludeTaskDetails
        {
            get;
        }

        public override bool Equals( Object? obj )
		{
			return this.Equals( obj as StockDeliveryInfoRequest );
		}
		
        public bool Equals( StockDeliveryInfoRequest? other )
		{
            return StockDeliveryInfoRequest.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
