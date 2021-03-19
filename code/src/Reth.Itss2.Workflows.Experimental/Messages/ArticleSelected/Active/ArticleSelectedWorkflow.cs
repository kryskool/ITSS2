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

using Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ArticleSelected;
using Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ArticleSelected.Active;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Workflows.Standard;

namespace Reth.Itss2.Workflows.Experimental.Messages.ArticleSelected.Active
{
    public class ArticleSelectedWorkflow:SubscribedWorkflow<IArticleSelectedDialog>, IArticleSelectedWorkflow
    {
        public ArticleSelectedWorkflow( IArticleSelectedDialog dialog,
                                        ISubscription subscription  )
        :
            base( dialog, subscription )
        {
        }

        internal void SendMessage( ArticleSelectedMessage message )
        {
            this.SendMessage(   message,
                                () =>
                                {
                                    this.Dialog.SendMessage( message );
                                } );
        }

        internal Task SendMessageAsync( ArticleSelectedMessage message, CancellationToken cancellationToken = default )
        {
            return this.SendMessageAsync(   message,
                                            () =>
                                            {
                                                return this.Dialog.SendMessageAsync( message, cancellationToken );
                                            } );
        }

        public void NotifySelection( ArticleSelectedArticle article )
        {
            ArticleSelectedMessage message = this.CreateMessage(    (   MessageId id,
                                                                        SubscriberId localSubscriberId,
                                                                        SubscriberId remoteSubscriberId    ) =>
                                                                    {
                                                                        return new ArticleSelectedMessage(  id,
                                                                                                            localSubscriberId,
                                                                                                            remoteSubscriberId,
                                                                                                            article );
                                                                    }   );

            this.SendMessage( message );
        }
        
        public Task NotifySelectionAsync( ArticleSelectedArticle article, CancellationToken cancellationToken = default )
        {
            ArticleSelectedMessage message = this.CreateMessage(    (   MessageId id,
                                                                        SubscriberId localSubscriberId,
                                                                        SubscriberId remoteSubscriberId    ) =>
                                                                    {
                                                                        return new ArticleSelectedMessage(  id,
                                                                                                            localSubscriberId,
                                                                                                            remoteSubscriberId,
                                                                                                            article );
                                                                    }   );

            return this.SendMessageAsync( message, cancellationToken );
        }
    }
}
