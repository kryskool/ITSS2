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

using Reth.Itss2.Dialogs.Standard.Diagnostics;

namespace Reth.Itss2.Dialogs.Standard
{
    public static class ExtensionMethods
    {
        public static void ThrowIfEmpty( this String value )
        {
            bool isNullOrEmpty = String.IsNullOrEmpty( value );

            if( isNullOrEmpty == true )
            {
                throw Assert.Exception( new ArgumentException( $"String value is empty.", nameof( value ) ) );
            }
        }

        public static void ThrowIfNegative( this int value )
        {
            if( value < 0 )
            {
                throw Assert.Exception( new ArgumentOutOfRangeException( nameof( value ), $"A negative value ({ value }) is not allowed." ) );
            }
        }

        public static void ThrowIfNegative( this long value )
        {
            if( value < 0 )
            {
                throw Assert.Exception( new ArgumentOutOfRangeException( nameof( value ), $"A negative value ({ value }) is not allowed." ) );
            }
        }

        public static void ThrowIfNegative( this decimal value )
        {
            if( value < 0 )
            {
                throw Assert.Exception( new ArgumentOutOfRangeException( nameof( value ), $"A negative value ({ value }) is not allowed." ) );
            }
        }

        public static void ThrowIfNotPositive( this int value )
        {
            if( value <= 0 )
            {
                throw Assert.Exception( new ArgumentOutOfRangeException( nameof( value ), $"A positive value instead of ({ value }) is required." ) );
            }
        }

        public static void ThrowIfNotPositive( this decimal value )
        {
            if( value <= 0 )
            {
                throw Assert.Exception( new ArgumentOutOfRangeException( nameof( value ), $"A positive value instead of ({ value }) is required." ) );
            }
        }
    }
}
