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

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockDeliverySet;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockDeliverySet.Active;

namespace Reth.Itss2.Workflows.Standard.Messages.StockDeliverySet.Active
{
    public class StockDeliverySetWorkflow:SubscribedWorkflow<IStockDeliverySetDialog>, IStockDeliverySetWorkflow
    {
        public StockDeliverySetWorkflow(    IStockDeliverySetDialog dialog,
                                            ISubscription subscription  )
        :
            base( dialog, subscription )
        {
        }

        private StockDeliverySetRequest CreateRequest( IEnumerable<StockDelivery> deliveries )
        {
            return this.CreateRequest(  (   MessageId messageId,
                                            SubscriberId localSubscriberId,
                                            SubscriberId remoteSubscriberId ) =>
                                        {
                                            return new StockDeliverySetRequest( messageId,
                                                                                localSubscriberId,
                                                                                remoteSubscriberId,
                                                                                deliveries    ) ;
                                        }   );
        }

        public IStockDeliverySetFinishedProcessState StartProcess( IEnumerable<StockDelivery> deliveries )
        {
            StockDeliverySetRequest request = this.CreateRequest( deliveries );

            StockDeliverySetResponse response = this.SendRequest(   request,
                                                                    () =>
                                                                    {
                                                                        return this.Dialog.SendRequest( request );
                                                                    }   );

            return new StockDeliverySetFinishedProcessState( request, response );
        }

        public async Task<IStockDeliverySetFinishedProcessState> StartProcessAsync( IEnumerable<StockDelivery> deliveries, CancellationToken cancellationToken = default )
        {
            StockDeliverySetRequest request = this.CreateRequest( deliveries );

            StockDeliverySetResponse response = await this.SendRequestAsync(    request,
                                                                                () =>
                                                                                {
                                                                                    return this.Dialog.SendRequestAsync( request, cancellationToken );
                                                                                }   ).ConfigureAwait( continueOnCapturedContext:false );

            return new StockDeliverySetFinishedProcessState( request, response );
        }
    }
}
