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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Xml.Messages
{
    public class PackIdConverter
    {
        public String ConvertFrom( PackId value )
        {
            return value.Value;
        }

        public String? ConvertNullableFrom( PackId? value )
        {
            return value?.Value;
        }

        public PackId ConvertTo( String value )
        {
            return PackId.Parse( value );
        }

        public PackId? ConvertNullableTo( String? value )
        {
            PackId.TryParse( value, out PackId? result );

            return result;
        }
    }
}
