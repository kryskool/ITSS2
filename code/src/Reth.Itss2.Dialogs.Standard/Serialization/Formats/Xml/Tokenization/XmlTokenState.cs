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

using Reth.Itss2.Dialogs.Standard.Diagnostics;
using Reth.Itss2.Dialogs.Standard.Serialization.Tokenization;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Formats.Xml.Tokenization
{
    internal abstract class XmlTokenState:ITokenState
    {
        protected XmlTokenState()
        {
        }

        public abstract ITokenStateTransition? GetNextTransition(   ITokenStateTransition? previousTransition,
                                                                    ref ReadOnlySequence<byte> buffer,
                                                                    ref long position   );

        protected ITokenStateTransition? GetNextTransition( ITokenStateTransition? previousTransition,
                                                            ITokenPatternMatch nextTransitionMatch,
                                                            ref long position   )
        {
            ITokenStateTransition? result = null;

            position = nextTransitionMatch.EndIndex;

            ITokenPattern pattern = nextTransitionMatch.Pattern;

            if( XmlTokenPatterns.BeginOfMessage.Equals( pattern ) == true )
            {
                result = new XmlTokenStateTransition(   this,
                                                        XmlTokenStates.WithinMessage,
                                                        nextTransitionMatch   );
            }else if( XmlTokenPatterns.EndOfMessage.Equals( pattern ) == true )
            {
                if( previousTransition is not null )
                {
                    result = new XmlTokenStateTransition(   this,
                                                            previousTransition.From,
                                                            nextTransitionMatch   );
                }
            }else if( XmlTokenPatterns.BeginOfData.Equals( pattern ) == true )
            {
                result = new XmlTokenStateTransition(   this,
                                                        XmlTokenStates.WithinData,
                                                        nextTransitionMatch   );
            }else if( XmlTokenPatterns.EndOfData.Equals( pattern ) == true )
            {
                if( previousTransition is not null )
                {
                    result = new XmlTokenStateTransition(   this,
                                                            previousTransition.From,
                                                            nextTransitionMatch   );
                }
            }else
            {
                throw Assert.Exception( new ArgumentException( $"Unknown token pattern in match '{ nextTransitionMatch.Pattern }'.", nameof( nextTransitionMatch ) ) );
            }

            return result;
        }
    }
}
