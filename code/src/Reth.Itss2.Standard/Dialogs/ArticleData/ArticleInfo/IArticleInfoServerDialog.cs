using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs.ArticleData.ArticleInfo
{
    public interface IArticleInfoServerDialog:IDialog, IDisposable
    {
        ArticleInfoResponse SendRequest( ArticleInfoRequest request );
        
        Task<ArticleInfoResponse> SendRequestAsync( ArticleInfoRequest request );
        Task<ArticleInfoResponse> SendRequestAsync( ArticleInfoRequest request, CancellationToken cancellationToken );
    }
}