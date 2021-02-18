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
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockInfoDialog;
using Reth.Itss2.Dialogs.Standard.Protocol.Roles.StorageSystem;
using Reth.Itss2.Dialogs.Standard.Serialization;

namespace Reth.Itss2.Workflows.Standard.StorageSystem.StockInfoDialog
{
    internal class StockInfoWorkflow:Workflow<IStorageSystemStockInfoDialog>, IStockInfoWorkflow
    {
        public StockInfoWorkflow(   IStorageSystemWorkflowProvider workflowProvider,
                                    IStorageSystemDialogProvider dialogProvider,
                                    ISerializationProvider serializationProvider    )
        :
            base( workflowProvider, dialogProvider, serializationProvider, dialogProvider.StockInfoDialog )
        {
            this.Dialog.RequestReceived += this.Dialog_RequestReceived;
        }

        private void Dialog_RequestReceived( Object sender, MessageReceivedEventArgs e )
        {
            this.OnRequestReceived( ( StockInfoRequest )e.Message,
                                    this.RequestReceived,
                                    ( StockInfoResponse response ) =>
                                    {
                                        this.Dialog.SendResponse( response );
                                    }   );
        }

        public Func<StockInfoRequest, StockInfoResponse>? RequestReceived
        {
            get; set;
        }

        public void SendMessage( StockInfoMessage message )
        {
            this.OnSendMessage<StockInfoMessage>(   message,
                                                    ( StockInfoMessage message ) =>
                                                    {
                                                        this.Dialog.SendMessage( message );
                                                    }   );
        }

        public async Task SendMessageAsync( StockInfoMessage message, CancellationToken cancellationToken = default )
        {
            await this.OnSendMessageAsync<StockInfoMessage>(    message,
                                                                async ( StockInfoMessage message, CancellationToken cancellationToken ) =>
                                                                {
                                                                    await this.Dialog.SendMessageAsync( message );
                                                                }   );
        }

        protected override void Dispose( bool disposing )
        {
        }
    }
}
