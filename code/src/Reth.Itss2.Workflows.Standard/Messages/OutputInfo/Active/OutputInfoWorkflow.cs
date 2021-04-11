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
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.OutputInfo;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.OutputInfo.Active;

namespace Reth.Itss2.Workflows.Standard.Messages.OutputInfo.Active
{
    public class OutputInfoWorkflow:SubscribedWorkflow<IOutputInfoDialog>, IOutputInfoWorkflow
    {
        public OutputInfoWorkflow(  IOutputInfoDialog dialog,
                                    ISubscription subscription  )
        :
            base( dialog, subscription )
        {
        }

        private OutputInfoRequest CreateRequest( OutputInfoRequestTask task, bool? includeTaskDetails )
        {
            return this.CreateRequest(  (   MessageId messageId,
                                            SubscriberId localSubscriberId,
                                            SubscriberId remoteSubscriberId ) =>
                                        {
                                            return new OutputInfoRequest(   messageId,
                                                                            localSubscriberId,
                                                                            remoteSubscriberId,
                                                                            task,
                                                                            includeTaskDetails  ) ;
                                        }   );
        }

        public IOutputInfoFinishedProcessState StartProcess( OutputInfoRequestTask task, bool? includeTaskDetails )
        {
            OutputInfoRequest request = this.CreateRequest( task, includeTaskDetails );

            OutputInfoResponse response = this.SendRequest( request,
                                                            () =>
                                                            {
                                                                return this.Dialog.SendRequest( request );
                                                            }   );

            return new StockLocationInfoFinishedProcessState( request, response );
        }

        public async Task<IOutputInfoFinishedProcessState> StartProcessAsync(   OutputInfoRequestTask task,
                                                                                bool? includeTaskDetails,
                                                                                CancellationToken cancellationToken = default   )
        {
            OutputInfoRequest request = this.CreateRequest( task, includeTaskDetails );

            OutputInfoResponse response = await this.SendRequestAsync(  request,
                                                                        () =>
                                                                        {
                                                                            return this.Dialog.SendRequestAsync( request, cancellationToken );
                                                                        }   ).ConfigureAwait( continueOnCapturedContext:false );

            return new StockLocationInfoFinishedProcessState( request, response );
        }
    }
}
