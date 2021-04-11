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

using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Status;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Status.Active;

namespace Reth.Itss2.Workflows.Standard.Messages.Status.Active
{
    public class StatusWorkflow:SubscribedWorkflow<IStatusDialog>, IStatusWorkflow
    {
        public StatusWorkflow(  IStatusDialog dialog,
                                ISubscription subscription  )
        :
            base( dialog, subscription )
        {
        }

        private StatusRequest CreateRequest( bool? includeDetails )
        {
            return this.CreateRequest(  (   MessageId messageId,
                                            SubscriberId localSubscriberId,
                                            SubscriberId remoteSubscriberId ) =>
                                        {
                                            return new StatusRequest(   messageId,
                                                                        localSubscriberId,
                                                                        remoteSubscriberId,
                                                                        includeDetails    ) ;
                                        }   );
        }

        public IStatusFinishedProcessState StartProcess()
        {
            return this.StartProcess( includeDetails:null );
        }

        public IStatusFinishedProcessState StartProcess( bool? includeDetails )
        {
            StatusRequest request = this.CreateRequest( includeDetails );

            StatusResponse response = this.SendRequest( request,
                                                        () =>
                                                        {
                                                            return this.Dialog.SendRequest( request );
                                                        }   );

            return new StatusFinishedProcessState( request, response );
        }

        public Task<IStatusFinishedProcessState> StartProcessAsync( CancellationToken cancellationToken = default )
        {
            return this.StartProcessAsync( includeDetails:null, cancellationToken );
        }

        public async Task<IStatusFinishedProcessState> StartProcessAsync( bool? includeDetails, CancellationToken cancellationToken = default )
        {
            StatusRequest request = this.CreateRequest( includeDetails );

            StatusResponse response = await this.SendRequestAsync(  request,
                                                                    () =>
                                                                    {
                                                                        return this.Dialog.SendRequestAsync( request, cancellationToken );
                                                                    }   ).ConfigureAwait( continueOnCapturedContext:false );

            return new StatusFinishedProcessState( request, response );
        }
    }
}
