// Implementation of the WWKS2 protocol.
// Copyright (C) 2020  Thomas Reth

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.

// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Dialogs.Standard.Protocol;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.TaskCancelOutput;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.TaskCancelOutput.Reactive;

namespace Reth.Itss2.Workflows.Standard.Messages.TaskCancelOutput.Reactive
{
    public class TaskCancelOutputWorkflow:SubscribedWorkflow<ITaskCancelOutputDialog>, ITaskCancelOutputWorkflow
    {
        public event EventHandler<ProcessStartedEventArgs<ITaskCancelOutputRequestedProcessState>>? ProcessStarted;

        public TaskCancelOutputWorkflow(    ITaskCancelOutputDialog dialog,
                                            ISubscription subscription  )
        :
            base( dialog, subscription )
        {
            dialog.RequestReceived += this.Dialog_RequestReceived;
        }        

        internal void SendResponse( TaskCancelOutputResponse response )
        {
            this.SendResponse(  response,
                                () =>
                                {
                                    this.Dialog.SendResponse( response );
                                } );
        }

        internal Task SendResponseAsync( TaskCancelOutputResponse response, CancellationToken cancellationToken = default )
        {
            return this.SendResponseAsync(  response,
                                            () =>
                                            {
                                                return this.Dialog.SendResponseAsync( response, cancellationToken );
                                            } );
        }

        private void Dialog_RequestReceived( Object? sender, MessageReceivedEventArgs<TaskCancelOutputRequest> e )
        {
            TaskCancelOutputRequest request = e.Message;

            this.OnMessageReceived( request,
                                    () =>
                                    {
                                        ITaskCancelOutputRequestedProcessState processState = new TaskCancelOutputRequestedProcessState( this, request );

                                        this.ProcessStarted?.Invoke( this, new ProcessStartedEventArgs<ITaskCancelOutputRequestedProcessState>( processState ) );
                                    }   );
        }
    }
}
