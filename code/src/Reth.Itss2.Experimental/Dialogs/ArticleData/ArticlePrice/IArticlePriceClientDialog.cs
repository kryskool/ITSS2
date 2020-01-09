using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Experimental.Dialogs.ArticleData.ArticlePrice
{
    public interface IArticlePriceClientDialog:IDialog, IDisposable
    {
        event EventHandler<MessageReceivedEventArgs> RequestReceived;
        
        void SendResponse( ArticlePriceResponse response );
        
        Task SendResponseAsync( ArticlePriceResponse response );
        Task SendResponseAsync( ArticlePriceResponse response, CancellationToken cancellationToken );
    }
}