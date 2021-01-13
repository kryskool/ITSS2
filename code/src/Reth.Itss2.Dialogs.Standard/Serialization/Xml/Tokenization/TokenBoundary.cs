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
using System.Buffers;
using System.Text;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Xml.Tokenization
{
    internal class TokenBoundary
    {
        public TokenBoundary( TokenStateTransition start, TokenStateTransition end )
        {
            this.Start = start;
            this.End = end;
        }

        public TokenStateTransition Start
        {
            get;
        }

        public TokenStateTransition End
        {
            get;
        }

        public void GetToken( ref ReadOnlySequence<byte> buffer, out ReadOnlySequence<byte> token )
        {
            long startIndex = this.Start.Match.StartIndex;
            long length = this.End.Match.EndIndex - startIndex;

            token = buffer.Slice( startIndex, length );
        }

        public override String ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append( "From [" );
            result.Append( this.Start );
            result.Append( "] to [" );
            result.Append( this.End );
            result.Append( "]" );

            return result.ToString();
        }
    }
}
