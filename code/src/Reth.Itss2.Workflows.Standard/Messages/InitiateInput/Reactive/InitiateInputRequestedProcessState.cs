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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.InitiateInput;

namespace Reth.Itss2.Workflows.Standard.Messages.InitiateInput.Reactive
{
    public class InitiateInputRequestedProcessState:ProcessState, IInitiateInputRequestedProcessState
    {
        public InitiateInputRequestedProcessState( InitiateInputWorkflow workflow, InitiateInputRequest request )
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

        private InitiateInputResponse CreateResponse(   InitiateInputResponseStatus status,
                                                        IEnumerable<InitiateInputResponseArticle> articles,
                                                        int inputSource,
                                                        int? inputPoint )
        {
            InitiateInputResponseDetails details = new InitiateInputResponseDetails(    status,
                                                                                        inputSource,
                                                                                        inputPoint  );

            return new InitiateInputResponse( this.Request, details, articles );
        }

        public IInitiateInputAcceptedProcessState Accept(   IEnumerable<InitiateInputResponseArticle> articles,
                                                            int inputSource,
                                                            int? inputPoint  )
        {
            this.OnStateChange();

            InitiateInputResponse response = this.CreateResponse(   InitiateInputResponseStatus.Accepted,
                                                                    articles,
                                                                    inputSource,
                                                                    inputPoint  );

            this.Workflow.SendResponse( response );

            return new InitiateInputAcceptedProcessState( this.Workflow, this.Request );
        }

        public async Task<IInitiateInputAcceptedProcessState> AcceptAsync(  IEnumerable<InitiateInputResponseArticle> articles,
                                                                            int inputSource,
                                                                            int? inputPoint,
                                                                            CancellationToken cancellationToken = default   )
        {
            this.OnStateChange();

            InitiateInputResponse response = this.CreateResponse(   InitiateInputResponseStatus.Accepted,
                                                                    articles,
                                                                    inputSource,
                                                                    inputPoint  );
                                                                                        
            await this.Workflow.SendResponseAsync( response, cancellationToken ).ConfigureAwait( continueOnCapturedContext:false );

            return new InitiateInputAcceptedProcessState( this.Workflow, this.Request );
        }

        public void Reject( IEnumerable<InitiateInputResponseArticle> articles,
                            int inputSource,
                            int? inputPoint  )
        {
            this.OnStateChange();

            InitiateInputResponse response = this.CreateResponse(   InitiateInputResponseStatus.Rejected,
                                                                    articles,
                                                                    inputSource,
                                                                    inputPoint  );

            this.Workflow.SendResponse( response );
        }

        public Task RejectAsync(    IEnumerable<InitiateInputResponseArticle> articles,
                                    int inputSource,
                                    int? inputPoint,
                                    CancellationToken cancellationToken = default   )
        {
            this.OnStateChange();

            InitiateInputResponse response = this.CreateResponse(   InitiateInputResponseStatus.Rejected,
                                                                    articles,
                                                                    inputSource,
                                                                    inputPoint  );
                                                                                        
            return this.Workflow.SendResponseAsync( response, cancellationToken );
        }
    }
}
