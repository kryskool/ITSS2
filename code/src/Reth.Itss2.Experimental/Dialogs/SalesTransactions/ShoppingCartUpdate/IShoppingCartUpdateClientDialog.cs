using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Experimental.Dialogs.SalesTransactions.ShoppingCartUpdate
{
    public interface IShoppingCartUpdateClientDialog:IDialog, IDisposable
    {
        event EventHandler<MessageReceivedEventArgs> RequestReceived;
        
        void SendResponse( ShoppingCartUpdateResponse response );
        
        Task SendResponseAsync( ShoppingCartUpdateResponse response );
        Task SendResponseAsync( ShoppingCartUpdateResponse response, CancellationToken cancellationToken );

        void SendMessage( ShoppingCartUpdateMessage message );
        
        Task SendMessageAsync( ShoppingCartUpdateMessage message );
        Task SendMessageAsync( ShoppingCartUpdateMessage message, CancellationToken cancellationToken );
    }
}