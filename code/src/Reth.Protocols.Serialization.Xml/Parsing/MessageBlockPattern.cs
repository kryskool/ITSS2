using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Protocols.Serialization.Xml.Parsing
{
    internal class MessageBlockPattern:IEquatable<MessageBlockPattern>, IReadOnlyCollection<byte>
    {
        public static readonly MessageBlockPattern BeginOfMessage = new MessageBlockPattern( "<WWKS" );
        public static readonly MessageBlockPattern EndOfMessage = new MessageBlockPattern( "</WWKS>" );

        public static readonly MessageBlockPattern BeginOfComment = new MessageBlockPattern( "<!--" );
        public static readonly MessageBlockPattern EndOfComment = new MessageBlockPattern( "-->" );

        public static readonly MessageBlockPattern BeginOfData = new MessageBlockPattern( "<![CDATA[" );
        public static readonly MessageBlockPattern EndOfData = new MessageBlockPattern( "]]>" );

        public static readonly MessageBlockPattern BeginOfDeclaration = new MessageBlockPattern( "<?xml" );
        public static readonly MessageBlockPattern EndOfDeclaration = new MessageBlockPattern( "?>" );

        public static readonly MessageBlockPattern Empty = new MessageBlockPattern( "" );

        public static bool operator==( MessageBlockPattern left, MessageBlockPattern right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( MessageBlockPattern left, MessageBlockPattern right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( MessageBlockPattern left, MessageBlockPattern right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        return String.Equals( left.ToString(), right.ToString(), StringComparison.OrdinalIgnoreCase );
                                                    }   );
		}

        private MessageBlockPattern( String value )
        {
            value.ThrowIfNull();

            this.Value = Encoding.UTF8.GetBytes( value );
        }

        public byte[] Value
        {
            get;
        }

        public int Count
        {
            get{ return this.Value.Length; }
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as MessageBlockPattern );
		}
		
		public bool Equals( MessageBlockPattern other )
		{
            return MessageBlockPattern.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

        public IEnumerator<byte> GetEnumerator()
        {
            return this.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Value.GetEnumerator();
        }

        public override String ToString()
        {
            return Encoding.UTF8.GetString( this.Value );
        }
    }
}
