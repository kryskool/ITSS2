using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs.General.KeepAlive
{
    public interface IKeepAliveDialog:IDialog, IDisposable
    {
        event EventHandler<MessageReceivedEventArgs> RequestReceived;

        KeepAliveResponse SendRequest( KeepAliveRequest request );
        
        Task<KeepAliveResponse> SendRequestAsync( KeepAliveRequest request );
        Task<KeepAliveResponse> SendRequestAsync( KeepAliveRequest request, CancellationToken cancellationToken );
        
        void SendResponse( KeepAliveResponse response );
        
        Task SendResponseAsync( KeepAliveResponse response );
        Task SendResponseAsync( KeepAliveResponse response, CancellationToken cancellationToken );
    }
}
