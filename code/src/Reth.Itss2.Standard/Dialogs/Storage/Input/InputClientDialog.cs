using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;
using Reth.Protocols.Extensions.EventArgsExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.Input
{
    internal class InputClientDialog:Dialog, IInputClientDialog
    {
        private volatile bool isDisposed;

        public event EventHandler<MessageReceivedEventArgs> RequestReceived;
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        internal InputClientDialog( IMessageTransceiver messageTransceiver )
        :
            base( DialogName.Input, messageTransceiver )
        {
            this.RequestInterceptor = new MessageInterceptor( messageTransceiver, typeof( InputRequest ) );
            this.MessageInterceptor = new MessageInterceptor( messageTransceiver, typeof( InputMessage ) );

            this.RequestInterceptor.Intercepted += this.OnRequestReceived;
            this.MessageInterceptor.Intercepted += this.OnMessageReceived;
        }

        ~InputClientDialog()
        {
            this.Dispose( false );
        }

        private MessageInterceptor RequestInterceptor
        {
            get;
        }

        private MessageInterceptor MessageInterceptor
        {
            get;
        }

        public void SendResponse( InputResponse response )
        {
           base.PostMessage( response );
        }

        public Task SendResponseAsync( InputResponse response )
        {
            return this.SendResponseAsync( response, CancellationToken.None );
        }

        public Task SendResponseAsync( InputResponse response, CancellationToken cancellationToken )
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
                this.RequestInterceptor.Intercepted -= this.OnRequestReceived;
                this.MessageInterceptor.Intercepted -= this.OnMessageReceived;

                if( disposing == true )
                {
                    this.RequestInterceptor.Dispose();
                    this.MessageInterceptor.Dispose();
                }

                this.isDisposed = true;
            }
        }
    }
}