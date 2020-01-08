using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs.ArticleData.StockDeliveryInfo
{
    internal class StockDeliveryInfoClientDialog:Dialog, IStockDeliveryInfoClientDialog
    {
        internal StockDeliveryInfoClientDialog( IMessageTransceiver messageTransceiver )
        :
            base( DialogName.StockDeliveryInfo, messageTransceiver )
        {
        }

        ~StockDeliveryInfoClientDialog()
        {
            this.Dispose( false );
        }

        public StockDeliveryInfoResponse SendRequest( StockDeliveryInfoRequest request )
        {
            return base.SendRequest<StockDeliveryInfoRequest, StockDeliveryInfoResponse>( request );
        }

        public Task<StockDeliveryInfoResponse> SendRequestAsync( StockDeliveryInfoRequest request )
        {
            return base.SendRequestAsync<StockDeliveryInfoRequest, StockDeliveryInfoResponse>( request, CancellationToken.None );
        }

        public Task<StockDeliveryInfoResponse> SendRequestAsync( StockDeliveryInfoRequest request, CancellationToken cancellationToken )
        {
            return base.SendRequestAsync<StockDeliveryInfoRequest, StockDeliveryInfoResponse>( request, cancellationToken );
        }

        public void Dispose()
        {
            this.Dispose( true );

            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
        }
    }
}