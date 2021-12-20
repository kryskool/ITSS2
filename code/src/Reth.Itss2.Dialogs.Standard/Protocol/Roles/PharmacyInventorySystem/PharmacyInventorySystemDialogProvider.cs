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

using System;

using Reth.Itss2.Serialization;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.ArticleInfo.Reactive;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.ArticleMasterSet.Active;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Hello.Active;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.InitiateInput.Active;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Input.Reactive;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.KeepAlive;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Output.Active;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.OutputInfo.Active;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Status.Active;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockDeliveryInfo.Active;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockDeliverySet.Active;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockInfo.Active;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockLocationInfo.Active;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.TaskCancelOutput.Active;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Unprocessed;

namespace Reth.Itss2.Dialogs.Standard.Protocol.Roles.PharmacyInventorySystem
{
    public class PharmacyInventorySystemDialogProvider:DialogProvider, IPharmacyInventorySystemDialogProvider
    {
        public PharmacyInventorySystemDialogProvider()
        {
            this.ArticleInfoDialog = new ArticleInfoDialog( this );
            this.ArticleMasterSetDialog = new ArticleMasterSetDialog( this );
            this.HelloDialog = new HelloDialog( this );
            this.InitiateInputDialog = new InitiateInputDialog( this );
            this.InputDialog = new InputDialog( this );
            this.KeepAliveDialog = new KeepAliveDialog( this );
            this.OutputDialog = new OutputDialog( this );
            this.OutputInfoDialog = new OutputInfoDialog( this );
            this.StatusDialog = new StatusDialog( this );
            this.StockDeliveryInfoDialog = new StockDeliveryInfoDialog( this );
            this.StockDeliverySetDialog = new StockDeliverySetDialog( this );
            this.StockInfoDialog = new StockInfoDialog( this );
            this.StockLocationInfoDialog = new StockLocationInfoDialog( this );
            this.TaskCancelOutputDialog = new TaskCancelOutputDialog( this );
            this.UnprocessedDialog = new UnprocessedDialog( this );
        }

        public IArticleMasterSetDialog ArticleMasterSetDialog{ get; }
        public IArticleInfoDialog ArticleInfoDialog{ get; }
        public IHelloDialog HelloDialog{ get; }
        public IInitiateInputDialog InitiateInputDialog{ get; }
        public IInputDialog InputDialog{ get; }
        public IKeepAliveDialog KeepAliveDialog{ get; }
        public IOutputDialog OutputDialog{ get; }
        public IOutputInfoDialog OutputInfoDialog{ get; }
        public IStatusDialog StatusDialog{ get; }
        public IStockDeliveryInfoDialog StockDeliveryInfoDialog{ get; }
        public IStockDeliverySetDialog StockDeliverySetDialog{ get; }
        public IStockInfoDialog StockInfoDialog{ get; }
        public IStockLocationInfoDialog StockLocationInfoDialog{ get; }
        public ITaskCancelOutputDialog TaskCancelOutputDialog{ get; }
        public IUnprocessedDialog UnprocessedDialog{ get; }

        public override String[] GetSupportedDialogs()
        {
            return new String[]{    this.ArticleInfoDialog.Name,
                                    this.ArticleMasterSetDialog.Name,
                                    this.HelloDialog.Name,
                                    this.InitiateInputDialog.Name,
                                    this.InputDialog.Name,
                                    this.KeepAliveDialog.Name,
                                    this.OutputDialog.Name,
                                    this.OutputInfoDialog.Name,
                                    this.StatusDialog.Name,
                                    this.StockDeliverySetDialog.Name,
                                    this.StockDeliveryInfoDialog.Name,
                                    this.StockInfoDialog.Name,
                                    this.StockLocationInfoDialog.Name,
                                    this.TaskCancelOutputDialog.Name,
                                    this.UnprocessedDialog.Name };
        }

        protected override void ConnectDialogs( IMessageTransmitter messageTransmitter )
        {
            this.ArticleInfoDialog.Connect( messageTransmitter );
            this.ArticleMasterSetDialog.Connect( messageTransmitter );
            this.HelloDialog.Connect( messageTransmitter );
            this.InitiateInputDialog.Connect( messageTransmitter );
            this.InputDialog.Connect( messageTransmitter );
            this.KeepAliveDialog.Connect( messageTransmitter );
            this.OutputDialog.Connect( messageTransmitter );
            this.OutputInfoDialog.Connect( messageTransmitter );
            this.StatusDialog.Connect( messageTransmitter );
            this.StockDeliveryInfoDialog.Connect( messageTransmitter );
            this.StockDeliverySetDialog.Connect( messageTransmitter );
            this.StockInfoDialog.Connect( messageTransmitter );
            this.StockLocationInfoDialog.Connect( messageTransmitter );
            this.TaskCancelOutputDialog.Connect( messageTransmitter );
            this.UnprocessedDialog.Connect( messageTransmitter );
        }
    }
}
