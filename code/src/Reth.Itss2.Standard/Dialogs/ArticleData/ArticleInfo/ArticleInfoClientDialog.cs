using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;
using Reth.Protocols.Extensions.EventArgsExtensions;

namespace Reth.Itss2.Standard.Dialogs.ArticleData.ArticleInfo
{
    internal class ArticleInfoClientDialog:Dialog, IArticleInfoClientDialog
    {
        private volatile bool isDisposed;

        public event EventHandler<MessageReceivedEventArgs> RequestReceived;

        internal ArticleInfoClientDialog( IMessageTransceiver messageTransceiver )
        :
            base( DialogName.ArticleInfo, messageTransceiver )
        {
            this.RequestInterceptor = new MessageInterceptor( messageTransceiver, typeof( ArticleInfoRequest ) );

            this.RequestInterceptor.Intercepted += this.OnRequestReceived;
        }

        ~ArticleInfoClientDialog()
        {
            this.Dispose( false );
        }

        private MessageInterceptor RequestInterceptor
        {
            get;
        }

        public void SendResponse( ArticleInfoResponse response )
        {
            base.PostMessage( response );
        }

        public Task SendResponseAsync( ArticleInfoResponse response )
        {
            return this.SendResponseAsync( response, CancellationToken.None );
        }

        public Task SendResponseAsync( ArticleInfoResponse response, CancellationToken cancellationToken )
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