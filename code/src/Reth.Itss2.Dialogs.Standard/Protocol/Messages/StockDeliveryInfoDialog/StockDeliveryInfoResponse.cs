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
    public class StockDeliveryInfoResponse:SubscribedResponse, IEquatable<StockDeliveryInfoResponse>
    {
        public static bool operator==( StockDeliveryInfoResponse? left, StockDeliveryInfoResponse? right )
		{
            return StockDeliveryInfoResponse.Equals( left, right );
		}
		
		public static bool operator!=( StockDeliveryInfoResponse? left, StockDeliveryInfoResponse? right )
		{
			return !( StockDeliveryInfoResponse.Equals( left, right ) );
		}

        public static bool Equals( StockDeliveryInfoResponse? left, StockDeliveryInfoResponse? right )
		{
            bool result = SubscribedResponse.Equals( left, right );

            result &= ( result ? StockDeliveryInfoResponseTask.Equals( left?.Task, right?.Task ) : false );

            return result;
		}

        public StockDeliveryInfoResponse(   MessageId id,
									        SubscriberId source,
                                            SubscriberId destination,
                                            StockDeliveryInfoResponseTask task  )
        :
            base( id, source, destination )
        {
            this.Task = task;
        }

        public StockDeliveryInfoResponse(   StockDeliveryInfoRequest request,
                                            StockDeliveryInfoResponseTask task  )
        :
            base( request )
        {
            this.Task = task;
        }

        public StockDeliveryInfoResponseTask Task
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as StockDeliveryInfoResponse );
		}
		
        public bool Equals( StockDeliveryInfoResponse? other )
		{
            return StockDeliveryInfoResponse.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
