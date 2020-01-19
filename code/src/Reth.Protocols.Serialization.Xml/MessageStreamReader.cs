using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols.Diagnostics;
using Reth.Protocols.Extensions.ObjectExtensions;
using Reth.Protocols.Serialization.Xml.Parsing;

namespace Reth.Protocols.Serialization.Xml
{
    internal class MessageStreamReader:IMessageStreamReader
    {
        private const int BufferSize = 8192;
        private const int MaximumMessageSize = 104857600;

        public MessageStreamReader( IInteractionLog interactionLog, ITypeMappings typeMappings )
        {
            typeMappings.ThrowIfNull();

            this.InteractionLog = interactionLog;
            this.TypeMappings = typeMappings;
        }

        ~MessageStreamReader()
        {
            this.Dispose( false );
        }

        private IInteractionLog InteractionLog
        {
            get;
        }

        private ITypeMappings TypeMappings
        {
            get;
        }

        public Task RunAsync( IMessageWriteQueue messageQueue, Stream stream )
        {
            return this.RunAsync( messageQueue, stream, CancellationToken.None );
        }

        public Task RunAsync( IMessageWriteQueue messageQueue, Stream stream, CancellationToken cancellationToken )
        {
            messageQueue.ThrowIfNull();
            stream.ThrowIfNull();

            Task result = null;

            if( cancellationToken.IsCancellationRequested == true )
            {
                result = Task.FromCanceled( cancellationToken );
            }else
            {
                result = Task.Factory.StartNew( () =>
                                                {
                                                    ExecutionLogProvider.LogInformation( "Message stream reader started." );

                                                    try
                                                    {
                                                        int readBytes = 0;
                                                        int bufferSize = MessageStreamReader.BufferSize;

                                                        byte[] readBuffer = new byte[ bufferSize ];
                                                        
                                                        MessageExtractor messageExtractor = new MessageExtractor(   messageQueue,
                                                                                                                    this.InteractionLog,
                                                                                                                    this.TypeMappings,
                                                                                                                    MessageStreamReader.BufferSize,
                                                                                                                    MessageStreamReader.MaximumMessageSize );
                                                    
                                                        do
                                                        {
                                                            readBytes = stream.Read( readBuffer, 0, bufferSize );

                                                            if( readBytes > 0 )
                                                            {
                                                                ExecutionLogProvider.LogInformation( $"Read bytes: { readBytes }" );

                                                                messageExtractor.Run( new MessageBlock( readBuffer, readBytes ) );
                                                            }else
                                                            {
                                                                ExecutionLogProvider.LogInformation( "No more bytes available. " );
                                                            }
                                                        }while( readBytes > 0 );
                                                    }catch( Exception ex )
                                                    {
                                                        ExecutionLogProvider.LogError( ex );
                                                        ExecutionLogProvider.LogError( "Error while reading from stream." );

                                                        throw new MessageSerializationException( "Message stream reader ended prematurely.", ex );
                                                    }

                                                    ExecutionLogProvider.LogInformation( "Message stream reader finished." );
                                                },
                                                CancellationToken.None,
                                                TaskCreationOptions.DenyChildAttach | TaskCreationOptions.LongRunning,
                                                TaskScheduler.Current   );
            }

            return result;
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
