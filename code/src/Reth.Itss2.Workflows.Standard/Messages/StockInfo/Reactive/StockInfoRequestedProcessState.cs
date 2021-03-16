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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockInfo;

namespace Reth.Itss2.Workflows.Standard.Messages.StockInfo.Reactive
{
    internal class StockInfoRequestedProcessState:ProcessState, IStockInfoRequestedProcessState
    {
        public StockInfoRequestedProcessState( StockInfoWorkflow workflow, StockInfoRequest request )
        {
            this.Workflow = workflow;
            this.Request = request;
        }

        private StockInfoWorkflow Workflow
        {
            get;
        }

        public StockInfoRequest Request
        {
            get;
        }

        public void Finish()
        {
            this.OnStateChange();

            this.Workflow.SendResponse( new StockInfoResponse( this.Request ) );
        }

        public void Finish( IEnumerable<StockInfoArticle> articles )
        {
            this.OnStateChange();

            this.Workflow.SendResponse( new StockInfoResponse( this.Request, articles ) );
        }

        public Task FinishAsync( CancellationToken cancellationToken = default )
        {
            this.OnStateChange();

            return this.Workflow.SendResponseAsync( new StockInfoResponse( this.Request ), cancellationToken );
        }

        public Task FinishAsync( IEnumerable<StockInfoArticle> articles, CancellationToken cancellationToken = default  )
        {
            this.OnStateChange();

            return this.Workflow.SendResponseAsync( new StockInfoResponse( this.Request, articles ), cancellationToken );
        }
    }
}
