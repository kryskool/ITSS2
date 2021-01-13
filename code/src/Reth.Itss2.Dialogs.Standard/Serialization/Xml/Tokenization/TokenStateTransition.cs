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
using System.Diagnostics;
using System.Text;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Xml.Tokenization
{
    internal class TokenStateTransition
    {
        public TokenStateTransition(    TokenState fromState,
                                        TokenState toState,
                                        TokenPatternMatch match )
        {
            Debug.Assert( fromState != toState, $"{ nameof( fromState ) } != { nameof( toState ) }" );

            if( fromState == toState )
            {
                throw new InvalidOperationException( $"A state transition can only occur between different states. Equal states: '{ fromState }'" );
            }

            this.FromState = fromState;
            this.ToState = toState;
            this.Match = match;
        }

        public TokenState FromState
        {
            get;
        }

        public TokenState ToState
        {
            get;
        }

        public TokenPatternMatch Match
        {
            get;
        }

        public override String ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append( this.FromState.ToString() );
            result.Append( " -> " );
            result.Append( this.ToState.ToString() );
            result.Append( " with " );
            result.Append( this.Match.ToString() );

            return result.ToString();
        }
    }
}
