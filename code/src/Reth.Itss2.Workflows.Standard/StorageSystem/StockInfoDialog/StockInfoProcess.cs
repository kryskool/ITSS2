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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockInfoDialog;

namespace Reth.Itss2.Workflows.Standard.StorageSystem.StockInfoDialog
{
    internal class StockInfoProcess:IStockInfoProcess
    {
        public StockInfoProcess( StockInfoWorkflow workflow, StockInfoRequest request )
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

        public void SendResponse()
        {
            this.Workflow.SendResponse( new StockInfoResponse( this.Request ) );
        }

        public void SendResponse( IEnumerable<StockInfoArticle> articles )
        {
            this.Workflow.SendResponse( new StockInfoResponse( this.Request, articles ) );
        }

        public Task SendResponseAsync()
        {
            return this.Workflow.SendResponseAsync( new StockInfoResponse( this.Request ) );
        }

        public Task SendResponseAsync( IEnumerable<StockInfoArticle> articles )
        {
            return this.Workflow.SendResponseAsync( new StockInfoResponse( this.Request, articles ) );
        }

        public void SendMessage()
        {
            this.Workflow.SendMessage( new StockInfoMessage( this.Request ) );
        }

        public void SendMessage( IEnumerable<StockInfoArticle> articles )
        {
            this.Workflow.SendMessage( new StockInfoMessage( this.Request, articles ) );
        }

        public Task SendMessageAsync()
        {
            return this.Workflow.SendMessageAsync( new StockInfoMessage( this.Request ) );
        }

        public Task SendMessageAsync( IEnumerable<StockInfoArticle> articles )
        {
            return this.Workflow.SendMessageAsync( new StockInfoMessage( this.Request, articles ) );
        }
    }
}
