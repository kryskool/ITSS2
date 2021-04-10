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

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.ArticleMasterSet;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.ArticleMasterSet.Active;

namespace Reth.Itss2.Workflows.Standard.Messages.ArticleMasterSet.Active
{
    public class ArticleMasterSetWorkflow:SubscribedWorkflow<IArticleMasterSetDialog>, IArticleMasterSetWorkflow
    {
        public ArticleMasterSetWorkflow(    IArticleMasterSetDialog dialog,
                                            ISubscription subscription  )
        :
            base( dialog, subscription )
        {
        }

        private ArticleMasterSetRequest CreateRequest( IEnumerable<ArticleMasterSetArticle> articles )
        {
            return this.CreateRequest(  (   MessageId messageId,
                                            SubscriberId localSubscriberId,
                                            SubscriberId remoteSubscriberId ) =>
                                        {
                                            return new ArticleMasterSetRequest( messageId,
                                                                                localSubscriberId,
                                                                                remoteSubscriberId,
                                                                                articles    ) ;
                                        }   );
        }

        public IArticleMasterSetFinishedProcessState StartProcess( IEnumerable<ArticleMasterSetArticle> articles )
        {
            ArticleMasterSetRequest request = this.CreateRequest( articles );

            ArticleMasterSetResponse response = this.SendRequest(   request,
                                                                    () =>
                                                                    {
                                                                        return this.Dialog.SendRequest( request );
                                                                    }   );

            return new ArticleMasterSetFinishedProcessState( request, response );
        }

        public async Task<IArticleMasterSetFinishedProcessState> StartProcessAsync( IEnumerable<ArticleMasterSetArticle> articles, CancellationToken cancellationToken = default )
        {
            ArticleMasterSetRequest request = this.CreateRequest( articles );

            ArticleMasterSetResponse response = await this.SendRequestAsync(    request,
                                                                                () =>
                                                                                {
                                                                                    return this.Dialog.SendRequestAsync( request, cancellationToken );
                                                                                }   ).ConfigureAwait( continueOnCapturedContext:false );

            return new ArticleMasterSetFinishedProcessState( request, response );
        }
    }
}
