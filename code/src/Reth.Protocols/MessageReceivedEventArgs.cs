using System;

using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Protocols
{
    public class MessageReceivedEventArgs:EventArgs
    {
        protected internal MessageReceivedEventArgs( IMessage message )
        {
            message.ThrowIfNull();

            this.Message = message;
        }

        public IMessage Message
        {
            get;
        }

        public bool IsHandled
        {
            get; set;
        }

        public override String ToString()
        {
            return this.Message.ToString();
        }
    }
}
