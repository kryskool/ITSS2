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

using Reth.Itss2.Dialogs.Experimental.Protocol.Roles.SalesSupportSystem;
using Reth.Itss2.Dialogs.Standard.Protocol;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Hello;
using Reth.Itss2.Dialogs.Standard.Serialization;
using Reth.Itss2.Workflows.Standard;
using Reth.Itss2.Workflows.Experimental.Messages.ArticleInfo.Active;
using Reth.Itss2.Workflows.Experimental.Messages.ArticlePrice.Active;
using Reth.Itss2.Workflows.Experimental.Messages.ArticleSelected.Active;
using Reth.Itss2.Workflows.Experimental.Messages.ShoppingCart.Active;
using Reth.Itss2.Workflows.Standard.Messages.Hello.Reactive;
using Reth.Itss2.Workflows.Standard.Messages.KeepAlive;
using Reth.Itss2.Workflows.Standard.Messages.Unprocessed;

namespace Reth.Itss2.Workflows.Experimental.Roles.SalesSupportSystem
{
    public class SalesSupportSystemWorkflowProvider:WorkflowProvider, ISalesSupportSystemWorkflowProvider
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
            base( serializationProvider, localSubscriber, dialogProvider )
        {
            this.ArticleInfoWorkflow = new ArticleInfoWorkflow( this, dialogProvider.ArticleInfoDialog );
            this.ArticlePriceWorkflow = new ArticlePriceWorkflow( this, dialogProvider.ArticlePriceDialog );
            this.ArticleSelectedWorkflow = new ArticleSelectedWorkflow( this, dialogProvider.ArticleSelectedDialog );
            this.HelloWorkflow = new HelloWorkflow( this, dialogProvider.HelloDialog );
            this.KeepAliveWorkflow = new KeepAliveWorkflow( this, dialogProvider.KeepAliveDialog );
            this.ShoppingCartWorkflow = new ShoppingCartWorkflow( this, dialogProvider.ShoppingCartDialog );
            this.UnprocessedWorkflow = new UnprocessedWorkflow( this, dialogProvider.UnprocessedDialog );

            this.ArticleInfoWorkflow.MessageProcessingError += this.OnMessageProcessingError;
            this.ArticlePriceWorkflow.MessageProcessingError += this.OnMessageProcessingError;
            this.ArticleSelectedWorkflow.MessageProcessingError += this.OnMessageProcessingError;
            this.HelloWorkflow.MessageProcessingError += this.OnMessageProcessingError;
            this.KeepAliveWorkflow.MessageProcessingError += this.OnMessageProcessingError;
            this.ShoppingCartWorkflow.MessageProcessingError += this.OnMessageProcessingError;
            this.UnprocessedWorkflow.MessageProcessingError += this.OnMessageProcessingError;

            this.HelloWorkflow.RequestAccepted += this.OnHelloRequestAccepted;
        }

        public IArticleInfoWorkflow ArticleInfoWorkflow{ get; }
        public IArticlePriceWorkflow ArticlePriceWorkflow{ get; }
        public IArticleSelectedWorkflow ArticleSelectedWorkflow{ get; }
        public IHelloWorkflow HelloWorkflow{ get; }
        public IKeepAliveWorkflow KeepAliveWorkflow{ get; }
        public IShoppingCartWorkflow ShoppingCartWorkflow{ get; }
        public IUnprocessedWorkflow UnprocessedWorkflow{ get; }

        private void OnHelloRequestAccepted( Object sender, MessageReceivedEventArgs<HelloRequest> e )
        {
            this.SetRemoteSubscriber( e.Message.Subscriber );
        }

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
                    this.UnprocessedWorkflow.Dispose();
                }

                base.Dispose( disposing );

                this.isDisposed = true;
            }
        }
    }
}
