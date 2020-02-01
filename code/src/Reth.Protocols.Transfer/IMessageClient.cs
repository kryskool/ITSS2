using System;

namespace Reth.Protocols.Transfer
{
    public interface IMessageClient:IDisposable
    {
        event EventHandler Disconnected;

        IMessageTransceiver MessageTransceiver{ get; }

        String LocalName{ get; }
        String RemoteName{ get; }
    }
}
