using System;
using System.Diagnostics;

using Reth.Protocols.Dialogs;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Protocols
{
    public sealed class RawMessage:Message, IEquatable<RawMessage>
    {
        public static bool operator==( RawMessage left, RawMessage right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( RawMessage left, RawMessage right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( RawMessage left, RawMessage right )
		{
            bool result = Message.Equals( left, right );

            if( result == true )
            {
                result &= String.Equals( left.Value, right.Value, StringComparison.OrdinalIgnoreCase );
            }

            return result;
		}

        private String value = String.Empty;

        public RawMessage( String value )
        :
            base( DialogName.Raw, MessageId.DefaultId )
        {
            this.Value = value;
        }

        public String Value
        {
            get{ return this.value; }

            private set
            {
                value.ThrowIfNull();

                this.value = value;
            }
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as RawMessage );
		}
		
		public bool Equals( RawMessage other )
		{
            return RawMessage.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public override String ToString()
        {
            return this.Value;
        }

        public override void Dispatch( IMessageDispatcher dispatcher )
        {
            Debug.Assert( false, "false" );

            throw new InvalidOperationException( "Dispatching of raw messages is not supported." );
        }
    }
}