using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;
using Reth.Protocols.Extensions.EventArgsExtensions;

namespace Reth.Itss2.Standard.Dialogs.ArticleData.ArticleInfo
{
    internal class ArticleInfoServerDialog:Dialog, IArticleInfoServerDialog
    {
        internal ArticleInfoServerDialog( IMessageTransceiver messageTransceiver )
        :
            base( DialogName.ArticleInfo, messageTransceiver )
        {
        }

        ~ArticleInfoServerDialog()
        {
            this.Dispose( false );
        }

        public ArticleInfoResponse SendRequest( ArticleInfoRequest request )
        {
            return base.SendRequest<ArticleInfoRequest, ArticleInfoResponse>( request );
        }

        public Task<ArticleInfoResponse> SendRequestAsync( ArticleInfoRequest request )
        {
            return base.SendRequestAsync<ArticleInfoRequest, ArticleInfoResponse>( request, CancellationToken.None );
        }

        public Task<ArticleInfoResponse> SendRequestAsync( ArticleInfoRequest request, CancellationToken cancellationToken )
        {
            return base.SendRequestAsync<ArticleInfoRequest, ArticleInfoResponse>( request, cancellationToken );
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