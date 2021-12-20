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
using Reth.Itss2.Serialization;

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages.Output.Active
{
    public class OutputDialog:Dialog, IOutputDialog
    {
        public event EventHandler<MessageReceivedEventArgs<OutputMessage>>? MessageReceived;

        public OutputDialog( IDialogProvider dialogProvider )
        :
            base( StandardDialogs.Output, dialogProvider )
        {
        }

        protected void OnMessageReceived( OutputMessage message )
        {
            this.MessageReceived?.Invoke( this, new MessageReceivedEventArgs<OutputMessage>( message, this.DialogProvider ) );
        }

        public override void Connect( IMessageTransmitter messageTransmitter )
        {
            base.Connect(   messageTransmitter,
                            ( IMessage message ) =>
                            {
                                this.Dispatch<OutputMessage>( message, this.OnMessageReceived );
                            }   );
        }

        public OutputResponse SendRequest( OutputRequest request )
        {
            return base.SendRequest<OutputRequest, OutputResponse>( request );
        }

        public Task<OutputResponse> SendRequestAsync( OutputRequest request, CancellationToken cancellationToken = default )
        {
            return base.SendRequestAsync<OutputRequest, OutputResponse>( request, cancellationToken );
        }
    }
}
