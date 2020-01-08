using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs.Storage.TaskCancelOutput
{
    public interface ITaskCancelOutputClientDialog:IDialog, IDisposable
    {
        TaskCancelOutputResponse SendRequest( TaskCancelOutputRequest request );
        
        Task<TaskCancelOutputResponse> SendRequestAsync( TaskCancelOutputRequest request );
        Task<TaskCancelOutputResponse> SendRequestAsync( TaskCancelOutputRequest request, CancellationToken cancellationToken );
    }
}