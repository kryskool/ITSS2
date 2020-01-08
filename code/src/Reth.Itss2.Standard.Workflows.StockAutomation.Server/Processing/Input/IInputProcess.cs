using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Standard.Dialogs.Storage.Input;
using Reth.Protocols;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Server.Processing.Input
{
    public interface IInputProcess
    {
        IMessageId Id{ get; }

        InputResponse Start(    InputRequestArticle article,
                                Nullable<bool> isNewDelivery,
                                Nullable<bool> setPickingIndicator  );

        Task<InputResponse> StartAsync( InputRequestArticle article,
                                        Nullable<bool> isNewDelivery,
                                        Nullable<bool> setPickingIndicator  );

        Task<InputResponse> StartAsync( InputRequestArticle article,
                                        Nullable<bool> isNewDelivery,
                                        Nullable<bool> setPickingIndicator,
                                        CancellationToken cancellationToken );

        void Finish( InputMessageArticle article, Nullable<bool> isNewDelivery );

        Task FinishAsync( InputMessageArticle article, Nullable<bool> isNewDelivery );
        Task FinishAsync( InputMessageArticle article, Nullable<bool> isNewDelivery, CancellationToken cancellationToken );
    }
}
