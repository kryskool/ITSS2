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

using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ShoppingCart;
using Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ShoppingCart.Active;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Workflows.Standard;

namespace Reth.Itss2.Workflows.Experimental.Messages.ShoppingCart.Active
{
    public class ShoppingCartWorkflow:Workflow<IShoppingCartDialog>, IShoppingCartWorkflow
    {
        public ShoppingCartWorkflow(    IWorkflowProvider workflowProvider,
                                        IShoppingCartDialog dialog  )
        :
            base( workflowProvider, dialog )
        {
        }

        private ShoppingCartRequest CreateRequest( ShoppingCartCriteria criteria )
        {
            return this.CreateRequest(  (   MessageId messageId,
                                            SubscriberId localSubscriberId,
                                            SubscriberId remoteSubscriberId ) =>
                                        {
                                            return new ShoppingCartRequest( messageId,
                                                                            localSubscriberId,
                                                                            remoteSubscriberId,
                                                                            criteria    );
                                        }   );
        }

        public IShoppingCartFinishedProcessState StartProcess( ShoppingCartCriteria criteria )
        {
            ShoppingCartRequest request = this.CreateRequest( criteria );

            ShoppingCartResponse response = this.SendRequest(   request,
                                                                () =>
                                                                {
                                                                    return this.Dialog.SendRequest( request );
                                                                }   );

            return new ShoppingCartFinishedProcessState( request, response );
        }

        public async Task<IShoppingCartFinishedProcessState> StartProcessAsync( ShoppingCartCriteria criteria,
                                                                                CancellationToken cancellationToken = default   )
        {
            ShoppingCartRequest request = this.CreateRequest( criteria );

            ShoppingCartResponse response = await this.SendRequestAsync(    request,
                                                                            () =>
                                                                            {
                                                                                return this.Dialog.SendRequestAsync( request, cancellationToken );
                                                                            }   ).ConfigureAwait( continueOnCapturedContext:false );

            return new ShoppingCartFinishedProcessState( request, response );
        }
    }
}
