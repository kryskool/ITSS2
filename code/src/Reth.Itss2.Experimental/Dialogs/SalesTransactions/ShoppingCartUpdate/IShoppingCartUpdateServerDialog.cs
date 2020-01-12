using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Experimental.Dialogs.SalesTransactions.ShoppingCartUpdate
{
    public interface IShoppingCartUpdateServerDialog:IDialog, IDisposable
    {
        event EventHandler<MessageReceivedEventArgs> MessageReceived;

        ShoppingCartUpdateResponse SendRequest( ShoppingCartUpdateRequest request );
        
        Task<ShoppingCartUpdateResponse> SendRequestAsync( ShoppingCartUpdateRequest request );
        Task<ShoppingCartUpdateResponse> SendRequestAsync( ShoppingCartUpdateRequest request, CancellationToken cancellationToken );
    }
}