using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs.Storage.OutputInfo
{
    internal class OutputInfoClientDialog:Dialog, IOutputInfoClientDialog
    {
        internal OutputInfoClientDialog( IMessageTransceiver messageTransceiver )
        :
            base( DialogName.OutputInfo, messageTransceiver )
        {
        }

        ~OutputInfoClientDialog()
        {
            this.Dispose( false );
        }

        public OutputInfoResponse SendRequest( OutputInfoRequest request )
        {
            return base.SendRequest<OutputInfoRequest, OutputInfoResponse>( request );
        }

        public Task<OutputInfoResponse> SendRequestAsync( OutputInfoRequest request )
        {
            return base.SendRequestAsync<OutputInfoRequest, OutputInfoResponse>( request, CancellationToken.None );
        }

        public Task<OutputInfoResponse> SendRequestAsync( OutputInfoRequest request, CancellationToken cancellationToken )
        {
            return base.SendRequestAsync<OutputInfoRequest, OutputInfoResponse>( request, cancellationToken );
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