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
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Output;

namespace Reth.Itss2.Workflows.Standard.Messages.Output.Reactive
{
    internal abstract class OutputProcessState:ProcessState
    {
        protected OutputProcessState( OutputWorkflow workflow, OutputRequest request )
        {
            this.Workflow = workflow;
            this.Request = request;
        }

        protected OutputWorkflow Workflow
        {
            get;
        }

        public OutputRequest Request
        {
            get;
        }

        private OutputResponse CreateResponse( OutputResponseStatus status )
        {
            OutputRequestDetails requestDetails = this.Request.Details;

            OutputResponseDetails responseDetails = new OutputResponseDetails(  requestDetails.OutputDestination,
                                                                                status,
                                                                                requestDetails.Priority,
                                                                                requestDetails.OutputPoint  );

            return new OutputResponse( this.Request, responseDetails );
        }

        private OutputMessageDetails CreateDetails( OutputMessageStatus status )
        {
            OutputRequestDetails requestDetails = this.Request.Details;

            OutputMessageDetails messageDetails = new OutputMessageDetails( requestDetails.OutputDestination,
                                                                            status,
                                                                            requestDetails.Priority,
                                                                            requestDetails.OutputPoint  );

            return messageDetails;
        }

        private OutputMessage CreateMessage(    OutputMessageStatus status,
                                                IEnumerable<OutputArticle>? articles,
                                                IEnumerable<OutputBox>? boxes    )
        {
            return new OutputMessage( this.Request, this.CreateDetails( status ), articles, boxes );
        }

        protected void SendResponse( OutputResponseStatus status )
        {
            OutputResponse response = this.CreateResponse( status );

            this.Workflow.SendResponse( response );
        }

        protected Task SendResponseAsync( OutputResponseStatus status, CancellationToken cancellationToken = default )
        {
            OutputResponse response = this.CreateResponse( status );

            return this.Workflow.SendResponseAsync( response, cancellationToken );
        }

        protected void SendMessage( OutputMessageStatus status,
                                    IEnumerable<OutputArticle>? articles,
                                    IEnumerable<OutputBox>? boxes    )
        {
            OutputMessage message = this.CreateMessage( status, articles, boxes );

            this.Workflow.SendMessage( message );
        }

        protected Task SendMessageAsync(    OutputMessageStatus status,
                                            IEnumerable<OutputArticle>? articles,
                                            IEnumerable<OutputBox>? boxes,
                                            CancellationToken cancellationToken = default   )
        {
            OutputMessage message = this.CreateMessage( status, articles, boxes );

            return this.Workflow.SendMessageAsync( message, cancellationToken );
        }
    }
}
