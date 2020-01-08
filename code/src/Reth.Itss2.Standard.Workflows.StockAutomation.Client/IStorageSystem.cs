using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.ArticleData.ArticleInfo;
using Reth.Itss2.Standard.Dialogs.ArticleData.ArticleMasterSet;
using Reth.Itss2.Standard.Dialogs.ArticleData.StockDeliveryInfo;
using Reth.Itss2.Standard.Dialogs.ArticleData.StockDeliverySet;
using Reth.Itss2.Standard.Dialogs.Storage.Input;
using Reth.Itss2.Standard.Dialogs.Storage.OutputInfo;
using Reth.Itss2.Standard.Dialogs.Storage.Status;
using Reth.Itss2.Standard.Dialogs.Storage.StockInfo;
using Reth.Itss2.Standard.Dialogs.Storage.StockLocationInfo;
using Reth.Itss2.Standard.Dialogs.Storage.TaskCancelOutput;
using Reth.Itss2.Standard.Workflows.StockAutomation.Client.Processing.InitiateInput;
using Reth.Itss2.Standard.Workflows.StockAutomation.Client.Processing.Output;
using Reth.Protocols;
using Reth.Protocols.Transfer;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Client
{
    public interface IStorageSystem:IDisposable, ISubscriberNode
    {
        event EventHandler<InputRequestReceivedEventArgs> InputRequestReceived;

        event EventHandler<MessageReceivedEventArgs> OutputMessageReceived;
        event EventHandler<MessageReceivedEventArgs> StockInfoMessageReceived;

        event EventHandler<UnhandledMessageEventArgs> MessageUnhandled;
        event EventHandler Disconnected;

        Func<IStorageSystem, ArticleInfoRequest, ArticleInfoResponse> ArticleInfoRequestReceivedCallback{ get; set; }

        ILocalMessageClient MessageClient{ get; }
        ILocalClientDialogProvider DialogProvider{ get; }

        void Connect();
        Task ConnectAsync();

        void Disconnect();

        StatusResponse GetStatus( bool includeDetails );
        Task<StatusResponse> GetStatusAsync( bool includeDetails );
        Task<StatusResponse> GetStatusAsync( bool includeDetails, CancellationToken cancellationToken );

        StockLocationInfoResponse GetStockLocationInfo();
        Task<StockLocationInfoResponse> GetStockLocationInfoAsync();
        Task<StockLocationInfoResponse> GetStockLocationInfoAsync( CancellationToken cancellationToken );

        OutputInfoResponse GetOutputInfo( MessageId outputId, bool includeTaskDetails );
        Task<OutputInfoResponse> GetOutputInfoAsync( MessageId outputId, bool includeTaskDetails );
        Task<OutputInfoResponse> GetOutputInfoAsync( MessageId outputId, bool includeTaskDetails, CancellationToken cancellationToken );

        StockDeliveryInfoResponse GetStockDeliveryInfo( String deliveryNumber, bool includeTaskDetails );
        Task<StockDeliveryInfoResponse> GetStockDeliveryInfoAsync( String deliveryNumber, bool includeTaskDetails );
        Task<StockDeliveryInfoResponse> GetStockDeliveryInfoAsync( String deliveryNumber, bool includeTaskDetails, CancellationToken cancellationToken );

        StockInfoResponse GetStockInfo( bool includePacks,
                                        bool includeArticleDetails,
                                        IEnumerable<StockInfoRequestCriteria> criterias );

        Task<StockInfoResponse> GetStockInfoAsync(  bool includePacks,
                                                    bool includeArticleDetails,
                                                    IEnumerable<StockInfoRequestCriteria> criterias );

        Task<StockInfoResponse> GetStockInfoAsync(  bool includePacks,
                                                    bool includeArticleDetails,
                                                    IEnumerable<StockInfoRequestCriteria> criterias,
                                                    CancellationToken cancellationToken );

        ArticleMasterSetResponse SetArticleMaster( IEnumerable<ArticleMasterSetArticle> articles );
        Task<ArticleMasterSetResponse> SetArticleMasterAsync( IEnumerable<ArticleMasterSetArticle> articles );
        Task<ArticleMasterSetResponse> SetArticleMasterAsync( IEnumerable<ArticleMasterSetArticle> articles, CancellationToken cancellationToken );

        StockDeliverySetResponse SetStockDelivery( IEnumerable<StockDelivery> deliveries );
        Task<StockDeliverySetResponse> SetStockDeliveryAsync( IEnumerable<StockDelivery> deliveries );
        Task<StockDeliverySetResponse> SetStockDeliveryAsync( IEnumerable<StockDelivery> deliveries, CancellationToken cancellationToken );

        TaskCancelOutputResponse CancelOutput( MessageId outputId );
        Task<TaskCancelOutputResponse> CancelOutputAsync( MessageId outputId );
        Task<TaskCancelOutputResponse> CancelOutputAsync( MessageId outputId, CancellationToken cancellationToken );

        IInitiateInputProcess CreateInitiateInputProcess();
        IOutputProcess CreateOutputProcess();
    }
}
