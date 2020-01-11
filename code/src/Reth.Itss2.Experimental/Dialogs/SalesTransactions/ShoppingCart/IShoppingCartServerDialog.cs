using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Experimental.Dialogs.SalesTransactions.ShoppingCart
{
    public interface IShoppingCartServerDialog:IDialog, IDisposable
    {
        ShoppingCartResponse SendRequest( ShoppingCartRequest request );
        
        Task<ShoppingCartResponse> SendRequestAsync( ShoppingCartRequest request );
        Task<ShoppingCartResponse> SendRequestAsync( ShoppingCartRequest request, CancellationToken cancellationToken );
    }
}