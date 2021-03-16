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

using Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ArticleInfo.Reactive;
using Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ArticlePrice.Reactive;
using Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ArticleSelected.Reactive;
using Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ShoppingCart.Reactive;
using Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ShoppingCartUpdate.Reactive;
using Reth.Itss2.Dialogs.Standard.Protocol;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Hello.Active;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.KeepAlive;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Unprocessed;

namespace Reth.Itss2.Dialogs.Experimental.Protocol.Roles.PharmacyInventorySystem
{
    public interface IPharmacyInventorySystemDialogProvider:IDialogProvider
    {
        IArticleInfoDialog ArticleInfoDialog{ get; }
        IArticlePriceDialog ArticlePriceDialog{ get; }
        IArticleSelectedDialog ArticleSelectedDialog{ get; }
        IHelloDialog HelloDialog{ get; }
        IKeepAliveDialog KeepAliveDialog{ get; }
        IShoppingCartDialog ShoppingCartDialog{ get; }
        IShoppingCartUpdateDialog ShoppingCartUpdateDialog{ get; }
        IUnprocessedDialog UnprocessedDialog{ get; }
    }
}
