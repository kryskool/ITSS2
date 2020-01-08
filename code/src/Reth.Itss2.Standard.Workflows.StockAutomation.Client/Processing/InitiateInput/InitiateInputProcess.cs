using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage.InitiateInput;
using Reth.Protocols;
using Reth.Protocols.Diagnostics;
using Reth.Protocols.Extensions.EventArgsExtensions;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Client.Processing.InitiateInput
{
    internal class InitiateInputProcess:IInitiateInputProcess
    {
        public event EventHandler<InitiateInputFinishedEventArgs> Finished;

        public InitiateInputProcess( StorageSystem storageSystem )
        {
            storageSystem.ThrowIfNull();

            this.StorageSystem = storageSystem;
            this.Id = MessageId.NewId();
        }

        public StorageSystem StorageSystem{ get; }
        public IMessageId Id{ get; }

        private IMessageTransceiver MessageTransceiver
        {
            get{ return this.StorageSystem.DialogProvider; }
        }

        private InitiateInputRequest CreateRequest( InitiateInputRequestDetails details,
                                                    InitiateInputRequestArticle article,
                                                    bool isNewDelivery,
                                                    bool setPickingIndicator )
        {
            return new InitiateInputRequest(    this.Id,
                                                this.StorageSystem.LocalSubscriber.Id,
                                                this.StorageSystem.RemoteSubscriber.Id,
                                                details,
                                                article,
                                                isNewDelivery,
                                                setPickingIndicator );
        }

        private EventHandler<MessageReceivedEventArgs> CreateMessageInterceptedCallback()
        {
            return new EventHandler<MessageReceivedEventArgs>(  ( Object sender, MessageReceivedEventArgs e ) =>
                                                                {
                                                                    try
                                                                    {
                                                                        ExecutionLogProvider.LogInformation( "Initiate input process finished." );

                                                                        this.Finished?.SafeInvoke( this, new InitiateInputFinishedEventArgs( ( InitiateInputMessage )( e.Message ) ) );
                                                                    }catch( Exception ex )
                                                                    {
                                                                        ExecutionLogProvider.LogError( ex );
                                                                        ExecutionLogProvider.LogError( "Error within initiate input process finished callback." );
                                                                    }
                                                                }   );
        }

        public InitiateInputResponse Start( InitiateInputRequestDetails details,
                                            InitiateInputRequestArticle article,
                                            bool isNewDelivery,
                                            bool setPickingIndicator    )
        {
            return this.StartAsync( details, article, isNewDelivery, setPickingIndicator ).Result;
        }

        public Task<InitiateInputResponse> StartAsync(  InitiateInputRequestDetails details,
                                                        InitiateInputRequestArticle article,
                                                        bool isNewDelivery,
                                                        bool setPickingIndicators   )
        {
            return this.StartAsync( details, article, isNewDelivery, setPickingIndicators, CancellationToken.None );
        }

        public async Task<InitiateInputResponse> StartAsync(    InitiateInputRequestDetails details,
                                                                InitiateInputRequestArticle article,
                                                                bool isNewDelivery,
                                                                bool setPickingIndicator,
                                                                CancellationToken cancellationToken )
        {
            InitiateInputResponse response = null;

            InitiateInputRequest request = this.CreateRequest( details, article, isNewDelivery, setPickingIndicator );

            using( MessageInterceptor interceptor = new MessageInterceptor( this.MessageTransceiver, typeof( InitiateInputMessage ), request.Id ) )
            {
                ExecutionLogProvider.LogInformation( "Installing message interceptor for initiate input process." );

                interceptor.Intercepted += this.CreateMessageInterceptedCallback();

                ExecutionLogProvider.LogInformation( "Sending initiate input request." );

                IInitiateInputClientDialog dialog = this.StorageSystem.DialogProvider.InitiateInput;

                response = await dialog.SendRequestAsync( request, cancellationToken ).ConfigureAwait( false );

                InitiateInputResponseStatus status = response.Details.Status;

                ExecutionLogProvider.LogInformation( $"Status of initiate input response: { status.ToString() }." );

                if( status != InitiateInputResponseStatus.Rejected )
                {
                    try
                    {
                        if( interceptor.Wait( Timeouts.Processing, cancellationToken ) == false )
                        {
                            ExecutionLogProvider.LogError( "Initiate input process has been timed out." );

                            UnhandledMessageHandler.Invoke( new UnhandledMessage( UnhandledReason.Timeout, MessageDirection.Outgoing, request ) );
                        }
                    }catch( OperationCanceledException ex )
                    {
                        ExecutionLogProvider.LogError( ex );
                        ExecutionLogProvider.LogError( "Initiate input process has been canceled." );

                        UnhandledMessageHandler.Invoke( new UnhandledMessage( UnhandledReason.Cancelled, MessageDirection.Outgoing, request ) );

                        throw new InitiateInputProcessException( "Expected initiate input message has not been received.", ex );
                    }
                }
            }

            return response;
        }
    }
}
