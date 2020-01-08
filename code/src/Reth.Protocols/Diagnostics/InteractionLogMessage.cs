using System;

using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Protocols.Diagnostics
{
    public abstract class InteractionLogMessage
    {
        protected InteractionLogMessage( MessageDirection direction, String message )
        {
            message.ThrowIfNull();

            this.Timestamp = DateTimeOffset.UtcNow;
            this.Direction = direction;
            this.Message = message;
        }

        public DateTimeOffset Timestamp
        {
            get;
        }

        public MessageDirection Direction
        {
            get;
        }

        public abstract String MessageName
        {
            get;
        }

        public String Message
        {
            get;
        }

        public override String ToString()
        {
            return this.Message;
        }
    }
}