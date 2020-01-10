using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Experimental.Dialogs.SalesTransactions.ArticleSelected
{
    internal class ArticleSelectedServerDialog:Dialog, IArticleSelectedServerDialog
    {
        internal ArticleSelectedServerDialog( IMessageTransceiver messageTransceiver )
        :
            base( DialogName.ArticlePrice, messageTransceiver )
        {
        }

        ~ArticleSelectedServerDialog()
        {
            this.Dispose( false );
        }

        public void SendMessage( ArticleSelectedMessage message )
        {
            base.PostMessage( message );
        }

        public Task SendMessageAsync( ArticleSelectedMessage message )
        {
            return this.SendMessageAsync( message, CancellationToken.None );
        }

        public Task SendMessageAsync( ArticleSelectedMessage message, CancellationToken cancellationToken )
        {
            return Task.Run(    () =>
                                {
                                    this.SendMessage( message );
                                },
                                cancellationToken   );
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