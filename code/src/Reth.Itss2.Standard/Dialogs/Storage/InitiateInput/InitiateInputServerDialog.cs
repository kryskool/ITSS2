using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;
using Reth.Protocols.Extensions.EventArgsExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.InitiateInput
{
    internal class InitiateInputServerDialog:Dialog, IInitiateInputServerDialog
    {
        private volatile bool isDisposed;

        public event EventHandler<MessageReceivedEventArgs> RequestReceived;

        internal InitiateInputServerDialog( IMessageTransceiver messageTransceiver )
        :
            base( DialogName.InitiateInput, messageTransceiver )
        {
            this.RequestInterceptor = new MessageInterceptor( messageTransceiver, typeof( InitiateInputRequest ) );

            this.RequestInterceptor.Intercepted += this.OnRequestReceived;
        }

        ~InitiateInputServerDialog()
        {
            this.Dispose( false );
        }

        private MessageInterceptor RequestInterceptor
        {
            get;
        }

        public void SendResponse( InitiateInputResponse response )
        {
           base.PostMessage( response );
        }

        public Task SendResponseAsync( InitiateInputResponse response )
        {
            return this.SendResponseAsync( response, CancellationToken.None );
        }

        public Task SendResponseAsync( InitiateInputResponse response, CancellationToken cancellationToken )
        {
            return Task.Run(    () =>
                                {
                                    this.SendResponse( response );
                                },
                                cancellationToken   );
        }

        public void SendMessage( InitiateInputMessage message )
        {
            base.PostMessage( message );
        }

        public Task SendMessageAsync( InitiateInputMessage message )
        {
            return this.SendMessageAsync( message, CancellationToken.None );
        }

        public Task SendMessageAsync( InitiateInputMessage message, CancellationToken cancellationToken )
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