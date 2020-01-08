using Reth.Itss2.Standard.Dialogs.ArticleData.ArticleInfo;
using Reth.Itss2.Standard.Dialogs.ArticleData.ArticleMasterSet;
using Reth.Itss2.Standard.Dialogs.ArticleData.StockDeliveryInfo;
using Reth.Itss2.Standard.Dialogs.ArticleData.StockDeliverySet;
using Reth.Itss2.Standard.Dialogs.General.Hello;
using Reth.Itss2.Standard.Dialogs.General.KeepAlive;
using Reth.Itss2.Standard.Dialogs.General.Unprocessed;
using Reth.Itss2.Standard.Dialogs.Storage.InitiateInput;
using Reth.Itss2.Standard.Dialogs.Storage.Input;
using Reth.Itss2.Standard.Dialogs.Storage.Output;
using Reth.Itss2.Standard.Dialogs.Storage.OutputInfo;
using Reth.Itss2.Standard.Dialogs.Storage.Status;
using Reth.Itss2.Standard.Dialogs.Storage.StockInfo;
using Reth.Itss2.Standard.Dialogs.Storage.StockLocationInfo;
using Reth.Itss2.Standard.Dialogs.Storage.TaskCancelOutput;

namespace Reth.Itss2.Standard.Dialogs
{
    public interface IRemoteClientDialogProvider:IDialogProvider
    {
        IArticleInfoServerDialog ArticleInfo{ get; }
        IArticleMasterSetServerDialog ArticleMasterSet{ get; }
        IHelloServerDialog Hello{ get; }
        IInitiateInputServerDialog InitiateInput{ get; }
        IInputServerDialog Input{ get; }
        IKeepAliveServerDialog KeepAlive{ get; }
        IOutputInfoServerDialog OutputInfo{ get; }
        IOutputServerDialog Output{ get; }
        IStatusServerDialog Status{ get; }
        IStockDeliveryInfoServerDialog StockDeliveryInfo{ get; }
        IStockDeliverySetServerDialog StockDeliverySet{ get; }
        IStockInfoServerDialog StockInfo{ get; }
        IStockLocationInfoServerDialog StockLocationInfo{ get; }
        ITaskCancelOutputServerDialog TaskCancelOutput{ get; }
        IUnprocessedServerDialog Unprocessed{ get; }
    }
}