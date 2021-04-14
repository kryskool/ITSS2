﻿// Implementation of the WWKS2 protocol.
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
using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Dialogs.Standard.Protocol;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.InitiateInput;

namespace Reth.Itss2.Workflows.Standard.Messages.InitiateInput.Active
{
    public class InitiateInputCreatedProcessState:ProcessState, IInitiateInputCreatedProcessState
    {
        public InitiateInputCreatedProcessState( InitiateInputWorkflow workflow, InitiateInputRequest request )
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

        public IInitiateInputStartedProcessState StartProcess( Action<MessageReceivedEventArgs<InitiateInputMessage>> initiateInputFinishedCallback )
        {
            this.OnStateChange();

            return new InitiateInputStartedProcessState( this.Workflow, this.Request, initiateInputFinishedCallback );
        }

        public Task<IInitiateInputStartedProcessState> StartProcessAsync( Action<MessageReceivedEventArgs<InitiateInputMessage>> initiateInputFinishedCallback, CancellationToken cancellationToken = default )
        {
            this.OnStateChange();

            return Task.FromResult<IInitiateInputStartedProcessState>( new InitiateInputStartedProcessState( this.Workflow, this.Request, initiateInputFinishedCallback ) );
        }
    }
}
