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
using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.Experimental.Protocol.Messages;
using Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ShoppingCartUpdate;
using Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ShoppingCartUpdate.Reactive;
using Reth.Itss2.Workflows.Standard;

namespace Reth.Itss2.Workflows.Experimental.Messages.ShoppingCartUpdate.Reactive
{
    public class ShoppingCartUpdateWorkflow:SubscribedWorkflow<IShoppingCartUpdateDialog>, IShoppingCartUpdateWorkflow
    {
        public event EventHandler<ProcessStartedEventArgs<IShoppingCartUpdateRequestedProcessState>>? ProcessStarted;

        public ShoppingCartUpdateWorkflow(  IShoppingCartUpdateDialog dialog,
                                            ISubscription subscription  )
        :
            base( dialog, subscription )
        {
            dialog.RequestReceived += this.Dialog_RequestReceived;
        }

        internal void SendResponse( ShoppingCartUpdateResponse response )
        {
            this.SendResponse(  response,
                                () =>
                                {
                                    this.Dialog.SendResponse( response );
                                } );
        }

        internal Task SendResponseAsync( ShoppingCartUpdateResponse response, CancellationToken cancellationToken = default )
        {
            return this.SendResponseAsync(  response,
                                            () =>
                                            {
                                                return this.Dialog.SendResponseAsync( response, cancellationToken );
                                            } );
        }

        private ShoppingCartUpdateMessage CreateMessage( ShoppingCartContent shoppingCart )
        {
            return this.CreateMessage(  (   MessageId id,
                                            SubscriberId localSubscriberId,
                                            SubscriberId remoteSubscriberId  ) =>
                                        {
                                            return new ShoppingCartUpdateMessage( id, localSubscriberId, remoteSubscriberId, shoppingCart );
                                        }   );
        }

        public void NotifyContentChange( ShoppingCartContent shoppingCart )
        {
            ShoppingCartUpdateMessage message = this.CreateMessage( shoppingCart );

            this.SendMessage(   message,
                                () =>
                                {
                                    this.Dialog.SendMessage( message );
                                }   );
        }

        public Task NotifyContentChangeAsync( ShoppingCartContent shoppingCart, CancellationToken cancellationToken = default  )
        {
            ShoppingCartUpdateMessage message = this.CreateMessage( shoppingCart );

            return this.SendMessageAsync(   message,
                                            () =>
                                            {
                                                return this.Dialog.SendMessageAsync( message, cancellationToken );
                                            }   );
        }

        private void Dialog_RequestReceived( Object sender, MessageReceivedEventArgs<ShoppingCartUpdateRequest> e )
        {
            ShoppingCartUpdateRequest request = e.Message;

            this.OnMessageReceived( request,
                                    () =>
                                    {
                                        IShoppingCartUpdateRequestedProcessState processState = new ShoppingCartUpdateRequestedProcessState( this, request );

                                        this.ProcessStarted?.Invoke( this, new ProcessStartedEventArgs<IShoppingCartUpdateRequestedProcessState>( processState ) );
                                    }   );
        }
    }
}
