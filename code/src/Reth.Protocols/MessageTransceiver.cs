using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols.Diagnostics;
using Reth.Protocols.Dialogs;
using Reth.Protocols.Extensions.EventArgsExtensions;
using Reth.Protocols.Serialization;

namespace Reth.Protocols
{
    internal class MessageTransceiver:IMessageTransceiver
    {
        private volatile bool isDisposed;
        private volatile bool isStarted;

        public event EventHandler Terminated;

        public event EventHandler<MessageReceivedEventArgs> MessageReceived
        {
            add
            {
                this.MessageReader.MessageReceived += value;
            }

            remove
            {
                this.MessageReader.MessageReceived -= value;
            }
        }

        public MessageTransceiver(  MessageInitializer outgoingInitializer,
                                    MessageInitializer incomingInitializer,
                                    IMessageSerializer messageSerializer,
                                    IInteractionLog interactionLog,
                                    IEnumerable<IDialogName> supportedDialogs   )
        {
            this.InteractionLog = interactionLog;

            this.MessageFilter = new MessageFilter( supportedDialogs );
            this.MessageReader = new MessageReader( incomingInitializer, messageSerializer, this.MessageFilter, interactionLog );
            this.MessageWriter = new MessageWriter( outgoingInitializer, messageSerializer, this.MessageFilter, interactionLog );
        }

        ~MessageTransceiver()
        {
            this.Dispose( false );
        }

        private Object SyncRoot
        {
            get;
        } = new Object();

        private CancellationTokenSource CancellationTokenSource
        {
            get; set;
        }

        private bool IsStarted
        {
            get
            {
                lock( this.SyncRoot )
                {
                    return this.isStarted;
                }
            }

            set
            {
                lock( this.SyncRoot )
                {
                    this.isStarted = value;
                }
            }
        }

        private Task Messaging
        {
            get; set;
        }

        private IInteractionLog InteractionLog
        {
            get;
        }

        public MessageFilter MessageFilter
        {
            get;
        }

        private MessageReader MessageReader
        {
            get;
        }

        private MessageWriter MessageWriter
        {
            get;
        }

        public void Start( Stream stream )
        {
            lock( this.SyncRoot )
            {
                ExecutionLogProvider.LogInformation( "Starting message transceiver." );

                if( this.IsStarted == false )
                {
                    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

                    this.CancellationTokenSource = cancellationTokenSource;

                    CancellationToken cancellationToken = cancellationTokenSource.Token;

                    Task[] tasks =  new Task[]
                                    {
                                        this.MessageWriter.RunAsync( stream, cancellationToken ),
                                        this.MessageReader.RunAsync( stream, cancellationToken )
                                    };

                    ExecutionLogProvider.LogInformation( $"Task id of message writer: { tasks[ 0 ].Id }" );
                    ExecutionLogProvider.LogInformation( $"Task id of message reader: { tasks[ 1 ].Id }" );

                    this.Messaging = Task.Factory.ContinueWhenAny(  tasks,
                                                                    ( Task result ) =>
                                                                    {
                                                                        ExecutionLogProvider.LogInformation( $"Finished messaging task: { result.Id }" );

                                                                        this.CancellationTokenSource?.Cancel();

                                                                        bool waitResult = Task.WaitAll( tasks, Timeouts.Termination );

                                                                        if( waitResult == false )
                                                                        {
                                                                            ExecutionLogProvider.LogInformation( "Waiting for message reader and message writer timed out." );
                                                                        }

                                                                        if( this.IsStarted == true )
                                                                        {
                                                                            this.IsStarted = false;
                                                                            
                                                                            this.Terminated?.SafeInvoke( this, EventArgs.Empty );
                                                                        }
                                                                    },
                                                                    TaskContinuationOptions.LongRunning );

                    this.IsStarted = true;
                }
            }
        }

        public void Terminate()
        {
            lock( this.SyncRoot )
            {
                ExecutionLogProvider.LogInformation( "Canceling message transceiver." );

                if( this.IsStarted == true )
                {
                    this.IsStarted = false;

                    this.CancellationTokenSource?.Cancel();
                    this.CancellationTokenSource?.Dispose();
                    this.CancellationTokenSource = null;
                }
            }
        }

        public bool PostMessage( IMessage message )
        {
            return this.MessageWriter.PostMessage( message );
        }

        public TResponse SendRequest<TRequest, TResponse>( TRequest request )
            where TRequest:IRequest
            where TResponse:IResponse
        {
            RequestProcess requestProcess = new RequestProcess( this );

            return requestProcess.SendRequest<TRequest, TResponse>( request );
        }

        public Task<TResponse> SendRequestAsync<TRequest, TResponse>( TRequest request )
            where TRequest:IRequest
            where TResponse:IResponse
        {
            return this.SendRequestAsync<TRequest, TResponse>( request, CancellationToken.None );
        }

        public Task<TResponse> SendRequestAsync<TRequest, TResponse>( TRequest request, CancellationToken cancellationToken )
            where TRequest:IRequest
            where TResponse:IResponse
        {
            RequestProcess requestProcess = new RequestProcess( this );

            return requestProcess.SendRequestAsync<TRequest, TResponse>( request );
        }

        public void Dispose()
        {
            this.Dispose( true );

            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
            ExecutionLogProvider.LogInformation( "Disposing message transceiver." );

            if( this.isDisposed == false )
            {
                if( disposing == true )
                {
                    lock( this.SyncRoot )
                    {
                        if( this.IsStarted == true )
                        {
                            this.Terminate();

                            ExecutionLogProvider.LogInformation( "Waiting for messaging to terminate..." );

                            if( this.Messaging.Wait( Timeouts.Termination ) == false )
                            {
                                ExecutionLogProvider.LogWarning( "Waiting for messaging timed out." );
                            }
                        }
                    }

                    this.InteractionLog?.Dispose();
                }

                this.isDisposed = true;
            }
        }
    }
}
