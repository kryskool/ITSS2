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
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Diagnostics;

namespace Reth.Itss2.Tokenization
{
    public abstract class Tokenizer<TState>:ITokenizer
        where TState:Enum
    {
        private bool isDisposed;

        protected Tokenizer( ITokenReader<TState> tokenReader )
        {
            this.TokenReader = tokenReader;
            
            this.Observable = System.Reactive.Linq.Observable.Create<ReadOnlySequence<byte>>( this.RunAsync );
        }

        ~Tokenizer()
        {
            this.Dispose( false );
        }

        protected ITokenReader<TState> TokenReader
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

        private void OnError( IObserver<ReadOnlySequence<byte>> observer, Exception error )
        {
            try
            {
                observer.OnError( error );
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

        private Task RunAsync( IObserver<ReadOnlySequence<byte>> observer, CancellationToken cancellationToken )
        {
            return Task.Factory.StartNew(   () =>
                                            {
                                                try
                                                {
                                                    while( cancellationToken.IsCancellationRequested == false )
                                                    {
                                                        bool readResult = this.TokenReader.Read(    out ReadOnlySequence<byte> token,
                                                                                                    cancellationToken   );
                                                            
                                                        if( readResult == true )
                                                        {
                                                            this.OnNext( observer, token );
                                                        }else
                                                        {
                                                            this.OnCompleted( observer );
                                                        }
                                                    }
                                                }catch( Exception ex )
                                                {
                                                    this.OnError( observer, ex );
                                                }finally
                                                {
                                                    this.Finished.Set();
                                                }
                                            },
                                            cancellationToken,
                                            TaskCreationOptions.LongRunning,
                                            TaskScheduler.Default   );
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
                this.Finished.WaitDebuggerAware( millisecondsTimeout:5000 );

                if( disposing == true )
                {
                    this.Finished.Dispose();

                    this.TokenReader.Dispose();
                }

                this.isDisposed = true;
            }
        }
    }
}
