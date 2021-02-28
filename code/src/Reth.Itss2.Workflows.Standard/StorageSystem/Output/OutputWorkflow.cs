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
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.OutputDialog;
using Reth.Itss2.Dialogs.Standard.Protocol.Roles.StorageSystem;

namespace Reth.Itss2.Workflows.Standard.StorageSystem.OutputDialog
{
    internal class OutputWorkflow:Workflow, IOutputWorkflow
    {
        public event EventHandler<ProcessStartEventArgs<IOutputProcess>>? ProcessStarted;

        public OutputWorkflow( IStorageSystemWorkflowProvider workflowProvider )
        :
            base( workflowProvider )
        {
            this.Dialog.RequestReceived += this.Dialog_RequestReceived;
        }

        private IStorageSystemOutputDialog Dialog
        {
            get{ return this.DialogProvider.OutputDialog; }
        }

        public void SendResponse( OutputResponse response )
        {
            this.SendResponse(  response,
                                () =>
                                {
                                    this.Dialog.SendResponse( response );
                                } );
        }

        public Task SendResponseAsync( OutputResponse response, CancellationToken cancellationToken = default )
        {
            return this.SendResponseAsync(  response,
                                            () =>
                                            {
                                                return this.Dialog.SendResponseAsync( response, cancellationToken );
                                            } );
        }

        public void SendMessage( OutputMessage message )
        {
            this.SendMessage(   message,
                                () =>
                                {
                                    this.Dialog.SendMessage( message );
                                } );
        }

        public Task SendMessageAsync( OutputMessage message, CancellationToken cancellationToken = default )
        {
            return this.SendMessageAsync(   message,
                                            () =>
                                            {
                                                return this.Dialog.SendMessageAsync( message, cancellationToken );
                                            } );
        }

        private void Dialog_RequestReceived( Object sender, MessageReceivedEventArgs e )
        {
            OutputRequest request = ( OutputRequest )e.Message;

            this.OnRequestReceived( request,
                                    () =>
                                    {
                                        IOutputProcess process = new OutputProcess( this, request );

                                        this.ProcessStarted?.Invoke( this, new ProcessStartEventArgs<IOutputProcess>( process ) );
                                    }   );
        }
    }
}
