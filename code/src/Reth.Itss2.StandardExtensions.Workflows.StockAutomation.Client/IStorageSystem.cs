using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.StandardExtensions.Dialogs.Storage.ConfigurationGet;

namespace Reth.Itss2.StandardExtensions.Workflows.StockAutomation.Client
{
    public interface IStorageSystem:Reth.Itss2.Standard.Workflows.StockAutomation.Client.IStorageSystem
    {
        ConfigurationGetResponse GetConfiguration();
        Task<ConfigurationGetResponse> GetConfigurationAsync();
        Task<ConfigurationGetResponse> GetConfigurationAsync( CancellationToken cancellationToken );
    }
}
