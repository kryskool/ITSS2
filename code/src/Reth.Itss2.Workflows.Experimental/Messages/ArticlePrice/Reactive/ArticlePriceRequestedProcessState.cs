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

using Reth.Itss2.Dialogs.Experimental.Protocol.Messages;
using Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ArticlePrice;
using Reth.Itss2.Workflows.Standard;

namespace Reth.Itss2.Workflows.Experimental.Messages.ArticlePrice.Reactive
{
    internal class ArticlePriceRequestedProcessState:ProcessState, IArticlePriceRequestedProcessState
    {
        public ArticlePriceRequestedProcessState( ArticlePriceWorkflow workflow, ArticlePriceRequest request )
        {
            this.Workflow = workflow;
            this.Request = request;
        }

        private ArticlePriceWorkflow Workflow
        {
            get;
        }

        public ArticlePriceRequest Request
        {
            get;
        }

        public void Finish( IEnumerable<ArticlePriceResponseArticle> articles )
        {
            this.Finish( articles, currency:null );
        }

        public void Finish( IEnumerable<ArticlePriceResponseArticle> articles, Iso4217Code? currency )
        {
            this.OnStateChange();

            this.Workflow.SendResponse( new ArticlePriceResponse( this.Request, articles, currency ) );
        }

        public Task FinishAsync( IEnumerable<ArticlePriceResponseArticle> articles, CancellationToken cancellationToken = default )
        {
            return this.FinishAsync( articles, currency:null, cancellationToken );
        }

        public Task FinishAsync( IEnumerable<ArticlePriceResponseArticle> articles, Iso4217Code? currency, CancellationToken cancellationToken = default )
        {
            this.OnStateChange();

            return this.Workflow.SendResponseAsync( new ArticlePriceResponse( this.Request, articles, currency ), cancellationToken );
        }
    }
}
