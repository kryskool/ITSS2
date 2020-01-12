using System.Collections.Generic;

using Reth.Itss2.Experimental.Dialogs;
using Reth.Itss2.Experimental.Dialogs.ArticleData.ArticlePrice;
using Reth.Itss2.Experimental.Dialogs.SalesTransactions.ArticleSelected;
using Reth.Itss2.Experimental.Dialogs.SalesTransactions.ShoppingCart;
using Reth.Itss2.Experimental.Dialogs.SalesTransactions.ShoppingCartUpdate;
using Reth.Itss2.Experimental.Serialization;
using Reth.Protocols.Diagnostics;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.StandardExtensions.Dialogs
{
    public class LocalClientDialogProvider:Reth.Itss2.Standard.Dialogs.LocalClientDialogProvider, Reth.Itss2.Experimental.Dialogs.ILocalClientDialogProvider
    {        
        private volatile bool isDisposed;

        public LocalClientDialogProvider(   IProtocolProvider protocolProvider,
                                            IInteractionLog interactionLog  )
        :
            this(   protocolProvider,
                    interactionLog,
                    DialogName.GetAvailableNames()  )
        {
        }

        public LocalClientDialogProvider(   IProtocolProvider protocolProvider,
                                            IInteractionLog interactionLog,
                                            IEnumerable<IDialogName> supportedDialogs   )
        :
            base( protocolProvider, interactionLog, supportedDialogs )
        {
            this.ArticlePrice = new ArticlePriceClientDialog( this );
            this.ArticleSelected = new ArticleSelectedClientDialog( this );
            this.ShoppingCart = new ShoppingCartClientDialog( this );
            this.ShoppingCartUpdate = new ShoppingCartUpdateClientDialog( this );
        }

        public IArticlePriceClientDialog ArticlePrice
        {
            get;
        }

        public IArticleSelectedClientDialog ArticleSelected
        {
            get;
        }

        public IShoppingCartClientDialog ShoppingCart
        {
            get;
        }

        public IShoppingCartUpdateClientDialog ShoppingCartUpdate
        {
            get;
        }

        protected override void Dispose( bool disposing )
        {
            ExecutionLogProvider.LogInformation( "Disposing local client dialog provider." );

            if( this.isDisposed == false )
            {
                this.isDisposed = true;
            }

            base.Dispose( disposing );
        }
    }
}