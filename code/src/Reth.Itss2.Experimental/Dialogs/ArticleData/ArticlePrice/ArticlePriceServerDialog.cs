using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Experimental.Dialogs.ArticleData.ArticlePrice
{
    internal class ArticlePriceServerDialog:Dialog, IArticlePriceServerDialog
    {
        internal ArticlePriceServerDialog( IMessageTransceiver messageTransceiver )
        :
            base( DialogName.ArticlePrice, messageTransceiver )
        {
        }

        ~ArticlePriceServerDialog()
        {
            this.Dispose( false );
        }

        

        public ArticlePriceResponse SendRequest( ArticlePriceRequest request )
        {
            return base.SendRequest<ArticlePriceRequest, ArticlePriceResponse>( request );
        }

        public Task<ArticlePriceResponse> SendRequestAsync( ArticlePriceRequest request )
        {
            return base.SendRequestAsync<ArticlePriceRequest, ArticlePriceResponse>( request, CancellationToken.None );
        }

        public Task<ArticlePriceResponse> SendRequestAsync( ArticlePriceRequest request, CancellationToken cancellationToken )
        {
            return base.SendRequestAsync<ArticlePriceRequest, ArticlePriceResponse>( request, cancellationToken );
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