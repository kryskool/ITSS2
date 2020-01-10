using Reth.Itss2.Experimental.Dialogs.ArticleData.ArticlePrice;
using Reth.Itss2.Experimental.Dialogs.SalesTransactions.ArticleSelected;

namespace Reth.Itss2.Experimental.Dialogs
{
    public interface IRemoteClientDialogProvider:Reth.Itss2.Standard.Dialogs.IRemoteClientDialogProvider
    {
        IArticlePriceServerDialog ArticlePrice{ get; }
        IArticleSelectedServerDialog ArticleSelected{ get; }
    }
}