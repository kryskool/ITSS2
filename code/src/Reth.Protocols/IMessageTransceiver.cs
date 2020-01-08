using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Reth.Protocols
{
    public interface IMessageTransceiver:IDisposable
    {
        event EventHandler<MessageReceivedEventArgs> MessageReceived;
        event EventHandler Terminated;

        bool PostMessage( IMessage message );

        TResponse SendRequest<TRequest, TResponse>( TRequest request )
            where TRequest:IRequest
            where TResponse:IResponse;

        Task<TResponse> SendRequestAsync<TRequest, TResponse>( TRequest request )
            where TRequest:IRequest
            where TResponse:IResponse;

        Task<TResponse> SendRequestAsync<TRequest, TResponse>( TRequest request, CancellationToken cancellationToken )
            where TRequest:IRequest
            where TResponse:IResponse;

        void Start( Stream stream );
        void Terminate();
    }
}
