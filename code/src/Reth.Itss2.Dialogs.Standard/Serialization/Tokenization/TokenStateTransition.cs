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
using System.Text;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Tokenization
{
    internal abstract class TokenStateTransition:ITokenStateTransition
    {
        public static bool IsMessageBoundary(   ITokenStateTransition first,
                                                ITokenStateTransition second )
        {
            bool isBegin = first.IsMessageBegin();
            bool isEnd = second.IsMessageEnd();

            return ( isBegin == true ) && ( isEnd == true );
        }

        protected TokenStateTransition( ITokenState from,
                                        ITokenState to,
                                        ITokenPatternMatch match    )
        {
            this.From = from;
            this.To = to;
            this.Match = match;
        }

        public ITokenState From
        {
            get;
        }

        public ITokenState To
        {
            get;
        }

        public ITokenPatternMatch Match
        {
            get;
        }

        public abstract bool IsMessageBegin();
        public abstract bool IsMessageEnd();

        public override String ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append( this.From.ToString() );
            result.Append( " -> " );
            result.Append( this.To.ToString() );
            result.Append( ", by " );
            result.Append( this.Match.ToString() );

            return result.ToString();
        }
    }
}
