using System;

namespace Reth.Protocols.Transfer
{
    public interface IMessageClient:IDisposable
    {
        IMessageTransceiver MessageTransceiver{ get; }

        String LocalName{ get; }
        String RemoteName{ get; }
    }
}
