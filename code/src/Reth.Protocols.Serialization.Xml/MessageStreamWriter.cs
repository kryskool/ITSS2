using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols.Diagnostics;
using Reth.Protocols.Extensions.ObjectExtensions;
using Reth.Protocols.Serialization.Xml.Diagnostics;
using Reth.Protocols.Serialization.Xml.Parsing;

namespace Reth.Protocols.Serialization.Xml
{
    internal class MessageStreamWriter:IMessageStreamWriter
    {
        private const int BufferSize = 8192;

        public MessageStreamWriter( IInteractionLog interactionLog, ITypeMappings typeMappings )
        {
            typeMappings.ThrowIfNull();

            this.InteractionLog = interactionLog;
            this.TypeMappings = typeMappings;
        }

        ~MessageStreamWriter()
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
        
        public Task RunAsync( IMessageReadQueue messageQueue, Stream stream )
        {
            return this.RunAsync( messageQueue, stream, CancellationToken.None );
        }

        public Task RunAsync( IMessageReadQueue messageQueue, Stream stream, CancellationToken cancellationToken )
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
                                                    ExecutionLogProvider.LogInformation( "Message stream writer started." );

                                                    MessageParser messageParser = new MessageParser( this.TypeMappings );

                                                    using( StreamWriter writer = new StreamWriter( stream, new UTF8Encoding( false ), MessageStreamWriter.BufferSize, true ) )
                                                    {
                                                        IMessage message = messageQueue.GetMessage();

                                                        while( !( message is null ) )
                                                        {
                                                            if( cancellationToken.IsCancellationRequested == false )
                                                            {
                                                                String parsedMessage = messageParser.Parse( message );

                                                                ExecutionLogProvider.LogInformation( $"Writing message: { message.ToString() } ({ message.GetType().FullName })" );

                                                                try
                                                                {
                                                                    this.InteractionLog?.LogMessage( new XmlInteractionLogMessage(  MessageDirection.Outgoing,
                                                                                                                                    parsedMessage ) );
                                                                }catch
                                                                {
                                                                }

                                                                try
                                                                {
                                                                    try
                                                                    {
                                                                        writer.Write( parsedMessage );
                                                                        writer.Flush();
                                                                    }catch( IOException ex )
                                                                    {
                                                                        ExecutionLogProvider.LogError( ex );

                                                                        UnhandledMessageHandler.Invoke( new UnhandledMessage(   UnhandledReason.ConnectionError,
                                                                                                                                MessageDirection.Outgoing,
                                                                                                                                message,
                                                                                                                                ex  ) );
                                                                    }

                                                                    message = messageQueue.GetMessage();
                                                                }catch( Exception ex )
                                                                {
                                                                    ExecutionLogProvider.LogError( ex );

                                                                    UnhandledMessageHandler.Invoke( new UnhandledMessage(   UnhandledReason.UnknownError,
                                                                                                                            MessageDirection.Outgoing,
                                                                                                                            message,
                                                                                                                            ex  ) );

                                                                    throw new SerializationException( "Message stream writer ended prematurely.", ex );
                                                                }
                                                            }else
                                                            {
                                                                ExecutionLogProvider.LogInformation( $"Writing message is rejected due to cancellation: { message.ToString() } ({ message.GetType().FullName })." );
                                                            }
                                                        }
                                                    }

                                                    ExecutionLogProvider.LogInformation( "Message stream writer finished." );
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