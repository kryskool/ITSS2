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

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockDeliverySet
{
    public class StockDeliveryLine:IEquatable<StockDeliveryLine>
    {
        public static bool operator==( StockDeliveryLine? left, StockDeliveryLine? right )
		{
            return StockDeliveryLine.Equals( left, right );
		}
		
		public static bool operator!=( StockDeliveryLine? left, StockDeliveryLine? right )
		{
			return !( StockDeliveryLine.Equals( left, right ) );
		}

        public static bool Equals( StockDeliveryLine? left, StockDeliveryLine? right )
		{
            bool result = PackId.Equals( left?.Id, right?.Id );

            result &= ( result ? String.Equals( left?.BatchNumber, right?.BatchNumber, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? String.Equals( left?.ExternalId, right?.ExternalId, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? String.Equals( left?.SerialNumber, right?.SerialNumber, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? String.Equals( left?.MachineLocation, right?.MachineLocation, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? StockLocationId.Equals( left?.StockLocationId, right?.StockLocationId ) : false );
            result &= ( result ? PackDate.Equals( left?.ExpiryDate, right?.ExpiryDate ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.Quantity, right?.Quantity ) : false );

            return result;
		}

        public StockDeliveryLine( ArticleId id )
        {
            this.Id = id;
        }

        public StockDeliveryLine(   ArticleId id,
                                    String? batchNumber,
                                    String? externalId,
                                    String? serialNumber,
                                    String? machineLocation,
                                    StockLocationId? stockLocationId,
                                    PackDate? expiryDate,
                                    int? quantity   )
        {
            quantity?.ThrowIfNegative();

            this.Id = id;
            this.BatchNumber = batchNumber;
            this.ExternalId = externalId;
            this.SerialNumber = serialNumber;
            this.MachineLocation = machineLocation;
            this.StockLocationId = stockLocationId;
            this.ExpiryDate = expiryDate;
            this.Quantity = quantity;
        }

        public ArticleId Id
        {
            get;
        }

        public String? BatchNumber
        {
            get;
        }

        public String? ExternalId
        {
            get;
        }

        public String? SerialNumber
        {
            get;
        }

        public String? MachineLocation
        {
            get;
        }

        public StockLocationId? StockLocationId
        {
            get;
        }

        public PackDate? ExpiryDate
        {
            get;
        }

        public int? Quantity
        {
            get;
        }

        public override bool Equals( Object? obj )
		{
			return this.Equals( obj as StockDeliveryLine );
		}
		
		public bool Equals( StockDeliveryLine? other )
		{
            return StockDeliveryLine.Equals( this, other );
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
