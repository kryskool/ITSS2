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

using Reth.Itss2.Dialogs.Standard.Protocol;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockLocationInfoDialog;
using Reth.Itss2.Dialogs.Standard.Protocol.Roles.StorageSystem;
using Reth.Itss2.Dialogs.Standard.Serialization;

namespace Reth.Itss2.Workflows.Standard.StorageSystem.StockLocationInfoDialog
{
    internal class StockLocationInfoWorkflow:Workflow<IStorageSystemStockLocationInfoDialog>, IStockLocationInfoWorkflow
    {
        public StockLocationInfoWorkflow(   IStorageSystemWorkflowProvider workflowProvider,
                                            IStorageSystemDialogProvider dialogProvider,
                                            ISerializationProvider serializationProvider    )
        :
            base( workflowProvider, dialogProvider, serializationProvider, dialogProvider.StockLocationInfoDialog )
        {
            this.Dialog.RequestReceived += this.Dialog_RequestReceived;
        }

        private void Dialog_RequestReceived( Object sender, MessageReceivedEventArgs e )
        {
            this.OnRequestReceived( ( StockLocationInfoRequest )e.Message,
                                    this.RequestReceived,
                                    ( StockLocationInfoResponse response ) =>
                                    {
                                        this.Dialog.SendResponse( response );
                                    }   );
        }

        public Func<StockLocationInfoRequest, StockLocationInfoResponse>? RequestReceived
        {
            get; set;
        }

        protected override void Dispose( bool disposing )
        {
        }
    }
}
