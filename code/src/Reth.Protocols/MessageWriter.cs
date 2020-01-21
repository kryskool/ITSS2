using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols.Diagnostics;
using Reth.Protocols.Extensions.ObjectExtensions;
using Reth.Protocols.Serialization;

namespace Reth.Protocols
{
    internal class MessageWriter
    {
        public MessageWriter(   MessageInitializer messageInitializer,
                                IMessageSerializer messageSerializer,
                                MessageFilter messageFilter,
                                IInteractionLog interactionLog  )
        {
            messageSerializer.ThrowIfNull();
            messageInitializer.ThrowIfNull();
            messageFilter.ThrowIfNull();

            this.MessageSerializer = messageSerializer;
            this.MessageInitializer = messageInitializer;
            this.MessageFilter = messageFilter;
            this.InteractionLog = interactionLog;
        }

        public Object SyncRoot
        {
            get;
        } = new Object();

        public MessageInitializer MessageInitializer
        {
            get;
        }

        public IMessageSerializer MessageSerializer
        {
            get;
        }

        public MessageFilter MessageFilter
        {
            get;
        }

        public IInteractionLog InteractionLog
        {
            get;
        }

        private MessageQueue MessageQueue
        {
            get; set;
        }

        public bool PostMessage( IMessage message )
        {
            message.ThrowIfNull();

            bool result = false;

            lock( this.SyncRoot )
            {
                if( !( this.MessageQueue is null ) )
                {
                    if( this.MessageInitializer.Initialize( message ) == true )
                    {
                        if( this.MessageFilter.Granted( message ) == true )
                        {
                            result = this.MessageQueue.PostMessage( message );
                        }else
                        {
                            ExecutionLogProvider.LogWarning( $"Message '{ message.GetType().Name }' is unsupported." );

                            UnhandledMessageHandler.Invoke( new UnhandledMessage( UnhandledReason.Unsupported, MessageDirection.Outgoing, message ) );
                        }
                    }else
                    {
                        ExecutionLogProvider.LogWarning( $"Message '{ message.GetType().Name }' is skipped due to missing initialization." );

                        UnhandledMessageHandler.Invoke( new UnhandledMessage( UnhandledReason.Shutdown, MessageDirection.Outgoing, message ) );
                    }
                }else
                {
                    ExecutionLogProvider.LogWarning( $"Posting of message '{ message.GetType().Name }' is not possible, because message writer is stopped." );

                    UnhandledMessageHandler.Invoke( new UnhandledMessage( UnhandledReason.Shutdown, MessageDirection.Outgoing, message ) );
                }
            }

            return result;
        }

        private void CheckIfAlreadyRunning()
        {
            bool isAlreadyRunning = ( this.MessageQueue != null );

            Debug.Assert( isAlreadyRunning == false, $"{ isAlreadyRunning } == false" );

            if( isAlreadyRunning == true )
            {
                throw new InvalidOperationException( "An asynchronous writer operation is already running." );
            }
        }

        private void Terminate()
        {
            lock( this.SyncRoot )
            {
                this.MessageQueue?.Terminate();
                this.MessageQueue = null;
            }
        }

        public Task RunAsync( Stream stream )
        {
            return this.RunAsync( stream, CancellationToken.None );
        }

        public Task RunAsync( Stream stream, CancellationToken cancellationToken )
        {
            this.CheckIfAlreadyRunning();
            
            Task result = null;

            if( cancellationToken.IsCancellationRequested == true )
            {
                result = Task.FromCanceled( cancellationToken );
            }else
            {
                ExecutionLogProvider.LogInformation( "Message writer is running." );

                cancellationToken.Register( () =>
                                            {
                                                this.Terminate();
                                            }   );
                
                IMessageStreamWriter messageWriter = null;
                Task writerTask = null;

                lock( this.SyncRoot )
                {
                    try 
                    {
                        this.MessageQueue = new MessageQueue();

                        messageWriter = this.MessageSerializer.GetStreamWriter( this.InteractionLog );

                        writerTask = messageWriter.RunAsync( this.MessageQueue, stream, cancellationToken );
                    }catch
                    {
                        messageWriter?.Dispose();

                        throw;
                    }
                }

                result = Task.Factory.StartNew( () =>
                                                {
                                                    try
                                                    {
                                                        ExecutionLogProvider.LogInformation( "Message writer started." );

                                                        writerTask.Wait();

                                                        this.Terminate();

                                                        ExecutionLogProvider.LogInformation( "Message writer terminated." );
                                                    }finally
                                                    {
                                                        messageWriter?.Dispose();
                                                    }
                                                },
                                                CancellationToken.None,
                                                TaskCreationOptions.LongRunning,
                                                TaskScheduler.Current   );
            }

            return result;
        }
    }
}
