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

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Input;
using Reth.Itss2.Serialization;

namespace Reth.Itss2.Workflows.Standard.Messages.Input.Reactive
{
    internal class InputRequestedProcessState:ProcessState, IInputRequestedProcessState
    {
        public InputRequestedProcessState( InputWorkflow workflow, InputRequest request )
        {
            this.Workflow = workflow;
            this.Request = request;
        }

        private InputWorkflow Workflow
        {
            get;
        }

        public InputRequest Request
        {
            get;
        }

        public IInputRespondedProcessState Respond( Action<MessageReceivedEventArgs<InputMessage>> inputFinishedCallback,
                                                    IEnumerable<InputResponseArticle> articles  )
        {
            this.OnStateChange();

            InputResponse response = new InputResponse( this.Request, articles );

            return new InputRespondedProcessState( this.Workflow, this.Request, response, inputFinishedCallback );
        }

        public Task<IInputRespondedProcessState> RespondAsync(  Action<MessageReceivedEventArgs<InputMessage>> inputFinishedCallback,
                                                                IEnumerable<InputResponseArticle> articles,
                                                                CancellationToken cancellationToken = default   )
        {
            this.OnStateChange();

            InputResponse response = new InputResponse( this.Request, articles );

            return Task.FromResult<IInputRespondedProcessState>( new InputRespondedProcessState( this.Workflow, this.Request, response, inputFinishedCallback ) );
        }
    }
}
