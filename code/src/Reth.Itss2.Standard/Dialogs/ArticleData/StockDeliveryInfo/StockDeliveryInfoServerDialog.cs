using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;
using Reth.Protocols.Extensions.EventArgsExtensions;

namespace Reth.Itss2.Standard.Dialogs.ArticleData.StockDeliveryInfo
{
    internal class StockDeliveryInfoServerDialog:Dialog, IStockDeliveryInfoServerDialog
    {
        private volatile bool isDisposed;

        public event EventHandler<MessageReceivedEventArgs> RequestReceived;

        internal StockDeliveryInfoServerDialog( IMessageTransceiver messageTransceiver )
        :
            base( DialogName.StockDeliveryInfo, messageTransceiver )
        {
            this.RequestInterceptor = new MessageInterceptor( messageTransceiver, typeof( StockDeliveryInfoRequest ) );

            this.RequestInterceptor.Intercepted += this.OnRequestReceived;
        }

        ~StockDeliveryInfoServerDialog()
        {
            this.Dispose( false );
        }

        private MessageInterceptor RequestInterceptor
        {
            get;
        }

        public void SendResponse( StockDeliveryInfoResponse response )
        {
            base.PostMessage( response );
        }

        public Task SendResponseAsync( StockDeliveryInfoResponse response )
        {
            return this.SendResponseAsync( response, CancellationToken.None );
        }

        public Task SendResponseAsync( StockDeliveryInfoResponse response, CancellationToken cancellationToken )
        {
            return Task.Run(    () =>
                                {
                                    this.SendResponse( response );
                                },
                                cancellationToken   );
        }

        private void OnRequestReceived( Object sender, MessageReceivedEventArgs args )
        {
            this.RequestReceived?.SafeInvoke( this, args );
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
                    this.RequestInterceptor.Dispose();
                }

                this.isDisposed = true;
            }
        }
    }
}