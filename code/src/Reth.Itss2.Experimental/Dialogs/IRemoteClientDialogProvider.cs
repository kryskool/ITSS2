using Reth.Itss2.Experimental.Dialogs.ArticleData.ArticlePrice;

namespace Reth.Itss2.Experimental.Dialogs
{
    public interface IRemoteClientDialogProvider:Reth.Itss2.Standard.Dialogs.IRemoteClientDialogProvider
    {
        IArticlePriceServerDialog ArticlePrice{ get; }
    }
}