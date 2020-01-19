using System;

using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.General.Unprocessed
{
    public class UnprocessedMessage:TraceableMessage, IEquatable<UnprocessedMessage>
    {
        public static bool operator==( UnprocessedMessage left, UnprocessedMessage right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( UnprocessedMessage left, UnprocessedMessage right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( UnprocessedMessage left, UnprocessedMessage right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = TraceableRequest.Equals( left, right );

                                                        result &= left.Message.Equals( right.Message );
                                                        result &= Nullable.Equals( left.Reason, right.Reason );
                                                        result &= String.Equals( left.Text, right.Text, StringComparison.OrdinalIgnoreCase );

                                                        return result;
                                                    }   );
		}

        public static UnprocessedMessage Convert(   UnhandledMessage unhandledMessage,
                                                    SubscriberId localSubscriberId,
                                                    SubscriberId remoteSubscriberId )
        {
            IMessage innerMessage = unhandledMessage.InnerMessage;

            Nullable<UnprocessedReason> unprocessedReason = null;

            UnhandledReason unhandledReason = unhandledMessage.Reason;

            switch( unhandledReason )
            {
                case UnhandledReason.InvalidFormat:
                    unprocessedReason = UnprocessedReason.SyntaxError;
                    break;

                default:
                    unprocessedReason = UnprocessedReason.NotSupported;
                    break;
            }

            return new UnprocessedMessage(  MessageId.NewId(),
                                            localSubscriberId,
                                            remoteSubscriberId,
                                            new UnprocessedContent( innerMessage ),
                                            unprocessedReason,
                                            null    );
        }

        public UnprocessedMessage(  IMessageId id,
                                    SubscriberId source,
                                    SubscriberId destination,
                                    UnprocessedContent message  )
        :
            this(   id,
                    source,
                    destination,
                    message,
                    null,
                    null    )
        {
        }

        public UnprocessedMessage(  IMessageId id,
                                    SubscriberId source,
                                    SubscriberId destination,
                                    UnprocessedContent message,
                                    Nullable<UnprocessedReason> reason,
                                    String text    )
        :
            base( DialogName.Unprocessed, id, source, destination )
        {
            message.ThrowIfNull();

            this.Message = message;
            this.Reason = reason;
            this.Text = text;
        }

        public UnprocessedContent Message
        {
            get;
        }

        public Nullable<UnprocessedReason> Reason
        {
            get;
        }

        public String Text
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as UnprocessedMessage );
		}
		
		public bool Equals( UnprocessedMessage other )
		{
            return UnprocessedMessage.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override void Dispatch( IMessageDispatcher dispatcher )
        {
            dispatcher.ThrowIfNull();
            dispatcher.Dispatch( this );
        }
    }
}