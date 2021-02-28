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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.OutputDialog;

namespace Reth.Itss2.Workflows.Standard.StorageSystem.OutputDialog
{
    internal class OutputProcess:IOutputProcess
    {
        public OutputProcess( OutputWorkflow workflow, OutputRequest request )
        {
            this.Workflow = workflow;
            this.Request = request;
        }

        private OutputWorkflow Workflow
        {
            get;
        }

        public OutputRequest Request
        {
            get;
        }

        public void SendResponse( OutputResponseDetails details )
        {
            this.Workflow.SendResponse( new OutputResponse( this.Request, details ) );
        }

        public Task SendResponseAsync( OutputResponseDetails details )
        {
            return this.Workflow.SendResponseAsync( new OutputResponse( this.Request, details ) );
        }

        public void SendMessage( OutputMessageDetails details, IEnumerable<OutputArticle> articles )
        {
            this.Workflow.SendMessage( new OutputMessage( this.Request, details, articles ) );
        }

        public void SendMessage(    OutputMessageDetails details,
                                    IEnumerable<OutputArticle> articles,
                                    IEnumerable<OutputBox>? boxes   )
        {
            this.Workflow.SendMessage( new OutputMessage( this.Request,
                                                          details,
                                                          articles,
                                                          boxes ) );
        }

        public Task SendMessageAsync( OutputMessageDetails details, IEnumerable<OutputArticle> articles )
        {
            return this.Workflow.SendMessageAsync( new OutputMessage( this.Request, details, articles ) );
        }

        public Task SendMessageAsync(   OutputMessageDetails details,
                                        IEnumerable<OutputArticle> articles,
                                        IEnumerable<OutputBox>? boxes   )
        {
            return this.Workflow.SendMessageAsync( new OutputMessage( this.Request,
                                                                      details,
                                                                      articles,
                                                                      boxes ) );
        }
    }
}
