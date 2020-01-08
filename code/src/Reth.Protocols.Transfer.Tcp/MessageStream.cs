using System;
using System.IO;
using System.Net.Sockets;

using Reth.Protocols.Diagnostics;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Protocols.Transfer.Tcp
{
    internal class MessageStream:Stream
    {
        private volatile bool isDisposed;

        public MessageStream( NetworkStream baseStream )
        {
            baseStream.ThrowIfNull();

            this.BaseStream = baseStream;
        }

        public NetworkStream BaseStream
        {
            get; private set;
        }

        public override bool CanRead
        {
            get
            {
                return this.BaseStream.CanRead;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return this.BaseStream.CanSeek;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return this.BaseStream.CanWrite;
            }
        }

        public override long Length
        {
            get
            {
                return this.BaseStream.Length;
            }
        }

        public override long Position
        {
            get
            {
                return this.BaseStream.Position;
            }

            set
            {
                this.BaseStream.Position = value;
            }
        }

        public override void Flush()
        {
            this.BaseStream.Flush();
        }

        public override long Seek( long offset, SeekOrigin origin )
        {
            return this.BaseStream.Seek( offset, origin );
        }

        public override void SetLength( long value )
        {
            this.BaseStream.SetLength( value );
        }

        public override int Read( byte[] buffer, int offset, int count )
        {
            Nullable<int> read = 0;
            
            try
            {
                read = this.BaseStream?.Read( buffer, offset, count );
            }catch( IOException ex )
            {
                SocketException socketException = ex.InnerException as SocketException;

                if( socketException.SocketErrorCode != SocketError.Interrupted )
                {
                    throw;
                }
            }

            return read.GetValueOrDefault();
        }

        public override void Write( byte[] buffer, int offset, int count )
        {
            this.BaseStream.Write( buffer, offset, count );
        }

        protected override void Dispose( bool disposing )
        {
            ExecutionLogProvider.LogInformation( "Disposing message stream." );

            if( this.isDisposed == false )
            {
                if( disposing == true )
                {
                    this.BaseStream.Dispose();
                }

                this.isDisposed = true;
            }

            base.Dispose( disposing );
        }
    }
}