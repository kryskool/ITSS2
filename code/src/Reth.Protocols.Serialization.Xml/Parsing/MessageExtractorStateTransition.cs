using System;
using System.Diagnostics;
using System.Text;

using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Protocols.Serialization.Xml.Parsing
{
    internal class MessageExtractorStateTransition
    {
        public static int Distance( MessageExtractorStateTransition first, MessageExtractorStateTransition second )
        {
            first.ThrowIfNull();
            second.ThrowIfNull();

            MessageBlockPatternMatch secondToMatch = second.ToMatch;

            int result = ( secondToMatch.StartIndex + secondToMatch.Length );

            result -= first.ToMatch.StartIndex;

            return result;
        }

        public MessageExtractorStateTransition( MessageExtractorState fromState,
                                                MessageExtractorState toState,
                                                MessageBlockPatternMatch fromMatch,
                                                MessageBlockPatternMatch toMatch   )
        {
            fromMatch.ThrowIfNull();
            toMatch.ThrowIfNull();

            Debug.Assert( fromState != toState, $"{ nameof( fromState ) } != { nameof( toState ) }" );

            if( fromState == toState )
            {
                throw new InvalidOperationException( $"A state transition can only occur between different states. Equal state: '{ fromState }'" );
            }

            this.FromState = fromState;
            this.ToState = toState;
            this.FromMatch = fromMatch;
            this.ToMatch = toMatch;
        }

        public MessageExtractorState FromState
        {
            get;
        }

        public MessageExtractorState ToState
        {
            get;
        }

        public MessageBlockPatternMatch FromMatch
        {
            get;
        }

        public MessageBlockPatternMatch ToMatch
        {
            get;
        }

        public override String ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append( this.FromMatch.ToString() );
            result.Append( " -> " );
            result.Append( this.ToMatch.ToString() );

            return result.ToString();
        }
    }
}
