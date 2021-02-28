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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.InputDialog;

namespace Reth.Itss2.Workflows.Standard.StorageSystem.InputDialog
{
    internal class InputProcess:IInputProcess
    {
        public InputProcess( InputWorkflow workflow, InputRequest request, InputResponse response )
        {
            this.Workflow = workflow;
            this.Request = request;
            this.Response = response;
        }

        private InputWorkflow Workflow
        {
            get;
        }

        public InputRequest Request
        {
            get;
        }

        public InputResponse Response
        {
            get;
        }

        public void SendMessage( IEnumerable<InputMessageArticle> articles )
        {
            this.Workflow.SendMessage( new InputMessage( this.Request, articles ) );
        }

        public Task SendMessageAsync( IEnumerable<InputMessageArticle> articles )
        {
            return this.Workflow.SendMessageAsync( new InputMessage( this.Request, articles ) );
        }
    }
}
