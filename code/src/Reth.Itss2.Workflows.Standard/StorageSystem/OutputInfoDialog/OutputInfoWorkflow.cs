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
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.OutputInfoDialog;
using Reth.Itss2.Dialogs.Standard.Protocol.Roles.StorageSystem;

namespace Reth.Itss2.Workflows.Standard.StorageSystem.OutputInfoDialog
{
    internal class OutputInfoWorkflow:Workflow, IOutputInfoWorkflow
    {
        public event EventHandler<ProcessStartEventArgs<IOutputInfoProcess>>? ProcessStarted;

        public OutputInfoWorkflow( IStorageSystemWorkflowProvider workflowProvider )
        :
            base( workflowProvider )
        {
            this.Dialog.RequestReceived += this.Dialog_RequestReceived;
        }

        private IStorageSystemOutputInfoDialog Dialog
        {
            get{ return this.DialogProvider.OutputInfoDialog; }
        }

        public void SendResponse( OutputInfoResponse response )
        {
            this.SendResponse(  response,
                                () =>
                                {
                                    this.Dialog.SendResponse( response );
                                } );
        }

        public Task SendResponseAsync( OutputInfoResponse response, CancellationToken cancellationToken = default )
        {
            return this.SendResponseAsync(  response,
                                            () =>
                                            {
                                                return this.Dialog.SendResponseAsync( response, cancellationToken );
                                            } );
        }

        private void Dialog_RequestReceived( Object sender, MessageReceivedEventArgs e )
        {
            OutputInfoRequest request = ( OutputInfoRequest )e.Message;

            this.OnRequestReceived( request,
                                    () =>
                                    {
                                        IOutputInfoProcess process = new OutputInfoProcess( this, request );

                                        this.ProcessStarted?.Invoke( this, new ProcessStartEventArgs<IOutputInfoProcess>( process ) );
                                    }   );
        }
    }
}
