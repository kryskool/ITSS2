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
using System.Threading.Tasks;

using Reth.Itss2.Diagnostics;
using Reth.Itss2.Dialogs;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Messaging;

namespace Reth.Itss2.Workflows
{
    public abstract class SubscribedWorkflow<TDialog>:Workflow<TDialog>
        where TDialog:IDialog
    {
        private SubscriberInfo subscriberInfo;

        protected SubscribedWorkflow( TDialog dialog, ISubscription subscription )
        :
            base( dialog, subscription )
        {
            this.Subscription.Subscribed += this.Subscription_Subscribed;    

            this.subscriberInfo = new SubscriberInfo( subscription.LocalSubscriber );
        }

        ~SubscribedWorkflow()
        {
            this.Dispose( false );
        }

        private Object SyncRoot
        {
            get;
        } = new Object();

        public SubscriberInfo SubscriberInfo
        {
            get
            {
                lock( this.SyncRoot )
                {
                    return this.subscriberInfo;
                }
            }

            private set
            {
                lock( this.SyncRoot )
                {
                    this.subscriberInfo = value;
                }
            }
        }

        private void Subscription_Subscribed( Object? sender, SubscribedEventArgs e )
        {
            this.SubscriberInfo = e.SubscriberInfo;
        }

        private bool IsSupportedLocally( IMessage message )
        {
            return this.SubscriberInfo.IsSupportedLocally( message.DialogName );
        }

        private bool IsSupportedRemotely( IMessage message )
        {
            return this.SubscriberInfo.IsSupportedRemotely( message.DialogName );
        }

        private void CheckHandshake()
        {
            if( this.SubscriberInfo.HasRemoteSubscriber == false )
            {
                throw Assert.Exception( new InvalidOperationException( $"No handshake executed." ) );
            }
        }

        private void CheckRemoteSupport( IMessage message )
        {
            if( this.IsSupportedRemotely( message ) == false )
            {
                throw Assert.Exception( new InvalidOperationException( $"Remote subscriber doesn't support message '{ message.Name }'." ) );
            }
        }

        protected void OnMessageReceived<TMessage>( TMessage message, Action processMessageCallback )
            where TMessage:IMessage
        {
            if( this.SubscriberInfo.HasRemoteSubscriber == true )
            {
                if( this.IsSupportedLocally( message ) == true )
                {
                    try
                    {
                        processMessageCallback();
                    }catch( Exception ex )
                    {
                        this.OnMessageProcessingError( new MessageProcessingErrorEventArgs( "Error on processing message.", ex ) );        
                    }
                }else
                {
                    this.OnMessageProcessingError( new MessageProcessingErrorEventArgs( $"Dialog '{ message.DialogName }' is not supported locally." ) );
                }
            }else
            {
                this.OnMessageProcessingError( new MessageProcessingErrorEventArgs( $"No handshake executed." ) );
            }
        }

        protected TMessage CreateMessage<TMessage>( Func<MessageId, SubscriberId, SubscriberId, TMessage> createMessageCallback )
            where TMessage:IMessage
        {
            SubscriberInfo subscriberInfo = this.SubscriberInfo;

            return createMessageCallback(   MessageId.NextId(),
                                            subscriberInfo.LocalSubscriber.Id,
                                            subscriberInfo.GetRemoteSubscriber().Id );
        }

        protected TRequest CreateRequest<TRequest>( Func<MessageId, SubscriberId, SubscriberId, TRequest> createRequestCallback )
            where TRequest:IRequest
        {
            SubscriberInfo subscriberInfo = this.SubscriberInfo;

            return createRequestCallback(   MessageId.NextId(),
                                            subscriberInfo.LocalSubscriber.Id,
                                            subscriberInfo.GetRemoteSubscriber().Id );
        }

        public TResponse SendRequest<TRequest, TResponse>( TRequest request, Func<TResponse> sendRequestCallback )
            where TRequest:IRequest
            where TResponse:IResponse
        {
            this.CheckHandshake();
            this.CheckRemoteSupport( request );

            return sendRequestCallback();
        }

        public Task<TResponse> SendRequestAsync<TRequest, TResponse>(    TRequest request,
                                                                            Func<Task<TResponse>> sendRequestCallback   )
            where TRequest:IRequest
            where TResponse:IResponse
        {
            this.CheckHandshake();
            this.CheckRemoteSupport( request );

            return sendRequestCallback();
        }

        public void SendResponse<TResponse>( TResponse response, Action sendResponseCallback )
            where TResponse:IResponse
        {
            this.SendMessage( response, sendResponseCallback );
        }

        public Task SendResponseAsync<TResponse>(    TResponse response,
                                                        Func<Task> sendResponseCallback   )
            where TResponse:IResponse
        {
            return this.SendMessageAsync( response, sendResponseCallback );
        }

        public void SendMessage<TMessage>( TMessage message, Action sendMessageCallback )
            where TMessage:IMessage
        {
            this.CheckHandshake();
            this.CheckRemoteSupport( message );

            sendMessageCallback();
        }

        public Task SendMessageAsync<TMessage>(  TMessage message,
                                                 Func<Task> sendMessageCallback   )
            where TMessage:IMessage
        {
            this.CheckHandshake();
            this.CheckRemoteSupport( message );

            return sendMessageCallback();
        }
    }
}
