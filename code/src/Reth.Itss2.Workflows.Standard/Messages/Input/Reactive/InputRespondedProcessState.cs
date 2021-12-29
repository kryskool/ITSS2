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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Input;
using Reth.Itss2.Messaging;
using Reth.Itss2.Workflows.Messaging;

namespace Reth.Itss2.Workflows.Standard.Messages.Input.Reactive
{
    internal class InputRespondedProcessState:ProcessState, IInputRespondedProcessState
    {
        private bool isDisposed;

        public InputRespondedProcessState(  InputWorkflow workflow,
                                            InputRequest request,
                                            InputResponse response,
                                            Action<MessageReceivedEventArgs<InputMessage>> inputFinishedCallback  )
        {
            this.Workflow = workflow;
            this.Request = request;
            this.Response = response;
            
            this.Interceptor = new MessageInterceptor<InputMessage>(    this.Workflow.Dialog,
                                                                        new MessageFilter( this.Request.Id ),
                                                                        ( MessageReceivedEventArgs<InputMessage> e ) =>
                                                                        {
                                                                            inputFinishedCallback( e );
                                                                        }   );

            this.Workflow.SendResponse( this.Response,
                                        () =>
                                        {
                                            this.Workflow.Dialog.SendResponse( response );
                                        }   );
        }

        private InputWorkflow Workflow
        {
            get;
        }

        private MessageInterceptor<InputMessage> Interceptor
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
