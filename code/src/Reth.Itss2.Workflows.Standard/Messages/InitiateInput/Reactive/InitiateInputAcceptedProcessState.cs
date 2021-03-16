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
    public class InitiateInputAcceptedProcessState:ProcessState, IInitiateInputAcceptedProcessState
    {
        public InitiateInputAcceptedProcessState( InitiateInputWorkflow workflow, InitiateInputRequest request )
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

        private InitiateInputMessage CreateMessage( InitiateInputMessageStatus status,
                                                    IEnumerable<InitiateInputMessageArticle> articles,
                                                    int inputSource,
                                                    int? inputPoint )
        {
            InitiateInputMessageDetails details = new InitiateInputMessageDetails(  status,
                                                                                    inputSource,
                                                                                    inputPoint  );

            return new InitiateInputMessage( this.Request, details, articles );
        }


        public void Complete(   IEnumerable<InitiateInputMessageArticle> articles,
                                int inputSource,
                                int? inputPoint )
        {
            this.OnStateChange();

            InitiateInputMessage message = this.CreateMessage(  InitiateInputMessageStatus.Completed,
                                                                articles,
                                                                inputSource,
                                                                inputPoint  );

            this.Workflow.SendMessage( message );
        }

        public Task CompleteAsync(  IEnumerable<InitiateInputMessageArticle> articles,
                                    int inputSource,
                                    int? inputPoint,
                                    CancellationToken cancellationToken = default   )
        {
            this.OnStateChange();

            InitiateInputMessage message = this.CreateMessage(  InitiateInputMessageStatus.Completed,
                                                                articles,
                                                                inputSource,
                                                                inputPoint  );

            return this.Workflow.SendMessageAsync( message, cancellationToken );
        }

        public void Incomplete( IEnumerable<InitiateInputMessageArticle> articles,
                                int inputSource,
                                int? inputPoint )
        {
            this.OnStateChange();

            InitiateInputMessage message = this.CreateMessage(  InitiateInputMessageStatus.Incomplete,
                                                                articles,
                                                                inputSource,
                                                                inputPoint  );

            this.Workflow.SendMessage( message );
        }

        public Task IncompleteAsync(    IEnumerable<InitiateInputMessageArticle> articles,
                                        int inputSource,
                                        int? inputPoint,
                                        CancellationToken cancellationToken = default   )
        {
            this.OnStateChange();

            InitiateInputMessage message = this.CreateMessage(  InitiateInputMessageStatus.Incomplete,
                                                                articles,
                                                                inputSource,
                                                                inputPoint  );

            return this.Workflow.SendMessageAsync( message, cancellationToken );
        }
    }
}
