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
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Xml.Tokenization
{
    internal static class ExtensionMethods
    {
        private const char Space = ( char )0x20;
        private const char Tab = ( char )0x09;
        private const char LineFeed = ( char )0x0A;
        private const char CarriageReturn = ( char )0x0D;
        
        public static String Escape( this String instance )
        {
            String result = instance;

            if( String.IsNullOrWhiteSpace( instance ) == false )
            {
                StringBuilder buffer = new StringBuilder( ( int )( instance.Length * 1.3 ) );
                
                foreach( char character in instance )
                {
                    if( character < ExtensionMethods.Space )
                    {
                        switch( character )
                        {
                            case ExtensionMethods.Tab:
                            case ExtensionMethods.LineFeed:
                            case ExtensionMethods.CarriageReturn:
                                buffer.Append( character );
                                break;

                            default:
                                buffer.AppendFormat( CultureInfo.InvariantCulture, @"\x{0:X2}", Convert.ToByte( character ) );
                                break;
                        }
                    }else
                    {
                        buffer.Append( character );
                    }
                }

                result = buffer.ToString();
            }

            return result;
        }

        public static String Unescape( this String instance )
        {
            String result = instance;

            if( String.IsNullOrWhiteSpace( instance ) == false )
            {                              
                result = Regex.Replace( instance,
                                        @"\\x([0-9A-F]{2})",
                                        new MatchEvaluator( ( Match match ) =>
                                                            {
                                                                byte value = byte.Parse( match.Groups[ 1 ].Value, NumberStyles.HexNumber, CultureInfo.InvariantCulture );

                                                                String replacement = XmlSerializationSettings.Encoding.GetString( new byte[]{ value } );

                                                                return replacement;
                                                            } ),
                                        RegexOptions.IgnoreCase | RegexOptions.Compiled );
            }

            return result;
        }

        internal static TokenPatternMatch FirstMatch( this ReadOnlySequence<byte> instance, TokenPattern pattern )
        {
            return instance.FirstMatch( pattern, 0 );
        }

        internal static TokenPatternMatch FirstMatch( this ReadOnlySequence<byte> instance, TokenPattern pattern, long startIndex )
        {
            SequenceReader<byte> sequenceReader = new SequenceReader<byte>( instance );

            sequenceReader.Advance( startIndex );

            ReadOnlySpan<byte> givenPattern = new ReadOnlySpan<byte>( pattern.Value );

            bool found = false;
            bool hasRead = true;

            long matchIndex = -1;

            do
            {
                hasRead = sequenceReader.TryReadTo( out _, givenPattern, false );

                if( hasRead == true )
                {
                    found = true;
                    matchIndex = sequenceReader.Consumed;

                    for( int i = 0; i < pattern.Count; i++ )
                    {
                        if( sequenceReader.TryRead( out byte value ) )
                        {
                            if( value != pattern[ i ] )
                            {
                                found = false;
                                matchIndex = -1;
                                break;
                            }
                        }else
                        {
                            found = false;
                            matchIndex = -1;
                            break;
                        }
                    }
                }
            }while( !found && hasRead );

            return new TokenPatternMatch( pattern, matchIndex );
        }

        internal static IReadOnlyList<TokenBoundary> GetBoundaries( this IReadOnlyList<TokenStateTransition> instance )
        {
            List<TokenBoundary> result = new List<TokenBoundary>();

            TokenStateTransition? startTransition = null;
            
            foreach( TokenStateTransition transition in instance )
            {
                if( transition.FromState == TokenState.OutOfMessage &&
                    transition.ToState == TokenState.WithinMessage  )
                {
                    startTransition = transition;
                }else if(   transition.FromState == TokenState.WithinMessage &&
                            transition.ToState == TokenState.OutOfMessage  )
                {
                    if( startTransition is not null )
                    {
                        result.Add( new TokenBoundary( startTransition, transition ) );

                        startTransition = null;
                    }
                }
            }

            return result;
        }
    }
}
