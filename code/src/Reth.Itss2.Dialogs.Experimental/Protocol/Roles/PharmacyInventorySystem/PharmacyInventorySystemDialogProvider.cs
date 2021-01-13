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

using Reth.Itss2.Dialogs.Standard.Protocol;

namespace Reth.Itss2.Dialogs.Experimental.Protocol.Roles.PharmacyInventorySystem
{
    public class PharmacyInventorySystemDialogProvider:DialogProvider, IPharmacyInventorySystemDialogProvider
    {
        public PharmacyInventorySystemDialogProvider()
        :
            base()
        {
            this.ArticleInfoDialog = new PharmacyInventorySystemArticleInfoDialog( this );
            this.ArticlePriceDialog = new PharmacyInventorySystemArticlePriceDialog( this );
            this.ArticleSelectedDialog = new PharmacyInventorySystemArticleSelectedDialog( this );
            this.HelloDialog = new PharmacyInventorySystemHelloDialog( this );
            this.KeepAliveDialog = new PharmacyInventorySystemKeepAliveDialog( this );
            this.ShoppingCartDialog = new PharmacyInventorySystemShoppingCartDialog( this );
            this.ShoppingCartUpdateDialog = new PharmacyInventorySystemShoppingCartUpdateDialog( this );
            this.UnprocessedDialog = new PharmacyInventorySystemUnprocessedDialog( this );
        }

        public IPharmacyInventorySystemArticleInfoDialog ArticleInfoDialog{ get; }
        public IPharmacyInventorySystemArticlePriceDialog ArticlePriceDialog{ get; }
        public IPharmacyInventorySystemArticleSelectedDialog ArticleSelectedDialog{ get; }
        public IPharmacyInventorySystemHelloDialog HelloDialog{ get; }
        public IPharmacyInventorySystemKeepAliveDialog KeepAliveDialog{ get; }
        public IPharmacyInventorySystemShoppingCartDialog ShoppingCartDialog{ get; }
        public IPharmacyInventorySystemShoppingCartUpdateDialog ShoppingCartUpdateDialog{ get; }
        public IPharmacyInventorySystemUnprocessedDialog UnprocessedDialog{ get; }

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
