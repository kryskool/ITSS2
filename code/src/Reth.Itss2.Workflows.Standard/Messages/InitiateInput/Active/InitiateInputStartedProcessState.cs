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

using Reth.Itss2.Dialogs.Standard.Protocol;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.InitiateInput;

namespace Reth.Itss2.Workflows.Standard.Messages.InitiateInput.Active
{
    public class InitiateInputStartedProcessState:ProcessState, IInitiateInputStartedProcessState
    {
        public event EventHandler<MessageReceivedEventArgs<InitiateInputMessage>>? InitiateInputFinished;

        public InitiateInputStartedProcessState( InitiateInputWorkflow workflow, InitiateInputRequest request, InitiateInputResponse response )
        {
            this.Workflow = workflow;
            this.Request = request;
            this.Response = response;

            this.Workflow.Dialog.MessageReceived += this.Dialog_MessageReceived;
        }

        private InitiateInputWorkflow Workflow
        {
            get;
        }

        public InitiateInputRequest Request
        {
            get;
        }

        public InitiateInputResponse Response
        {
            get;
        }

        private void Dialog_MessageReceived( Object sender, MessageReceivedEventArgs<InitiateInputMessage> e )
        {
            if( e.Message.Id.Equals( this.Request.Id ) == true )
            {
                this.InitiateInputFinished?.Invoke( this, e );
            }
        }
    }
}
