using System;
using System.Diagnostics;
using System.IO;
using System.Text;

using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Protocols.Serialization.Xml.Parsing
{
    internal class MessageBuffer:IDisposable
    {
        private volatile bool isDisposed;

        public MessageBuffer( int defaultCapacity, int maximumSize )
        {
            Debug.Assert( maximumSize >= 0, $"{ nameof( maximumSize ) } >= 0" );

            if( maximumSize < 0 )
            {
                throw new ArgumentOutOfRangeException( nameof( maximumSize ), maximumSize, "The maximum message size cannot be negative." );
            }

            this.Stream = new MemoryStream( defaultCapacity );

            this.DefaultCapacity = defaultCapacity;
            this.MaximumSize = maximumSize;
        }

        ~MessageBuffer()
        {
            this.Dispose( false );
        }

        private MemoryStream Stream
        {
            get; set;
        }

        private int DefaultCapacity
        {
            get;
        }

        private int MaximumSize
        {
            get;
        }

        public long Position
        {
            get
            {
                return this.Stream.Position;
            }

            set
            {
                this.Stream.Position = value;
            }
        }

        public void Append( MessageBlock messageBlock )
        {
            if( !( messageBlock is null ) )
            {
                int actualSize = ( int )( this.Stream.Length + messageBlock.Length );

                Debug.Assert( actualSize <= this.MaximumSize, $"{ nameof( actualSize ) } <= this.{ nameof( MessageBuffer.MaximumSize ) }" );

                if( actualSize > this.MaximumSize )
                {
                    throw new InvalidOperationException( $"Maximum buffer size exceeded. Maximum size: '{ this.MaximumSize }' Bytes, actual size: '{ actualSize }' Bytes" );
                }

                long oldPosition = this.Position;

                this.Position = this.Stream.Length;
                this.Stream.Write(  messageBlock.Data,
                                    0,
                                    messageBlock.Length  );

                this.Position = oldPosition;
            }
        }

        public void Shift( MessageBlockPatternMatch start )
        {
            if( !( start is null ) )
            {
                byte[] buffer = this.Stream.GetBuffer();

                int startIndex = start.StartIndex + start.Length;
                int length = ( int )( this.Stream.Length - startIndex );

                Array.Copy( buffer,
                            startIndex,
                            buffer,
                            0,
                            length   );

                this.Stream.SetLength( length );

                this.Shrink();
                this.Clear( length );
            
                this.Position = 0;
            }
        }

        private void Shrink()
        {
            if( this.Stream.Capacity > this.DefaultCapacity )
            {
                MemoryStream newStream = new MemoryStream( Math.Max( this.DefaultCapacity, ( int )( this.Stream.Length ) ) );

                long length = this.Stream.Length;

                newStream.SetLength( length );

                Array.Copy( this.Stream.GetBuffer(),
                            0,
                            newStream.GetBuffer(),
                            0,
                            length    );

                this.Stream.Dispose();
                this.Stream = newStream;   
            }
        }

        private void Clear( int startIndex )
        {
            byte[] buffer = this.Stream.GetBuffer();

            Array.Clear(    buffer,
                            startIndex,
                            buffer.Length - startIndex );
        }

        public String GetString( MessageExtractorStateTransition endTransition )
        {           
            String result = String.Empty;

            if( !( endTransition is null ) )
            {                    
                byte[] buffer = this.Stream.GetBuffer();

                return Encoding.UTF8.GetString( buffer,
                                                0,
                                                endTransition.ToMatch.StartIndex  );
            }

            return result;
        }

        public String GetString(    MessageExtractorStateTransition firstTransition,
                                    MessageExtractorStateTransition secondTransition  )
        {
            firstTransition.ThrowIfNull();
            secondTransition.ThrowIfNull();

            int lengthOfMessageInBytes = MessageExtractorStateTransition.Distance( firstTransition, secondTransition );
                    
            byte[] buffer = this.Stream.GetBuffer();

            return Encoding.UTF8.GetString( buffer,
                                            firstTransition.ToMatch.StartIndex,
                                            lengthOfMessageInBytes  );
        }

        public MessageBlockPatternMatch Lookup( params MessageBlockPattern[] patterns )
        {
            MessageBlockPatternMatch result = MessageBlockPatternMatch.None;
            MessageBlockPatternMatch match = MessageBlockPatternMatch.None;

            foreach( MessageBlockPattern pattern in patterns )
            {
                long oldPosition = this.Position;

                match = this.Lookup( pattern );

                if( match.Success == true )
                {
                    if( result.Success == true )
                    {
                        if( match.StartIndex < result.StartIndex )
                        {
                            result = match;
                        }
                    }else
                    {
                        result = match;
                    }
                }

                this.Position = oldPosition;
            }

            if( result.Success == true )
            {
                this.Position = result.StartIndex + result.Pattern.Count;
            }

            return result;
        }

        public MessageBlockPatternMatch Lookup( MessageBlockPattern pattern )
        {
            MessageBlockPatternMatch result = MessageBlockPatternMatch.None;

            if( !( pattern is null ) )
            {
                byte[] patternValue = pattern.Value;
                byte[] bufferValue = this.Stream.GetBuffer();

                long oldPosition = this.Position;

                int patternLength = pattern.Count;

                int startIndex = Math.Max( ( int )( oldPosition - ( patternLength - 1 ) ), 0 );
                int length = bufferValue.Length;

                int foundIndex = -1;

                for( int i = startIndex; i < length; i++ )
                {
                    foundIndex = i;

                    for( int j = 0; j < patternValue.Length; j++ )
                    {
                        if( bufferValue[ i + j ] != patternValue[ j ] )
                        {
                            foundIndex = -1;
                            break;
                        }
                    }

                    if( foundIndex >= 0 )
                    {
                        result = new MessageBlockPatternMatch( pattern, foundIndex );

                        this.Position = foundIndex + pattern.Count;
                        break;
                    }
                }
            }

            return result;
        }

        public override String ToString()
        {
            return Encoding.UTF8.GetString( this.Stream.GetBuffer() );
        }

        public void Dispose()
        {
            this.Dispose( true );

            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
            if( this.isDisposed == false )
            {
                if( disposing == true )
                {
                    this.Stream.Dispose();
                }

                this.isDisposed = true;
            }
        }
    }
}
