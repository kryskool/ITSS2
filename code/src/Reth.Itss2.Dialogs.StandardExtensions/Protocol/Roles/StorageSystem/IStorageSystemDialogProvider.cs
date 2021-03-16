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

using Reth.Itss2.Dialogs.Standard.Protocol;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.ArticleInfo.Active;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Hello.Reactive;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.InitiateInput.Reactive;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Input.Active;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.KeepAlive;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Output.Reactive;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.OutputInfo.Reactive;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Status.Reactive;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockDeliveryInfo.Reactive;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockDeliverySet.Reactive;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockInfo.Reactive;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockLocationInfo.Reactive;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.TaskCancelOutput.Reactive;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Unprocessed;
using Reth.Itss2.Dialogs.StandardExtensions.Protocol.Messages.ArticleMasterSet.Reactive;
using Reth.Itss2.Dialogs.StandardExtensions.Protocol.Messages.ConfigurationGet.Reactive;

namespace Reth.Itss2.Dialogs.StandardExtensions.Protocol.Roles.StorageSystem
{
    public interface IStorageSystemDialogProvider:IDialogProvider
    {
        IArticleInfoDialog ArticleInfoDialog{ get; }
        IArticleMasterSetDialog ArticleMasterSetDialog{ get; }
        IConfigurationGetDialog ConfigurationGetDialog{ get; }
        IHelloDialog HelloDialog{ get; }
        IInitiateInputDialog InitiateInputDialog{ get; }
        IInputDialog InputDialog{ get; }
        IKeepAliveDialog KeepAliveDialog{ get; }
        IOutputDialog OutputDialog{ get; }
        IOutputInfoDialog OutputInfoDialog{ get; }
        IStatusDialog StatusDialog{ get; }
        IStockDeliveryInfoDialog StockDeliveryInfoDialog{ get; }
        IStockDeliverySetDialog StockDeliverySetDialog{ get; }
        IStockInfoDialog StockInfoDialog{ get; }
        IStockLocationInfoDialog StockLocationInfoDialog{ get; }
        ITaskCancelOutputDialog TaskCancelOutputDialog{ get; }
        IUnprocessedDialog UnprocessedDialog{ get; }
    }
}
