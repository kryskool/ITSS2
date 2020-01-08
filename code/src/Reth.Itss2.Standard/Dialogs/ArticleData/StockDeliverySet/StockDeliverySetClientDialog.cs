using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs.ArticleData.StockDeliverySet
{
    internal class StockDeliverySetClientDialog:Dialog, IStockDeliverySetClientDialog
    {
        internal StockDeliverySetClientDialog( IMessageTransceiver messageTransceiver )
        :
            base( DialogName.StockDeliverySet, messageTransceiver )
        {
        }

        ~StockDeliverySetClientDialog()
        {
            this.Dispose( false );
        }

        public StockDeliverySetResponse SendRequest( StockDeliverySetRequest request )
        {
            return base.SendRequest<StockDeliverySetRequest, StockDeliverySetResponse>( request );
        }

        public Task<StockDeliverySetResponse> SendRequestAsync( StockDeliverySetRequest request )
        {
            return base.SendRequestAsync<StockDeliverySetRequest, StockDeliverySetResponse>( request, CancellationToken.None );
        }

        public Task<StockDeliverySetResponse> SendRequestAsync( StockDeliverySetRequest request, CancellationToken cancellationToken )
        {
            return base.SendRequestAsync<StockDeliverySetRequest, StockDeliverySetResponse>( request, cancellationToken );
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