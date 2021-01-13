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
using System.Collections;
using System.Collections.Generic;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Xml.Tokenization
{
    internal class TokenPattern:IEquatable<TokenPattern>, IReadOnlyCollection<byte>
    {
        public static readonly TokenPattern BeginOfMessage = new TokenPattern( "<WWKS" );
        public static readonly TokenPattern EndOfMessage = new TokenPattern( "</WWKS>" );

        public static readonly TokenPattern BeginOfComment = new TokenPattern( "<!--" );
        public static readonly TokenPattern EndOfComment = new TokenPattern( "-->" );

        public static readonly TokenPattern BeginOfData = new TokenPattern( "<![CDATA[" );
        public static readonly TokenPattern EndOfData = new TokenPattern( "]]>" );

        public static readonly TokenPattern BeginOfDeclaration = new TokenPattern( "<?xml" );
        public static readonly TokenPattern EndOfDeclaration = new TokenPattern( "?>" );

        public static readonly TokenPattern Empty = new TokenPattern( String.Empty );

        public static bool operator==( TokenPattern? left, TokenPattern? right )
		{
			return TokenPattern.Equals( left, right );
		}
		
		public static bool operator!=( TokenPattern? left, TokenPattern? right )
		{
			return !TokenPattern.Equals( left, right );
		}

        public static bool Equals( TokenPattern? left, TokenPattern? right )
		{
            return String.Equals( left?.ToString(), right?.ToString(), StringComparison.OrdinalIgnoreCase );
		}

        private TokenPattern( String value )
        {
            this.Value = XmlSerializationSettings.Encoding.GetBytes( value );
        }

        public byte[] Value
        {
            get;
        }

        public byte this[ int index ]
        {
            get{ return this.Value[ index ]; }
        }

        public int Count
        {
            get{ return this.Value.Length; }
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as TokenPattern );
		}
		
		public bool Equals( TokenPattern? other )
		{
            return TokenPattern.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

        public IEnumerator<byte> GetEnumerator()
        {
            return ( ( IEnumerable<byte> )this.Value ).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Value.GetEnumerator();
        }

        public override String ToString()
        {
            return XmlSerializationSettings.Encoding.GetString( this.Value );
        }
    }
}
