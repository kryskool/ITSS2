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
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Dialogs.Standard.Diagnostics;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.Standard.Serialization;

namespace Reth.Itss2.Dialogs.Standard.Protocol
{
    public class MessageTransmitter:IMessageTransmitter
    {
#if DEBUG
        public static TimeSpan DefaultMessageRoundTripTimeout
        {
            get
            {
                TimeSpan result = TimeSpan.FromSeconds( 10 );

                if( System.Diagnostics.Debugger.IsAttached )
                {
                    result = TimeSpan.FromMinutes( 30 );
                }

                return result;
            }
        }
#else
        public static TimeSpan DefaultMessageRoundTripTimeout
        {
            get;
        } = TimeSpan.FromSeconds( 10 );
#endif
        
        private bool isDisposed;

        public MessageTransmitter(  IMessageStreamReader messageStreamReader,
                                    IMessageStreamWriter messageStreamWriter    )
        :
            this(   messageStreamReader,
                    messageStreamWriter,
                    MessageTransmitter.DefaultMessageRoundTripTimeout   )
        {
        }

        public MessageTransmitter(  IMessageStreamReader messageStreamReader,
                                    IMessageStreamWriter messageStreamWriter,
                                    TimeSpan messageRoundTripTimeout    )
        {
            this.MessageStreamReader = messageStreamReader;
            this.MessageStreamWriter = messageStreamWriter;
            this.MessageRoundTripTimeout = messageRoundTripTimeout;

            this.Observable = ( from messageEnvelope in this.MessageStreamReader
                                select messageEnvelope.Message  ).Publish();
        }

        ~MessageTransmitter()
        {
            this.Dispose( false );
        }

        private IMessageStreamReader MessageStreamReader
        {
            get;
        }

        private IMessageStreamWriter MessageStreamWriter
        {
            get;
        }

        public TimeSpan MessageRoundTripTimeout
        {
            get;
        }
        
        private IConnectableObservable<IMessage> Observable
        {
            get;
        }

        public IDisposable Subscribe( IObserver<IMessage> observer )
        {
            return this.Observable.Subscribe( observer );
        }

        public IDisposable Connect()
        {
            return this.Observable.Connect();
        }

        public void SendMessage( String messageEnvelope )
        {
            this.MessageStreamWriter.Write( messageEnvelope );
        }

        public void SendMessage( IMessage message )
        {
            this.MessageStreamWriter.Write( new MessageEnvelope( message ) );
        }

        public Task SendMessageAsync( String messageEnvelope )
        {
            return this.MessageStreamWriter.WriteAsync( messageEnvelope );

        }
        public Task SendMessageAsync( IMessage message )
        {
            return this.MessageStreamWriter.WriteAsync( new MessageEnvelope( message ) );
        }

        public TResponse SendRequest<TRequest, TResponse>( TRequest request )
            where TRequest:IRequest
            where TResponse:IResponse
        {
            try
            {
                IObservable<IMessage> responseObservable = this.Observable.Where(   ( IMessage message ) =>
                                                                                    {
                                                                                        return ( message.Id.Equals( request.Id ) );
                                                                                    }   );

                IObservable<IMessage> timeoutObservable = responseObservable.Timeout( this.MessageRoundTripTimeout );

                IObservable<IMessage> waitObservable = timeoutObservable.FirstOrDefaultAsync();

                this.SendMessage( request );
                                
                IMessage? response = waitObservable.Wait();

                if( response is null )
                {
                    throw Assert.Exception( new MessageTransmissionException( $"Sending of '{ request.GetName() }' with id '{ request.Id }' received no response." ) );
                }

                return ( TResponse )( response );
            }catch( TimeoutException ex )
            {
                throw Assert.Exception( new MessageTransmissionException( $"Sending of '{ request.GetName() }' with id '{ request.Id }' timed out.", ex ) );
            }catch( Exception ex )
                when ( ex is not MessageTransmissionException )
            {
                throw Assert.Exception( new MessageTransmissionException( $"Sending of '{ request.GetName() }' with id '{ request.Id }' failed.", ex ) );
            }
        }

        public Task<TResponse> SendRequestAsync<TRequest, TResponse>( TRequest request )
            where TRequest:IRequest
            where TResponse:IResponse
        {
            return this.SendRequestAsync<TRequest, TResponse>( request, CancellationToken.None );
        }

        public Task<TResponse> SendRequestAsync<TRequest, TResponse>( TRequest request, CancellationToken cancellationToken )
            where TRequest:IRequest
            where TResponse:IResponse
        {
            return Task.Run<TResponse>( () =>
                                        {
                                            return this.SendRequest<TRequest, TResponse>( request );
                                        },
                                        cancellationToken   );
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
                    this.MessageStreamReader.Dispose();
                    this.MessageStreamWriter.Dispose();
                }

                this.isDisposed = true;
            }
        }
    }
}
