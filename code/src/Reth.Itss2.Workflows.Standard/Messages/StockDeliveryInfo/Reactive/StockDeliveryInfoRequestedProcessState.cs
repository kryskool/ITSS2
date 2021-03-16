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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockDeliveryInfo;

namespace Reth.Itss2.Workflows.Standard.Messages.StockDeliveryInfo.Reactive
{
    internal class StockDeliveryInfoRequestedProcessState:ProcessState, IStockDeliveryInfoRequestedProcessState
    {
        public StockDeliveryInfoRequestedProcessState( StockDeliveryInfoWorkflow workflow, StockDeliveryInfoRequest request )
        {
            this.Workflow = workflow;
            this.Request = request;
        }

        private StockDeliveryInfoWorkflow Workflow
        {
            get;
        }

        public StockDeliveryInfoRequest Request
        {
            get;
        }

        public void Finish( StockDeliveryInfoResponseTask task )
        {
            this.OnStateChange();

            this.Workflow.SendResponse( new StockDeliveryInfoResponse( this.Request, task ) );
        }

        public Task FinishAsync( StockDeliveryInfoResponseTask task, CancellationToken cancellationToken = default )
        {
            this.OnStateChange();

            return this.Workflow.SendResponseAsync( new StockDeliveryInfoResponse( this.Request, task ), cancellationToken );
        }
    }
}
