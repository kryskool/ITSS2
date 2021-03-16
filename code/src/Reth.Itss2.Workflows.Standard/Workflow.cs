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

using Reth.Itss2.Dialogs.Standard.Diagnostics;
using Reth.Itss2.Dialogs.Standard.Protocol;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.Standard.Serialization;

namespace Reth.Itss2.Workflows.Standard
{
    public abstract class Workflow<TDialog>:IWorkflow
        where TDialog:IDialog
    {
        public event EventHandler<MessageProcessingErrorEventArgs>? MessageProcessingError;

        protected Workflow( IWorkflowProvider workflowProvider, TDialog dialog )
        {
            this.WorkflowProvider = workflowProvider;
            this.Dialog = dialog;
        }

        ~Workflow()
        {
            this.Dispose( false );
        }

        public IWorkflowProvider WorkflowProvider
        {
            get;
        }

        protected TDialog Dialog
        {
            get;
        }

        protected IDialogProvider DialogProvider
        {
            get{ return this.WorkflowProvider.DialogProvider; }
        }

        protected ISerializationProvider SerializationProvider
        {
            get{ return this.WorkflowProvider.SerializationProvider; }
        }

        protected SubscriberInfo GetSubscriberInfo()
        {
            return this.WorkflowProvider.GetSubscriberInfo();
        }

        private bool IsSupportedLocally( IMessage message )
        {
            return this.GetSubscriberInfo().IsSupportedLocally( message.DialogName );
        }

        private bool IsSupportedRemotely( IMessage message )
        {
            return this.GetSubscriberInfo().IsSupportedRemotely( message.DialogName );
        }

        private void CheckHandshake()
        {
            if( this.GetSubscriberInfo().HasRemoteSubscriber == false )
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

        protected void OnMessageProcessingError( MessageProcessingErrorEventArgs e )
        {
            this.MessageProcessingError?.Invoke( this, e );
        }

        protected void OnRequestReceived<TRequest>( TRequest request, Action sendResponseCallback )
            where TRequest:IRequest
        {
            if( this.GetSubscriberInfo().HasRemoteSubscriber == true )
            {
                if( this.IsSupportedLocally( request ) == true )
                {
                    try
                    {
                        sendResponseCallback();
                    }catch( Exception ex )
                    {
                        this.OnMessageProcessingError( new MessageProcessingErrorEventArgs( "Error on sending response.", ex ) );        
                    }
                }else
                {
                    this.OnMessageProcessingError( new MessageProcessingErrorEventArgs( $"Dialog '{ request.DialogName }' is not supported locally." ) );
                }
            }else
            {
                this.OnMessageProcessingError( new MessageProcessingErrorEventArgs( $"No handshake executed." ) );
            }
        }

        protected TMessage CreateMessage<TMessage>( Func<MessageId, SubscriberId, SubscriberId, TMessage> createMessageCallback )
            where TMessage:IMessage
        {
            SubscriberInfo subscriberInfo = this.GetSubscriberInfo();

            return createMessageCallback(   MessageId.NextId(),
                                            subscriberInfo.LocalSubscriber.Id,
                                            subscriberInfo.GetRemoteSubscriber().Id );
        }

        protected TRequest CreateRequest<TRequest>( Func<MessageId, SubscriberId, SubscriberId, TRequest> createRequestCallback )
            where TRequest:IRequest
        {
            SubscriberInfo subscriberInfo = this.GetSubscriberInfo();

            return createRequestCallback(   MessageId.NextId(),
                                            subscriberInfo.LocalSubscriber.Id,
                                            subscriberInfo.GetRemoteSubscriber().Id );
        }

        protected TResponse SendRequest<TRequest, TResponse>( TRequest request, Func<TResponse> sendRequestCallback )
            where TRequest:IRequest
            where TResponse:IResponse
        {
            this.CheckHandshake();
            this.CheckRemoteSupport( request );

            return sendRequestCallback();
        }

        protected Task<TResponse> SendRequestAsync<TRequest, TResponse>(    TRequest request,
                                                                            Func<Task<TResponse>> sendRequestCallback   )
            where TRequest:IRequest
            where TResponse:IResponse
        {
            this.CheckHandshake();
            this.CheckRemoteSupport( request );

            return sendRequestCallback();
        }

        protected void SendResponse<TResponse>( TResponse response, Action sendResponseCallback )
            where TResponse:IResponse
        {
            this.SendMessage( response, sendResponseCallback );
        }

        protected Task SendResponseAsync<TResponse>(    TResponse response,
                                                        Func<Task> sendResponseCallback   )
            where TResponse:IResponse
        {
            return this.SendMessageAsync( response, sendResponseCallback );
        }

        protected void SendMessage<TMessage>( TMessage message, Action sendMessageCallback )
            where TMessage:IMessage
        {
            this.CheckHandshake();
            this.CheckRemoteSupport( message );

            sendMessageCallback();
        }

        protected Task SendMessageAsync<TMessage>(  TMessage message,
                                                    Func<Task> sendMessageCallback   )
            where TMessage:IMessage
        {
            this.CheckHandshake();
            this.CheckRemoteSupport( message );

            return sendMessageCallback();
        }

        public void Dispose()
        {
            this.Dispose( true );

            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
        }
    }
}
