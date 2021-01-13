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

using System.Buffers;
using System.Collections.Generic;
using System.Linq;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Xml.Tokenization
{
    internal class WithinCommentTokenSequence:TokenSequence
    {
        public WithinCommentTokenSequence( IEnumerable<TokenStateTransition> transitions, long position )
        :
            base( transitions, position )
        {
        }

        public override TokenState State => TokenState.WithinComment;

        public override TokenSequence Traverse( ref ReadOnlySequence<byte> buffer )
        {
            TokenSequence result = this;

            TokenPatternMatch match = buffer.FirstMatch( TokenPattern.EndOfComment, this.Position );

            if( match.Success )
            {
                TokenStateTransition lastTransition = this.Transitions.LastOrDefault();

                if( lastTransition is not null )
                {
                    TokenState fromState = lastTransition.FromState;

                    List<TokenStateTransition> transitions = new List<TokenStateTransition>( this.Transitions.Count + 1 );

                    TokenStateTransition transition = new TokenStateTransition( this.State,
                                                                                fromState,
                                                                                match   );

                    transitions.AddRange( this.Transitions );
                    transitions.Add( transition );

                    result = TokenSequenceFactory.Create( fromState, transitions, transition.Match.EndIndex ).Traverse( ref buffer );
                }
            }

            return result;
        }
    }
}
