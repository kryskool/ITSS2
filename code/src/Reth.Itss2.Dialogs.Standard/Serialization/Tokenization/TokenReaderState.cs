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

namespace Reth.Itss2.Dialogs.Standard.Serialization.Tokenization
{
    internal abstract class TokenReaderState:ITokenReaderState
    {
        protected TokenReaderState( ITokenState defaultState )
        {
            this.DefaultState = defaultState;
        }

        protected TokenReaderState( ITokenState defaultState,
                                    long position,
                                    Stack<ITokenStateTransition> transitions    )
        {
            position.ThrowIfNegative();

            this.DefaultState = defaultState;
            this.Position = position;
            this.Transitions = transitions;
        }

        public ITokenState DefaultState
        {
            get;
        }

        public long Position
        {
            get;
        }

        public Stack<ITokenStateTransition> Transitions
        {
            get;
        } = new Stack<ITokenStateTransition>();

        public ITokenState GetTokenState()
        {
            if( this.Transitions.Count > 0 )
            {
                return this.Transitions.Peek().To;
            }

            return this.DefaultState;
        }

        public override String ToString()
        {
            return $"{ this.Position }";
        }
    }
}
