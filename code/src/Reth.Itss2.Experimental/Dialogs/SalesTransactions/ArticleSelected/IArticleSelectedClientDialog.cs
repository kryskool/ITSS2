using System;

using Reth.Protocols;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Experimental.Dialogs.SalesTransactions.ArticleSelected
{
    public interface IArticleSelectedClientDialog:IDialog, IDisposable
    {
        event EventHandler<MessageReceivedEventArgs> MessageReceived;
    }
}