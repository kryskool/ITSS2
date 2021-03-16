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

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockInfo
{
    public class StockInfoRequestCriteria:IEquatable<StockInfoRequestCriteria>
    {
        public static bool operator==( StockInfoRequestCriteria? left, StockInfoRequestCriteria? right )
		{
            return StockInfoRequestCriteria.Equals( left, right );
		}
		
		public static bool operator!=( StockInfoRequestCriteria? left, StockInfoRequestCriteria? right )
		{
			return !( StockInfoRequestCriteria.Equals( left, right ) );
		}

        public static bool Equals( StockInfoRequestCriteria? left, StockInfoRequestCriteria? right )
		{
            bool result = ArticleId.Equals( left?.ArticleId, right?.ArticleId );

            result &= ( result ? String.Equals( left?.BatchNumber, right?.BatchNumber, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? String.Equals( left?.ExternalId, right?.ExternalId, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? String.Equals( left?.SerialNumber, right?.SerialNumber, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? String.Equals( left?.MachineLocation, right?.MachineLocation, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? StockLocationId.Equals( left?.StockLocationId, right?.StockLocationId ) : false );

            return result;
		}

        public StockInfoRequestCriteria()
        {
        }

        public StockInfoRequestCriteria(    ArticleId? articleId,
                                            String? batchNumber,
                                            String? externalId,
                                            String? serialNumber,
                                            String? machineLocation,
                                            StockLocationId? stockLocationId )
        {
            this.ArticleId = articleId;
            this.BatchNumber = batchNumber;
            this.ExternalId = externalId;
            this.SerialNumber = serialNumber;
            this.MachineLocation = machineLocation;
            this.StockLocationId = stockLocationId;
        }

        public ArticleId? ArticleId
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

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as StockInfoRequestCriteria );
		}
		
        public bool Equals( StockInfoRequestCriteria? other )
		{
            return StockInfoRequestCriteria.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return HashCode.Combine( this.ArticleId, this.BatchNumber );
		}
    }
}
