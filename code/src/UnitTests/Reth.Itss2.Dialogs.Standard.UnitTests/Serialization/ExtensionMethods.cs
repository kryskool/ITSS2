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

namespace Reth.Itss2.Dialogs.Standard.UnitTests.Serialization
{
    internal static class ExtensionMethods
    {
        public static ( String, String ) Divide( this String instance )
        {
            ( String left, String right ) result = ( String.Empty, String.Empty );

            if( instance.Length > 0 )
            {
                int leftLength = instance.Length / 2;

                String right = instance.Substring( leftLength );
                String left = instance.Remove( leftLength, right.Length );

                result.left = left;
                result.right = right;
            }

            return result;
        }
    }
}
