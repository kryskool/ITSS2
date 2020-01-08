using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs.ArticleData.StockDeliverySet
{
    public interface IStockDeliverySetServerDialog:IDialog, IDisposable
    {
        event EventHandler<MessageReceivedEventArgs> RequestReceived;
        
        void SendResponse( StockDeliverySetResponse response );
        
        Task SendResponseAsync( StockDeliverySetResponse response );
        Task SendResponseAsync( StockDeliverySetResponse response, CancellationToken cancellationToken );
    }
}