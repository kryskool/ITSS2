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
using System.Threading.Tasks;

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockLocationInfoDialog;

namespace Reth.Itss2.Workflows.Standard.StorageSystem.StockLocationInfoDialog
{
    internal class StockLocationInfoProcess:IStockLocationInfoProcess
    {
        public StockLocationInfoProcess( StockLocationInfoWorkflow workflow, StockLocationInfoRequest request )
        {
            this.Workflow = workflow;
            this.Request = request;
        }

        private StockLocationInfoWorkflow Workflow
        {
            get;
        }

        public StockLocationInfoRequest Request
        {
            get;
        }

        public void SendResponse( IEnumerable<StockLocation> stockLocations )
        {
            this.Workflow.SendResponse( new StockLocationInfoResponse( this.Request, stockLocations ) );
        }

        public Task SendResponseAsync( IEnumerable<StockLocation> stockLocations )
        {
            return this.Workflow.SendResponseAsync( new StockLocationInfoResponse( this.Request, stockLocations ) );
        }
    }
}
