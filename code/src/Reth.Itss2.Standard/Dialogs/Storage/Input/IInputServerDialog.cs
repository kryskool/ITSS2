using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs.Storage.Input
{
    public interface IInputServerDialog:IDialog, IDisposable
    {
        InputResponse SendRequest( InputRequest request );
        
        Task<InputResponse> SendRequestAsync( InputRequest request );
        Task<InputResponse> SendRequestAsync( InputRequest request, CancellationToken cancellationToken );
        
        void SendMessage( InputMessage message );
        
        Task SendMessageAsync( InputMessage message );
        Task SendMessageAsync( InputMessage message, CancellationToken cancellationToken );
    }
}