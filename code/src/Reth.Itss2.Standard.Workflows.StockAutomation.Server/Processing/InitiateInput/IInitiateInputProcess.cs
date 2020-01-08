using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Standard.Dialogs.Storage.InitiateInput;
using Reth.Protocols;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Server.Processing.InitiateInput
{
    public interface IInitiateInputProcess
    {
        IMessageId Id{ get; }

        void Start( InitiateInputResponseDetails details,
                    InitiateInputResponseArticle article );

        Task StartAsync(    InitiateInputResponseDetails details,
                            InitiateInputResponseArticle article    );

        Task StartAsync(    InitiateInputResponseDetails details,
                            InitiateInputResponseArticle article,
                            CancellationToken cancellationToken );
        
        void Finish(    InitiateInputMessageDetails details,
                        InitiateInputMessageArticle article    );

        void FinishAsync(   InitiateInputMessageDetails details,
                            InitiateInputMessageArticle article    );

        void FinishAsync(   InitiateInputMessageDetails details,
                            InitiateInputMessageArticle article,
                            CancellationToken cancellationToken );
    }
}
