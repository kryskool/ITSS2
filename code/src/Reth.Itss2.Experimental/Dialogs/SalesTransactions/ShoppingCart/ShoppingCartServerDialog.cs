using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Experimental.Dialogs.SalesTransactions.ShoppingCart
{
    internal class ShoppingCartServerDialog:Dialog, IShoppingCartServerDialog
    {
        internal ShoppingCartServerDialog( IMessageTransceiver messageTransceiver )
        :
            base( DialogName.ArticlePrice, messageTransceiver )
        {
        }

        ~ShoppingCartServerDialog()
        {
            this.Dispose( false );
        }

        public ShoppingCartResponse SendRequest( ShoppingCartRequest request )
        {
            return base.SendRequest<ShoppingCartRequest, ShoppingCartResponse>( request );
        }

        public Task<ShoppingCartResponse> SendRequestAsync( ShoppingCartRequest request )
        {
            return base.SendRequestAsync<ShoppingCartRequest, ShoppingCartResponse>( request, CancellationToken.None );
        }

        public Task<ShoppingCartResponse> SendRequestAsync( ShoppingCartRequest request, CancellationToken cancellationToken )
        {
            return base.SendRequestAsync<ShoppingCartRequest, ShoppingCartResponse>( request, cancellationToken );
        }

        public void Dispose()
        {
            this.Dispose( true );

            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
        }
    }
}