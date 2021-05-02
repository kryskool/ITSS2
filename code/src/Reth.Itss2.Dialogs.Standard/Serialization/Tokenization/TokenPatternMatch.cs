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
using System.Linq;
using System.Text;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Tokenization
{
    internal class TokenPatternMatch:IEquatable<TokenPatternMatch>, ITokenPatternMatch
    {
        public static ITokenPatternMatch? GetFirstMatch(    IEnumerable<ITokenPattern> patterns,
                                                            ref ReadOnlySequence<byte> buffer,
                                                            ref long position   )
        {
            List<ITokenPatternMatch?> matches = new List<ITokenPatternMatch?>();

            foreach( ITokenPattern pattern in patterns )
            {
                matches.Add( pattern.GetFirstMatch( ref buffer, position ) );
            }

            return TokenPatternMatch.GetFirstMatch( matches );
        }

        public static ITokenPatternMatch? GetFirstMatch( IEnumerable<ITokenPatternMatch?> matches )
        {
            return  matches.Aggregate(  seed:null,
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

        public static bool operator==( TokenPatternMatch? left, TokenPatternMatch? right )
		{
			return TokenPatternMatch.Equals( left, right );
		}
		
		public static bool operator!=( TokenPatternMatch? left, TokenPatternMatch? right )
		{
			return !TokenPatternMatch.Equals( left, right );
		}

        public static bool Equals( TokenPatternMatch? left, TokenPatternMatch? right )
		{
            bool result = EqualityComparer<ITokenPattern?>.Default.Equals( left?.Pattern, right?.Pattern );

            result &= ( result ? EqualityComparer<long?>.Default.Equals( left?.StartIndex, right?.StartIndex ) : false );
                                                        
            return result;
		}

        public TokenPatternMatch( ITokenPattern pattern, long startIndex )
        {
            startIndex.ThrowIfNegative();

            this.Pattern = pattern;
            this.StartIndex = startIndex;
        }

        public ITokenPattern Pattern
        {
            get;
        }

        public long StartIndex
        {
            get;
        }

        public long EndIndex => this.StartIndex + this.Length;

        public long Length => this.Pattern.Length;
        
        public override bool Equals( Object obj )
		{
			return this.Equals( obj as TokenPatternMatch );
		}
		
		public bool Equals( TokenPatternMatch? other )
		{
            return TokenPatternMatch.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return this.Pattern.GetHashCode();
		}

        public override String ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append( "'" );
            result.Append( this.Pattern.ToString() );
            result.Append( "' at '" );
            result.Append( this.StartIndex.ToString( CultureInfo.InvariantCulture ) );
            result.Append( "'" );

            return result.ToString();
        }
    }
}
