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

using Reth.Itss2.Diagnostics;

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages
{
    public abstract class LimitedStringId<TInstance>:IEquatable<TInstance>
        where TInstance:LimitedStringId<TInstance>
    {
        public const int MaxLength = 64;

        protected static bool TryParse( String? value, Func<String, TInstance> factory, out TInstance? result )
        {
            result = default( TInstance );

            bool success = false;

            if( String.IsNullOrEmpty( value ) == false )
            {
                result = factory( value );

                success = true;
            }

            return success;
        }

        public static bool operator==( LimitedStringId<TInstance>? left, LimitedStringId<TInstance>? right )
		{
			return LimitedStringId<TInstance>.Equals( left, right );
		}
		
		public static bool operator!=( LimitedStringId<TInstance>? left, LimitedStringId<TInstance>? right )
		{
			return !LimitedStringId<TInstance>.Equals( left, right );
		}

        public static bool Equals( LimitedStringId<TInstance>? left, LimitedStringId<TInstance>? right )
		{
            return String.Equals( left?.Value, right?.Value, StringComparison.OrdinalIgnoreCase );
		}
        
		protected LimitedStringId( String value )
		{
            if( value.Length <= 0 )
            {
                throw Assert.Exception( new ArgumentOutOfRangeException( nameof( value ), $"ID value of type '{ this.GetType().FullName }' must not be empty." ) );
            }

            if( value.Length > LimitedStringId<TInstance>.MaxLength )
            {
                throw Assert.Exception( new ArgumentOutOfRangeException( nameof( value ), $"ID value of type '{ this.GetType().FullName }' exceeds maximum length of { LimitedStringId<TInstance>.MaxLength }." ) );
            }

            this.Value = value;
		}

        public String Value
        {
            get;
        }

        public override bool Equals( Object? obj )
		{
			return this.Equals( obj as TInstance );
		}
		
        public bool Equals( TInstance? other )
		{
            return LimitedStringId<TInstance>.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public override String? ToString()
        {
            return this.Value;
        }
    }
}
