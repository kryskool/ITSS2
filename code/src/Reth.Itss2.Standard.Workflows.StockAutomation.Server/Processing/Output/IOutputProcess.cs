using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Standard.Dialogs.Storage;
using Reth.Itss2.Standard.Dialogs.Storage.Output;
using Reth.Protocols;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Server.Processing.Output
{
    public interface IOutputProcess
    {
        IMessageId Id{ get; }

        void Start( OutputResponseDetails details );
        Task StartAsync( OutputResponseDetails details );
        Task StartAsync( OutputResponseDetails details, CancellationToken cancellationToken );
        
        void ReportProgress(    OutputMessageDetails details,
                                IEnumerable<OutputArticle> articles,
                                IEnumerable<OutputBox> boxes    );

        void ReportProgressAsync(   OutputMessageDetails details,
                                    IEnumerable<OutputArticle> articles,
                                    IEnumerable<OutputBox> boxes    );

        void ReportProgressAsync(   OutputMessageDetails details,
                                    IEnumerable<OutputArticle> articles,
                                    IEnumerable<OutputBox> boxes,
                                    CancellationToken cancellationToken );

        void Finish(    OutputMessageDetails details,
                        IEnumerable<OutputArticle> articles,
                        IEnumerable<OutputBox> boxes    );

        void FinishAsync(   OutputMessageDetails details,
                            IEnumerable<OutputArticle> articles,
                            IEnumerable<OutputBox> boxes    );

        void FinishAsync(   OutputMessageDetails details,
                            IEnumerable<OutputArticle> articles,
                            IEnumerable<OutputBox> boxes,
                            CancellationToken cancellationToken );
    }
}
