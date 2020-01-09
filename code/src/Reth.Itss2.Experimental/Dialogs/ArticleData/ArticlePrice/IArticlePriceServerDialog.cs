using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Experimental.Dialogs.ArticleData.ArticlePrice
{
    public interface IArticlePriceServerDialog:IDialog, IDisposable
    {
        ArticlePriceResponse SendRequest( ArticlePriceRequest request );
        
        Task<ArticlePriceResponse> SendRequestAsync( ArticlePriceRequest request );
        Task<ArticlePriceResponse> SendRequestAsync( ArticlePriceRequest request, CancellationToken cancellationToken );
    }
}