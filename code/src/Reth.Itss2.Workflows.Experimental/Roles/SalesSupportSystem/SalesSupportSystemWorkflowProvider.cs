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

using Reth.Itss2.Dialogs.Experimental.Protocol.Roles.SalesSupportSystem;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Serialization;
using Reth.Itss2.Workflows.Standard;
using Reth.Itss2.Workflows.Experimental.Messages.ArticleInfo.Active;
using Reth.Itss2.Workflows.Experimental.Messages.ArticlePrice.Active;
using Reth.Itss2.Workflows.Experimental.Messages.ArticleSelected.Active;
using Reth.Itss2.Workflows.Experimental.Messages.ShoppingCart.Active;
using Reth.Itss2.Workflows.Experimental.Messages.ShoppingCartUpdate.Active;
using Reth.Itss2.Workflows.Standard.Messages.Hello.Reactive;
using Reth.Itss2.Workflows.Standard.Messages.KeepAlive;
using Reth.Itss2.Workflows.Standard.Messages.Unprocessed;

namespace Reth.Itss2.Workflows.Experimental.Roles.SalesSupportSystem
{
    public class SalesSupportSystemWorkflowProvider:WorkflowProvider<ISalesSupportSystemDialogProvider>, ISalesSupportSystemWorkflowProvider
    {
        public SalesSupportSystemWorkflowProvider(  ISerializationProvider serializationProvider,
                                                    Subscriber localSubscriber )
        :
            this( serializationProvider, localSubscriber, new SalesSupportSystemDialogProvider() )
        {
        }

        protected SalesSupportSystemWorkflowProvider(   ISerializationProvider serializationProvider,
                                                        Subscriber localSubscriber,
                                                        ISalesSupportSystemDialogProvider dialogProvider )
        :
            base( dialogProvider )
        {
            Subscription subscription = new Subscription( localSubscriber );

            this.ArticleInfoWorkflow = new ArticleInfoWorkflow( dialogProvider.ArticleInfoDialog, subscription );
            this.ArticlePriceWorkflow = new ArticlePriceWorkflow( dialogProvider.ArticlePriceDialog, subscription );
            this.ArticleSelectedWorkflow = new ArticleSelectedWorkflow( dialogProvider.ArticleSelectedDialog, subscription );
            this.HelloWorkflow = new HelloWorkflow( dialogProvider.HelloDialog, subscription, dialogProvider, serializationProvider );
            this.KeepAliveWorkflow = new KeepAliveWorkflow( dialogProvider.KeepAliveDialog, subscription );
            this.ShoppingCartWorkflow = new ShoppingCartWorkflow( dialogProvider.ShoppingCartDialog, subscription );
            this.ShoppingCartUpdateWorkflow = new ShoppingCartUpdateWorkflow( dialogProvider.ShoppingCartUpdateDialog, subscription );
            this.UnprocessedWorkflow = new UnprocessedWorkflow( dialogProvider.UnprocessedDialog, subscription, serializationProvider );

            this.ArticleInfoWorkflow.MessageProcessingError += this.OnMessageProcessingError;
            this.ArticlePriceWorkflow.MessageProcessingError += this.OnMessageProcessingError;
            this.ArticleSelectedWorkflow.MessageProcessingError += this.OnMessageProcessingError;
            this.HelloWorkflow.MessageProcessingError += this.OnMessageProcessingError;
            this.KeepAliveWorkflow.MessageProcessingError += this.OnMessageProcessingError;
            this.ShoppingCartWorkflow.MessageProcessingError += this.OnMessageProcessingError;
            this.ShoppingCartUpdateWorkflow.MessageProcessingError += this.OnMessageProcessingError;
            this.UnprocessedWorkflow.MessageProcessingError += this.OnMessageProcessingError;
        }

        public IArticleInfoWorkflow ArticleInfoWorkflow{ get; }
        public IArticlePriceWorkflow ArticlePriceWorkflow{ get; }
        public IArticleSelectedWorkflow ArticleSelectedWorkflow{ get; }
        public IHelloWorkflow HelloWorkflow{ get; }
        public IKeepAliveWorkflow KeepAliveWorkflow{ get; }
        public IShoppingCartWorkflow ShoppingCartWorkflow{ get; }
        public IShoppingCartUpdateWorkflow ShoppingCartUpdateWorkflow{ get; }
        public IUnprocessedWorkflow UnprocessedWorkflow{ get; }

        protected override void Dispose( bool disposing )
        {
            if( this.isDisposed == false )
            {
                if( disposing == true )
                {
                    this.ArticleInfoWorkflow.Dispose();
                    this.ArticlePriceWorkflow.Dispose();
                    this.ArticleSelectedWorkflow.Dispose();
                    this.HelloWorkflow.Dispose();
                    this.KeepAliveWorkflow.Dispose();
                    this.ShoppingCartWorkflow.Dispose();
                    this.ShoppingCartUpdateWorkflow.Dispose();
                    this.UnprocessedWorkflow.Dispose();
                }

                base.Dispose( disposing );

                this.isDisposed = true;
            }
        }
    }
}
