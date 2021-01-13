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

using Reth.Itss2.Dialogs.Experimental.Protocol.Messages;

namespace Reth.Itss2.Dialogs.Experimental.Serialization.Xml.Messages
{
    public class SalesPersonIdConverter
    {
        public String ConvertFrom( SalesPersonId value )
        {
            return value.ToString();
        }

        public String? ConvertNullableFrom( SalesPersonId? value )
        {
            return value?.ToString();
        }

        public SalesPersonId ConvertTo( String value )
        {
            return SalesPersonId.Parse( value );
        }

        public SalesPersonId? ConvertNullableTo( String? value )
        {
            SalesPersonId.TryParse( value, out SalesPersonId? result );

            return result;
        }
    }
}
