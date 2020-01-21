using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.ArticleData.ArticleInfo;
using Reth.Itss2.Standard.Dialogs.ArticleData.ArticleMasterSet;
using Reth.Itss2.Standard.Dialogs.ArticleData.StockDeliveryInfo;
using Reth.Itss2.Standard.Dialogs.ArticleData.StockDeliverySet;
using Reth.Itss2.Standard.Dialogs.General.Hello;
using Reth.Itss2.Standard.Dialogs.Storage;
using Reth.Itss2.Standard.Dialogs.Storage.Output;
using Reth.Itss2.Standard.Dialogs.Storage.OutputInfo;
using Reth.Itss2.Standard.Dialogs.Storage.Status;
using Reth.Itss2.Standard.Dialogs.Storage.StockInfo;
using Reth.Itss2.Standard.Dialogs.Storage.StockLocationInfo;
using Reth.Itss2.Standard.Dialogs.Storage.TaskCancelOutput;
using Reth.Itss2.Standard.Workflows.StockAutomation.Server.Processing.Input;
using Reth.Protocols;
using Reth.Protocols.Transfer;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Server
{
    public interface IStorageSystem:IDisposable, ISubscriberNode
    {
        event EventHandler<InitiateInputRequestReceivedEventArgs> InitiateInputRequestReceived;
        event EventHandler<OutputRequestReceivedEventArgs> OutputRequestReceived;
        event EventHandler<UnhandledMessageEventArgs> MessageUnhandled;
        event EventHandler Disconnected;

        Func<IStorageSystem, ArticleMasterSetRequest, ArticleMasterSetResponse> ArticleMasterSetRequestReceivedCallback{ get; set; }
        Func<IStorageSystem, HelloRequest, HelloResponse> HelloRequestReceivedCallback{ get; set; }
        Func<IStorageSystem, OutputInfoRequest, OutputInfoResponse> OutputInfoRequestReceivedCallback{ get; set; }
        Func<IStorageSystem, StatusRequest, StatusResponse> StatusRequestReceivedCallback{ get; set; }
        Func<IStorageSystem, StockDeliveryInfoRequest, StockDeliveryInfoResponse> StockDeliveryInfoRequestReceivedCallback{ get; set; }
        Func<IStorageSystem, StockDeliverySetRequest, StockDeliverySetResponse> StockDeliverySetRequestReceivedCallback{ get; set; }
        Func<IStorageSystem, StockInfoRequest, StockInfoResponse> StockInfoRequestReceivedCallback{ get; set; }
        Func<IStorageSystem, StockLocationInfoRequest, StockLocationInfoResponse> StockLocationInfoRequestReceivedCallback{ get; set; }
        Func<IStorageSystem, TaskCancelOutputRequest, TaskCancelOutputResponse> TaskCancelOutputRequestReceivedCallback{ get; set; }

        IRemoteMessageClient MessageClient{ get; }
        IRemoteClientDialogProvider DialogProvider{ get; }

        void Start();
        void Terminate();

        ArticleInfoResponse GetArticleInfo( IEnumerable<ArticleInfoRequestArticle> articles );
        Task<ArticleInfoResponse> GetArticleInfoAsync( IEnumerable<ArticleInfoRequestArticle> articles );
        Task<ArticleInfoResponse> GetArticleInfoAsync( IEnumerable<ArticleInfoRequestArticle> articles, CancellationToken cancellationToken );

        void SendOutputMessage( OutputMessageDetails details,
                                IEnumerable<OutputArticle> articles,
                                IEnumerable<OutputBox> boxes );

        Task SendOutputMessageAsync(    OutputMessageDetails details,
                                        IEnumerable<OutputArticle> articles,
                                        IEnumerable<OutputBox> boxes );

        Task SendOutputMessageAsync(    OutputMessageDetails details,
                                        IEnumerable<OutputArticle> articles,
                                        IEnumerable<OutputBox> boxes,
                                        CancellationToken cancellationToken );

        void SendStockInfoMessage( IEnumerable<StockInfoArticle> articles );
        Task SendStockInfoMessageAsync( IEnumerable<StockInfoArticle> articles );
        Task SendStockInfoMessageAsync( IEnumerable<StockInfoArticle> articles, CancellationToken cancellationToken );

        IInputProcess CreateInputProcess();
        IInputProcess CreateInputProcess( IMessageId messageId );
    }
}
