using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;
using Reth.Protocols.Extensions.EventArgsExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.InitiateInput
{
    internal class InitiateInputClientDialog:Dialog, IInitiateInputClientDialog
    {
        private volatile bool isDisposed;

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        internal InitiateInputClientDialog( IMessageTransceiver messageTransceiver )
        :
            base( DialogName.InitiateInput, messageTransceiver )
        {
            this.MessageInterceptor = new MessageInterceptor( messageTransceiver, typeof( InitiateInputMessage ) );

            this.MessageInterceptor.Intercepted += this.OnMessageReceived;
        }

        ~InitiateInputClientDialog()
        {
            this.Dispose( false );
        }

        private MessageInterceptor MessageInterceptor
        {
            get;
        }

        public InitiateInputResponse SendRequest( InitiateInputRequest request )
        {
            return base.SendRequest<InitiateInputRequest, InitiateInputResponse>( request );
        }

        public Task<InitiateInputResponse> SendRequestAsync( InitiateInputRequest request )
        {
            return base.SendRequestAsync<InitiateInputRequest, InitiateInputResponse>( request, CancellationToken.None );
        }

        public Task<InitiateInputResponse> SendRequestAsync( InitiateInputRequest request, CancellationToken cancellationToken )
        {
            return base.SendRequestAsync<InitiateInputRequest, InitiateInputResponse>( request, cancellationToken );
        }

        private void OnMessageReceived( Object sender, MessageReceivedEventArgs args )
        {
            this.MessageReceived?.SafeInvoke( this, args );
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
                    this.MessageInterceptor.Dispose();
                }

                this.isDisposed = true;
            }
        }
    }
}