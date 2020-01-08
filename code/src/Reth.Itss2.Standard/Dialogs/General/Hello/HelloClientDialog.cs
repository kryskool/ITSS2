using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs.General.Hello
{
    internal class HelloClientDialog:Dialog, IHelloClientDialog
    {
        internal HelloClientDialog( IMessageTransceiver messageTransceiver )
        :
            base( DialogName.Hello, messageTransceiver )
        {
        }

        ~HelloClientDialog()
        {
            this.Dispose( false );
        }

        public HelloResponse SendRequest( HelloRequest request )
        {
            return base.MessageTransceiver.SendRequest<HelloRequest, HelloResponse>( request );
        }

        public Task<HelloResponse> SendRequestAsync( HelloRequest request )
        {
            return base.SendRequestAsync<HelloRequest, HelloResponse>( request, CancellationToken.None );
        }

        public Task<HelloResponse> SendRequestAsync( HelloRequest request, CancellationToken cancellationToken )
        {
            return base.SendRequestAsync<HelloRequest, HelloResponse>( request, cancellationToken );
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