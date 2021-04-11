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

using Reth.Itss2.Dialogs.Standard.Protocol;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Output;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Output.Active;

namespace Reth.Itss2.Workflows.Standard.Messages.Output.Active
{
    public class OutputWorkflow:SubscribedWorkflow<IOutputDialog>, IOutputWorkflow
    {
        public event EventHandler<MessageReceivedEventArgs<OutputMessage>>? ManualOutputFinished;

        public OutputWorkflow(  IOutputDialog dialog,
                                ISubscription subscription  )
        :
            base( dialog, subscription )
        {
            dialog.MessageReceived += this.Dialog_MessageReceived;
        }

        private OutputRequest CreateRequest(    OutputRequestDetails details,
                                                IEnumerable<OutputCriteria> criterias,
                                                String? boxNumber    )
        {
            return this.CreateRequest(  (   MessageId messageId,
                                            SubscriberId localSubscriberId,
                                            SubscriberId remoteSubscriberId ) =>
                                        {
                                            return new OutputRequest(   messageId,
                                                                        localSubscriberId,
                                                                        remoteSubscriberId,
                                                                        details,
                                                                        criterias,
                                                                        boxNumber ) ;
                                        }   );
        }

        public IOutputStartedProcessState StartProcess( OutputRequestDetails details,
                                                        IEnumerable<OutputCriteria> criterias,
                                                        String? boxNumber    )
        {
            OutputRequest request = this.CreateRequest( details, criterias, boxNumber );

            OutputResponse response = this.SendRequest(  request,
                                                        () =>
                                                        {
                                                            return this.Dialog.SendRequest( request );
                                                        }   );

            return new OutputStartedProcessState( this, request, response );
        }

        public async Task<IOutputStartedProcessState> StartProcessAsync(    OutputRequestDetails details,
                                                                            IEnumerable<OutputCriteria> criterias,
                                                                            String? boxNumber,
                                                                            CancellationToken cancellationToken = default   )
        {
            OutputRequest request = this.CreateRequest( details, criterias, boxNumber );

            OutputResponse response = await this.SendRequestAsync(  request,
                                                                    () =>
                                                                    {
                                                                        return this.Dialog.SendRequestAsync( request, cancellationToken );
                                                                    }   ).ConfigureAwait( continueOnCapturedContext:false );

            return new OutputStartedProcessState( this, request, response );
        }

        private void Dialog_MessageReceived( Object sender, MessageReceivedEventArgs<OutputMessage> e )
        {
            if( e.Message.Id.Equals( ManualOutput.DefaultId ) == true )
            {
                this.ManualOutputFinished?.Invoke( this, e );
            }
        }
    }
}
