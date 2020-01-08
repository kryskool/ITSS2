using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs.Storage.Input
{
    public interface IInputClientDialog:IDialog, IDisposable
    {
        event EventHandler<MessageReceivedEventArgs> RequestReceived;
        event EventHandler<MessageReceivedEventArgs> MessageReceived;
        
        void SendResponse( InputResponse response );
        
        Task SendResponseAsync( InputResponse response );
        Task SendResponseAsync( InputResponse response, CancellationToken cancellationToken );
    }
}