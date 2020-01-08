using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs.Storage.OutputInfo
{
    public interface IOutputInfoClientDialog:IDialog, IDisposable
    {
        OutputInfoResponse SendRequest( OutputInfoRequest request );
        
        Task<OutputInfoResponse> SendRequestAsync( OutputInfoRequest request );
        Task<OutputInfoResponse> SendRequestAsync( OutputInfoRequest request, CancellationToken cancellationToken );
    }
}