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
using System.Text;

namespace Reth.Itss2.Dialogs.Standard.Serialization
{
    public static class ExtensionMethods
    {
        public static T ConvertTo<T>( this String instance )
            where T:struct, Enum
        {
            return Enum.Parse<T>( instance, true );
        }

        public static T? ConvertNullableTo<T>( this String? instance )
            where T:struct, Enum
        {
            T? result = default( T? );

            if( String.IsNullOrEmpty( instance ) == false )
            {
                result = Enum.Parse<T>( instance, true );
            }

            return result;
        }

        public static String Capitalize( this String instance )
        {
            StringBuilder result = new( instance.ToLowerInvariant() );

            if( result.Length > 0 )
            {
                result[ 0 ] = result[ 0 ].ToString().ToUpperInvariant()[ 0 ];
            }

            return result.ToString();
        }
    }
}
