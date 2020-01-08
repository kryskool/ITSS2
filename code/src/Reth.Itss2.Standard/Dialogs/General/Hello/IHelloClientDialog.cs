using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs.General.Hello
{
    public interface IHelloClientDialog:IDialog, IDisposable
    {
        HelloResponse SendRequest( HelloRequest request );
        
        Task<HelloResponse> SendRequestAsync( HelloRequest request );
        Task<HelloResponse> SendRequestAsync( HelloRequest request, CancellationToken cancellationToken );
    }
}