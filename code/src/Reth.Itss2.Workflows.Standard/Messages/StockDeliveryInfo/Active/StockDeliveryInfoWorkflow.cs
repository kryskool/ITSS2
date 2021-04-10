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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockDeliveryInfo;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockDeliveryInfo.Active;

namespace Reth.Itss2.Workflows.Standard.Messages.StockDeliveryInfo.Active
{
    public class StockDeliveryInfoWorkflow:SubscribedWorkflow<IStockDeliveryInfoDialog>, IStockDeliveryInfoWorkflow
    {
        public StockDeliveryInfoWorkflow(   IStockDeliveryInfoDialog dialog,
                                            ISubscription subscription  )
        :
            base( dialog, subscription )
        {
        }

        private StockDeliveryInfoRequest CreateRequest( StockDeliveryInfoRequestTask task, bool includeTaskDetails )
        {
            return this.CreateRequest(  (   MessageId messageId,
                                            SubscriberId localSubscriberId,
                                            SubscriberId remoteSubscriberId ) =>
                                        {
                                            return new StockDeliveryInfoRequest(    messageId,
                                                                                    localSubscriberId,
                                                                                    remoteSubscriberId,
                                                                                    task,
                                                                                    includeTaskDetails  ) ;
                                        }   );
        }

        public IStockDeliveryInfoFinishedProcessState StartProcess( StockDeliveryInfoRequestTask task, bool includeTaskDetails )
        {
            StockDeliveryInfoRequest request = this.CreateRequest( task, includeTaskDetails );

            StockDeliveryInfoResponse response = this.SendRequest(  request,
                                                                    () =>
                                                                    {
                                                                        return this.Dialog.SendRequest( request );
                                                                    }   );

            return new StockDeliveryInfoFinishedProcessState( request, response );
        }

        public async Task<IStockDeliveryInfoFinishedProcessState> StartProcessAsync(    StockDeliveryInfoRequestTask task,
                                                                                        bool includeTaskDetails,
                                                                                        CancellationToken cancellationToken = default   )
        {
            StockDeliveryInfoRequest request = this.CreateRequest( task, includeTaskDetails );

            StockDeliveryInfoResponse response = await this.SendRequestAsync(   request,
                                                                                () =>
                                                                                {
                                                                                    return this.Dialog.SendRequestAsync( request, cancellationToken );
                                                                                }   ).ConfigureAwait( continueOnCapturedContext:false );

            return new StockDeliveryInfoFinishedProcessState( request, response );
        }
    }
}
