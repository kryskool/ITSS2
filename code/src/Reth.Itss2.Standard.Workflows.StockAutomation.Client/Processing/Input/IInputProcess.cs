using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Standard.Dialogs.Storage.Input;
using Reth.Protocols;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Client.Processing.Input
{
    public interface IInputProcess
    {
        event EventHandler<InputFinishedEventArgs> Finished;

        IMessageId Id{ get; }
        
        void Start( InputResponseArticle article );
        Task StartAsync( InputResponseArticle article );
        Task StartAsync( InputResponseArticle article, CancellationToken cancellationToken );
    }
}
