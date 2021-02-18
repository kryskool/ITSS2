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

namespace Reth.Itss2.Dialogs.Standard.Serialization.Tokenization
{
    internal abstract class Tokenizer:ITokenizer
    {
        public const int DefaultBufferSize = 4096;

        private bool isDisposed;

        protected Tokenizer( Stream baseStream )
        :
            this( baseStream, NullInteractionLog.Instance, Tokenizer.DefaultBufferSize, SerializationSettings.MaximumMessageSize )
        {
        }

        protected Tokenizer( Stream baseStream, IInteractionLog interactionLog )
        :
            this( baseStream, interactionLog, Tokenizer.DefaultBufferSize, SerializationSettings.MaximumMessageSize )
        {
        }

        protected Tokenizer( Stream baseStream, IInteractionLog interactionLog, int bufferSize, int maximumMessageSize )
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

        protected abstract ITokenReader CreateTokenReader();
        protected abstract ITokenReaderState CreateTokenReaderState();

        private void OnNext( IObserver<ReadOnlySequence<byte>> observer, ReadOnlySequence<byte> token )
        {
            try
            {
                observer.OnNext( token );
            }catch( Exception ex )
            {
                throw Assert.Exception( ex );
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

        private bool TryRead(   ref ReadOnlySequence<byte> buffer,
                                ref ITokenReaderState tokenReaderState,
                                out ReadOnlySequence<byte> token,
                                out long consumedBytes  )
        {
            token = new ReadOnlySequence<byte>();
            consumedBytes = 0;

            bool result = false;
            
            long bufferLength = buffer.Length;

            if( bufferLength < this.MaximumMessageSize )
            {
                ITokenReader tokenReader = this.CreateTokenReader();
                
                result = tokenReader.Read(  ref tokenReaderState,
                                            ref buffer,
                                            out token,
                                            out consumedBytes   );
            }else
            {
                throw Assert.Exception( new InvalidOperationException( $"Maximum message size exceeded." ) );
            }

            return result;
        }

        private async Task Run( IObserver<ReadOnlySequence<byte>> observer, CancellationToken cancellationToken )
        {
            try
            {
                while( true )
                {
                    ReadResult readResult = await this.PipeReader.ReadAsync( cancellationToken );

                    if( readResult.IsCompleted == true ||
                        readResult.IsCanceled == true   )
                    {                        
                        this.OnCompleted( observer );
                        
                        break;
                    }

                    ReadOnlySequence<byte> buffer = readResult.Buffer;
                    ITokenReaderState tokenReaderState = this.CreateTokenReaderState();

                    long offset = 0;

                    while( this.TryRead(    ref buffer,
                                            ref tokenReaderState,
                                            out ReadOnlySequence<byte> token,
                                            out long consumedBytes  ) == true )
                    {
                        offset = consumedBytes;

                        this.OnNext( observer, token );
                    }
                    
                    if( readResult.IsCanceled == false &&
                        readResult.IsCompleted == false )
                    {
                        SequencePosition position = new SequencePosition();
                        
                        if( buffer.Length >= offset )
                        {
                            position = buffer.GetPosition( offset );
                        }

                        this.PipeReader.AdvanceTo( position, buffer.End );
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

                await this.Run( observer, cancellationToken ).ConfigureAwait( continueOnCapturedContext:false );
            }catch( Exception ex )
            {
                observer.OnError( ex );
            }finally
            {
                await this.PipeReader.CompleteAsync().ConfigureAwait( continueOnCapturedContext:false );
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
