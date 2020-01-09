using Reth.Itss2.Experimental.Dialogs.ArticleData.ArticlePrice;

namespace Reth.Itss2.Experimental.Dialogs
{
    public interface ILocalClientDialogProvider:Reth.Itss2.Standard.Dialogs.ILocalClientDialogProvider
    {
        IArticlePriceClientDialog ArticlePrice{ get; }
    }
}