using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;
using Reth.Protocols.Extensions.EventArgsExtensions;

namespace Reth.Itss2.Standard.Dialogs.General.KeepAlive
{
    internal class KeepAliveDialog:Dialog, IKeepAliveClientDialog, IKeepAliveServerDialog
    {
        private volatile bool isDisposed;

        public event EventHandler<MessageReceivedEventArgs> RequestReceived;

        internal KeepAliveDialog( IMessageTransceiver messageTransceiver )
        :
            base( DialogName.KeepAlive, messageTransceiver )
        {
            this.RequestInterceptor = new MessageInterceptor( messageTransceiver, typeof( KeepAliveRequest ) );

            this.RequestInterceptor.Intercepted += this.OnRequestReceived;
        }

        ~KeepAliveDialog()
        {
            this.Dispose( false );
        }

        private MessageInterceptor RequestInterceptor
        {
            get;
        }

        public KeepAliveResponse SendRequest( KeepAliveRequest request )
        {
            return base.SendRequest<KeepAliveRequest, KeepAliveResponse>( request );
        }

        public Task<KeepAliveResponse> SendRequestAsync( KeepAliveRequest request )
        {
            return base.SendRequestAsync<KeepAliveRequest, KeepAliveResponse>( request, CancellationToken.None );
        }

        public Task<KeepAliveResponse> SendRequestAsync( KeepAliveRequest request, CancellationToken cancellationToken )
        {
            return base.SendRequestAsync<KeepAliveRequest, KeepAliveResponse>( request, cancellationToken );
        }

        public void SendResponse( KeepAliveResponse response )
        {
            base.PostMessage( response );
        }

        public Task SendResponseAsync( KeepAliveResponse response )
        {
            return this.SendResponseAsync( response, CancellationToken.None );
        }

        public Task SendResponseAsync( KeepAliveResponse response, CancellationToken cancellationToken )
        {
            return Task.Run(    () =>
                                {
                                    this.SendResponse( response );
                                },
                                cancellationToken   );
        }

        private void OnRequestReceived( Object sender, MessageReceivedEventArgs args )
        {
            this.RequestReceived?.SafeInvoke( this, args );
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
                this.RequestInterceptor.Intercepted -= this.OnRequestReceived;

                if( disposing == true )
                {
                    this.RequestInterceptor.Dispose();
                }

                this.isDisposed = true;
            }
        }
    }
}