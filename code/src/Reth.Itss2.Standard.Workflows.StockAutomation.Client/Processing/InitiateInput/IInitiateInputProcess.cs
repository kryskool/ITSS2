using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Standard.Dialogs.Storage.InitiateInput;
using Reth.Protocols;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Client.Processing.InitiateInput
{
    public interface IInitiateInputProcess
    {
        event EventHandler<InitiateInputFinishedEventArgs> Finished;

        IMessageId Id{ get; }

        InitiateInputResponse Start(    InitiateInputRequestDetails details,
                                        InitiateInputRequestArticle article,
                                        bool isNewDelivery,
                                        bool setPickingIndicator    );

        Task<InitiateInputResponse> StartAsync( InitiateInputRequestDetails details,
                                                InitiateInputRequestArticle article,
                                                bool isNewDelivery,
                                                bool setPickingIndicator   );

        Task<InitiateInputResponse> StartAsync( InitiateInputRequestDetails details,
                                                InitiateInputRequestArticle article,
                                                bool isNewDelivery,
                                                bool setPickingIndicator,
                                                CancellationToken cancellationToken );
    }
}
