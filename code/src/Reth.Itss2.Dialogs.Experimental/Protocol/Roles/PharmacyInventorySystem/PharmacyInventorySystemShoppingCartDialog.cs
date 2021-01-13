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

using Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ShoppingCartDialog;
using Reth.Itss2.Dialogs.Standard.Protocol;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages;

namespace Reth.Itss2.Dialogs.Experimental.Protocol.Roles.PharmacyInventorySystem
{
    public class PharmacyInventorySystemShoppingCartDialog:Dialog, IPharmacyInventorySystemShoppingCartDialog
    {
        public event EventHandler<MessageReceivedEventArgs>? RequestReceived;

        public PharmacyInventorySystemShoppingCartDialog( IDialogProvider dialogProvider )
        :
            base( "ShoppingCart", dialogProvider )
        {
        }

        protected void OnRequestReceived( ShoppingCartRequest request )
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
                                this.Dispatch<ShoppingCartRequest>( message, this.OnRequestReceived );
                            }   );
        }

        public void SendResponse( ShoppingCartResponse response )
        {
            base.SendResponse( response );
        }
        
        public Task SendResponseAsync( ShoppingCartResponse response )
        {
            return base.SendResponseAsync( response );
        }
    }
}
