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
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockInfo;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockInfo.Active;

namespace Reth.Itss2.Workflows.Standard.Messages.StockInfo.Active
{
    public class StockInfoWorkflow:SubscribedWorkflow<IStockInfoDialog>, IStockInfoWorkflow
    {
        public event EventHandler<MessageReceivedEventArgs<StockInfoMessage>>? StockChanged;

        public StockInfoWorkflow(   IStockInfoDialog dialog,
                                    ISubscription subscription  )
        :
            base( dialog, subscription )
        {
            this.Dialog.MessageReceived += this.Dialog_MessageReceived;
        }

        private void Dialog_MessageReceived( Object sender, MessageReceivedEventArgs<StockInfoMessage> e )
        {
            StockInfoMessage message = e.Message;

            this.OnMessageReceived( message,
                                    () =>
                                    {
                                        this.StockChanged?.Invoke( this, e );
                                    }   );
        }

        private StockInfoRequest CreateRequest( bool? includePacks,
                                                bool? includeArticleDetails,
                                                IEnumerable<StockInfoRequestCriteria>? criterias )
        {
            return this.CreateRequest(  (   MessageId messageId,
                                            SubscriberId localSubscriberId,
                                            SubscriberId remoteSubscriberId ) =>
                                        {
                                            return new StockInfoRequest(    messageId,
                                                                            localSubscriberId,
                                                                            remoteSubscriberId,
                                                                            includePacks,
                                                                            includeArticleDetails,
                                                                            criterias   ) ;
                                        }   );
        }

        public IStockInfoFinishedProcessState StartProcess()
        {
            return this.StartProcess(   includePacks:null,
                                        includeArticleDetails:null,
                                        criterias:null  );
        }

        public IStockInfoFinishedProcessState StartProcess( bool? includePacks,
                                                            bool? includeArticleDetails,
                                                            IEnumerable<StockInfoRequestCriteria>? criterias )
        {
            StockInfoRequest request = this.CreateRequest( includePacks, includeArticleDetails, criterias );

            StockInfoResponse response = this.SendRequest(  request,
                                                            () =>
                                                            {
                                                                return this.Dialog.SendRequest( request );
                                                            }   );

            return new StockInfoFinishedProcessState( request, response );
        }

        public Task<IStockInfoFinishedProcessState> StartProcessAsync( CancellationToken cancellationToken = default )
        {
            return this.StartProcessAsync(  includePacks:null,
                                            includeArticleDetails:null,
                                            criterias:null,
                                            cancellationToken   );
        }

        public async Task<IStockInfoFinishedProcessState> StartProcessAsync(    bool? includePacks,
                                                                                bool? includeArticleDetails,
                                                                                IEnumerable<StockInfoRequestCriteria>? criterias,
                                                                                CancellationToken cancellationToken = default   )
        {
            StockInfoRequest request = this.CreateRequest( includePacks, includeArticleDetails, criterias );

            StockInfoResponse response = await this.SendRequestAsync(   request,
                                                                        () =>
                                                                        {
                                                                            return this.Dialog.SendRequestAsync( request, cancellationToken );
                                                                        }   ).ConfigureAwait( continueOnCapturedContext:false );

            return new StockInfoFinishedProcessState( request, response );
        }
    }
}
