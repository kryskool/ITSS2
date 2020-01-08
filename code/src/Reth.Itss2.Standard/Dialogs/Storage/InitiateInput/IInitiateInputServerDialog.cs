using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs.Storage.InitiateInput
{
    public interface IInitiateInputServerDialog:IDialog, IDisposable
    {
        event EventHandler<MessageReceivedEventArgs> RequestReceived;
        
        void SendResponse( InitiateInputResponse response );
        
        Task SendResponseAsync( InitiateInputResponse response );
        Task SendResponseAsync( InitiateInputResponse response, CancellationToken cancellationToken );
        
        void SendMessage( InitiateInputMessage message );
        
        Task SendMessageAsync( InitiateInputMessage message );
        Task SendMessageAsync( InitiateInputMessage message, CancellationToken cancellationToken );
    }
}