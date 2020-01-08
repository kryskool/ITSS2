using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols.Diagnostics;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Protocols
{
    internal class RequestProcess
    {
        public RequestProcess( IMessageTransceiver messageTransceiver )
        {
            messageTransceiver.ThrowIfNull();

            this.MessageTransceiver = messageTransceiver;
        }

        public IMessageTransceiver MessageTransceiver
        {
            get;
        }

        private TResponse SendRequest<TRequest, TResponse>( TRequest request, Func<MessageInterceptor, bool> waitCallback )
            where TRequest:IRequest
            where TResponse:IResponse
        {
            TResponse result = default( TResponse );

            using( MessageInterceptor interceptor = new MessageInterceptor( this.MessageTransceiver, typeof( TResponse ), request.Id ) )
            {
                interceptor.Intercepted +=  ( Object sender, MessageReceivedEventArgs e ) =>
                                            {
                                                result = ( TResponse )( e.Message );

                                                e.IsHandled = true;

                                                ExecutionLogProvider.LogInformation( $"Response received: '{ result.Id }', ({ result.GetType().FullName })." );
                                            };
                
                ExecutionLogProvider.LogInformation( $"Sending request: '{ request.Id }' ({ request.GetType().FullName })." );
                
                if( this.MessageTransceiver.PostMessage( request ) == true )
                {
                    try
                    {
                        ExecutionLogProvider.LogInformation( "Waiting for response." );

                        if( waitCallback( interceptor ) == false )
                        {
                            UnhandledMessageHandler.Invoke( new UnhandledMessage( UnhandledReason.Timeout, MessageDirection.Outgoing, request ) );

                            throw new RequestException( "Sending request timed out." );
                        }
                    }catch( OperationCanceledException ex )
                    {
                        ExecutionLogProvider.LogError( ex );

                        UnhandledMessageHandler.Invoke( new UnhandledMessage( UnhandledReason.Cancelled, MessageDirection.Outgoing, request ) );

                        throw new RequestException( "Request canceled.", ex );
                    }
                }else
                {
                    throw new RequestException( "Sending request failed." );
                }
            }

            return result;
        }

        public TResponse SendRequest<TRequest, TResponse>( TRequest request )
            where TRequest:IRequest
            where TResponse:IResponse
        {
            return this.SendRequest<TRequest, TResponse>(   request,
                                                            ( MessageInterceptor interceptor ) =>
                                                            {
                                                                return interceptor.Wait( Timeouts.MessageRoundtrip );
                                                            }   );
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
            return Task<TResponse>.Run( () =>
                                        {
                                            return this.SendRequest<TRequest, TResponse>(   request,
                                                                                            ( MessageInterceptor interceptor ) =>
                                                                                            {
                                                                                                return interceptor.Wait( Timeouts.MessageRoundtrip, cancellationToken );
                                                                                            }   );
                                        }   );
        }
    }
}
