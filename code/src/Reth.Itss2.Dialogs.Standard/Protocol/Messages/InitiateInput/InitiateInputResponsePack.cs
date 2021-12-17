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

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages.InitiateInput
{
    public class InitiateInputResponsePack:IEquatable<InitiateInputResponsePack>
    {
        public static bool operator==( InitiateInputResponsePack? left, InitiateInputResponsePack? right )
		{
            return InitiateInputResponsePack.Equals( left, right );
		}
		
		public static bool operator!=( InitiateInputResponsePack? left, InitiateInputResponsePack? right )
		{
			return !( InitiateInputResponsePack.Equals( left, right ) );
		}

        public static bool Equals( InitiateInputResponsePack? left, InitiateInputResponsePack? right )
		{
            bool result = String.Equals( left?.ScanCode, right?.ScanCode, StringComparison.OrdinalIgnoreCase );
            
            result &= ( result ? String.Equals( left?.DeliveryNumber, right?.DeliveryNumber, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? String.Equals( left?.BatchNumber, right?.BatchNumber, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? String.Equals( left?.ExternalId, right?.ExternalId, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? String.Equals( left?.SerialNumber, right?.SerialNumber, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? StockLocationId.Equals( left?.StockLocationId, right?.StockLocationId ) : false );
            result &= ( result ? PackDate.Equals( left?.ExpiryDate, right?.ExpiryDate ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.Index, right?.Index ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.SubItemQuantity, right?.SubItemQuantity ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.Depth, right?.Depth ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.Width, right?.Width ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.Height, right?.Height ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.Weight, right?.Weight ) : false );
            result &= ( result ? PackShape.Equals( left?.Shape, right?.Shape ) : false );
            
            return result;
		}

        public InitiateInputResponsePack( String scanCode )
        {
            scanCode.ThrowIfEmpty();

            this.ScanCode = scanCode;
        }

        public InitiateInputResponsePack(   String scanCode,
                                            String? deliveryNumber,
                                            String? batchNumber,
                                            String? externalId,
                                            String? serialNumber,
                                            StockLocationId? stockLocationId,
                                            PackDate? expiryDate,
                                            int? index,
                                            int? subItemQuantity,
                                            int? depth,
                                            int? width,
                                            int? height,
                                            int? weight,
                                            PackShape? shape    )
        {
            scanCode.ThrowIfEmpty();
            index?.ThrowIfNegative();
            subItemQuantity?.ThrowIfNegative();
            depth?.ThrowIfNegative();
            width?.ThrowIfNegative();
            height?.ThrowIfNegative();
            weight?.ThrowIfNegative();

            this.ScanCode = scanCode;
            this.DeliveryNumber = deliveryNumber;
            this.BatchNumber = batchNumber;
            this.ExternalId = externalId;
            this.SerialNumber = serialNumber;
            this.StockLocationId = stockLocationId;
            this.ExpiryDate = expiryDate;
            this.Index = index;
            this.SubItemQuantity = subItemQuantity;
            this.Depth = depth;
            this.Width = width;
            this.Height = height;
            this.Weight = weight;
            this.Shape = shape;
        }
        
        public String ScanCode
        {
            get;
        }

        public String? DeliveryNumber
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

        public StockLocationId? StockLocationId
        {
            get;
        }

        public PackDate? ExpiryDate
        {
            get;
        }

        public int? Index
        {
            get;
        }

        public int? SubItemQuantity
        {
            get;
        }

        public int? Depth
        {
            get;
        }

        public int? Width
        {
            get;
        }

        public int? Height
        {
            get;
        }

        public int? Weight
        {
            get;
        }

        public PackShape? Shape
        {
            get;
        }

        public override bool Equals( Object? obj )
		{
			return this.Equals( obj as InitiateInputResponsePack );
		}
		
		public bool Equals( InitiateInputResponsePack? other )
		{
            return InitiateInputResponsePack.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return this.ScanCode.GetHashCode();
		}

        public override String? ToString()
        {
            return this.ScanCode;
        }
    }
}
