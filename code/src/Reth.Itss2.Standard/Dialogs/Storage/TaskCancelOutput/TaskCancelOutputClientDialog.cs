using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs.Storage.TaskCancelOutput
{
    internal class TaskCancelOutputClientDialog:Dialog, ITaskCancelOutputClientDialog
    {
        internal TaskCancelOutputClientDialog( IMessageTransceiver messageTransceiver )
        :
            base( DialogName.TaskCancelOutput, messageTransceiver )
        {
        }

        ~TaskCancelOutputClientDialog()
        {
            this.Dispose( false );
        }

        public TaskCancelOutputResponse SendRequest( TaskCancelOutputRequest request )
        {
            return base.SendRequest<TaskCancelOutputRequest, TaskCancelOutputResponse>( request );
        }

        public Task<TaskCancelOutputResponse> SendRequestAsync( TaskCancelOutputRequest request )
        {
            return base.SendRequestAsync<TaskCancelOutputRequest, TaskCancelOutputResponse>( request, CancellationToken.None );
        }

        public Task<TaskCancelOutputResponse> SendRequestAsync( TaskCancelOutputRequest request, CancellationToken cancellationToken )
        {
            return base.SendRequestAsync<TaskCancelOutputRequest, TaskCancelOutputResponse>( request, cancellationToken );
        }

        public void Dispose()
        {
            this.Dispose( true );

            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
        }
    }
}