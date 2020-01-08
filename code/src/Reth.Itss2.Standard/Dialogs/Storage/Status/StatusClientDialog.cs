using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs.Storage.Status
{
    internal class StatusClientDialog:Dialog, IStatusClientDialog
    {
        internal StatusClientDialog( IMessageTransceiver messageTransceiver )
        :
            base( DialogName.Status, messageTransceiver )
        {
        }

        ~StatusClientDialog()
        {
            this.Dispose( false );
        }

        public StatusResponse SendRequest( StatusRequest request )
        {
            return base.SendRequest<StatusRequest, StatusResponse>( request );
        }

        public Task<StatusResponse> SendRequestAsync( StatusRequest request )
        {
            return base.SendRequestAsync<StatusRequest, StatusResponse>( request, CancellationToken.None );
        }

        public Task<StatusResponse> SendRequestAsync( StatusRequest request, CancellationToken cancellationToken )
        {
            return base.SendRequestAsync<StatusRequest, StatusResponse>( request, cancellationToken );
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