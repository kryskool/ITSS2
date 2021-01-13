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
using System.Globalization;
using System.Linq;

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages.OutputDialog
{
    public class OutputCriteria:IEquatable<OutputCriteria>
    {
        public static bool operator==( OutputCriteria? left, OutputCriteria? right )
		{
            return OutputCriteria.Equals( left, right );
		}
		
		public static bool operator!=( OutputCriteria? left, OutputCriteria? right )
		{
			return !( OutputCriteria.Equals( left, right ) );
		}

        public static bool Equals( OutputCriteria? left, OutputCriteria? right )
		{
            bool result = EqualityComparer<int?>.Default.Equals( left?.Quantity, right?.Quantity );
            
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.SubItemQuantity, right?.SubItemQuantity ) : false );
            result &= ( result ? ArticleId.Equals( left?.ArticleId, right?.ArticleId ) : false );
            result &= ( result ? PackId.Equals( left?.PackId, right?.PackId ) : false );
            result &= ( result ? PackDate.Equals( left?.MinimumExpiryDate, right?.MinimumExpiryDate ) : false );
            result &= ( result ? String.Equals( left?.BatchNumber, right?.BatchNumber, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? String.Equals( left?.ExternalId, right?.ExternalId, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? String.Equals( left?.SerialNumber, right?.SerialNumber, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? String.Equals( left?.MachineLocation, right?.MachineLocation, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? StockLocationId.Equals( left?.StockLocationId, right?.StockLocationId ) : false );
            result &= ( result ? EqualityComparer<bool?>.Default.Equals( left?.SingleBatchNumber, right?.SingleBatchNumber ) : false );
            result &= ( result ? ( left?.Labels.SequenceEqual( right?.Labels ) ).GetValueOrDefault() : false );
            
            return result;
		}

        public OutputCriteria( int quantity )
        {
            quantity.ThrowIfNegative();

            this.Quantity = quantity;
        }

        public OutputCriteria(  int quantity,
                                int? subItemQuantity,
                                ArticleId? articleId,
                                PackId? packId,
                                PackDate? minimumExpiryDate,
                                String? batchNumber,
                                String? externalId,
                                String? serialNumber,
                                String? machineLocation,
                                StockLocationId? stockLocationId,
                                bool? singleBatchNumber,
                                IEnumerable<OutputLabel>? labels )
        {
            quantity.ThrowIfNegative();
            subItemQuantity?.ThrowIfNegative();

            this.Quantity = quantity;
            this.SubItemQuantity = subItemQuantity;
            this.ArticleId = articleId;
            this.PackId = packId;
            this.MinimumExpiryDate = minimumExpiryDate;
            this.BatchNumber = batchNumber;
            this.ExternalId = externalId;
            this.SerialNumber = serialNumber;
            this.MachineLocation = machineLocation;
            this.StockLocationId = stockLocationId;
            this.SingleBatchNumber = singleBatchNumber;

            if( labels is not null )
            {
                this.Labels.AddRange( labels );
            }
        }

        public int Quantity
        {
            get;
        }

        public int? SubItemQuantity
        {
            get;
        }

        public ArticleId? ArticleId
        {
            get;
        }

        public PackId? PackId
        {
            get;
        }

        public PackDate? MinimumExpiryDate
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

        public bool? SingleBatchNumber
        {
            get;
        }

        private List<OutputLabel> Labels
        {
            get;
        } = new List<OutputLabel>();

        public OutputLabel[] GetLabels()
        {
            return this.Labels.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as OutputCriteria );
		}
		
        public bool Equals( OutputCriteria? other )
		{
            return OutputCriteria.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return HashCode.Combine( this.ArticleId, this.PackId );
        }

        public override String ToString()
        {
            return this.Quantity.ToString( CultureInfo.InvariantCulture );
        }
    }
}
