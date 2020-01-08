using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs.Storage.Status
{
    public interface IStatusServerDialog:IDialog, IDisposable
    {
        event EventHandler<MessageReceivedEventArgs> RequestReceived;

        void SendResponse( StatusResponse response );
        
        Task SendResponseAsync( StatusResponse response );
        Task SendResponseAsync( StatusResponse response, CancellationToken cancellationToken );
    }
}