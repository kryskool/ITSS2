using Reth.Itss2.StandardExtensions.Dialogs.Storage.ConfigurationGet;

namespace Reth.Itss2.StandardExtensions.Dialogs
{
    public interface ILocalClientDialogProvider:Reth.Itss2.Standard.Dialogs.ILocalClientDialogProvider
    {
        IConfigurationGetClientDialog ConfigurationGet{ get; }
    }
}