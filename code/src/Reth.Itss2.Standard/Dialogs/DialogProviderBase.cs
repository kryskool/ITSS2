using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Standard.Dialogs.General.Unprocessed;
using Reth.Itss2.Standard.Serialization;

using Reth.Protocols;
using Reth.Protocols.Diagnostics;
using Reth.Protocols.Dialogs;
using Reth.Protocols.Extensions.EventArgsExtensions;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs
{
    public abstract class DialogProviderBase:IDialogProvider
    {
        private volatile bool isDisposed;

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event EventHandler Terminated;

        protected internal DialogProviderBase(  MessageInitializer outgoingInitializer,
                                                MessageInitializer incomingInitializer,
                                                IProtocolProvider protocolProvider,
                                                IInteractionLog interactionLog,
                                                IEnumerable<IDialogName> supportedDialogs   )
        {
            protocolProvider.ThrowIfNull();
            supportedDialogs.ThrowIfNull();

            this.SupportedDialogs = new List<IDialogName>( supportedDialogs );

            this.MessageTransceiver = MessageTransceiverFactory.Create( outgoingInitializer,
                                                                        incomingInitializer,
                                                                        protocolProvider.MessageSerializer,
                                                                        interactionLog,
                                                                        supportedDialogs    );

            this.MessageTransceiver.MessageReceived += this.MessageTransceiver_MessageReceived;
            this.MessageTransceiver.Terminated += this.MessageTransceiver_Terminated;
        }

        ~DialogProviderBase()
        {
            this.Dispose( false );
        }

        private IMessageTransceiver MessageTransceiver
        {
            get;
        }

        private List<IDialogName> SupportedDialogs
        {
            get;
        }

        public abstract IUnprocessedDialog Unprocessed{ get; }

        private void MessageTransceiver_MessageReceived( Object sender, MessageReceivedEventArgs e )
        {
            this.MessageReceived?.SafeInvoke( this, e );   
        }

        private void MessageTransceiver_Terminated( Object sender, EventArgs e )
        {
            this.Terminated?.SafeInvoke( this, e );
        }

        public IDialogName[] GetSupportedDialogs()
        {
            return this.SupportedDialogs.ToArray();
        }

        public bool PostMessage( IMessage message )
        {
            return this.MessageTransceiver.PostMessage( message );
        }

        public TResponse SendRequest<TRequest, TResponse>( TRequest request )
            where TRequest:IRequest
            where TResponse:IResponse
        {
            return this.MessageTransceiver.SendRequest<TRequest, TResponse>( request );
        }

        public Task<TResponse> SendRequestAsync<TRequest, TResponse>( TRequest request )
            where TRequest:IRequest
            where TResponse:IResponse
        {
            return this.MessageTransceiver.SendRequestAsync<TRequest, TResponse>( request );
        }

        public Task<TResponse> SendRequestAsync<TRequest, TResponse>( TRequest request, CancellationToken cancellationToken )
            where TRequest:IRequest
            where TResponse:IResponse
        {
            return this.MessageTransceiver.SendRequestAsync<TRequest, TResponse>( request, cancellationToken );
        }

        public void Start( Stream stream )
        {
            this.MessageTransceiver.Start( stream );
        }

        public void Terminate()
        {
            this.MessageTransceiver.Terminate();
        }

        public void Dispose()
        {
            this.Dispose( true );

            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
            ExecutionLogProvider.LogInformation( "Disposing dialog provider." );

            if( this.isDisposed == false )
            {
                this.MessageTransceiver.MessageReceived -= this.MessageTransceiver_MessageReceived;
                this.MessageTransceiver.Terminated -= this.MessageTransceiver_Terminated;

                if( disposing == true )
                {
                    this.MessageTransceiver.Dispose();
                }

                this.isDisposed = true;
            }
        }
    }
}