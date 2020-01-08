using System;

namespace Reth.Protocols.Transfer
{
    public interface ILocalMessageClient:IMessageClient
    {
        event EventHandler Disconnected;

        void Connect();
        void Disconnect();
    }
}
