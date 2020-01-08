using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs.ArticleData.StockDeliverySet
{
    public interface IStockDeliverySetClientDialog:IDialog, IDisposable
    {
        StockDeliverySetResponse SendRequest( StockDeliverySetRequest request );
        
        Task<StockDeliverySetResponse> SendRequestAsync( StockDeliverySetRequest request );
        Task<StockDeliverySetResponse> SendRequestAsync( StockDeliverySetRequest request, CancellationToken cancellationToken );
    }
}