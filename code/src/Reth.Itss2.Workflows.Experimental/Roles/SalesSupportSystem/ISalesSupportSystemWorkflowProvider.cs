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

using Reth.Itss2.Workflows.Experimental.Messages.ArticleInfo.Active;
using Reth.Itss2.Workflows.Experimental.Messages.ArticlePrice.Active;
using Reth.Itss2.Workflows.Experimental.Messages.ArticleSelected.Active;
using Reth.Itss2.Workflows.Experimental.Messages.ShoppingCart.Active;
using Reth.Itss2.Workflows.Experimental.Messages.ShoppingCartUpdate.Active;
using Reth.Itss2.Workflows.Standard;
using Reth.Itss2.Workflows.Standard.Messages.Hello.Reactive;
using Reth.Itss2.Workflows.Standard.Messages.KeepAlive;
using Reth.Itss2.Workflows.Standard.Messages.Unprocessed;

namespace Reth.Itss2.Workflows.Experimental.Roles.SalesSupportSystem
{
    public interface ISalesSupportSystemWorkflowProvider:IWorkflowProvider
    {
        IArticleInfoWorkflow ArticleInfoWorkflow{ get; }
        IArticlePriceWorkflow ArticlePriceWorkflow{ get; }
        IArticleSelectedWorkflow ArticleSelectedWorkflow{ get; }
        IHelloWorkflow HelloWorkflow{ get; }
        IKeepAliveWorkflow KeepAliveWorkflow{ get; }
        IShoppingCartWorkflow ShoppingCartWorkflow{ get; }
        IShoppingCartUpdateWorkflow ShoppingCartUpdateWorkflow{ get; }
        IUnprocessedWorkflow UnprocessedWorkflow{ get; }
    }
}
