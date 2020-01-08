using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols.Diagnostics;
using Reth.Protocols.Extensions.EventArgsExtensions;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Protocols
{
    internal class MessageDispatcher:IMessageDispatcher
    {
        public event EventHandler<MessageReceivedEventArgs> MessageReceived
        {
            add
            {
                if( !( value is null ) )
                {
                    lock( this.SyncRoot )
                    {
                        this.MessageReceivedCallbacks.Add( value );
                    }
                }
            }

            remove
            {
                if( !( value is null ) )
                {
                    lock( this.SyncRoot )
                    {
                        this.MessageReceivedCallbacks.Remove( value );
                    }
                }
            }
        }

        public MessageDispatcher(   MessageInitializer messageInitializer,
                                    MessageFilter messageFilter )
        {
            messageInitializer.ThrowIfNull();
            messageFilter.ThrowIfNull();

            this.MessageInitializer = messageInitializer;
            this.MessageFilter = messageFilter;
        }

        private Object SyncRoot
        {
            get;
        } = new Object();

        public MessageInitializer MessageInitializer
        {
            get;
        }

        public MessageFilter MessageFilter
        {
            get;
        }

        private HashSet<EventHandler<MessageReceivedEventArgs>> MessageReceivedCallbacks
        {
            get;
        } = new HashSet<EventHandler<MessageReceivedEventArgs>>();

        private void DoMessageReceived( IMessage message,
                                        EventHandler<MessageReceivedEventArgs>[] eventHandlers,
                                        MessageReceivedEventArgs args    )
        {
            Task callbackTask = Task.Run(   () =>
                                            {
                                                foreach( EventHandler<MessageReceivedEventArgs> eventHandler in eventHandlers )
                                                {
                                                    try
                                                    {
                                                        eventHandler?.SafeInvoke( this, args );
                                                    }catch( Exception ex )
                                                    {
                                                        ExecutionLogProvider.LogError( ex );
                                                        ExecutionLogProvider.LogError( "Error within message callback." );
                                                    }
                                                }
                                            }   );

            if( callbackTask.Wait( Timeouts.Callback ) == false )
            {
                ExecutionLogProvider.LogWarning( $"Callback of message '{ message.GetType().Name }' with ID '{ message.Id }' has timed out." );

                UnhandledMessageHandler.Invoke( new UnhandledMessage( UnhandledReason.Timeout, MessageDirection.Incoming, message ) );
            }
        }

        public void Dispatch<T>( T message )where T:IMessage
        {
            MessageReceivedEventArgs args = new MessageReceivedEventArgs( message );

            try
            {
                EventHandler<MessageReceivedEventArgs>[] eventHandlers = null;

                lock( this.SyncRoot )
                {
                    eventHandlers = new EventHandler<MessageReceivedEventArgs>[ this.MessageReceivedCallbacks.Count ];

                    this.MessageReceivedCallbacks.CopyTo( eventHandlers );
                }

                if( eventHandlers.Length != 0 )
                {
                    this.DoMessageReceived( message, eventHandlers, args );
                }else
                {
                    ExecutionLogProvider.LogWarning( $"Callback for message '{ message.GetType().Name }' not found." );

                    UnhandledMessageHandler.Invoke( new UnhandledMessage( UnhandledReason.NotDispatched, MessageDirection.Incoming, message ) );
                }
            }catch( Exception ex )
            {
                ExecutionLogProvider.LogError( ex );
                ExecutionLogProvider.LogError( "Error within message callback." );
            }

            if( args.IsHandled == false )
            {
                ExecutionLogProvider.LogWarning( $"Message '{ message.GetType().Name }' with ID '{ message.Id }' has not been processed." );

                UnhandledMessageHandler.Invoke( new UnhandledMessage( UnhandledReason.NotProcessed, MessageDirection.Incoming, message ) );
            }
        }

        public Task RunAsync( IMessageReadQueue readQueue )
        {
            return this.RunAsync( readQueue, CancellationToken.None );
        }

        public Task RunAsync(   IMessageReadQueue readQueue,
                                CancellationToken cancellationToken )
        {
            readQueue.ThrowIfNull();

            Task result = null;

            if( cancellationToken.IsCancellationRequested == true )
            {
                result = Task.FromCanceled( cancellationToken );
            }else
            {
                result = Task.Factory.StartNew( () =>
							                    {
                                                    ExecutionLogProvider.LogInformation( "Message dispatcher started." );
                                                                        
                                                    IMessage message = readQueue.GetMessage();

                                                    while( !( message is null ) )
                                                    {
                                                        if( cancellationToken.IsCancellationRequested == false )
                                                        {
                                                            if( this.MessageInitializer.Initialize( message ) == true )
                                                            {
                                                                if( this.MessageFilter.Granted( message ) == true )
                                                                {
                                                                    ExecutionLogProvider.LogInformation( $"Dispatching message with ID: '{ message.Id }' ({ message.GetType().FullName })." );

                                                                    message.Dispatch( this );
                                                                    message = readQueue.GetMessage();
                                                                }else
                                                                {
                                                                    ExecutionLogProvider.LogWarning( $"Message '{ message.GetType().Name }' is unsupported." );

                                                                    UnhandledMessageHandler.Invoke( new UnhandledMessage( UnhandledReason.Unsupported, MessageDirection.Incoming, message ) );
                                                                }
                                                            }else
                                                            {
                                                                ExecutionLogProvider.LogWarning( $"Message '{ message.GetType().Name }' is skipped due to missing initialization." );

                                                                UnhandledMessageHandler.Invoke( new UnhandledMessage( UnhandledReason.Shutdown, MessageDirection.Outgoing, message ) );
                                                            }
                                                        }else
                                                        {
                                                            ExecutionLogProvider.LogInformation( $"Dispatching message is rejected due to cancellation: { message.ToString() } ({ message.GetType().FullName })." );

                                                            UnhandledMessageHandler.Invoke( new UnhandledMessage( UnhandledReason.Shutdown, MessageDirection.Incoming, message ) );
                                                        }
                                                    }

                                                    ExecutionLogProvider.LogInformation( "Message dispatcher finished." );
							                    },
                                                CancellationToken.None,
                                                TaskCreationOptions.LongRunning,
                                                TaskScheduler.Current   );
            }

            return result;
        }
    }
}