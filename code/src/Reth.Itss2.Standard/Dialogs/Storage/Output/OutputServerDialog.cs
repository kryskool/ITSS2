using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;
using Reth.Protocols.Extensions.EventArgsExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.Output
{
    internal class OutputServerDialog:Dialog, IOutputServerDialog
    {
        private volatile bool isDisposed;

        public event EventHandler<MessageReceivedEventArgs> RequestReceived;

        internal OutputServerDialog( IMessageTransceiver messageTransceiver )
        :
            base( DialogName.Output, messageTransceiver )
        {
            this.RequestInterceptor = new MessageInterceptor( messageTransceiver, typeof( OutputRequest ) );

            this.RequestInterceptor.Intercepted += this.OnRequestReceived;
        }

        ~OutputServerDialog()
        {
            this.Dispose( false );
        }

        private MessageInterceptor RequestInterceptor
        {
            get;
        }

        public void SendResponse( OutputResponse response )
        {
           base.PostMessage( response );
        }

        public Task SendResponseAsync( OutputResponse response )
        {
            return this.SendResponseAsync( response, CancellationToken.None );
        }

        public Task SendResponseAsync( OutputResponse response, CancellationToken cancellationToken )
        {
            return Task.Run(    () =>
                                {
                                    this.SendResponse( response );
                                },
                                cancellationToken   );
        }

        public void SendMessage( OutputMessage message )
        {
            base.PostMessage( message );
        }

        public Task SendMessageAsync( OutputMessage message )
        {
            return this.SendMessageAsync( message, CancellationToken.None );
        }

        public Task SendMessageAsync( OutputMessage message, CancellationToken cancellationToken )
        {
            return Task.Run(    () =>
                                {
                                    this.SendMessage( message );
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