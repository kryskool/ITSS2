using System;

using Reth.Protocols;
using Reth.Protocols.Dialogs;
using Reth.Protocols.Extensions.EventArgsExtensions;

namespace Reth.Itss2.Experimental.Dialogs.SalesTransactions.ArticleSelected
{
    internal class ArticleSelectedClientDialog:Dialog, IArticleSelectedClientDialog
    {
        private volatile bool isDisposed;

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        internal ArticleSelectedClientDialog( IMessageTransceiver messageTransceiver )
        :
            base( DialogName.ArticleSelected, messageTransceiver )
        {
            this.MessageInterceptor = new MessageInterceptor( messageTransceiver, typeof( ArticleSelectedMessage ) );

            this.MessageInterceptor.Intercepted += this.OnMessageReceived;
        }

        ~ArticleSelectedClientDialog()
        {
            this.Dispose( false );
        }

        private MessageInterceptor MessageInterceptor
        {
            get;
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
                this.MessageInterceptor.Intercepted -= this.OnMessageReceived;

                if( disposing == true )
                {
                    this.MessageInterceptor.Dispose();
                }

                this.isDisposed = true;
            }
        }
    }
}