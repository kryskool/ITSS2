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

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockLocationInfo
{
    public class StockLocationInfoResponse:SubscribedResponse, IEquatable<StockLocationInfoResponse>
    {
        public static bool operator==( StockLocationInfoResponse? left, StockLocationInfoResponse? right )
		{
            return StockLocationInfoResponse.Equals( left, right );
		}
		
		public static bool operator!=( StockLocationInfoResponse? left, StockLocationInfoResponse? right )
		{
			return !( StockLocationInfoResponse.Equals( left, right ) );
		}

        public static bool Equals( StockLocationInfoResponse? left, StockLocationInfoResponse? right )
		{
            bool result = SubscribedResponse.Equals( left, right );

            result &= ( result ? ( left?.StockLocations.SequenceEqual( right?.StockLocations ) ).GetValueOrDefault() : false );

            return result;
		}

        public StockLocationInfoResponse(   MessageId id,
                                            SubscriberId source,
                                            SubscriberId destination,
                                            IEnumerable<StockLocation> stockLocations  )
        :
            base( id, StandardDialogs.StockLocationInfo, source, destination )
        {
            this.StockLocations.AddRange( stockLocations );
        }

        public StockLocationInfoResponse(   StockLocationInfoRequest request,
                                            IEnumerable<StockLocation> stockLocations  )
        :
            base( request )
        {
            this.StockLocations.AddRange( stockLocations );
        }

        private List<StockLocation> StockLocations
        {
            get;
        } = new List<StockLocation>();
        
        public StockLocation[] GetStockLocations()
        {
            return this.StockLocations.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as StockLocationInfoResponse );
		}
		
        public bool Equals( StockLocationInfoResponse? other )
		{
            return StockLocationInfoResponse.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
