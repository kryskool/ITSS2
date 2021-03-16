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

using Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ShoppingCartUpdate;
using Reth.Itss2.Dialogs.Standard.Serialization;

namespace Reth.Itss2.Dialogs.Experimental.Serialization.Conversion.Messages.ShoppingCartUpdate
{
    public class ShoppingCartUpdateStatusConverter
    {
        public String ConvertFrom( ShoppingCartUpdateStatus value )
        {
            return value.ToString();
        }

        public String? ConvertNullableFrom( ShoppingCartUpdateStatus? value )
        {
            return value?.ToString();
        }

        public ShoppingCartUpdateStatus ConvertTo( String value )
        {
            return value.ConvertTo<ShoppingCartUpdateStatus>();
        }

        public ShoppingCartUpdateStatus? ConvertNullableTo( String? value )
        {
            return value.ConvertNullableTo<ShoppingCartUpdateStatus>();
        }
    }
}
