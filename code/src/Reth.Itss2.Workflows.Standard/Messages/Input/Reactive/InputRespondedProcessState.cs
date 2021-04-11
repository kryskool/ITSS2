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
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Input;

namespace Reth.Itss2.Workflows.Standard.Messages.Input.Reactive
{
    internal class InputRespondedProcessState:ProcessState, IInputRespondedProcessState
    {
        public event EventHandler<MessageReceivedEventArgs<InputMessage>>? InputFinished;

        public InputRespondedProcessState( InputWorkflow workflow, InputRequest request, InputResponse response )
        {
            this.Workflow = workflow;
            this.Request = request;
            this.Response = response;

            this.Workflow.Dialog.MessageReceived += this.Dialog_MessageReceived;
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

        private void Dialog_MessageReceived( Object sender, MessageReceivedEventArgs<InputMessage> e )
        {
            if( e.Message.Id.Equals( this.Request.Id ) == true )
            {
                this.InputFinished?.Invoke( this, e );
            }
        }
    }
}
