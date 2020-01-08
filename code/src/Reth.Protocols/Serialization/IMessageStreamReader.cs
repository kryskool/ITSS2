using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Reth.Protocols.Serialization
{
    public interface IMessageStreamReader:IDisposable
    {
        Task RunAsync( IMessageWriteQueue messageQueue, Stream stream );
        Task RunAsync( IMessageWriteQueue messageQueue, Stream stream, CancellationToken cancellationToken );
    }
}