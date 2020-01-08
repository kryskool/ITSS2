using System;
using System.Collections.Generic;

namespace Reth.Protocols.Transfer
{
    public interface IMessageServer:IDisposable
    {
        int MaxClientConnections{ get; }

        IEnumerable<IMessageServerListener> GetListeners();

        void Start();
        void Terminate();
    }
}
