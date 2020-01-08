using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs.Storage.StockInfo
{
    public interface IStockInfoClientDialog:IDialog, IDisposable
    {
        event EventHandler<MessageReceivedEventArgs> MessageReceived;

        StockInfoResponse SendRequest( StockInfoRequest request );
        
        Task<StockInfoResponse> SendRequestAsync( StockInfoRequest request );
        Task<StockInfoResponse> SendRequestAsync( StockInfoRequest request, CancellationToken cancellationToken );
    }
}