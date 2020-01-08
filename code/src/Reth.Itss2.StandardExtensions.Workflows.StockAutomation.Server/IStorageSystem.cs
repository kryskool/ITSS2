using System;

using Reth.Itss2.StandardExtensions.Dialogs.Storage.ConfigurationGet;

namespace Reth.Itss2.StandardExtensions.Workflows.StockAutomation.Server
{
    public interface IStorageSystem:Reth.Itss2.Standard.Workflows.StockAutomation.Server.IStorageSystem
    {
        Func<IStorageSystem, ConfigurationGetRequest, ConfigurationGetResponse> ConfigurationGetRequestReceivedCallback{ get; set; }
    }
}
