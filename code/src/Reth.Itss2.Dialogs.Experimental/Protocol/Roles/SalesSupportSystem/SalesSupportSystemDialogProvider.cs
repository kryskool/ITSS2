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

namespace Reth.Itss2.Dialogs.Experimental.Protocol.Roles.SalesSupportSystem
{
    public class SalesSupportSystemDialogProvider:DialogProvider, ISalesSupportSystemDialogProvider
    {
        public SalesSupportSystemDialogProvider()
        :
            base()
        {
            this.ArticleInfoDialog = new SalesSupportSystemArticleInfoDialog( this );
            this.ArticlePriceDialog = new SalesSupportSystemArticlePriceDialog( this );
            this.ArticleSelectedDialog = new SalesSupportSystemArticleSelectedDialog( this );
            this.HelloDialog = new SalesSupportSystemHelloDialog( this );
            this.KeepAliveDialog = new SalesSupportSystemKeepAliveDialog( this );
            this.ShoppingCartDialog = new SalesSupportSystemShoppingCartDialog( this );
            this.ShoppingCartUpdateDialog = new SalesSupportSystemShoppingCartUpdateDialog( this );
            this.UnprocessedDialog = new SalesSupportSystemUnprocessedDialog( this );
        }

        public ISalesSupportSystemArticleInfoDialog ArticleInfoDialog{ get; }
        public ISalesSupportSystemArticlePriceDialog ArticlePriceDialog{ get; }
        public ISalesSupportSystemArticleSelectedDialog ArticleSelectedDialog{ get; }
        public ISalesSupportSystemHelloDialog HelloDialog{ get; }
        public ISalesSupportSystemKeepAliveDialog KeepAliveDialog{ get; }
        public ISalesSupportSystemShoppingCartDialog ShoppingCartDialog{ get; }
        public ISalesSupportSystemShoppingCartUpdateDialog ShoppingCartUpdateDialog{ get; }
        public ISalesSupportSystemUnprocessedDialog UnprocessedDialog{ get; }

        public override String[] GetSupportedDialogs()
        {
            return new String[]{    this.ArticlePriceDialog.Name,
                                    this.ArticleInfoDialog.Name,
                                    this.ArticleSelectedDialog.Name,
                                    this.HelloDialog.Name,
                                    this.KeepAliveDialog.Name,
                                    this.ShoppingCartDialog.Name,
                                    this.ShoppingCartUpdateDialog.Name,
                                    this.UnprocessedDialog.Name};
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
