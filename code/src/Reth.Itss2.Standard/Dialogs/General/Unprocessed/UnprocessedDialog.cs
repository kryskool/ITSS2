using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;
using Reth.Protocols.Extensions.EventArgsExtensions;

namespace Reth.Itss2.Standard.Dialogs.General.Unprocessed
{
    internal class UnprocessedDialog:Dialog, IUnprocessedDialog
    {
        private volatile bool isDisposed;

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        internal UnprocessedDialog( IMessageTransceiver messageTransceiver )
        :
            base( DialogName.Unprocessed, messageTransceiver )
        {
            this.MessageInterceptor = new MessageInterceptor( messageTransceiver, typeof( UnprocessedMessage ) );

            this.MessageInterceptor.Intercepted += this.OnMessageReceived;
        }

        ~UnprocessedDialog()
        {
            this.Dispose( false );
        }

        private MessageInterceptor MessageInterceptor
        {
            get;
        }

        public void SendMessage( UnprocessedMessage message )
        {
            base.PostMessage( message );
        }

        public Task SendMessageAsync( UnprocessedMessage message )
        {
            return this.SendMessageAsync( message, CancellationToken.None );
        }

        public Task SendMessageAsync( UnprocessedMessage message, CancellationToken cancellationToken )
        {
            return Task.Run(    () =>
                                {
                                    this.SendMessage( message );
                                },
                                cancellationToken   );
        }

        private void OnMessageReceived( Object sender, MessageReceivedEventArgs args )
        {
            this.MessageReceived?.SafeInvoke( this, args );
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
                    this.MessageInterceptor.Dispose();
                }

                this.isDisposed = true;
            }
        }
    }
}