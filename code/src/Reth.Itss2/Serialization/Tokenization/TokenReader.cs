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
using System.IO;
using System.IO.Pipelines;
using System.Linq;
using System.Threading;

using Reth.Itss2.Diagnostics;

namespace Reth.Itss2.Serialization.Tokenization
{
    internal abstract class TokenReader<TState>:ITokenReader<TState>
        where TState:Enum
    {
        public const int DefaultBufferSize = 4096;
        public const int DefaultMaximumMessageSize = 0x100000;

        private bool isDisposed;

        protected TokenReader( Stream stream )
        :
            this( stream, TokenReader<TState>.DefaultBufferSize, TokenReader<TState>.DefaultMaximumMessageSize )
        {
        }

        protected TokenReader( Stream stream, int bufferSize, int maximumMessageSize )
        {
            this.PipeReader = PipeReader.Create( stream, new StreamPipeReaderOptions( leaveOpen:true, bufferSize:bufferSize ) );
            this.MaximumMessageSize = maximumMessageSize;
        }

        ~TokenReader()
        {
            this.Dispose( false );
        }

        private PipeReader PipeReader
        {
            get;
        }

        private int MaximumMessageSize
        {
            get;
        }

        protected abstract ITokenFinder<TState> TokenFinder
        {
            get;
        }

        private List<ITokenTransition<TState>> Transitions
        {
            get;
        } = new List<ITokenTransition<TState>>();

        private long BufferPosition
        {
            get; set;
        }

        public bool Read( out ReadOnlySequence<byte> token, CancellationToken cancellationToken = default )
        {
            token = ReadOnlySequence<byte>.Empty;

            bool result = false;

            if( cancellationToken.IsCancellationRequested == false )
            {
                using( CancellationTokenRegistration cancellationTokenRegistration = cancellationToken.Register(    () =>
                                                                                                                    {
                                                                                                                        this.PipeReader.CancelPendingRead();
                                                                                                                    }   ) )
                {
                    ReadResult readResult = new ReadResult();

                    while(  cancellationToken.IsCancellationRequested == false &&
                            readResult.IsCompleted == false &&
                            readResult.IsCanceled == false &&
                            result == false )
                    {
                        result = this.TryGetMessage( out readResult, out token, cancellationToken );
                    }
                }
            }
                        
            return result;
        }

        private bool TryGetMessage( out ReadResult readResult,
                                    out ReadOnlySequence<byte> token,
                                    CancellationToken cancellationToken = default   )
        {
            token = ReadOnlySequence<byte>.Empty;

            readResult = this.PipeReader.ReadAsync( cancellationToken ).AsTask().Result;

            bool result = false;          

            if( readResult.IsCompleted == false &&
                readResult.IsCanceled == false  )
            {
                ReadOnlySequence<byte> buffer = readResult.Buffer;

                if( buffer.Length >= this.MaximumMessageSize )
                {
                    throw Assert.Exception( new InvalidOperationException( $"Maximum message size exceeded." ) );
                }

                SequenceReader<byte> sequenceReader = new SequenceReader<byte>( buffer );

                sequenceReader.Advance( this.BufferPosition );

                result = this.TryGetMessage( ref sequenceReader, out token );

                if( result == true )
                {
                    this.BufferPosition = 0;

                    this.PipeReader.AdvanceTo( sequenceReader.Position );
                }else
                {
                    this.BufferPosition = buffer.Length;
                                
                    this.PipeReader.AdvanceTo( buffer.GetPosition( 0 ), buffer.End );
                }
            }

            return result;
        }

        private bool TryGetMessage( ref SequenceReader<byte> sequenceReader,
                                    out ReadOnlySequence<byte> token    )
        {
            token = ReadOnlySequence<byte>.Empty;

            bool result = false;

            List<ITokenTransition<TState>> transitions = this.Transitions;

            ITokenTransition<TState>? currentTransition = this.TokenFinder.FindNextTransition( transitions, ref sequenceReader );

            while( currentTransition is not null && result == false )
            {
                transitions.Add( currentTransition );

                if( transitions.TryGetMessage(  ref sequenceReader,
                                                out token, 
                                                out ITokenTransition<TState>? _,
                                                out ITokenTransition<TState>? endOfMessage    ) == true )
                {
                    transitions.RemoveRange( 0, transitions.IndexOf( endOfMessage! ) + 1 );

                    result = true;
                }

                if( result == false )
                {
                    currentTransition = this.TokenFinder.FindNextTransition( transitions, ref sequenceReader );
                }
            }

            return result;
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
                this.PipeReader.Complete();

                this.isDisposed = true;
            }
        }
    }
}
