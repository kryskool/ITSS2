﻿// Implementation of the WWKS2 protocol.
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

using Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ArticleSelected;
using Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ArticleSelected.Reactive;
using Reth.Itss2.Serialization;
using Reth.Itss2.Workflows.Standard;

namespace Reth.Itss2.Workflows.Experimental.Messages.ArticleSelected.Reactive
{
    public class ArticleSelectedWorkflow:SubscribedWorkflow<IArticleSelectedDialog>, IArticleSelectedWorkflow
    {
        public event EventHandler<ProcessStartedEventArgs<IArticleSelectedFinishedProcessState>>? ProcessStarted;

        public ArticleSelectedWorkflow( IArticleSelectedDialog dialog,
                                        ISubscription subscription  )
        :
            base( dialog, subscription )
        {
            dialog.MessageReceived += this.Dialog_MessageReceived;
        }

        private void Dialog_MessageReceived( Object? sender, MessageReceivedEventArgs<ArticleSelectedMessage> e )
        {
            ArticleSelectedMessage message = e.Message;

            this.OnMessageReceived( message,
                                    () =>
                                    {
                                        IArticleSelectedFinishedProcessState processState = new ArticleSelectedFinishedProcessState( message );

                                        this.ProcessStarted?.Invoke( this, new ProcessStartedEventArgs<IArticleSelectedFinishedProcessState>( processState ) );
                                    }   );
        }
    }
}
