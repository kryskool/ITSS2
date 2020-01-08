using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs.Storage.StockLocationInfo
{
    public interface IStockLocationInfoClientDialog:IDialog, IDisposable
    {
        StockLocationInfoResponse SendRequest( StockLocationInfoRequest request );
        
        Task<StockLocationInfoResponse> SendRequestAsync( StockLocationInfoRequest request );
        Task<StockLocationInfoResponse> SendRequestAsync( StockLocationInfoRequest request, CancellationToken cancellationToken );
    }
}