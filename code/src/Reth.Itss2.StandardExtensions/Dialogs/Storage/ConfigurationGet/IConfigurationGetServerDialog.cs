using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.StandardExtensions.Dialogs.Storage.ConfigurationGet
{
    public interface IConfigurationGetServerDialog:IDialog, IDisposable
    {
        event EventHandler<MessageReceivedEventArgs> RequestReceived;
        
        void SendResponse( ConfigurationGetResponse response );
        
        Task SendResponseAsync( ConfigurationGetResponse response );
        Task SendResponseAsync( ConfigurationGetResponse response, CancellationToken cancellationToken );
    }
}