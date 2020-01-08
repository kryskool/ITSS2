using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Standard.Dialogs.Storage.Input;
using Reth.Protocols;
using Reth.Protocols.Diagnostics;
using Reth.Protocols.Extensions.EventArgsExtensions;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Client.Processing.Input
{
    internal class InputProcess:IInputProcess
    {
        public event EventHandler<InputFinishedEventArgs> Finished;

        public InputProcess( StorageSystem storageSystem, InputRequest request )
        {
            storageSystem.ThrowIfNull();
            request.ThrowIfNull();

            this.StorageSystem = storageSystem;
            this.Id = request.Id;

            this.Request = request;
        }

        public StorageSystem StorageSystem{ get; }
        public IMessageId Id{ get; }

        private InputRequest Request
        {
            get;
        }

        private IMessageTransceiver MessageTransceiver
        {
            get{ return this.StorageSystem.DialogProvider; }
        }

        private InputResponse CreateInputResponse( InputResponseArticle article )
        {
            return new InputResponse( this.Request, article );
        }

        private EventHandler<MessageReceivedEventArgs> CreateMessageInterceptedCallback()
        {
            return new EventHandler<MessageReceivedEventArgs>(  ( Object sender, MessageReceivedEventArgs e ) =>
                                                                {
                                                                    try
                                                                    {
                                                                        ExecutionLogProvider.LogInformation( "Input process finished." );

                                                                        this.Finished?.SafeInvoke( this, new InputFinishedEventArgs( ( InputMessage )( e.Message ) ) );
                                                                    }catch( Exception ex )
                                                                    {
                                                                        ExecutionLogProvider.LogError( ex );
                                                                        ExecutionLogProvider.LogError( "Error within input process finished callback." );
                                                                    }
                                                                }   );
        }

        public void Start( InputResponseArticle article )
        {
            this.StartAsync( article ).Wait();
        }

        public Task StartAsync( InputResponseArticle article )
        {
            return this.StartAsync( article, CancellationToken.None );
        }

        public async Task StartAsync( InputResponseArticle article, CancellationToken cancellationToken )
        {
            InputResponse response = this.CreateInputResponse( article );

            using( MessageInterceptor interceptor = new MessageInterceptor( this.MessageTransceiver, typeof( InputMessage ), this.Id ) )
            {
                ExecutionLogProvider.LogInformation( "Installing message interceptor for input process." );

                interceptor.Intercepted += this.CreateMessageInterceptedCallback();

                ExecutionLogProvider.LogInformation( "Sending input response." );         

                IInputClientDialog dialog = this.StorageSystem.DialogProvider.Input;

                await dialog.SendResponseAsync( response, cancellationToken ).ConfigureAwait( false );

                try
                {
                    if( interceptor.Wait( Timeouts.Processing, cancellationToken ) == false )
                    {
                        ExecutionLogProvider.LogError( "Input process has been timed out." );

                        UnhandledMessageHandler.Invoke( new UnhandledMessage( UnhandledReason.Timeout, MessageDirection.Outgoing, response ) );
                    }
                }catch( OperationCanceledException ex )
                {
                    ExecutionLogProvider.LogError( ex );
                    ExecutionLogProvider.LogError( "Input process has been canceled." );

                    UnhandledMessageHandler.Invoke( new UnhandledMessage( UnhandledReason.Cancelled, MessageDirection.Outgoing, response ) );

                    throw new InputProcessException( "Expected input message has not been received.", ex );
                }
            }
        }
    }
}
