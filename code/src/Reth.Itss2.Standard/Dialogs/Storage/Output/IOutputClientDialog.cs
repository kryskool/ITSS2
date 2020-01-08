using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs.Storage.Output
{
    public interface IOutputClientDialog:IDialog, IDisposable
    {
        event EventHandler<MessageReceivedEventArgs> MessageReceived;

        OutputResponse SendRequest( OutputRequest request );
        
        Task<OutputResponse> SendRequestAsync( OutputRequest request );
        Task<OutputResponse> SendRequestAsync( OutputRequest request, CancellationToken cancellationToken );
    }
}