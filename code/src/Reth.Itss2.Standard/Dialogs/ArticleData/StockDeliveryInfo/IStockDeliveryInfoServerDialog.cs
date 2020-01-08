using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs.ArticleData.StockDeliveryInfo
{
    public interface IStockDeliveryInfoServerDialog:IDialog, IDisposable
    {
        event EventHandler<MessageReceivedEventArgs> RequestReceived;

        void SendResponse( StockDeliveryInfoResponse response );
        
        Task SendResponseAsync( StockDeliveryInfoResponse response );
        Task SendResponseAsync( StockDeliveryInfoResponse response, CancellationToken cancellationToken );
    }
}