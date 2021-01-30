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

namespace Reth.Itss2.Dialogs.Standard.Serialization.Tokenization
{
    internal abstract class TokenPattern<TInstance>:IEquatable<TInstance>, ITokenPattern
        where TInstance:TokenPattern<TInstance>
    {
        public static bool operator==( TokenPattern<TInstance>? left, TokenPattern<TInstance>? right )
		{
			return TokenPattern<TInstance>.Equals( left, right );
		}
		
		public static bool operator!=( TokenPattern<TInstance>? left, TokenPattern<TInstance>? right )
		{
			return !TokenPattern<TInstance>.Equals( left, right );
		}

        public static bool Equals( TokenPattern<TInstance>? left, TokenPattern<TInstance>? right )
		{
            return String.Equals( left?.ToString(), right?.ToString(), StringComparison.OrdinalIgnoreCase );
		}

        protected TokenPattern( byte[] value )
        {
            this.Value = value;
        }

        protected byte[] Value
        {
            get;
        }

        public int Length
        {
            get{ return this.Value.Length; }
        }

        public ITokenPatternMatch? GetFirstMatch( ref ReadOnlySequence<byte> buffer )
        {
            return this.GetFirstMatch( ref buffer, 0, null );
        }

        public ITokenPatternMatch? GetFirstMatch( ref ReadOnlySequence<byte> buffer, long startIndex )
        {
            return this.GetFirstMatch( ref buffer, startIndex, null );
        }

        public ITokenPatternMatch? GetFirstMatch( ref ReadOnlySequence<byte> buffer, long startIndex, char? escape )
        {
            TokenPatternMatch? result = null;

            SequenceReader<byte> sequenceReader = new SequenceReader<byte>( buffer );

            sequenceReader.Advance( startIndex );

            ReadOnlySpan<byte> givenPattern = new ReadOnlySpan<byte>( this.Value );

            bool hasRead = true;        

            ( bool Result, long Index ) match = ( false, -1 );

            if( escape is null )
            {
                do
                {
                    hasRead = sequenceReader.TryReadTo( out _, givenPattern, false );

                    if( hasRead == true )
                    {
                        match = this.GetMatch( ref sequenceReader );
                    }
                }while( match.Result == false && hasRead == true );
            }else
            {
                do
                {
                    hasRead = sequenceReader.TryReadTo( out _, givenPattern, false );

                    if( hasRead == true )
                    {
                        if( this.IsEscaped( escape.Value, ref sequenceReader ) == false )
                        {
                            match = this.GetMatch( ref sequenceReader );
                        }
                    }
                }while( match.Result == false && hasRead == true );   
            }

            if( match.Result == true )
            {
                result = new TokenPatternMatch( this, match.Index );
            }

            return result;
        }

        private ( bool, long ) GetMatch( ref SequenceReader<byte> sequenceReader )
        {
            bool result = true;

            long matchIndex = sequenceReader.Consumed;

            for( int i = 0; i < this.Value.Length; i++ )
            {
                if( sequenceReader.TryRead( out byte value ) == true )
                {
                    if( value != this.Value[ i ] )
                    {
                        result = false;
                        matchIndex = -1;
                        break;
                    }
                }else
                {
                    result = false;
                    matchIndex = -1;
                    break;
                }
            }

            return ( result, matchIndex );
        }

        private bool IsEscaped( char escape, ref SequenceReader<byte> sequenceReader )
        {
            bool result = false;

            if( sequenceReader.Consumed > 0 )
            {
                sequenceReader.Rewind( 1 );
                    
                if( sequenceReader.TryRead( out byte currentValue ) == true )
                {
                    if( currentValue == escape )
                    {
                        result = true;

                        sequenceReader.Advance( 2 );
                    }
                }
            }

            return result;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as TInstance );
		}
		
		public bool Equals( TInstance? other )
		{
            return TokenPattern<TInstance>.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}
    }
}
