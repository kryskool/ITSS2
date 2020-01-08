using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Protocols.Serialization.Xml.Parsing
{
    internal class MessageBlock:IReadOnlyCollection<byte>
    {
        public MessageBlock( byte[] data, int length )
        {
            data.ThrowIfNull();

            Debug.Assert( length >= 0, $"{ nameof( length ) } >= 0" );

            if( length < 0 )
            {
                throw new ArgumentOutOfRangeException( nameof( length ), length, "The length of a message block cannot be negative." );
            }

            this.Data = data;
            this.Length = Math.Min( length, data.Length );
        }

        public byte[] Data
        {
            get;
        }

        public int Length
        {
            get;
        }

        int IReadOnlyCollection<byte>.Count
        {
            get{ return this.Data.Length; }
        }

        public IEnumerator<byte> GetEnumerator()
        {
            return this.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Data.GetEnumerator();
        }

        public override String ToString()
        {
            return Encoding.UTF8.GetString( this.Data );
        }
    }
}
