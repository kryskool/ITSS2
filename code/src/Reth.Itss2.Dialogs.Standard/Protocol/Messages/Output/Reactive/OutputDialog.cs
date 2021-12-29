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

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages.Output.Reactive
{
    public class OutputDialog:Dialog, IOutputDialog
    {
        public event EventHandler<MessageReceivedEventArgs<OutputRequest>>? RequestReceived;

        public OutputDialog( IDialogProvider dialogProvider )
        :
            base( StandardDialogs.Output, dialogProvider )
        {
        }

        protected void OnRequestReceived( OutputRequest request )
        {
            this.RequestReceived?.Invoke( this, new MessageReceivedEventArgs<OutputRequest>( request, this.DialogProvider ) );
        }

        public override void Connect( IMessageTransmitter messageTransmitter )
        {
            base.Connect(   messageTransmitter,
                            ( IMessage message ) =>
                            {
                                this.Dispatch<OutputRequest>( message, this.OnRequestReceived );
                            }   );
        }

        public void SendResponse( OutputResponse response )
        {
            base.SendResponse( response );
        }
        
        public Task SendResponseAsync( OutputResponse response, CancellationToken cancellationToken = default )
        {
            return base.SendResponseAsync( response, cancellationToken );
        }

        public void SendMessage( OutputMessage message )
        {
            base.SendMessage( message );
        }
        
        public Task SendMessageAsync( OutputMessage message, CancellationToken cancellationToken = default )
        {
            return base.SendMessageAsync( message, cancellationToken );
        }
    }
}
