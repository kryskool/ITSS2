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

namespace Reth.Itss2.Dialogs.Standard.Protocol.Roles.PharmacyInventorySystem
{
    public class PharmacyInventorySystemDialogProvider:DialogProvider, IPharmacyInventorySystemDialogProvider
    {
        public PharmacyInventorySystemDialogProvider()
        :
            base()
        {
            this.ArticleInfoDialog = new PharmacyInventorySystemArticleInfoDialog( this );
            this.ArticleMasterSetDialog = new PharmacyInventorySystemArticleMasterSetDialog( this );
            this.HelloDialog = new PharmacyInventorySystemHelloDialog( this );
            this.InitiateInputDialog = new PharmacyInventorySystemInitiateInputDialog( this );
            this.InputDialog = new PharmacyInventorySystemInputDialog( this );
            this.KeepAliveDialog = new PharmacyInventorySystemKeepAliveDialog( this );
            this.OutputDialog = new PharmacyInventorySystemOutputDialog( this );
            this.OutputInfoDialog = new PharmacyInventorySystemOutputInfoDialog( this );
            this.StatusDialog = new PharmacyInventorySystemStatusDialog( this );
            this.StockDeliveryInfoDialog = new PharmacyInventorySystemStockDeliveryInfoDialog( this );
            this.StockDeliverySetDialog = new PharmacyInventorySystemStockDeliverySetDialog( this );
            this.StockInfoDialog = new PharmacyInventorySystemStockInfoDialog( this );
            this.StockLocationInfoDialog = new PharmacyInventorySystemStockLocationInfoDialog( this );
            this.TaskCancelOutputDialog = new PharmacyInventorySystemTaskCancelOutputDialog( this );
            this.UnprocessedDialog = new PharmacyInventorySystemUnprocessedDialog( this );
        }

        public IPharmacyInventorySystemArticleMasterSetDialog ArticleMasterSetDialog{ get; }
        public IPharmacyInventorySystemArticleInfoDialog ArticleInfoDialog{ get; }
        public IPharmacyInventorySystemHelloDialog HelloDialog{ get; }
        public IPharmacyInventorySystemInitiateInputDialog InitiateInputDialog{ get; }
        public IPharmacyInventorySystemInputDialog InputDialog{ get; }
        public IPharmacyInventorySystemKeepAliveDialog KeepAliveDialog{ get; }
        public IPharmacyInventorySystemOutputDialog OutputDialog{ get; }
        public IPharmacyInventorySystemOutputInfoDialog OutputInfoDialog{ get; }
        public IPharmacyInventorySystemStatusDialog StatusDialog{ get; }
        public IPharmacyInventorySystemStockDeliveryInfoDialog StockDeliveryInfoDialog{ get; }
        public IPharmacyInventorySystemStockDeliverySetDialog StockDeliverySetDialog{ get; }
        public IPharmacyInventorySystemStockInfoDialog StockInfoDialog{ get; }
        public IPharmacyInventorySystemStockLocationInfoDialog StockLocationInfoDialog{ get; }
        public IPharmacyInventorySystemTaskCancelOutputDialog TaskCancelOutputDialog{ get; }
        public IPharmacyInventorySystemUnprocessedDialog UnprocessedDialog{ get; }

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
