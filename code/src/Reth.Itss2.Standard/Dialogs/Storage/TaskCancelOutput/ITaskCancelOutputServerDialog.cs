using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs.Storage.TaskCancelOutput
{
    public interface ITaskCancelOutputServerDialog:IDialog, IDisposable
    {
        event EventHandler<MessageReceivedEventArgs> RequestReceived;
        
        void SendResponse( TaskCancelOutputResponse response );
        
        Task SendResponseAsync( TaskCancelOutputResponse response );
        Task SendResponseAsync( TaskCancelOutputResponse response, CancellationToken cancellationToken );
    }
}