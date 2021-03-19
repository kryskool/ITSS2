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

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockDeliverySet
{
    public class StockDeliverySetResponse:SubscribedResponse, IEquatable<StockDeliverySetResponse>
    {
        public static bool operator==( StockDeliverySetResponse? left, StockDeliverySetResponse? right )
		{
            return StockDeliverySetResponse.Equals( left, right );
		}
		
		public static bool operator!=( StockDeliverySetResponse? left, StockDeliverySetResponse? right )
		{
			return !( StockDeliverySetResponse.Equals( left, right ) );
		}

        public static bool Equals( StockDeliverySetResponse? left, StockDeliverySetResponse? right )
		{
            bool result = SubscribedResponse.Equals( left, right );

            result &= ( result ? StockDeliverySetResult.Equals( left, right ) : false );

            return result;
		}

        public StockDeliverySetResponse(    MessageId id,
                                            SubscriberId source,
                                            SubscriberId destination,
                                            StockDeliverySetResult result   )
        :
            base( id, StandardDialogs.StockDeliverySet, source, destination )
        {
            this.Result = result;
        }

        public StockDeliverySetResponse(    StockDeliverySetRequest request,
                                            StockDeliverySetResult result   )
        :
            base( request )
        {
            this.Result = result;
        }

        public StockDeliverySetResult Result
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as StockDeliverySetResponse );
		}
		
        public bool Equals( StockDeliverySetResponse? other )
		{
            return StockDeliverySetResponse.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
