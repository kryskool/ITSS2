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

using Reth.Itss2.Workflows.Standard.Messages.ArticleInfo.Reactive;
using Reth.Itss2.Workflows.Standard.Messages.ArticleMasterSet.Active;
using Reth.Itss2.Workflows.Standard.Messages.Hello.Active;
using Reth.Itss2.Workflows.Standard.Messages.InitiateInput.Active;
using Reth.Itss2.Workflows.Standard.Messages.Input.Reactive;
using Reth.Itss2.Workflows.Standard.Messages.KeepAlive;
using Reth.Itss2.Workflows.Standard.Messages.Output.Active;
using Reth.Itss2.Workflows.Standard.Messages.OutputInfo.Active;
using Reth.Itss2.Workflows.Standard.Messages.Status.Active;
using Reth.Itss2.Workflows.Standard.Messages.StockDeliveryInfo.Active;
using Reth.Itss2.Workflows.Standard.Messages.StockDeliverySet.Active;
using Reth.Itss2.Workflows.Standard.Messages.StockInfo.Active;
using Reth.Itss2.Workflows.Standard.Messages.StockLocationInfo.Active;
using Reth.Itss2.Workflows.Standard.Messages.TaskCancelOutput.Active;
using Reth.Itss2.Workflows.Standard.Messages.Unprocessed;

namespace Reth.Itss2.Workflows.Standard.Roles.PharmacyInventorySystem
{
    public interface IPharmacyInventorySystemWorkflowProvider:IWorkflowProvider
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
