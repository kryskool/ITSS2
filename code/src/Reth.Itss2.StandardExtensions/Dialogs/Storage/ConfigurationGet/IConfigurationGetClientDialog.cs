using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols.Dialogs;

namespace Reth.Itss2.StandardExtensions.Dialogs.Storage.ConfigurationGet
{
    public interface IConfigurationGetClientDialog:IDialog, IDisposable
    {
        ConfigurationGetResponse SendRequest( ConfigurationGetRequest request );
        
        Task<ConfigurationGetResponse> SendRequestAsync( ConfigurationGetRequest request );
        Task<ConfigurationGetResponse> SendRequestAsync( ConfigurationGetRequest request, CancellationToken cancellationToken );
    }
}