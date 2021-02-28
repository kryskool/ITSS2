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

using Reth.Itss2.Dialogs.Standard.Protocol.Roles.StorageSystem;
using Reth.Itss2.Dialogs.Standard.Serialization;
using Reth.Itss2.Workflows.Standard.StorageSystem.ArticleInfoDialog;
using Reth.Itss2.Workflows.Standard.StorageSystem.ArticleMasterSetDialog;
using Reth.Itss2.Workflows.Standard.StorageSystem.HelloDialog;
using Reth.Itss2.Workflows.Standard.StorageSystem.InitiateInputDialog;
using Reth.Itss2.Workflows.Standard.StorageSystem.InputDialog;
using Reth.Itss2.Workflows.Standard.StorageSystem.KeepAliveDialog;
using Reth.Itss2.Workflows.Standard.StorageSystem.OutputDialog;
using Reth.Itss2.Workflows.Standard.StorageSystem.OutputInfoDialog;
using Reth.Itss2.Workflows.Standard.StorageSystem.StatusDialog;
using Reth.Itss2.Workflows.Standard.StorageSystem.StockDeliveryInfoDialog;
using Reth.Itss2.Workflows.Standard.StorageSystem.StockDeliverySetDialog;
using Reth.Itss2.Workflows.Standard.StorageSystem.StockInfoDialog;
using Reth.Itss2.Workflows.Standard.StorageSystem.StockLocationInfoDialog;
using Reth.Itss2.Workflows.Standard.StorageSystem.TaskCancelOutputDialog;
using Reth.Itss2.Workflows.Standard.StorageSystem.UnprocessedDialog;

namespace Reth.Itss2.Workflows.Standard.StorageSystem
{
    public interface IStorageSystemWorkflowProvider:IWorkflowProvider
    {
        IStorageSystemDialogProvider DialogProvider{ get; }

        ISerializationProvider SerializationProvider{ get; }

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
