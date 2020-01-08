using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs.ArticleData.StockDeliveryInfo
{
    public interface IStockDeliveryInfoClientDialog:IDialog, IDisposable
    {
        StockDeliveryInfoResponse SendRequest( StockDeliveryInfoRequest request );
        
        Task<StockDeliveryInfoResponse> SendRequestAsync( StockDeliveryInfoRequest request );
        Task<StockDeliveryInfoResponse> SendRequestAsync( StockDeliveryInfoRequest request, CancellationToken cancellationToken );
    }
}