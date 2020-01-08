using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols.Diagnostics;
using Reth.Protocols.Extensions.ObjectExtensions;
using Reth.Protocols.Serialization;

namespace Reth.Protocols
{
    internal class MessageReader
    {
        public event EventHandler<MessageReceivedEventArgs> MessageReceived
        {
            add
            {
                this.MessageDispatcher.MessageReceived += value;
            }

            remove
            {
                this.MessageDispatcher.MessageReceived -= value;
            }
        }

        public MessageReader(   MessageInitializer messageInitializer,
                                IMessageSerializer messageSerializer,
                                MessageFilter messageFilter,
                                IInteractionLog interactionLog  )
        {
            messageSerializer.ThrowIfNull();

            this.MessageSerializer = messageSerializer;
            this.InteractionLog = interactionLog;

            this.MessageDispatcher = new MessageDispatcher( messageInitializer, messageFilter );
        }

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
            get{ return this.MessageDispatcher.MessageFilter; }
        }

        public IInteractionLog InteractionLog
        {
            get;
        }

        private MessageDispatcher MessageDispatcher
        {
            get;
        }

        public Task RunAsync( Stream stream )
        {
            return this.RunAsync( stream, CancellationToken.None );
        }

        public Task RunAsync( Stream stream, CancellationToken cancellationToken )
        {
            Task result = null;

            if( cancellationToken.IsCancellationRequested == true )
            {
                result = Task.FromCanceled( cancellationToken );
            }else
            {
                ExecutionLogProvider.LogInformation( "Message reader running." );

                IMessageStreamReader messageReader = null;

                Task dispatchingTask = null;
                Task readerTask = null;

                MessageQueue readQueue = new MessageQueue();

                try
                {
                    messageReader = this.MessageSerializer.GetStreamReader( this.InteractionLog );

                    dispatchingTask = this.MessageDispatcher.RunAsync( readQueue, cancellationToken );

                    readerTask = messageReader.RunAsync( readQueue, stream, cancellationToken );
                }catch
                {
                    messageReader?.Dispose();
                    
                    throw;
                }

                result = Task.Factory.StartNew( () =>
                                                {
                                                    ExecutionLogProvider.LogInformation( "Message reader started." );

                                                    try
                                                    {
                                                        readerTask.Wait();
                                                    }catch( Exception ex )
                                                    {
                                                        ExecutionLogProvider.LogError( ex );
                                                    }

                                                    try
                                                    {
                                                        readQueue.Terminate();

                                                        ExecutionLogProvider.LogInformation( "Waiting for dispatching to terminate..." );

                                                        if( dispatchingTask?.Wait( Timeouts.Termination ) == false )
                                                        {
                                                            ExecutionLogProvider.LogWarning( "Waiting for dispatching termination timed out." );
                                                        }

                                                        ExecutionLogProvider.LogInformation( "Message reader terminated." );
                                                    }finally
                                                    {
                                                        messageReader?.Dispose();
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
