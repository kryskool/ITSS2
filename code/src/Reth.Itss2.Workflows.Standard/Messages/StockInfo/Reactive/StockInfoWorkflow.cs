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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockInfo;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockInfo.Reactive;
using Reth.Itss2.Messaging;
using Reth.Itss2.Serialization;

namespace Reth.Itss2.Workflows.Standard.Messages.StockInfo.Reactive
{
    public class StockInfoWorkflow:SubscribedWorkflow<IStockInfoDialog>, IStockInfoWorkflow
    {
        public event EventHandler<ProcessStartedEventArgs<IStockInfoRequestedProcessState>>? ProcessStarted;

        public StockInfoWorkflow(   IStockInfoDialog dialog,
                                    ISubscription subscription  )
        :
            base( dialog, subscription )
        {
            dialog.RequestReceived += this.Dialog_RequestReceived;
        }

        internal void SendResponse( StockInfoResponse response )
        {
            this.SendResponse(  response,
                                () =>
                                {
                                    this.Dialog.SendResponse( response );
                                } );
        }

        internal Task SendResponseAsync( StockInfoResponse response, CancellationToken cancellationToken = default )
        {
            return this.SendResponseAsync(  response,
                                            () =>
                                            {
                                                return this.Dialog.SendResponseAsync( response, cancellationToken );
                                            } );
        }

        private StockInfoMessage CreateMessage( IEnumerable<StockInfoArticle> articles )
        {
            return this.CreateMessage(  (   MessageId id,
                                            SubscriberId localSubscriberId,
                                            SubscriberId remoteSubscriberId  ) =>
                                        {
                                            return new StockInfoMessage( id, localSubscriberId, remoteSubscriberId, articles );
                                        }   );
        }

        public void NotifyStockChange( IEnumerable<StockInfoArticle> articles )
        {
            StockInfoMessage message = this.CreateMessage( articles );

            this.SendMessage(   message,
                                () =>
                                {
                                    this.Dialog.SendMessage( message );
                                }   );
        }

        public Task NotifyStockChangeAsync( IEnumerable<StockInfoArticle> articles, CancellationToken cancellationToken = default  )
        {
            StockInfoMessage message = this.CreateMessage( articles );

            return this.SendMessageAsync(   message,
                                            () =>
                                            {
                                                return this.Dialog.SendMessageAsync( message, cancellationToken );
                                            }   );
        }

        private void Dialog_RequestReceived( Object? sender, MessageReceivedEventArgs<StockInfoRequest> e )
        {
            StockInfoRequest request = e.Message;

            this.OnMessageReceived( request,
                                    () =>
                                    {
                                        IStockInfoRequestedProcessState processState = new StockInfoRequestedProcessState( this, request );

                                        this.ProcessStarted?.Invoke( this, new ProcessStartedEventArgs<IStockInfoRequestedProcessState>( processState ) );
                                    }   );
        }
    }
}
