using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;
using Reth.Protocols.Extensions.EventArgsExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.StockInfo
{
    internal class StockInfoClientDialog:Dialog, IStockInfoClientDialog
    {
        private volatile bool isDisposed;

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        internal StockInfoClientDialog( IMessageTransceiver messageTransceiver )
        :
            base( DialogName.StockInfo, messageTransceiver )
        {
            this.MessageInterceptor = new MessageInterceptor( messageTransceiver, typeof( StockInfoMessage ) );

            this.MessageInterceptor.Intercepted += this.OnMessageReceived;
        }

        ~StockInfoClientDialog()
        {
            this.Dispose( false );
        }

        private MessageInterceptor MessageInterceptor
        {
            get;
        }

        public StockInfoResponse SendRequest( StockInfoRequest request )
        {
            return base.SendRequest<StockInfoRequest, StockInfoResponse>( request );
        }

        public Task<StockInfoResponse> SendRequestAsync( StockInfoRequest request )
        {
            return base.SendRequestAsync<StockInfoRequest, StockInfoResponse>( request, CancellationToken.None );
        }

        public Task<StockInfoResponse> SendRequestAsync( StockInfoRequest request, CancellationToken cancellationToken )
        {
            return base.SendRequestAsync<StockInfoRequest, StockInfoResponse>( request, cancellationToken );
        }

        private void OnMessageReceived( Object sender, MessageReceivedEventArgs args )
        {
            this.MessageReceived?.SafeInvoke( this, args );
        }

        public void Dispose()
        {
            this.Dispose( true );

            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
            if( this.isDisposed == false )
            {
                if( disposing == true )
                {
                    this.MessageInterceptor.Dispose();
                }

                this.isDisposed = true;
            }
        }
    }
}