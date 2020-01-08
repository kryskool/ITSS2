using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs.ArticleData.ArticleInfo
{
    public interface IArticleInfoClientDialog:IDialog, IDisposable
    {
        event EventHandler<MessageReceivedEventArgs> RequestReceived;

        void SendResponse( ArticleInfoResponse response );
        
        Task SendResponseAsync( ArticleInfoResponse response );
        Task SendResponseAsync( ArticleInfoResponse response, CancellationToken cancellationToken );
    }
}