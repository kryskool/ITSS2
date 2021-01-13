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
using System.IO;
using System.IO.Pipelines;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Dialogs.Standard.Diagnostics;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Xml.Tokenization
{
    internal class Tokenizer:ITokenizer
    {
        public const int DefaultBufferSize = 4096;

        private bool isDisposed;

        public Tokenizer( Stream baseStream )
        :
            this( baseStream, NullInteractionLog.Instance, Tokenizer.DefaultBufferSize, SerializationSettings.MaximumMessageSize )
        {
        }

        public Tokenizer( Stream baseStream, IInteractionLog interactionLog )
        :
            this( baseStream, interactionLog, Tokenizer.DefaultBufferSize, SerializationSettings.MaximumMessageSize )
        {
        }

        public Tokenizer( Stream baseStream, IInteractionLog interactionLog, int bufferSize, int maximumMessageSize )
        {
            this.BaseStream = baseStream;
            this.InteractionLog = interactionLog;
            this.MaximumMessageSize = maximumMessageSize;

            this.Observable = System.Reactive.Linq.Observable.Create<ReadOnlySequence<byte>>( this.ReadAsync );
            this.PipeReader = PipeReader.Create( this.BaseStream, new StreamPipeReaderOptions( leaveOpen:true, bufferSize:bufferSize ) );
        }

        ~Tokenizer()
        {
            this.Dispose( false );
        }

        public Stream BaseStream
        {
            get;
        }

        public IInteractionLog InteractionLog
        {
            get;
        }

        private int MaximumMessageSize
        {
            get;
        }

        public PipeReader PipeReader
        {
            get;
        }

        private IObservable<ReadOnlySequence<byte>> Observable
        {
            get;
        }

        private ManualResetEventSlim Finished
        {
            get;
        } = new ManualResetEventSlim( initialState:false );

        public IDisposable Subscribe( IObserver<ReadOnlySequence<byte>> observer )
        {
            return this.Observable.Subscribe( observer );
        }

        private void SkipJunk( ref ReadOnlySequence<byte> buffer )
        {
            byte[] junkBytes = buffer.Slice( 0 ).ToArray();
            
            int length = junkBytes.Length;
            
            ExecutionLogProvider.Log.LogWarning( $"Skipped junk of { length } bytes (truncated): '{ junkBytes.Format( XmlSerializationSettings.Encoding ) }'" );
        }

        private bool TryRead( ref ReadOnlySequence<byte> buffer, out ReadOnlySequence<byte>[] result )
        {
            result = new ReadOnlySequence<byte>[]{};

            bool success = false;
            
            long bufferLength = buffer.Length;

            if( bufferLength < this.MaximumMessageSize )
            {
                TokenReader tokenReader = new TokenReader();

                tokenReader = tokenReader.Read( ref buffer, out ReadOnlySequence<byte>[] tokens );

                if( tokenReader.Sequence.State == TokenState.OutOfMessage )
                {
                    result = tokens;

                    success = true;
                }
            }else
            {
                ExecutionLogProvider.Log.LogWarning( $"Maximum message size reached, incoming data discarded." );

                buffer = buffer.Slice( bufferLength );
            }

            return success;
        }

        private void OnNext( IObserver<ReadOnlySequence<byte>> observer, ReadOnlySequence<byte>[] tokens )
        {
            if( tokens.Length > 0 )
            {
                try
                {
                    foreach( ReadOnlySequence<byte> token in tokens )
                    {
                        observer.OnNext( token );
                    }
                }catch( Exception ex )
                {
                    throw Assert.Exception( ex );
                }
            }
        }

        private void OnCompleted( IObserver<ReadOnlySequence<byte>> observer )
        {
            try
            {
                observer.OnCompleted();
            }catch( Exception ex )
            {
                throw Assert.Exception( ex );
            }
        }

        private async Task Run( IObserver<ReadOnlySequence<byte>> observer, CancellationToken cancellationToken )
        {
            try
            {
                while( true )
                {
                    ReadResult readResult = await this.PipeReader.ReadAsync( cancellationToken );

                    ReadOnlySequence<byte> buffer = readResult.Buffer;

                    try
                    {
                        if( readResult.IsCanceled == true )
                        {
                            this.OnCompleted( observer );

                            break;
                        }

                        if( this.TryRead( ref buffer, out ReadOnlySequence<byte>[] tokens ) == true )
                        {
                            this.OnNext( observer, tokens );
                        }

                        if( readResult.IsCompleted == true )
                        {
                            if( buffer.Length > 0 )
                            {
                                this.SkipJunk( ref buffer );
                            }
                        
                            this.OnCompleted( observer );
                        
                            break;
                        }
                    }finally
                    {
                        if( readResult.IsCanceled == false &&
                            readResult.IsCompleted == false )
                        {
                            this.PipeReader.AdvanceTo( buffer.Start, buffer.End );
                        }
                    }
                }
            }finally
            {
                this.Finished.Set();
            }
        }

        private async Task ReadAsync( IObserver<ReadOnlySequence<byte>> observer, CancellationToken cancellationToken )
        {
            try
            {
                cancellationToken.Register( () =>
                                            {
                                                this.PipeReader.CancelPendingRead();
                                            }   );

                await this.Run( observer, cancellationToken ).ConfigureAwait( false );
            }catch( Exception ex )
            {
                observer.OnError( ex );
            }finally
            {
                await this.PipeReader.CompleteAsync().ConfigureAwait( false );
            }
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
                this.PipeReader.CancelPendingRead();
                this.Finished.Wait( millisecondsTimeout:5000 );

                if( disposing == true )
                {
                    this.Finished.Dispose();
                }

                this.isDisposed = true;
            }
        }
    }
}
