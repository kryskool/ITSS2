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
using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Dialogs.Standard.Diagnostics;
using Reth.Itss2.Dialogs.Standard.Protocol;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.Standard.Protocol.Roles.StorageSystem;
using Reth.Itss2.Dialogs.Standard.Serialization;
using Reth.Itss2.Workflows.Standard.StorageSystem;

namespace Reth.Itss2.Workflows.Standard
{
    public abstract class Workflow<TDialog>:IWorkflow
        where TDialog:IDialog
    {
        public event EventHandler<MessageProcessingErrorEventArgs>? MessageProcessingError;

        protected Workflow( IStorageSystemWorkflowProvider workflowProvider,
                            IStorageSystemDialogProvider dialogProvider,
                            ISerializationProvider serializationProvider,
                            TDialog dialog  )
        {
            this.WorkflowProvider = workflowProvider;
            this.Dialog = dialog;

            this.DialogProvider = dialogProvider;
            this.SerializationProvider = serializationProvider;

            this.IsSupportedLocally = this.LocalSubscriber.IsSupported( dialog.Name );
        }

        ~Workflow()
        {
            this.Dispose( false );
        }

        private IStorageSystemWorkflowProvider WorkflowProvider
        {
            get;
        }

        protected IStorageSystemDialogProvider DialogProvider
        {
            get;
        }

        protected ISerializationProvider SerializationProvider
        {
            get;
        }

        protected TDialog Dialog
        {
            get;
        }

        protected Subscriber LocalSubscriber
        {
            get{ return this.WorkflowProvider.LocalSubscriber; }
        }

        protected Subscriber? RemoteSubscriber
        {
            get{ return this.WorkflowProvider.RemoteSubscriber; }
        }

        private bool IsSupportedLocally
        {
            get;
        }

        private bool IsSupportedRemotely( IMessage message )
        {
            return this.RemoteSubscriber?.IsSupported( message.DialogName ) ?? false;
        }

        protected void OnMessageProcessingError( MessageProcessingErrorEventArgs e )
        {
            this.MessageProcessingError?.Invoke( this, e );
        }
    
        protected void OnRequestReceived<TRequest, TResponse>(  TRequest request,
                                                                Func<TRequest, TResponse>? createResponseCallback,
                                                                Action<TResponse> sendResponseCallback  )
            where TRequest:IRequest
            where TResponse:IResponse
        {
            if( this.RemoteSubscriber is not null )
            {
                if( this.IsSupportedLocally == true )
                {
                    if( createResponseCallback is not null )
                    {
                        try
                        {
                            TResponse response = createResponseCallback( request );

                            try
                            {
                                sendResponseCallback( response );
                            }catch( Exception ex )
                            {
                                this.OnMessageProcessingError( new MessageProcessingErrorEventArgs( "Error on sending response.", ex ) );        
                            }
                        }catch( Exception ex )
                        {
                            this.OnMessageProcessingError( new MessageProcessingErrorEventArgs( "Error on creating response.", ex ) );        
                        }
                    }else
                    {
                        this.OnMessageProcessingError( new MessageProcessingErrorEventArgs( "No response creation possible." ) );
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

        protected void OnSendMessage<TMessage>( TMessage message, Action<TMessage> sendMessageCallback )
            where TMessage:IMessage
        {
            if( this.RemoteSubscriber is not null )
            {
                if( this.IsSupportedRemotely( message ) == true )
                {
                    sendMessageCallback( message );
                }else
                {
                    throw Assert.Exception( new ArgumentException( $"Remote subscriber doesn't support message '{ message.Name }'.", nameof( message ) ) );
                }
            }else
            {
                throw Assert.Exception( new InvalidOperationException( $"Handshake must occur only once." ) );
            }
        }

        protected async Task OnSendMessageAsync<TMessage>( TMessage message, Func<TMessage, CancellationToken, Task> sendMessageCallback, CancellationToken cancellationToken = default )
            where TMessage:IMessage
        {
            if( this.RemoteSubscriber is not null )
            {
                if( this.IsSupportedRemotely( message ) == true )
                {
                    await sendMessageCallback( message, cancellationToken );
                }else
                {
                    throw Assert.Exception( new ArgumentException( $"Remote subscriber doesn't support message '{ message.Name }'.", nameof( message ) ) );
                }
            }else
            {
                throw Assert.Exception( new InvalidOperationException( $"Handshake must occur only once." ) );
            }
        }

        public void Dispose()
        {
            this.Dispose( true );

            GC.SuppressFinalize( this );
        }

        protected abstract void Dispose( bool disposing );
    }
}
