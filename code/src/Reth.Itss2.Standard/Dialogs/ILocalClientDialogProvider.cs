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
    public interface ILocalClientDialogProvider:IDialogProvider
    {
        IArticleInfoClientDialog ArticleInfo{ get; }
        IArticleMasterSetClientDialog ArticleMasterSet{ get; }
        IHelloClientDialog Hello{ get; }
        IInitiateInputClientDialog InitiateInput{ get; }
        IInputClientDialog Input{ get; }
        IKeepAliveClientDialog KeepAlive{ get; }
        IOutputInfoClientDialog OutputInfo{ get; }
        IOutputClientDialog Output{ get; }
        IStatusClientDialog Status{ get; }
        IStockDeliveryInfoClientDialog StockDeliveryInfo{ get; }
        IStockDeliverySetClientDialog StockDeliverySet{ get; }
        IStockInfoClientDialog StockInfo{ get; }
        IStockLocationInfoClientDialog StockLocationInfo{ get; }
        ITaskCancelOutputClientDialog TaskCancelOutput{ get; }
        IUnprocessedClientDialog Unprocessed{ get; }
    }
}