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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.InitiateInputDialog;

namespace Reth.Itss2.Workflows.Standard.StorageSystem.InitiateInputDialog
{
    internal class InitiateInputProcess:IInitiateInputProcess
    {
        public InitiateInputProcess( InitiateInputWorkflow workflow, InitiateInputRequest request )
        {
            this.Workflow = workflow;
            this.Request = request;
        }

        private InitiateInputWorkflow Workflow
        {
            get;
        }

        public InitiateInputRequest Request
        {
            get;
        }

        public void SendResponse(   InitiateInputResponseDetails details,
                                    IEnumerable<InitiateInputResponseArticle> articles  )
        {
            this.Workflow.SendResponse( new InitiateInputResponse(  this.Request,
                                                                    details,
                                                                    articles    ) );
        }

        public Task SendResponseAsync(  InitiateInputResponseDetails details,
                                        IEnumerable<InitiateInputResponseArticle> articles  )
        {
            return this.Workflow.SendResponseAsync( new InitiateInputResponse(  this.Request,
                                                                                details,
                                                                                articles    )   );
        }

        public void SendMessage(    InitiateInputMessageDetails details,
                                    IEnumerable<InitiateInputMessageArticle> articles  )
        {
            this.Workflow.SendMessage( new InitiateInputMessage(    this.Request,
                                                                    details,
                                                                    articles    ) );
        }

        public Task SendMessageAsync(   InitiateInputMessageDetails details,
                                        IEnumerable<InitiateInputMessageArticle> articles   )
        {
            return this.Workflow.SendMessageAsync( new InitiateInputMessage(    this.Request,
                                                                                details,
                                                                                articles    ) );
        }
    }
}
