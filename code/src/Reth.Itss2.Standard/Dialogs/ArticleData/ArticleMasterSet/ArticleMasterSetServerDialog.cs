using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols.Extensions.EventArgsExtensions;
using Reth.Protocols;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs.ArticleData.ArticleMasterSet
{
    internal class ArticleMasterSetServerDialog:Dialog, IArticleMasterSetServerDialog
    {
        private volatile bool isDisposed;

        public event EventHandler<MessageReceivedEventArgs> RequestReceived;

        internal ArticleMasterSetServerDialog( IMessageTransceiver messageTransceiver )
        :
            base( DialogName.ArticleMasterSet, messageTransceiver )
        {
            this.RequestInterceptor = new MessageInterceptor( messageTransceiver, typeof( ArticleMasterSetRequest ) );

            this.RequestInterceptor.Intercepted += this.OnRequestReceived;
        }

        ~ArticleMasterSetServerDialog()
        {
            this.Dispose( false );
        }

        private MessageInterceptor RequestInterceptor
        {
            get;
        }

        public void SendResponse( ArticleMasterSetResponse response )
        {
            base.PostMessage( response );
        }

        public Task SendResponseAsync( ArticleMasterSetResponse response )
        {
            return this.SendResponseAsync( response, CancellationToken.None );
        }

        public Task SendResponseAsync( ArticleMasterSetResponse response, CancellationToken cancellationToken )
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
                this.RequestInterceptor.Intercepted -= this.OnRequestReceived;

                if( disposing == true )
                {
                    this.RequestInterceptor.Dispose();
                }

                this.isDisposed = true;
            }
        }
    }
}