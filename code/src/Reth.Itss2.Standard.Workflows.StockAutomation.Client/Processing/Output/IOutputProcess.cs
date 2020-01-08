using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Standard.Dialogs.Storage.Output;
using Reth.Protocols;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Client.Processing.Output
{
    public interface IOutputProcess
    {
        event EventHandler<OutputProcessingEventArgs> Processing;
        event EventHandler<OutputFinishedEventArgs> Finished;

        IMessageId Id{ get; }

        OutputResponse Start(   OutputRequestDetails details,
                                String boxNumber,
                                IEnumerable<OutputCriteria> criterias   );

        Task<OutputResponse> StartAsync(    OutputRequestDetails details,
                                            String boxNumber,
                                            IEnumerable<OutputCriteria> criterias   );

        Task<OutputResponse> StartAsync(    OutputRequestDetails details,
                                            String boxNumber,
                                            IEnumerable<OutputCriteria> criterias,
                                            CancellationToken cancellationToken );
    }
}
