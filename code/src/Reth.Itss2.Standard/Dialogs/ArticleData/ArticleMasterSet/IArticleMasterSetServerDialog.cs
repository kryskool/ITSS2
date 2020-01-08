using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs.ArticleData.ArticleMasterSet
{
    public interface IArticleMasterSetServerDialog:IDialog, IDisposable
    {
        event EventHandler<MessageReceivedEventArgs> RequestReceived;
        
        void SendResponse( ArticleMasterSetResponse response );
        
        Task SendResponseAsync( ArticleMasterSetResponse response );
        Task SendResponseAsync( ArticleMasterSetResponse response, CancellationToken cancellationToken );
    }
}