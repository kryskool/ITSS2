using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs.Storage.Status
{
    public interface IStatusClientDialog:IDialog, IDisposable
    {
        StatusResponse SendRequest( StatusRequest request );
        
        Task<StatusResponse> SendRequestAsync( StatusRequest request );
        Task<StatusResponse> SendRequestAsync( StatusRequest request, CancellationToken cancellationToken );
    }
}