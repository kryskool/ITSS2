using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage;
using Reth.Itss2.Standard.Dialogs.Storage.Output;
using Reth.Protocols;
using Reth.Protocols.Diagnostics;
using Reth.Protocols.Extensions.EventArgsExtensions;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Client.Processing.Output
{
    internal class OutputProcess:IOutputProcess
    {
        private bool isFinished;

        public event EventHandler<OutputProcessingEventArgs> Processing;
        public event EventHandler<OutputFinishedEventArgs> Finished;

        public OutputProcess( StorageSystem storageSystem )
        {
            storageSystem.ThrowIfNull();

            this.StorageSystem = storageSystem;
            this.Id = MessageId.NewId();
        }

        private Object SyncRoot
        {
            get;
        } = new Object();

        public StorageSystem StorageSystem{ get; }
        public IMessageId Id{ get; }

        private IMessageTransceiver MessageTransceiver
        {
            get{ return this.StorageSystem.DialogProvider; }
        }

        private bool IsFinished
        {
            get
            {
                lock( this.SyncRoot )
                {
                    return this.isFinished;
                }
            }

            set
            {
                lock( this.SyncRoot )
                {
                    this.isFinished = value;
                }
            }
        }

        private OutputRequest CreateRequest(    OutputRequestDetails details,
                                                String boxNumber,
                                                IEnumerable<OutputCriteria> criterias   )
        {
            return new OutputRequest(   this.Id,
                                        this.StorageSystem.LocalSubscriber.Id,
                                        this.StorageSystem.RemoteSubscriber.Id,
                                        details,
                                        boxNumber,
                                        criterias   );
        }

        private EventHandler<MessageReceivedEventArgs> CreateMessageInterceptedCallback()
        {
            return new EventHandler<MessageReceivedEventArgs>(  ( Object sender, MessageReceivedEventArgs e ) =>
                                                                {
                                                                    try
                                                                    {
                                                                        OutputMessage message = ( OutputMessage )( e.Message );

                                                                        OutputMessageStatus status = message.Details.Status;

                                                                        switch( status )
                                                                        {
                                                                            case OutputMessageStatus.Aborted:
                                                                            case OutputMessageStatus.Incomplete:
                                                                            case OutputMessageStatus.Completed:
                                                                                ExecutionLogProvider.LogInformation( $"Output process finished with status: { status }" );

                                                                                this.IsFinished = true;

                                                                                this.Finished?.SafeInvoke( this, new OutputFinishedEventArgs( message ) );
                                                                                break;

                                                                            default:
                                                                                ExecutionLogProvider.LogInformation( $"Output process processing status: { status }" );

                                                                                this.Processing?.SafeInvoke( this, new OutputProcessingEventArgs( message ) );
                                                                                break;
                                                                        }                                                                       
                                                                    }catch( Exception ex )
                                                                    {
                                                                        ExecutionLogProvider.LogError( ex );
                                                                        ExecutionLogProvider.LogError( "Error within output process callback." );
                                                                    }
                                                                }   );
        }

        public OutputResponse Start(    OutputRequestDetails details,
                                        String boxNumber,
                                        IEnumerable<OutputCriteria> criterias   )
        {
            return this.StartAsync( details, boxNumber, criterias ).Result;
        }

        public Task<OutputResponse> StartAsync( OutputRequestDetails details,
                                                String boxNumber,
                                                IEnumerable<OutputCriteria> criterias   )
        {
            return this.StartAsync( details, boxNumber, criterias, CancellationToken.None );
        }

        public async Task<OutputResponse> StartAsync(   OutputRequestDetails details,
                                                        String boxNumber,
                                                        IEnumerable<OutputCriteria> criterias,
                                                        CancellationToken cancellationToken )
        {
            OutputResponse response = null;

            OutputRequest request = this.CreateRequest( details, boxNumber, criterias );

            using( MessageInterceptor interceptor = new MessageInterceptor( this.MessageTransceiver, typeof( OutputMessage ), request.Id ) )
            {
                ExecutionLogProvider.LogInformation( "Installing message interceptor for output process." );

                interceptor.Intercepted += this.CreateMessageInterceptedCallback();

                ExecutionLogProvider.LogInformation( "Sending output request." );

                IOutputClientDialog dialog = this.StorageSystem.DialogProvider.Output;

                response = await dialog.SendRequestAsync( request, cancellationToken ).ConfigureAwait( false );

                OutputResponseStatus responseStatus = response.Details.Status;

                ExecutionLogProvider.LogInformation( $"Status of output response: { responseStatus.ToString() }." );

                if( responseStatus != OutputResponseStatus.Rejected )
                {
                    try
                    {
                        do
                        {
                            if( interceptor.Wait( Timeouts.Processing, cancellationToken ) == false )
                            {
                                ExecutionLogProvider.LogError( "Output process has been timed out." );

                                UnhandledMessageHandler.Invoke( new UnhandledMessage( UnhandledReason.Timeout, MessageDirection.Outgoing, request ) );
                            }
                        }while( this.IsFinished == false );
                    }catch( OperationCanceledException ex )
                    {
                        ExecutionLogProvider.LogError( ex );
                        ExecutionLogProvider.LogError( "Output process has been canceled." );

                        UnhandledMessageHandler.Invoke( new UnhandledMessage( UnhandledReason.Cancelled, MessageDirection.Outgoing, request ) );

                        throw new OutputProcessException( "Expected output message has not been received.", ex );
                    }
                }
            }

            return response;
        }
    }
}
