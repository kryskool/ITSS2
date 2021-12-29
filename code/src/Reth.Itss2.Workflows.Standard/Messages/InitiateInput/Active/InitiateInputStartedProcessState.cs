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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.InitiateInput;
using Reth.Itss2.Messaging;
using Reth.Itss2.Workflows.Messaging;

namespace Reth.Itss2.Workflows.Standard.Messages.InitiateInput.Active
{
    public class InitiateInputStartedProcessState:ProcessState, IInitiateInputStartedProcessState
    {
        private bool isDisposed;

        public InitiateInputStartedProcessState(    InitiateInputWorkflow workflow,
                                                    InitiateInputRequest request,
                                                    Action<MessageReceivedEventArgs<InitiateInputMessage>> initiateInputFinishedCallback  )
        {
            this.Workflow = workflow;
            this.Request = request;

            this.Interceptor = new MessageInterceptor<InitiateInputMessage>(    this.Workflow.Dialog,
                                                                                new MessageFilter( this.Request.Id ),
                                                                                ( MessageReceivedEventArgs<InitiateInputMessage> e ) =>
                                                                                {
                                                                                    initiateInputFinishedCallback( e );
                                                                                }   );
            
            this.Response = this.Workflow.SendRequest(  this.Request,
                                                        () =>
                                                        {
                                                            return this.Workflow.Dialog.SendRequest( request );
                                                        }   );
        }

        private InitiateInputWorkflow Workflow
        {
            get;
        }

        private MessageInterceptor<InitiateInputMessage> Interceptor
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

        protected override void Dispose( bool disposing )
        {
            base.Dispose( disposing );

            if( this.isDisposed == false )
            {
                if( disposing == true )
                {
                    this.Interceptor.Dispose();
                }

                this.isDisposed = true;
            }
        }
    }
}
