using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs.ArticleData.ArticleMasterSet
{
    internal class ArticleMasterSetClientDialog:Dialog, IArticleMasterSetClientDialog
    {
        internal ArticleMasterSetClientDialog( IMessageTransceiver messageTransceiver )
        :
            base( DialogName.ArticleMasterSet, messageTransceiver )
        {
        }

        ~ArticleMasterSetClientDialog()
        {
            this.Dispose( false );
        }

        public ArticleMasterSetResponse SendRequest( ArticleMasterSetRequest request )
        {
            return base.SendRequest<ArticleMasterSetRequest, ArticleMasterSetResponse>( request );
        }

        public Task<ArticleMasterSetResponse> SendRequestAsync( ArticleMasterSetRequest request )
        {
            return base.SendRequestAsync<ArticleMasterSetRequest, ArticleMasterSetResponse>( request, CancellationToken.None );
        }

        public Task<ArticleMasterSetResponse> SendRequestAsync( ArticleMasterSetRequest request, CancellationToken cancellationToken )
        {
            return base.SendRequestAsync<ArticleMasterSetRequest, ArticleMasterSetResponse>( request, cancellationToken );
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