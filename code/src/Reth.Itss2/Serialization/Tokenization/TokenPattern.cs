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
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Reth.Itss2.Serialization.Tokenization
{
    internal abstract class TokenPattern<TInstance>:IEquatable<TInstance>, ITokenPattern
        where TInstance:TokenPattern<TInstance>
    {
        public static bool operator==( TokenPattern<TInstance>? left, TokenPattern<TInstance>? right )
		{
			return TokenPattern<TInstance>.Equals( left, right );
		}
		
		public static bool operator!=( TokenPattern<TInstance>? left, TokenPattern<TInstance>? right )
		{
			return !TokenPattern<TInstance>.Equals( left, right );
		}

        public static bool Equals( TokenPattern<TInstance>? left, TokenPattern<TInstance>? right )
		{
            return String.Equals( left?.ToString(), right?.ToString(), StringComparison.OrdinalIgnoreCase );
		}

        protected TokenPattern( Encoding encoding, String value )
        {
            this.Encoding = encoding;
            this.Value = ImmutableArray.Create<byte>( encoding.GetBytes( value ) );
        }

        private Encoding Encoding
        {
            get;
        }

        public ImmutableArray<byte> Value
        {
            get;
        }

        public override bool Equals( Object? obj )
		{
			return this.Equals( obj as TInstance );
		}
		
		public bool Equals( TInstance? other )
		{
            return TokenPattern<TInstance>.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

        public override String? ToString()
        {
            return this.Encoding.GetString( this.Value.ToArray() );
        }
    }
}
