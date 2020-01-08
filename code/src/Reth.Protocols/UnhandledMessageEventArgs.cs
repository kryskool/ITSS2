using System;

using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Protocols
{
    public class UnhandledMessageEventArgs:EventArgs
    {
        public UnhandledMessageEventArgs( UnhandledMessage message )
        {
            message.ThrowIfNull();

            this.Message = message;
        }

        public UnhandledMessage Message
        {
            get;
        }

        public override String ToString()
        {
            return this.Message.ToString();
        }
    }
}
