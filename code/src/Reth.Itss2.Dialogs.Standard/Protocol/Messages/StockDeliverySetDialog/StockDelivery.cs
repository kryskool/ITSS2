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

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockDeliverySetDialog
{
    public class StockDelivery:IEquatable<StockDelivery>
    {
        public static bool operator==( StockDelivery? left, StockDelivery? right )
		{
            return StockDelivery.Equals( left, right );
		}
		
		public static bool operator!=( StockDelivery? left, StockDelivery? right )
		{
			return !( StockDelivery.Equals( left, right ) );
		}

        public static bool Equals( StockDelivery? left, StockDelivery? right )
		{
            bool result = String.Equals( left?.DeliveryNumber, right?.DeliveryNumber, StringComparison.OrdinalIgnoreCase );

            result &= ( result ? ( left?.Lines.SequenceEqual( right?.Lines ) ).GetValueOrDefault() : false );

            return result;
		}

        public StockDelivery(   String deliveryNumber,
                                IEnumerable<StockDeliveryLine>? lines  )
        {
            this.DeliveryNumber = deliveryNumber;
            
            if( lines is not null )
            {
                this.Lines.AddRange( lines );
            }
        }

        public String DeliveryNumber
        {
            get;
        }

        private List<StockDeliveryLine> Lines
        {
            get;
        } = new List<StockDeliveryLine>();

        public StockDeliveryLine[] GetLines()
        {
            return this.Lines.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as StockDelivery );
		}
		
		public bool Equals( StockDelivery? other )
		{
            return StockDelivery.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return this.DeliveryNumber.GetHashCode();
		}

        public override String ToString()
        {
            return this.DeliveryNumber;
        }
    }
}
