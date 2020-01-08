using System;

namespace Reth.Protocols.Transfer
{
    public interface IRemoteMessageClient:IMessageClient
    {
        event EventHandler Disconnected;

        void Run();
        void Terminate();
    }
}