using System;

namespace Reth.Protocols.Transfer
{
    public interface IMessageServerListener:IDisposable
    {
        String LocalName{ get; }

        void Start();
        void Terminate();
    }
}
