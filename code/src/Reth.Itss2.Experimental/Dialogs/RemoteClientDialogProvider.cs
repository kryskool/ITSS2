using System.Collections.Generic;

using Reth.Itss2.Experimental.Dialogs.ArticleData.ArticlePrice;
using Reth.Itss2.Experimental.Dialogs.SalesTransactions.ArticleSelected;
using Reth.Itss2.Experimental.Dialogs.SalesTransactions.ShoppingCart;
using Reth.Itss2.Experimental.Dialogs.SalesTransactions.ShoppingCartUpdate;
using Reth.Itss2.Experimental.Serialization;
using Reth.Protocols.Diagnostics;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Experimental.Dialogs
{
    public class RemoteClientDialogProvider:Reth.Itss2.Standard.Dialogs.RemoteClientDialogProvider, Reth.Itss2.Experimental.Dialogs.IRemoteClientDialogProvider
    {      
        private volatile bool isDisposed;

        public RemoteClientDialogProvider(  IProtocolProvider protocolProvider,
                                            IInteractionLog interactionLog  )
        :
            this( protocolProvider, interactionLog, DialogName.GetAvailableNames() )
        {
        }

        public RemoteClientDialogProvider(  IProtocolProvider protocolProvider,
                                            IInteractionLog interactionLog,
                                            IEnumerable<IDialogName> supportedDialogs   )
        :
            base( protocolProvider, interactionLog, supportedDialogs )
        {
            this.ArticlePrice = new ArticlePriceServerDialog( this );
            this.ArticleSelected = new ArticleSelectedServerDialog( this );
            this.ShoppingCart = new ShoppingCartServerDialog( this );
            this.ShoppingCartUpdate = new ShoppingCartUpdateServerDialog( this );
        }

        public IArticlePriceServerDialog ArticlePrice
        {
            get;
        }

        public IArticleSelectedServerDialog ArticleSelected
        {
            get;
        }

        public IShoppingCartServerDialog ShoppingCart
        {
            get;
        }

        public IShoppingCartUpdateServerDialog ShoppingCartUpdate
        {
            get;
        }

        protected override void Dispose( bool disposing )
        {
            ExecutionLogProvider.LogInformation( "Disposing remote client dialog provider." );

            if( this.isDisposed == false )
            {
                this.isDisposed = true;
            }

            base.Dispose( disposing );
        }
    }
}