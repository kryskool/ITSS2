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

using Reth.Itss2.Dialogs.Standard.Diagnostics;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Tokenization
{
    internal abstract class TokenReader:ITokenReader
    {
        protected TokenReader()
        {
        }

        protected abstract ITokenReaderState CreateTokenReaderState( long position, Stack<ITokenStateTransition> transitions );

        public bool Read(   ref ITokenReaderState state,
                            ref ReadOnlySequence<byte> buffer,
                            out ReadOnlySequence<byte> token,
                            out long consumedBytes  )
        {
            token = new ReadOnlySequence<byte>();
            consumedBytes = 0;

            bool messageFound = false;

            ITokenState tokenState = state.GetTokenState();

            Stack<ITokenStateTransition> transitions = state.Transitions;
            long position = state.Position;
            
            ITokenStateTransition? currentTransition = null;
            
            do
            {
                currentTransition = this.GetNextTransition( currentTransition,
                                                            tokenState,
                                                            transitions,
                                                            ref buffer,
                                                            ref position );

                if( currentTransition is not null )
                {
                    ExecutionLogProvider.Log.LogTrace( currentTransition.ToString() );

                    tokenState = currentTransition.To;

                    messageFound = this.HandleTransition( currentTransition, transitions, ref buffer, ref token, out consumedBytes );
                }
            }while( currentTransition is not null && messageFound == false );

            state = this.CreateTokenReaderState( position, transitions );

            return messageFound;
        }

        private bool HandleTransition(  ITokenStateTransition currentTransition,
                                        Stack<ITokenStateTransition> transitions,
                                        ref ReadOnlySequence<byte> buffer,
                                        ref ReadOnlySequence<byte> token,
                                        out long consumedBytes  )
        {
            consumedBytes = 0;

            bool result = false;

            if( transitions.Count == 1 )
            {
                ITokenStateTransition previousTransition = transitions.Peek();

                if( previousTransition.IsMessageBegin() == true &&
                    currentTransition.IsMessageEnd() == true  )
                {
                    if( this.GetMessageBoundary(    previousTransition,
                                                    currentTransition,
                                                    ref buffer,
                                                    ref token,
                                                    out consumedBytes  ) == true )
                    {
                        transitions.Pop();

                        result = true;
                    }
                }else
                {
                    this.PushOrPop( currentTransition, transitions );
                }
            }else
            {
                this.PushOrPop( currentTransition, transitions );
            }

            return result;
        }

        private void PushOrPop( ITokenStateTransition currentTransition,
                                Stack<ITokenStateTransition> transitions    )
        {
            if( transitions.Count > 0 )
            {
                ITokenStateTransition previousTransition = transitions.Peek();

                if( ( previousTransition.To == currentTransition.From ) &&
                    ( previousTransition.From == currentTransition.To ) &&
                    ( previousTransition.Match.Pattern.Equals( currentTransition.Match.Pattern ) == false ) )
                {
                    transitions.Pop();
                }else
                {
                    transitions.Push( currentTransition );
                }
            }else
            {
                if( currentTransition.IsMessageBegin() == true )
                {
                    transitions.Push( currentTransition );
                }
            }
        }

        private ITokenStateTransition? GetNextTransition(   ITokenStateTransition? currentTransition,
                                                            ITokenState tokenState,
                                                            Stack<ITokenStateTransition> transitions,
                                                            ref ReadOnlySequence<byte> buffer,
                                                            ref long position   )
        {
            ITokenStateTransition? previousTransition = currentTransition;

            if( transitions.Count > 0 )
            {
                previousTransition = transitions.Peek();
            }

            return tokenState.GetNextTransition( previousTransition, ref buffer, ref position );
        }

        private bool GetMessageBoundary(    ITokenStateTransition previousTransition,
                                            ITokenStateTransition currentTransition,
                                            ref ReadOnlySequence<byte> buffer,
                                            ref ReadOnlySequence<byte> token,
                                            out long consumedBytes  )
        {
            consumedBytes = 0;

            bool result = false;

            if( TokenStateTransition.IsMessageBoundary( previousTransition, currentTransition ) == true )
            {
                long startIndex = previousTransition.Match.StartIndex;
                long length = currentTransition.Match.EndIndex - startIndex;

                token = buffer.Slice( startIndex, length );
                consumedBytes = startIndex + length;

                result = true;
            }

            return result;
        }
    }
}
