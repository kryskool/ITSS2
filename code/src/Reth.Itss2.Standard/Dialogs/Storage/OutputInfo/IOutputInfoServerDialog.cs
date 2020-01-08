using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs.Storage.OutputInfo
{
    public interface IOutputInfoServerDialog:IDialog, IDisposable
    {
        event EventHandler<MessageReceivedEventArgs> RequestReceived;

        void SendResponse( OutputInfoResponse response );
        
        Task SendResponseAsync( OutputInfoResponse response );
        Task SendResponseAsync( OutputInfoResponse response, CancellationToken cancellationToken );
    }
}