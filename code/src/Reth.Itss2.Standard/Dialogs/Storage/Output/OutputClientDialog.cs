using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;
using Reth.Protocols.Extensions.EventArgsExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.Output
{
    internal class OutputClientDialog:Dialog, IOutputClientDialog
    {
        private volatile bool isDisposed;

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        internal OutputClientDialog( IMessageTransceiver messageTransceiver )
        :
            base( DialogName.Output, messageTransceiver )
        {
            this.MessageInterceptor = new MessageInterceptor( messageTransceiver, typeof( OutputMessage ) );

            this.MessageInterceptor.Intercepted += this.OnMessageReceived;
        }

        ~OutputClientDialog()
        {
            this.Dispose( false );
        }

        private MessageInterceptor MessageInterceptor
        {
            get;
        }

        public OutputResponse SendRequest( OutputRequest request )
        {
            return base.SendRequest<OutputRequest, OutputResponse>( request );
        }

        public Task<OutputResponse> SendRequestAsync( OutputRequest request )
        {
            return base.SendRequestAsync<OutputRequest, OutputResponse>( request, CancellationToken.None );
        }

        public Task<OutputResponse> SendRequestAsync( OutputRequest request, CancellationToken cancellationToken )
        {
            return base.SendRequestAsync<OutputRequest, OutputResponse>( request, cancellationToken );
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