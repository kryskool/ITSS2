using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;
using Reth.Protocols.Extensions.EventArgsExtensions;

namespace Reth.Itss2.Experimental.Dialogs.SalesTransactions.ShoppingCartUpdate
{
    internal class ShoppingCartUpdateServerDialog:Dialog, IShoppingCartUpdateServerDialog
    {
        private volatile bool isDisposed;

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        internal ShoppingCartUpdateServerDialog( IMessageTransceiver messageTransceiver )
        :
            base( DialogName.ShoppingCartUpdate, messageTransceiver )
        {
            this.MessageInterceptor = new MessageInterceptor( messageTransceiver, typeof( ShoppingCartUpdateMessage ) );

            this.MessageInterceptor.Intercepted += this.OnMessageReceived;
        }

        ~ShoppingCartUpdateServerDialog()
        {
            this.Dispose( false );
        }

        private MessageInterceptor MessageInterceptor
        {
            get;
        }

        public ShoppingCartUpdateResponse SendRequest( ShoppingCartUpdateRequest request )
        {
            return base.SendRequest<ShoppingCartUpdateRequest, ShoppingCartUpdateResponse>( request );
        }

        public Task<ShoppingCartUpdateResponse> SendRequestAsync( ShoppingCartUpdateRequest request )
        {
            return base.SendRequestAsync<ShoppingCartUpdateRequest, ShoppingCartUpdateResponse>( request, CancellationToken.None );
        }

        public Task<ShoppingCartUpdateResponse> SendRequestAsync( ShoppingCartUpdateRequest request, CancellationToken cancellationToken )
        {
            return base.SendRequestAsync<ShoppingCartUpdateRequest, ShoppingCartUpdateResponse>( request, cancellationToken );
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