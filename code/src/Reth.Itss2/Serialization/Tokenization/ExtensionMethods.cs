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
using System.Text;

namespace Reth.Itss2.Serialization.Tokenization
{
    internal static class ExtensionMethods
    {
        public static String? ToString( this ReadOnlySequence<byte> instance, Encoding encoding )
        {
            return encoding.GetString( instance );   
        }

        public static ITokenPatternMatch? Aggregate( this IEnumerable<ITokenPatternMatch?> instance )
        {
            return  instance.Aggregate( seed:null,
                                        ( ITokenPatternMatch? firstOccurence, ITokenPatternMatch? item ) =>
                                        {
                                            ITokenPatternMatch? result = firstOccurence;

                                            if( firstOccurence is not null &&
                                                item is not null    )
                                            {
                                                if( item.StartIndex < firstOccurence.StartIndex )
                                                {
                                                    result = item;
                                                }
                                            }else if( item is not null )
                                            {
                                                result = item;
                                            }else if( firstOccurence is not null )
                                            {
                                                result = firstOccurence;
                                            }

                                            return result;
                                        }   );
        }

        public static bool TryGetMessage<TState>(   this IEnumerable<ITokenTransition<TState>> instance,
                                                    ref SequenceReader<byte> sequenceReader,
                                                    out ReadOnlySequence<byte> token,
                                                    out ITokenTransition<TState>? firstTransition,
                                                    out ITokenTransition<TState>? lastTransition    )
            where TState:Enum
        {
            token = new ReadOnlySequence<byte>();

            bool result = false;

            firstTransition = instance.SkipWhile(   ( ITokenTransition<TState> item ) =>
                                                    {
                                                        return !( item.IsMessageBegin() );
                                                    }   ).FirstOrDefault();

            lastTransition = instance.Reverse().SkipWhile(  ( ITokenTransition<TState> item ) =>
                                                            {
                                                                return !( item.IsMessageEnd() );
                                                            }   ).FirstOrDefault();

            if( firstTransition is not null &&
                lastTransition is not null &&
                TokenTransition<TState>.EmbracesMessage( firstTransition, lastTransition ) == true )
            {
                var boundaries = TokenTransition<TState>.GetMessageBoundaries( firstTransition, lastTransition );

                token = new ReadOnlySequence<byte>( sequenceReader.Sequence.Slice( boundaries.StartIndex, boundaries.Length ).ToArray() );

                result = true;
            }else
            {
                firstTransition = null;
                lastTransition = null;
            }

            return result;
        }
    }
}
