using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Experimental.Dialogs.SalesTransactions.ArticleSelected
{
    public interface IArticleSelectedServerDialog:IDialog, IDisposable
    {
        void SendMessage( ArticleSelectedMessage message );
        
        Task SendMessageAsync( ArticleSelectedMessage message );
        Task SendMessageAsync( ArticleSelectedMessage message, CancellationToken cancellationToken );
    }
}