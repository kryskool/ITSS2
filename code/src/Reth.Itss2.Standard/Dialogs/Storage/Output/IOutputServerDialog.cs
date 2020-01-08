using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs.Storage.Output
{
    public interface IOutputServerDialog:IDialog, IDisposable
    {
        event EventHandler<MessageReceivedEventArgs> RequestReceived;
        
        void SendResponse( OutputResponse response );
        
        Task SendResponseAsync( OutputResponse response );
        Task SendResponseAsync( OutputResponse response, CancellationToken cancellationToken );
        
        void SendMessage( OutputMessage message );
        
        Task SendMessageAsync( OutputMessage message );
        Task SendMessageAsync( OutputMessage message, CancellationToken cancellationToken );
    }
}