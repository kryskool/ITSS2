using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Protocols.Dialogs
{
    public abstract class Dialog:IDialog
    {
        protected Dialog( IDialogName name, IMessageTransceiver messageTransceiver )
        {
            name.ThrowIfNull();
            messageTransceiver.ThrowIfNull();

            this.Name = name;
            this.MessageTransceiver = messageTransceiver;
        }

        public IDialogName Name
        {
            get;
        }

        protected IMessageTransceiver MessageTransceiver
        {
            get;
        }

        protected TResponse SendRequest<TRequest, TResponse>( TRequest request )
            where TRequest:IRequest
            where TResponse:IResponse
        {
            return this.MessageTransceiver.SendRequest<TRequest, TResponse>( request );
        }

        protected Task<TResponse> SendRequestAsync<TRequest, TResponse>( TRequest request )
            where TRequest:IRequest
            where TResponse:IResponse
        {
            return this.SendRequestAsync<TRequest, TResponse>( request, CancellationToken.None );   
        }

        protected Task<TResponse> SendRequestAsync<TRequest, TResponse>( TRequest request, CancellationToken cancellationToken )
            where TRequest:IRequest
            where TResponse:IResponse
        {
            return this.MessageTransceiver.SendRequestAsync<TRequest, TResponse>( request, cancellationToken );
        }

        protected bool PostMessage( IMessage message )
        {
            return this.MessageTransceiver.PostMessage( message );
        }

        public override String ToString()
        {
            return this.Name.Value;
        }
    }
}