using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs.ArticleData.ArticleMasterSet
{
    public interface IArticleMasterSetClientDialog:IDialog, IDisposable
    {
        ArticleMasterSetResponse SendRequest( ArticleMasterSetRequest request );
        
        Task<ArticleMasterSetResponse> SendRequestAsync( ArticleMasterSetRequest request );
        Task<ArticleMasterSetResponse> SendRequestAsync( ArticleMasterSetRequest request, CancellationToken cancellationToken );
    }
}