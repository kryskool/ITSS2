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
using System.Threading.Tasks;

using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.TaskCancelOutputDialog;

namespace Reth.Itss2.Dialogs.Standard.Protocol.Roles.StorageSystem
{
    public class StorageSystemTaskCancelOutputDialog:Dialog, IStorageSystemTaskCancelOutputDialog
    {
        public event EventHandler<MessageReceivedEventArgs>? RequestReceived;

        public StorageSystemTaskCancelOutputDialog( IDialogProvider dialogProvider )
        :
            base( Dialogs.TaskCancelOutput, dialogProvider )
        {
        }

        protected void OnRequestReceived( TaskCancelOutputRequest request )
        {
            if( this.RequestReceived is not null )
            {
                this.RequestReceived.Invoke( this, new MessageReceivedEventArgs( request, this.DialogProvider ) );
            }
        }

        public override void Connect( IMessageTransmitter messageTransmitter )
        {
            base.Connect(   messageTransmitter,
                            ( IMessage message ) =>
                            {
                                this.Dispatch<TaskCancelOutputRequest>( message, this.OnRequestReceived );
                            }   );
        }

        public void SendResponse( TaskCancelOutputResponse response )
        {
            base.SendResponse( response );
        }
        
        public Task SendResponseAsync( TaskCancelOutputResponse response )
        {
            return base.SendResponseAsync( response );
        }
    }
}
