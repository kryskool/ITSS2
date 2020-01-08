using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs.General.Hello
{
    public interface IHelloServerDialog:IDialog, IDisposable
    {
        event EventHandler<MessageReceivedEventArgs> RequestReceived;
        
        void SendResponse( HelloResponse response );
        
        Task SendResponseAsync( HelloResponse response );
        Task SendResponseAsync( HelloResponse response, CancellationToken cancellationToken );
    }
}