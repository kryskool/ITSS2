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

using Reth.Itss2.Dialogs.Experimental.Protocol.Messages;
using Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ShoppingCartUpdate;
using Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ShoppingCartUpdate.Active;
using Reth.Itss2.Dialogs.Standard.Protocol;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Workflows.Standard;

namespace Reth.Itss2.Workflows.Experimental.Messages.ShoppingCartUpdate.Active
{
    public class ShoppingCartUpdateWorkflow:SubscribedWorkflow<IShoppingCartUpdateDialog>, IShoppingCartUpdateWorkflow
    {
        public event EventHandler<MessageReceivedEventArgs<ShoppingCartUpdateMessage>>? ContentChanged;

        public ShoppingCartUpdateWorkflow(  IShoppingCartUpdateDialog dialog,
                                            ISubscription subscription  )
        :
            base( dialog, subscription )
        {
            dialog.MessageReceived += this.Dialog_MessageReceived;
        }

        private ShoppingCartUpdateRequest CreateRequest( ShoppingCartContent shoppingCart )
        {
            return this.CreateRequest(  (   MessageId messageId,
                                            SubscriberId localSubscriberId,
                                            SubscriberId remoteSubscriberId ) =>
                                        {
                                            return new ShoppingCartUpdateRequest(   messageId,
                                                                                    localSubscriberId,
                                                                                    remoteSubscriberId,
                                                                                    shoppingCart    );
                                        }   );
        }

        public IShoppingCartUpdateFinishedProcessState StartProcess( ShoppingCartContent shoppingCart )
        {
            ShoppingCartUpdateRequest request = this.CreateRequest( shoppingCart );

            ShoppingCartUpdateResponse response = this.SendRequest( request,
                                                                    () =>
                                                                    {
                                                                        return this.Dialog.SendRequest( request );
                                                                    }   );

            return new ShoppingCartUpdateFinishedProcessState( request, response );
        }

        public async Task<IShoppingCartUpdateFinishedProcessState> StartProcessAsync( ShoppingCartContent shoppingCart, CancellationToken cancellationToken = default )
        {
            ShoppingCartUpdateRequest request = this.CreateRequest( shoppingCart );

            ShoppingCartUpdateResponse response = await this.SendRequestAsync(  request,
                                                                                () =>
                                                                                {
                                                                                    return this.Dialog.SendRequestAsync( request, cancellationToken );
                                                                                }   ).ConfigureAwait( continueOnCapturedContext:false );

            return new ShoppingCartUpdateFinishedProcessState( request, response );
        }

        private void Dialog_MessageReceived( Object? sender, MessageReceivedEventArgs<ShoppingCartUpdateMessage> e )
        {
            ShoppingCartUpdateMessage message = e.Message;

            this.OnMessageReceived( message,
                                    () =>
                                    {
                                        this.ContentChanged?.Invoke( this, e );
                                    }   );
        }
    }
}
