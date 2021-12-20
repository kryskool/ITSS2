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

using Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ArticleInfo.Reactive;
using Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ArticlePrice.Reactive;
using Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ArticleSelected.Reactive;
using Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ShoppingCart.Reactive;
using Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ShoppingCartUpdate.Reactive;
using Reth.Itss2.Dialogs.Standard.Protocol;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Hello.Active;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.KeepAlive;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Unprocessed;
using Reth.Itss2.Serialization;

namespace Reth.Itss2.Dialogs.Experimental.Protocol.Roles.PharmacyInventorySystem
{
    public class PharmacyInventorySystemDialogProvider:DialogProvider, IPharmacyInventorySystemDialogProvider
    {
        public PharmacyInventorySystemDialogProvider()
        {
            this.ArticleInfoDialog = new ArticleInfoDialog( this );
            this.ArticlePriceDialog = new ArticlePriceDialog( this );
            this.ArticleSelectedDialog = new ArticleSelectedDialog( this );
            this.HelloDialog = new HelloDialog( this );
            this.KeepAliveDialog = new KeepAliveDialog( this );
            this.ShoppingCartDialog = new ShoppingCartDialog( this );
            this.ShoppingCartUpdateDialog = new ShoppingCartUpdateDialog( this );
            this.UnprocessedDialog = new UnprocessedDialog( this );
        }

        public IArticleInfoDialog ArticleInfoDialog{ get; }
        public IArticlePriceDialog ArticlePriceDialog{ get; }
        public IArticleSelectedDialog ArticleSelectedDialog{ get; }
        public IHelloDialog HelloDialog{ get; }
        public IKeepAliveDialog KeepAliveDialog{ get; }
        public IShoppingCartDialog ShoppingCartDialog{ get; }
        public IShoppingCartUpdateDialog ShoppingCartUpdateDialog{ get; }
        public IUnprocessedDialog UnprocessedDialog{ get; }

        public override String[] GetSupportedDialogs()
        {
            return new String[]{    this.ArticleInfoDialog.Name,
                                    this.ArticlePriceDialog.Name,
                                    this.ArticleSelectedDialog.Name,
                                    this.HelloDialog.Name,
                                    this.KeepAliveDialog.Name,
                                    this.ShoppingCartDialog.Name,
                                    this.ShoppingCartUpdateDialog.Name,
                                    this.UnprocessedDialog.Name };
        }

        protected override void ConnectDialogs( IMessageTransmitter messageTransmitter )
        {
            this.ArticleInfoDialog.Connect( messageTransmitter );
            this.ArticlePriceDialog.Connect( messageTransmitter );
            this.ArticleSelectedDialog.Connect( messageTransmitter );
            this.HelloDialog.Connect( messageTransmitter );
            this.KeepAliveDialog.Connect( messageTransmitter );
            this.ShoppingCartDialog.Connect( messageTransmitter );
            this.ShoppingCartUpdateDialog.Connect( messageTransmitter );
            this.UnprocessedDialog.Connect( messageTransmitter );
        }
    }
}
