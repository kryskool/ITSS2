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

namespace Reth.Itss2.Dialogs.Standard.Protocol.Roles.StorageSystem
{
    public class StorageSystemDialogProvider:DialogProvider, IStorageSystemDialogProvider
    {
        public StorageSystemDialogProvider()
        {
            this.ArticleInfoDialog = new StorageSystemArticleInfoDialog( this );
            this.ArticleMasterSetDialog = new StorageSystemArticleMasterSetDialog( this );
            this.HelloDialog = new StorageSystemHelloDialog( this );
            this.InitiateInputDialog = new StorageSystemInitiateInputDialog( this );
            this.InputDialog = new StorageSystemInputDialog( this );
            this.KeepAliveDialog = new StorageSystemKeepAliveDialog( this );
            this.OutputDialog = new StorageSystemOutputDialog( this );
            this.OutputInfoDialog = new StorageSystemOutputInfoDialog( this );
            this.StatusDialog = new StorageSystemStatusDialog( this );
            this.StockDeliveryInfoDialog = new StorageSystemStockDeliveryInfoDialog( this );
            this.StockDeliverySetDialog = new StorageSystemStockDeliverySetDialog( this );
            this.StockInfoDialog = new StorageSystemStockInfoDialog( this );
            this.StockLocationInfoDialog = new StorageSystemStockLocationInfoDialog( this );
            this.TaskCancelOutputDialog = new StorageSystemTaskCancelOutputDialog( this );
            this.UnprocessedDialog = new StorageSystemUnprocessedDialog( this );
        }

        public IStorageSystemArticleInfoDialog ArticleInfoDialog{ get; }
        public IStorageSystemArticleMasterSetDialog ArticleMasterSetDialog{ get; }
        public IStorageSystemHelloDialog HelloDialog{ get; }
        public IStorageSystemInitiateInputDialog InitiateInputDialog{ get; }
        public IStorageSystemInputDialog InputDialog{ get; }
        public IStorageSystemKeepAliveDialog KeepAliveDialog{ get; }
        public IStorageSystemOutputDialog OutputDialog{ get; }
        public IStorageSystemOutputInfoDialog OutputInfoDialog{ get; }
        public IStorageSystemStatusDialog StatusDialog{ get; }
        public IStorageSystemStockDeliveryInfoDialog StockDeliveryInfoDialog{ get; }
        public IStorageSystemStockDeliverySetDialog StockDeliverySetDialog{ get; }
        public IStorageSystemStockInfoDialog StockInfoDialog{ get; }
        public IStorageSystemStockLocationInfoDialog StockLocationInfoDialog{ get; }
        public IStorageSystemTaskCancelOutputDialog TaskCancelOutputDialog{ get; }
        public IStorageSystemUnprocessedDialog UnprocessedDialog{ get; }

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
                                    this.StockDeliveryInfoDialog.Name,
                                    this.StockDeliverySetDialog.Name,
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
            this.TaskCancelOutputDialog.Connect( messageTransmitter );
            this.UnprocessedDialog.Connect( messageTransmitter );
        }
    }
}
