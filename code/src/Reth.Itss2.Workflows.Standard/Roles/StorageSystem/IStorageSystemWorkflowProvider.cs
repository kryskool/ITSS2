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

using Reth.Itss2.Workflows.Standard.Messages.ArticleInfo.Active;
using Reth.Itss2.Workflows.Standard.Messages.ArticleMasterSet.Reactive;
using Reth.Itss2.Workflows.Standard.Messages.Hello.Reactive;
using Reth.Itss2.Workflows.Standard.Messages.InitiateInput.Reactive;
using Reth.Itss2.Workflows.Standard.Messages.Input.Active;
using Reth.Itss2.Workflows.Standard.Messages.KeepAlive;
using Reth.Itss2.Workflows.Standard.Messages.Output.Reactive;
using Reth.Itss2.Workflows.Standard.Messages.OutputInfo.Reactive;
using Reth.Itss2.Workflows.Standard.Messages.Status.Reactive;
using Reth.Itss2.Workflows.Standard.Messages.StockDeliveryInfo.Reactive;
using Reth.Itss2.Workflows.Standard.Messages.StockDeliverySet.Reactive;
using Reth.Itss2.Workflows.Standard.Messages.StockInfo.Reactive;
using Reth.Itss2.Workflows.Standard.Messages.StockLocationInfo.Reactive;
using Reth.Itss2.Workflows.Standard.Messages.TaskCancelOutput.Reactive;
using Reth.Itss2.Workflows.Standard.Messages.Unprocessed;

namespace Reth.Itss2.Workflows.Standard.Roles.StorageSystem
{
    public interface IStorageSystemWorkflowProvider:IWorkflowProvider
    {
        IArticleInfoWorkflow ArticleInfoWorkflow{ get; }
        IArticleMasterSetWorkflow ArticleMasterSetWorkflow{ get; }
        IHelloWorkflow HelloWorkflow{ get; }
        IInitiateInputWorkflow InitiateInputWorkflow{ get; }
        IInputWorkflow InputWorkflow{ get; }
        IKeepAliveWorkflow KeepAliveWorkflow{ get; }
        IOutputWorkflow OutputWorkflow{ get; }
        IOutputInfoWorkflow OutputInfoWorkflow{ get; }
        IStatusWorkflow StatusWorkflow{ get; }
        IStockDeliveryInfoWorkflow StockDeliveryInfoWorkflow{ get; }
        IStockDeliverySetWorkflow StockDeliverySetWorkflow{ get; }
        IStockInfoWorkflow StockInfoWorkflow{ get; }
        IStockLocationInfoWorkflow StockLocationInfoWorkflow{ get; }
        ITaskCancelOutputWorkflow TaskCancelOutputWorkflow{ get; }
        IUnprocessedWorkflow UnprocessedWorkflow{ get; }
    }
}
