using System;
using System.Collections.Generic;

namespace Reth.Protocols.Transfer
{
    public interface IMessageServer:IDisposable
    {
        IEnumerable<IMessageServerListener> GetListeners();

        void Start();
        void Terminate();
    }
}
