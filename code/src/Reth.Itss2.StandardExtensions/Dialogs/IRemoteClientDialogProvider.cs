using Reth.Itss2.StandardExtensions.Dialogs.Storage.ConfigurationGet;

namespace Reth.Itss2.StandardExtensions.Dialogs
{
    public interface IRemoteClientDialogProvider:Reth.Itss2.Standard.Dialogs.IRemoteClientDialogProvider
    {
        IConfigurationGetServerDialog ConfigurationGet{ get; }
    }
}