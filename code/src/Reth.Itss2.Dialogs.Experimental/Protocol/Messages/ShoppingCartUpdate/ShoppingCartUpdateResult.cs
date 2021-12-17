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

namespace Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ShoppingCartUpdate
{
    public class ShoppingCartUpdateResult:IEquatable<ShoppingCartUpdateResult>
    {
        public static bool operator==( ShoppingCartUpdateResult? left, ShoppingCartUpdateResult? right )
		{
            return ShoppingCartUpdateResult.Equals( left, right );
		}
		
		public static bool operator!=( ShoppingCartUpdateResult? left, ShoppingCartUpdateResult? right )
		{
			return !( ShoppingCartUpdateResult.Equals( left, right ) );
		}

        public static bool Equals( ShoppingCartUpdateResult? left, ShoppingCartUpdateResult? right )
		{
            bool result = EqualityComparer<ShoppingCartUpdateStatus?>.Default.Equals( left?.Status, right?.Status );

            result &= ( result ? String.Equals( left?.Description, right?.Description, StringComparison.OrdinalIgnoreCase ) : false );
            
            return result;
		}

        public ShoppingCartUpdateResult( ShoppingCartUpdateStatus status )
        :
            this( status, null )
        {
        }

        public ShoppingCartUpdateResult( ShoppingCartUpdateStatus status, String? description )
        {
            this.Status = status;
            this.Description = description;
        }

        public ShoppingCartUpdateStatus Status
        {
            get;
        }

        public String? Description
        {
            get;
        }

        public override bool Equals( Object? obj )
		{
			return this.Equals( obj as ShoppingCartUpdateResult );
		}
		
        public bool Equals( ShoppingCartUpdateResult? other )
		{
            return ShoppingCartUpdateResult.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return this.Status.GetHashCode();
        }

        public override String? ToString()
        {
            return this.Status.ToString();
        }
    }
}
