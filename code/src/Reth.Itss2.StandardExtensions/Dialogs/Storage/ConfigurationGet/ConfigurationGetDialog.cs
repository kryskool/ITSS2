using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;
using Reth.Protocols.Extensions.EventArgsExtensions;

namespace Reth.Itss2.StandardExtensions.Dialogs.Storage.ConfigurationGet
{
    internal class ConfigurationGetDialog:Dialog, IConfigurationGetClientDialog, IConfigurationGetServerDialog
    {
        private volatile bool isDisposed;

        public event EventHandler<MessageReceivedEventArgs> RequestReceived;

        internal ConfigurationGetDialog( IMessageTransceiver messageTransceiver )
        :
            base( DialogName.ConfigurationGet, messageTransceiver )
        {
            this.RequestInterceptor = new MessageInterceptor( messageTransceiver, typeof( ConfigurationGetRequest ) );

            this.RequestInterceptor.Intercepted += this.OnRequestReceived;
        }

        ~ConfigurationGetDialog()
        {
            this.Dispose( false );
        }

        private MessageInterceptor RequestInterceptor
        {
            get;
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

        public void SendResponse( ConfigurationGetResponse response )
        {
            base.PostMessage( response );
        }

        public Task SendResponseAsync( ConfigurationGetResponse response )
        {
            return this.SendResponseAsync( response, CancellationToken.None );
        }

        public Task SendResponseAsync( ConfigurationGetResponse response, CancellationToken cancellationToken )
        {
            return Task.Run(    () =>
                                {
                                    this.SendResponse( response );
                                },
                                cancellationToken   );
        }

        private void OnRequestReceived( Object sender, MessageReceivedEventArgs args )
        {
            this.RequestReceived?.SafeInvoke( this, args );
        }

        public void Dispose()
        {
            this.Dispose( true );

            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
            if( this.isDisposed == false )
            {
                if( disposing == true )
                {
                    this.RequestInterceptor.Dispose();
                }

                this.isDisposed = true;
            }
        }
    }
}