using System;
using System.Threading;

using Reth.Protocols.Diagnostics;
using Reth.Protocols.Extensions.EventArgsExtensions;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Protocols
{
    public class MessageInterceptor:IDisposable
    {
        private volatile bool isDisposed;

        public event EventHandler<MessageReceivedEventArgs> Intercepted;

        public MessageInterceptor( IMessageTransceiver messageTransceiver )
        :
            this( messageTransceiver, null, null )
        {
        }

        public MessageInterceptor( IMessageTransceiver messageTransceiver, Type messageType )
        :
            this( messageTransceiver, messageType, null )
        {
        }

        public MessageInterceptor( IMessageTransceiver messageTransceiver, Type messageType, IMessageId messageId )
        {
            messageTransceiver.ThrowIfNull();

            this.MessageTransceiver = messageTransceiver;
            this.MessageType = messageType;
            this.MessageId = messageId;

            this.InterceptionCallback = this.CreateInterceptionCallback( this.MessageInterceptedEvent );

            this.MessageTransceiver.MessageReceived += this.InterceptionCallback;
        }

        ~MessageInterceptor()
        {
            this.Dispose( false );
        }

        private ManualResetEventSlim MessageInterceptedEvent
        {
            get;
        } = new ManualResetEventSlim( false );

        private EventHandler<MessageReceivedEventArgs> InterceptionCallback
        {
            get; set;
        }

        public IMessageTransceiver MessageTransceiver
        {
            get;
        }

        public Type MessageType
        {
            get;
        }

        public IMessageId MessageId
        {
            get;
        }

        private EventHandler<MessageReceivedEventArgs> CreateInterceptionCallback( ManualResetEventSlim messageInterceptedEvent )
        {
            return new EventHandler<MessageReceivedEventArgs>(  ( Object sender, MessageReceivedEventArgs e ) =>
                                                                {
                                                                    IMessage message = e.Message;

                                                                    bool intercept = false;

                                                                    if( this.MessageType is null && this.MessageId is null )
                                                                    {
                                                                        intercept = true;
                                                                    }else
                                                                    {
                                                                        if( !( this.MessageType is null ) )
                                                                        {
                                                                            if( this.MessageType.Equals( message.GetType() ) == true )
                                                                            {
                                                                                intercept = true;
                                                                            }
                                                                        }

                                                                        if( !( this.MessageId is null ) )
                                                                        {
                                                                            if( this.MessageId.Equals( message.Id ) == true )
                                                                            {
                                                                                intercept &= true;
                                                                            }else
                                                                            {
                                                                                intercept = false;
                                                                            }
                                                                        }
                                                                    }

                                                                    if( intercept == true )
                                                                    {
                                                                        try
                                                                        {
                                                                            this.Intercepted?.SafeInvoke( this, e );

                                                                            messageInterceptedEvent.Set();
                                                                        }catch( Exception ex )
                                                                        {
                                                                            ExecutionLogProvider.LogError( ex );
                                                                            ExecutionLogProvider.LogError( "Error within interception callback." );
                                                                        }
                                                                    }
                                                                }   );
        }

        public bool Wait( TimeSpan timeout )
        {
            return this.Wait( timeout, CancellationToken.None );
        }

        public bool Wait( TimeSpan timeout, CancellationToken cancellationToken )
        {
            bool result = false;

            ExecutionLogProvider.LogError( "Waiting for message..." );

            int millisecondsTimeout = int.MaxValue;
            double totalMilliseconds = timeout.TotalMilliseconds;

            if( totalMilliseconds < int.MaxValue )
            {
                millisecondsTimeout = ( int )totalMilliseconds;
            }

            if( this.MessageInterceptedEvent.Wait( millisecondsTimeout, cancellationToken ) == true )
            {
                this.MessageInterceptedEvent.Reset();

                result = true;
            }else
            {
                ExecutionLogProvider.LogError( "Intercepting message timed out." );
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
            if( this.isDisposed == false )
            {
                this.MessageTransceiver.MessageReceived -= this.InterceptionCallback;

                if( disposing == true )
                {
                    this.MessageInterceptedEvent.Dispose();
                }

                this.isDisposed = true;
            }
        }
    }
}
