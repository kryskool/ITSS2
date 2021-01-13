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

namespace Reth.Itss2.Dialogs.Standard.Protocol.Roles.PharmacyInventorySystem
{
    public interface IPharmacyInventorySystemDialogProvider:IDialogProvider
    {
        IPharmacyInventorySystemArticleInfoDialog ArticleInfoDialog{ get; }
        IPharmacyInventorySystemArticleMasterSetDialog ArticleMasterSetDialog{ get; }
        IPharmacyInventorySystemHelloDialog HelloDialog{ get; }
        IPharmacyInventorySystemInputDialog InputDialog{ get; }
        IPharmacyInventorySystemInitiateInputDialog InitiateInputDialog{ get; }
        IPharmacyInventorySystemKeepAliveDialog KeepAliveDialog{ get; }
        IPharmacyInventorySystemOutputDialog OutputDialog{ get; }
        IPharmacyInventorySystemOutputInfoDialog OutputInfoDialog{ get; }
        IPharmacyInventorySystemStatusDialog StatusDialog{ get; }
        IPharmacyInventorySystemStockDeliveryInfoDialog StockDeliveryInfoDialog{ get; }
        IPharmacyInventorySystemStockDeliverySetDialog StockDeliverySetDialog{ get; }
        IPharmacyInventorySystemStockInfoDialog StockInfoDialog{ get; }
        IPharmacyInventorySystemStockLocationInfoDialog StockLocationInfoDialog{ get; }
        IPharmacyInventorySystemUnprocessedDialog UnprocessedDialog{ get; }
    }
}

