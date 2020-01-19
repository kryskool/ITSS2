using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Reth.Protocols.Diagnostics;
using Reth.Protocols.Serialization.Xml.Diagnostics;

namespace Reth.Protocols.Serialization.Xml.Parsing
{
    internal class MessageExtractor:IDisposable
    {
        private volatile bool isDisposed;

        public MessageExtractor(    IMessageWriteQueue messageQueue,
                                    IInteractionLog interactionLog,
                                    ITypeMappings typeMappings,
                                    int bufferSize,
                                    int maximumMessageSize  )
        {
            Debug.Assert( bufferSize >= 0, $"{ nameof( bufferSize ) } >= 0" );

            if( bufferSize < 0 )
            {
                throw new ArgumentOutOfRangeException( nameof( bufferSize ), bufferSize, "The buffer size must not be negative." );
            }

            this.MessageQueue = messageQueue;
            this.InteractionLog = interactionLog;

            this.MessageParser = new MessageParser( typeMappings );
            this.MessageBuffer = new MessageBuffer( bufferSize, maximumMessageSize );
        }

        ~MessageExtractor()
        {
            this.Dispose( false );
        }

        private IMessageWriteQueue MessageQueue
        {
            get;
        }

        private IInteractionLog InteractionLog
        {
            get;
        }

        private MessageParser MessageParser
        {
            get;
        }

        private MessageBuffer MessageBuffer
        {
            get; set;
        }

        private List<MessageExtractorStateTransition> Transitions
        {
            get;
        } = new List<MessageExtractorStateTransition>();

        private MessageExtractorState State
        {
            get;
            set;
        } = MessageExtractorState.OutOfMessage;

        private MessageBlockPatternMatch Match
        {
            get;
            set;
        } = MessageBlockPatternMatch.None;

        public void Run( MessageBlock messageBlock )
        {
            if( !( messageBlock is null ) )
            {
                if( this.Append( messageBlock ) == true )
                {
                    bool matchSuccess = false;

                    do
                    {
                        ( MessageBlockPatternMatch, MessageExtractorState ) preparation = this.PrepareTransition();

                        this.HandleTransition( preparation.Item1, preparation.Item2 );

                        matchSuccess = preparation.Item1.Success;
                    }while( matchSuccess == true );
                }
            }
        }

        private bool Append( MessageBlock messageBlock )
        {
            bool result = false;

            try
            {
                this.MessageBuffer.Append( messageBlock );

                result = true;
            }catch( InvalidOperationException ex )
            {
                ExecutionLogProvider.LogError( ex );

                UnhandledMessageHandler.Invoke( new UnhandledMessage(   UnhandledReason.TooLarge,
                                                                        MessageDirection.Incoming,
                                                                        new RawMessage( messageBlock.ToString() ),
                                                                        ex  ) );
            }

            return result;
        }

        private ( MessageBlockPatternMatch, MessageExtractorState ) PrepareTransition()
        {
            MessageBlockPatternMatch match = MessageBlockPatternMatch.None;
            MessageExtractorState state = this.State;

            switch( this.State )
            {
                case MessageExtractorState.OutOfMessage:
                    match = this.MessageBuffer.Lookup( MessageBlockPattern.BeginOfMessage );

                    if( match.Success == true )
                    {
                        state = MessageExtractorState.WithinMessage;
                    }
                    break;

                case MessageExtractorState.WithinMessage:
                    match = this.MessageBuffer.Lookup(  MessageBlockPattern.EndOfMessage,
                                                        MessageBlockPattern.BeginOfData,                        
                                                        MessageBlockPattern.BeginOfComment  );
                            
                    if( match.Success == true )
                    {
                        if( match.Pattern == MessageBlockPattern.EndOfMessage )
                        {
                            state = MessageExtractorState.OutOfMessage;    
                        }else if( match.Pattern == MessageBlockPattern.BeginOfComment )
                        {
                            state = MessageExtractorState.WithinComment;    
                        }else if( match.Pattern == MessageBlockPattern.BeginOfData )
                        {
                            state = MessageExtractorState.WithinData;
                        }
                    }
                    break;

                case MessageExtractorState.WithinComment:
                    match = this.MessageBuffer.Lookup( MessageBlockPattern.EndOfComment );

                    if( match.Success == true )
                    {
                        MessageExtractorStateTransition lastTransition = this.Transitions.Last();

                        state = lastTransition.FromState;
                    }
                    break;

                case MessageExtractorState.WithinData:
                    match = this.MessageBuffer.Lookup( MessageBlockPattern.EndOfData );

                    if( match.Success == true )
                    {
                        MessageExtractorStateTransition lastTransition = this.Transitions.Last();

                        state = lastTransition.FromState;
                    }
                    break;
            }

            return ( match, state );
        }
        
        private void HandleTransition( MessageBlockPatternMatch match, MessageExtractorState state )
        {            
            if( match.Success == true )
            {
                MessageExtractorStateTransition lastTransition = new MessageExtractorStateTransition( this.State,
                                                                                                      state,
                                                                                                      this.Match,
                                                                                                      match   );
                
                if( match.Pattern == MessageBlockPattern.EndOfMessage )
                {
                    MessageExtractorStateTransition beginOfMessageTransition = null;

                    foreach( MessageExtractorStateTransition transition in this.Transitions )
                    {
                        if( transition.ToState == MessageExtractorState.WithinMessage )
                        {
                            beginOfMessageTransition = transition;
                            break;
                        }
                    }

                    this.SkipJunk( beginOfMessageTransition );
                    this.PostMessage( beginOfMessageTransition, lastTransition );
                                      
                    this.MessageBuffer.Shift( lastTransition.ToMatch );
                                        
                    this.Transitions.Clear();

                    this.State = MessageExtractorState.OutOfMessage;
                    this.Match = MessageBlockPatternMatch.None;
                }else
                {
                    this.Transitions.Add( lastTransition );

                    this.State = state;
                    this.Match = match;
                }
            }
        }

        private void SkipJunk( MessageExtractorStateTransition beginOfMessageTransition )
        {
            if( beginOfMessageTransition.ToMatch.StartIndex != 0 )
            {
                String junk = this.MessageBuffer.GetString( beginOfMessageTransition );

                if( String.IsNullOrWhiteSpace( junk ) == false )
                {
                    ExecutionLogProvider.LogWarning( $"Junk detected: { junk }" );

                    UnhandledMessageHandler.Invoke( new UnhandledMessage(   UnhandledReason.InvalidFormat,
                                                                            MessageDirection.Incoming,
                                                                            new RawMessage( junk )  ) );
                }
            }
        }

        private void PostMessage( MessageExtractorStateTransition beginOfMessageTransition, MessageExtractorStateTransition lastTransition )
        {
            String messageString = this.MessageBuffer.GetString( beginOfMessageTransition, lastTransition );

            try
            {
                this.InteractionLog?.LogMessage( new XmlInteractionLogMessage( MessageDirection.Incoming,
                                                                               messageString ) );
            }catch
            {
            }

            IMessage message = null;
            
            try
            {
                message = this.MessageParser.Parse( messageString );
            }catch( MessageTypeException ex )
            {
                ExecutionLogProvider.LogError( ex );

                UnhandledMessageHandler.Invoke( new UnhandledMessage(   UnhandledReason.Unsupported,
                                                                        MessageDirection.Incoming,
                                                                        message,
                                                                        ex  ) );
            }catch( MessageSerializationException ex )
            {
                ExecutionLogProvider.LogError( ex );
                ExecutionLogProvider.LogError( $"Deserialization of message failed: '{ message }'" );

                UnhandledMessageHandler.Invoke( new UnhandledMessage(   UnhandledReason.InvalidFormat,
                                                                        MessageDirection.Incoming,
                                                                        message,
                                                                        ex  ) );
            }

            if( this.MessageQueue?.PostMessage( message ) == false )
            {
                UnhandledMessageHandler.Invoke( new UnhandledMessage(   UnhandledReason.Shutdown,
                                                                        MessageDirection.Incoming,
                                                                        message  ) );
            }
        }

        public void Dispose()
        {
            this.Dispose( true );

            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
            if( this.isDisposed == false )
            {
                if( disposing == true )
                {
                    this.MessageBuffer.Dispose();
                }

                this.isDisposed = true;
            }
        }
    }
}
