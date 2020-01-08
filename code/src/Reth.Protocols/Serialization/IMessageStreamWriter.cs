using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Reth.Protocols.Serialization
{
    public interface IMessageStreamWriter:IDisposable
    {
        Task RunAsync( IMessageReadQueue messageQueue, Stream stream );
        Task RunAsync( IMessageReadQueue messageQueue, Stream stream, CancellationToken cancellationToken );
    }
}