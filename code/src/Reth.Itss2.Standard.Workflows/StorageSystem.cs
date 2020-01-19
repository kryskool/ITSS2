using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.General.Hello;
using Reth.Itss2.Standard.Dialogs.General.Unprocessed;
using Reth.Protocols;
using Reth.Protocols.Diagnostics;
using Reth.Protocols.Dialogs;
using Reth.Protocols.Extensions.EventArgsExtensions;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Workflows
{
    public abstract class StorageSystem
    {
        public event EventHandler<UnhandledMessageEventArgs> MessageUnhandled;

        public event EventHandler<MessageReceivedEventArgs> UnprocessedMessageReceived;

        protected StorageSystem( IDialogProvider dialogProvider )
        {
            dialogProvider.ThrowIfNull();

            dialogProvider.Unprocessed.MessageReceived += this.Unprocessed_MessageReceived;
        }

        public Subscriber LocalSubscriber
        {
            get; protected set;
        }

        public Subscriber RemoteSubscriber
        {
            get; protected set;
        }

        private void Unprocessed_MessageReceived( Object sender, MessageReceivedEventArgs e )
        {
            try
            {
                this.UnprocessedMessageReceived?.SafeInvoke( this, e );
            }catch( Exception ex )
            {
                ExecutionLogProvider.LogError( ex );
            }
        }

        protected HashSet<IDialogName> RemoteCapabilities
        {
            get;
        } = new HashSet<IDialogName>();

        protected void ReportUnhandledMessage( TraceableMessage message, UnhandledReason reason )
        {
            try
            {
                UnhandledMessageEventArgs args = new UnhandledMessageEventArgs( new UnhandledMessage(   reason,
                                                                                                        MessageDirection.Outgoing,
                                                                                                        message,
                                                                                                        null   )   );

                this.MessageUnhandled?.SafeInvoke( this, args );
            }catch( Exception ex )
            {
                ExecutionLogProvider.LogError( ex );
                ExecutionLogProvider.LogError( "Message unhandled callback failed." );
            }
        }

        protected SubscriberId GetRemoteSubscriberId()
        {
            SubscriberId result = SubscriberId.Invalid;

            if( !( this.RemoteSubscriber is null ) )
            {
                result = this.RemoteSubscriber.Id;
            }

            return result;
        }

        protected bool VerifyCapability( DialogName dialogName, TraceableMessage message )
        {
            bool result = true;

            if( !( this.RemoteSubscriber is null ) )
            {
                if( this.RemoteCapabilities.Contains( dialogName ) == false )
                {
                    this.ReportUnhandledMessage( message, UnhandledReason.Unsupported );
                }
            }else
            {
                this.ReportUnhandledMessage( message, UnhandledReason.ConnectionError );
            }

            return result;
        }

        protected virtual void OnMessageUnhandled( UnhandledMessage message )
        {
            if( !( message is null ) )
            {
                ExecutionLogProvider.LogWarning( $"Unhandled message: { message.ToString() }" );

                SubscriberId localSubscriberId = this.LocalSubscriber.Id;
                SubscriberId remoteSubscriberId = this.GetRemoteSubscriberId();

                if( message.Direction == MessageDirection.Incoming )
                {
                    IMessage innerMessage = message.InnerMessage;

                    if( innerMessage.GetType().IsInstanceOfType( typeof( UnprocessedMessage ) ) == false )
                    {
                        try
                        {
                            UnprocessedMessage unprocessedMessage = UnprocessedMessage.Convert( message, localSubscriberId, remoteSubscriberId );

                            this.SendUnprocessedMessageAsync( unprocessedMessage );
                        }catch( Exception ex )
                        {
                            ExecutionLogProvider.LogError( ex );
                            ExecutionLogProvider.LogError( "Failed to handle unhandled message." );
                        }
                    }
                }

                this.MessageUnhandled?.SafeInvoke( this, new UnhandledMessageEventArgs( message ) );
            }
        }

        protected abstract void SendUnprocessedMessage( UnprocessedMessage message );
        protected abstract Task SendUnprocessedMessageAsync( UnprocessedMessage message );
        protected abstract Task SendUnprocessedMessageAsync( UnprocessedMessage message, CancellationToken cancellationToken );
    }
}
