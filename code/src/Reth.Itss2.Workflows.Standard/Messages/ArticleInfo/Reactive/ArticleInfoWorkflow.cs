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
using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Dialogs.Standard.Protocol;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.ArticleInfo;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.ArticleInfo.Reactive;

namespace Reth.Itss2.Workflows.Standard.Messages.ArticleInfo.Reactive
{
    public class ArticleInfoWorkflow:SubscribedWorkflow<IArticleInfoDialog>, IArticleInfoWorkflow
    {
        public event EventHandler<ProcessStartedEventArgs<IArticleInfoRequestedProcessState>>? ProcessStarted;

        public ArticleInfoWorkflow( IArticleInfoDialog dialog,
                                    ISubscription subscription  )
        :
            base( dialog, subscription )
        {
            dialog.RequestReceived += this.Dialog_RequestReceived;
        }

        internal void SendResponse( ArticleInfoResponse response )
        {
            this.SendResponse(  response,
                                () =>
                                {
                                    this.Dialog.SendResponse( response );
                                } );
        }

        internal Task SendResponseAsync( ArticleInfoResponse response, CancellationToken cancellationToken = default )
        {
            return this.SendResponseAsync(  response,
                                            () =>
                                            {
                                                return this.Dialog.SendResponseAsync( response, cancellationToken );
                                            } );
        }

        private void Dialog_RequestReceived( Object sender, MessageReceivedEventArgs<ArticleInfoRequest> e )
        {
            ArticleInfoRequest request = e.Message;

            this.OnMessageReceived( request,
                                    () =>
                                    {
                                        IArticleInfoRequestedProcessState processState = new ArticleInfoRequestedProcessState( this, request );

                                        this.ProcessStarted?.Invoke( this, new ProcessStartedEventArgs<IArticleInfoRequestedProcessState>( processState ) );
                                    }   );
        }
    }
}
