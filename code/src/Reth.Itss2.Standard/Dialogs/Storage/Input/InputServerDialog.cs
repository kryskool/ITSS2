using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;
using Reth.Protocols.Extensions.EventArgsExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.Input
{
    internal class InputServerDialog:Dialog, IInputServerDialog
    {
        internal InputServerDialog( IMessageTransceiver messageTransceiver )
        :
            base( DialogName.Input, messageTransceiver )
        {
        }

        ~InputServerDialog()
        {
            this.Dispose( false );
        }

        public InputResponse SendRequest( InputRequest request )
        {
            return base.SendRequest<InputRequest, InputResponse>( request );
        }

        public Task<InputResponse> SendRequestAsync( InputRequest request )
        {
            return base.SendRequestAsync<InputRequest, InputResponse>( request, CancellationToken.None );
        }

        public Task<InputResponse> SendRequestAsync( InputRequest request, CancellationToken cancellationToken )
        {
            return base.SendRequestAsync<InputRequest, InputResponse>( request, cancellationToken );
        }

        public void SendMessage( InputMessage message )
        {
            base.PostMessage( message );
        }

        public Task SendMessageAsync( InputMessage message )
        {
            return this.SendMessageAsync( message, CancellationToken.None );
        }

        public Task SendMessageAsync( InputMessage message, CancellationToken cancellationToken )
        {
            return Task.Run(    () =>
                                {
                                    this.SendMessage( message );
                                },
                                cancellationToken   );
        }

        public void Dispose()
        {
            this.Dispose( true );

            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
        }
    }
}