using Reth.Itss2.Experimental.Dialogs.ArticleData.ArticlePrice;
using Reth.Itss2.Experimental.Dialogs.SalesTransactions.ArticleSelected;
using Reth.Itss2.Experimental.Dialogs.SalesTransactions.ShoppingCart;
using Reth.Itss2.Experimental.Dialogs.SalesTransactions.ShoppingCartUpdate;

namespace Reth.Itss2.Experimental.Dialogs
{
    public interface IRemoteClientDialogProvider:Reth.Itss2.Standard.Dialogs.IRemoteClientDialogProvider
    {
        IArticlePriceServerDialog ArticlePrice{ get; }
        IArticleSelectedServerDialog ArticleSelected{ get; }
        IShoppingCartServerDialog ShoppingCart{ get; }
        IShoppingCartUpdateServerDialog ShoppingCartUpdate{ get; }
    }
}