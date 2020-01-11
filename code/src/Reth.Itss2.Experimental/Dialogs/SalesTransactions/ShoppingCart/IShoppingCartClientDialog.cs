using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Experimental.Dialogs.SalesTransactions.ShoppingCart
{
    public interface IShoppingCartClientDialog:IDialog, IDisposable
    {
        event EventHandler<MessageReceivedEventArgs> RequestReceived;
        
        void SendResponse( ShoppingCartResponse response );
        
        Task SendResponseAsync( ShoppingCartResponse response );
        Task SendResponseAsync( ShoppingCartResponse response, CancellationToken cancellationToken );
    }
}