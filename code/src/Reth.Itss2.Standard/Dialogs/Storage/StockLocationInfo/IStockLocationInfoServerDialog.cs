using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs.Storage.StockLocationInfo
{
    public interface IStockLocationInfoServerDialog:IDialog, IDisposable
    {
        event EventHandler<MessageReceivedEventArgs> RequestReceived;

        void SendResponse( StockLocationInfoResponse response );
        
        Task SendResponseAsync( StockLocationInfoResponse response );
        Task SendResponseAsync( StockLocationInfoResponse response, CancellationToken cancellationToken );
    }
}