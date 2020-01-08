using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs.General.Unprocessed
{
    public interface IUnprocessedDialog:IDialog, IDisposable
    {
        event EventHandler<MessageReceivedEventArgs> MessageReceived;

        void SendMessage( UnprocessedMessage message );
        
        Task SendMessageAsync( UnprocessedMessage message );
        Task SendMessageAsync( UnprocessedMessage message, CancellationToken cancellationToken );
    }
}