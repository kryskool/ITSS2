using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.StandardExtensions.Dialogs.Storage.ConfigurationGet
{
    internal class ConfigurationGetClientDialog:Dialog, IConfigurationGetClientDialog
    {
        internal ConfigurationGetClientDialog( IMessageTransceiver messageTransceiver )
        :
            base( DialogName.ConfigurationGet, messageTransceiver )
        {
        }

        ~ConfigurationGetClientDialog()
        {
            this.Dispose( false );
        }

        public ConfigurationGetResponse SendRequest( ConfigurationGetRequest request )
        {
            return base.SendRequest<ConfigurationGetRequest, ConfigurationGetResponse>( request );
        }

        public Task<ConfigurationGetResponse> SendRequestAsync( ConfigurationGetRequest request )
        {
            return base.SendRequestAsync<ConfigurationGetRequest, ConfigurationGetResponse>( request, CancellationToken.None );
        }

        public Task<ConfigurationGetResponse> SendRequestAsync( ConfigurationGetRequest request, CancellationToken cancellationToken )
        {
            return base.SendRequestAsync<ConfigurationGetRequest, ConfigurationGetResponse>( request, cancellationToken );
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