using System;

using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Protocols.Transfer
{
    public class RemoteMessageClientEventArgs:EventArgs
    {
        public RemoteMessageClientEventArgs( IRemoteMessageClient messageClient )
        {
            messageClient.ThrowIfNull();

            this.MessageClient = messageClient;
        }

        public IRemoteMessageClient MessageClient
        {
            get;
        }

        public override String ToString()
        {
            return this.MessageClient.ToString();
        }
    }
}
