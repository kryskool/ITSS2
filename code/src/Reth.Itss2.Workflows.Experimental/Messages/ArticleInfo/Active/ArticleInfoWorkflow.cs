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

using Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ArticleInfo;
using Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ArticleInfo.Active;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Workflows.Standard;

using ArticleInfoRequestArticle = Reth.Itss2.Dialogs.Standard.Protocol.Messages.ArticleInfo.ArticleInfoRequestArticle;

namespace Reth.Itss2.Workflows.Experimental.Messages.ArticleInfo.Active
{
    public class ArticleInfoWorkflow:Workflow<IArticleInfoDialog>, IArticleInfoWorkflow
    {
        public ArticleInfoWorkflow( IWorkflowProvider workflowProvider,
                                    IArticleInfoDialog dialog  )
        :
            base( workflowProvider, dialog )
        {
        }

        private ArticleInfoRequest CreateRequest( IEnumerable<ArticleInfoRequestArticle> articles )
        {
            return this.CreateRequest(  (   MessageId messageId,
                                            SubscriberId localSubscriberId,
                                            SubscriberId remoteSubscriberId ) =>
                                        {
                                            return new ArticleInfoRequest(  messageId,
                                                                            localSubscriberId,
                                                                            remoteSubscriberId,
                                                                            articles    );
                                        }   );
        }

        public IArticleInfoFinishedProcessState StartProcess( IEnumerable<ArticleInfoRequestArticle> articles )
        {
            ArticleInfoRequest request = this.CreateRequest( articles );

            ArticleInfoResponse response = this.SendRequest(    request,
                                                                () =>
                                                                {
                                                                    return this.Dialog.SendRequest( request );
                                                                }   );

            return new ArticleInfoFinishedProcessState( request, response );
        }

        public async Task<IArticleInfoFinishedProcessState> StartProcessAsync( IEnumerable<ArticleInfoRequestArticle> articles, CancellationToken cancellationToken = default )
        {
            ArticleInfoRequest request = this.CreateRequest( articles );

            ArticleInfoResponse response = await this.SendRequestAsync( request,
                                                                        () =>
                                                                        {
                                                                            return this.Dialog.SendRequestAsync( request, cancellationToken );
                                                                        }   ).ConfigureAwait( continueOnCapturedContext:false );

            return new ArticleInfoFinishedProcessState( request, response );
        }
    }
}
