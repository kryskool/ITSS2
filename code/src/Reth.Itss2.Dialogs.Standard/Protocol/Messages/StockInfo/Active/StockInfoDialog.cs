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

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockInfo.Active
{
    public class StockInfoDialog:Dialog, IStockInfoDialog
    {
        public event EventHandler<MessageReceivedEventArgs<StockInfoMessage>>? MessageReceived;

        public StockInfoDialog( IDialogProvider dialogProvider )
        :
            base( Dialogs.StockInfo, dialogProvider )
        {
        }

        protected void OnMessageReceived( StockInfoMessage message )
        {
            this.MessageReceived?.Invoke( this, new MessageReceivedEventArgs<StockInfoMessage>( message, this.DialogProvider ) );
        }

        public override void Connect( IMessageTransmitter messageTransmitter )
        {
            base.Connect(   messageTransmitter,
                            ( IMessage message ) =>
                            {
                                this.Dispatch<StockInfoMessage>( message, this.OnMessageReceived );
                            }   );
        }

        public StockInfoResponse SendRequest( StockInfoRequest request )
        {
            return base.SendRequest<StockInfoRequest, StockInfoResponse>( request );
        }

        public Task<StockInfoResponse> SendRequestAsync( StockInfoRequest request, CancellationToken cancellationToken = default )
        {
            return base.SendRequestAsync<StockInfoRequest, StockInfoResponse>( request, cancellationToken );
        }
    }
}
