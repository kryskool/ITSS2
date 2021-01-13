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
using System.Collections.Generic;

using Reth.Itss2.Dialogs.Standard.Diagnostics;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Xml.Tokenization
{
    internal static class TokenSequenceFactory
    {
        public static TokenSequence Create( TokenState tokenState, IEnumerable<TokenStateTransition> transitions, long position )
        {
            switch( tokenState )
            {
                case TokenState.OutOfMessage:
                    return new OutOfMessageTokenSequence( transitions, position );

                case TokenState.WithinMessage:
                    return new WithinMessageTokenSequence( transitions, position );

                case TokenState.WithinComment:
                    return new WithinCommentTokenSequence( transitions, position );

                case TokenState.WithinData:
                    return new WithinDataTokenSequence( transitions, position );
            }

            throw Assert.Exception( new ArgumentException( $"Unknown token state '{ nameof( tokenState ) }'.", nameof( tokenState ) ) );
        }
    }
}
