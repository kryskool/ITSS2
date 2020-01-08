using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;
using Reth.Protocols.Extensions.EventArgsExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.StockLocationInfo
{
    internal class StockLocationInfoClientDialog:Dialog, IStockLocationInfoClientDialog
    {
        internal StockLocationInfoClientDialog( IMessageTransceiver messageTransceiver )
        :
            base( DialogName.StockLocationInfo, messageTransceiver )
        {     
        }

        ~StockLocationInfoClientDialog()
        {
            this.Dispose( false );
        }

        public StockLocationInfoResponse SendRequest( StockLocationInfoRequest request )
        {
            return base.SendRequest<StockLocationInfoRequest, StockLocationInfoResponse>( request );
        }

        public Task<StockLocationInfoResponse> SendRequestAsync( StockLocationInfoRequest request )
        {
            return base.SendRequestAsync<StockLocationInfoRequest, StockLocationInfoResponse>( request, CancellationToken.None );
        }

        public Task<StockLocationInfoResponse> SendRequestAsync( StockLocationInfoRequest request, CancellationToken cancellationToken )
        {
            return base.SendRequestAsync<StockLocationInfoRequest, StockLocationInfoResponse>( request, cancellationToken );
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