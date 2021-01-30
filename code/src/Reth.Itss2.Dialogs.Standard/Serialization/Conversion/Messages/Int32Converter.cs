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
using System.Globalization;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Conversion.Messages
{
    public class Int32Converter
    {
        public String ConvertFrom( int value )
        {
            return value.ToString().ToUpperInvariant();
        }

        public String? ConvertNullableFrom( int? value )
        {
            return value?.ToString().ToUpperInvariant();
        }

        public int ConvertTo( String value )
        {
            return int.Parse( value, NumberStyles.Integer );
        }

        public int? ConvertNullableTo( String? value )
        {
            int? result = null;

            if( value is not null )
            {
                result = this.ConvertTo( value );
            }

            return result;
        }
    }
}
