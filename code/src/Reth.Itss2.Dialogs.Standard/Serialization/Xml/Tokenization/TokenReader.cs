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
using System.Collections.Generic;
using System.Linq;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Xml.Tokenization
{
    internal class TokenReader
    {
        public TokenReader()
        :
            this( new OutOfMessageTokenSequence() )
        {
        }

        private TokenReader( TokenSequence sequence )
        {
            this.Sequence = sequence;
        }

        public TokenSequence Sequence
        {
            get;
        }

        public TokenReader Read( ref ReadOnlySequence<byte> buffer, out ReadOnlySequence<byte>[] tokens )
        {
            TokenSequence sequence = this.Sequence.Traverse( ref buffer );

            IReadOnlyList<TokenBoundary> boundaries = sequence.Transitions.GetBoundaries();

            if( boundaries.Count > 0 )
            {
                List<ReadOnlySequence<byte>> slices = new List<ReadOnlySequence<byte>>();

                foreach( TokenBoundary boundary in boundaries )
                {
                    boundary.GetToken( ref buffer, out ReadOnlySequence<byte> token );

                    slices.Add( token );
                }

                buffer = buffer.Slice( boundaries.Last<TokenBoundary>().End.Match.EndIndex );

                tokens = slices.ToArray();

                return new TokenReader();
            }else
            {
                tokens = new ReadOnlySequence<byte>[]{};

                return new TokenReader( sequence );
            }
        }

        public override String ToString()
        {
            return this.Sequence.ToString();
        }
    }
}
