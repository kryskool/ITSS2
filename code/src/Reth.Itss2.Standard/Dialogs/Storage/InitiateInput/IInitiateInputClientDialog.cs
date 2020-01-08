using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs.Storage.InitiateInput
{
    public interface IInitiateInputClientDialog:IDialog, IDisposable
    {
        event EventHandler<MessageReceivedEventArgs> MessageReceived;

        InitiateInputResponse SendRequest( InitiateInputRequest request );
        
        Task<InitiateInputResponse> SendRequestAsync( InitiateInputRequest request );
        Task<InitiateInputResponse> SendRequestAsync( InitiateInputRequest request, CancellationToken cancellationToken );
    }
}