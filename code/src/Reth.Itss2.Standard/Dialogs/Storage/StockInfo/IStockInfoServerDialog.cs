using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs.Storage.StockInfo
{
    public interface IStockInfoServerDialog:IDialog, IDisposable
    {
        event EventHandler<MessageReceivedEventArgs> RequestReceived;
        
        void SendResponse( StockInfoResponse response );
        
        Task SendResponseAsync( StockInfoResponse response );
        Task SendResponseAsync( StockInfoResponse response, CancellationToken cancellationToken );
        
        void SendMessage( StockInfoMessage message );
        
        Task SendMessageAsync( StockInfoMessage message );
        Task SendMessageAsync( StockInfoMessage message, CancellationToken cancellationToken );
    }
}