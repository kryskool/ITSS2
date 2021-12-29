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

using Reth.Itss2.Messaging;

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages.KeepAlive
{
    public class KeepAliveDialog:Dialog, IKeepAliveDialog
    {
        public event EventHandler<MessageReceivedEventArgs<KeepAliveRequest>>? RequestReceived;

        public KeepAliveDialog( IDialogProvider dialogProvider )
        :
            base( StandardDialogs.KeepAlive, dialogProvider )
        {
        }

        protected void OnRequestReceived( KeepAliveRequest request )
        {
            this.RequestReceived?.Invoke( this, new MessageReceivedEventArgs<KeepAliveRequest>( request, this.DialogProvider ) );
        }

        public override void Connect( IMessageTransmitter messageTransmitter )
        {
            base.Connect(   messageTransmitter,
                            ( IMessage message ) =>
                            {
                                this.Dispatch<KeepAliveRequest>( message, this.OnRequestReceived );
                            }   );
        }

        public KeepAliveResponse SendRequest( KeepAliveRequest request )
        {
            return base.SendRequest<KeepAliveRequest, KeepAliveResponse>( request );
        }

        public Task<KeepAliveResponse> SendRequestAsync( KeepAliveRequest request, CancellationToken cancellationToken = default )
        {
            return base.SendRequestAsync<KeepAliveRequest, KeepAliveResponse>( request, cancellationToken );
        }
        
        public void SendResponse( KeepAliveResponse response )
        {
            base.SendResponse( response );
        }
        
        public Task SendResponseAsync( KeepAliveResponse response, CancellationToken cancellationToken = default )
        {
            return base.SendResponseAsync( response, cancellationToken );
        }
    }
}
