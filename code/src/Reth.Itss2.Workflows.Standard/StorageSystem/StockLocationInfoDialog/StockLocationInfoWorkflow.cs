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
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockLocationInfoDialog;
using Reth.Itss2.Dialogs.Standard.Protocol.Roles.StorageSystem;

namespace Reth.Itss2.Workflows.Standard.StorageSystem.StockLocationInfoDialog
{
    internal class StockLocationInfoWorkflow:Workflow, IStockLocationInfoWorkflow
    {
        public event EventHandler<ProcessStartEventArgs<IStockLocationInfoProcess>>? ProcessStarted;

        public StockLocationInfoWorkflow( IStorageSystemWorkflowProvider workflowProvider )
        :
            base( workflowProvider )
        {
            this.Dialog.RequestReceived += this.Dialog_RequestReceived;
        }

        private IStorageSystemStockLocationInfoDialog Dialog
        {
            get{ return this.DialogProvider.StockLocationInfoDialog; }
        }

        public void SendResponse( StockLocationInfoResponse response )
        {
            this.SendResponse(  response,
                                () =>
                                {
                                    this.Dialog.SendResponse( response );
                                } );
        }

        public Task SendResponseAsync( StockLocationInfoResponse response, CancellationToken cancellationToken = default )
        {
            return this.SendResponseAsync(  response,
                                            () =>
                                            {
                                                return this.Dialog.SendResponseAsync( response, cancellationToken );
                                            } );
        }

        private void Dialog_RequestReceived( Object sender, MessageReceivedEventArgs e )
        {
            StockLocationInfoRequest request = ( StockLocationInfoRequest )e.Message;

            this.OnRequestReceived( request,
                                    () =>
                                    {
                                        IStockLocationInfoProcess process = new StockLocationInfoProcess( this, request );

                                        this.ProcessStarted?.Invoke( this, new ProcessStartEventArgs<IStockLocationInfoProcess>( process ) );
                                    }   );
        }
    }
}
