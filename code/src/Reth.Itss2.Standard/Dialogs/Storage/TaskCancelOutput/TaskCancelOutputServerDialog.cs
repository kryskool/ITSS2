using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols;
using Reth.Protocols.Dialogs;
using Reth.Protocols.Extensions.EventArgsExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.TaskCancelOutput
{
    internal class TaskCancelOutputServerDialog:Dialog, ITaskCancelOutputServerDialog
    {
        private volatile bool isDisposed;

        public event EventHandler<MessageReceivedEventArgs> RequestReceived;

        internal TaskCancelOutputServerDialog( IMessageTransceiver messageTransceiver )
        :
            base( DialogName.TaskCancelOutput, messageTransceiver )
        {
            this.RequestInterceptor = new MessageInterceptor( messageTransceiver, typeof( TaskCancelOutputRequest ) );

            this.RequestInterceptor.Intercepted += this.OnRequestReceived;
        }

        ~TaskCancelOutputServerDialog()
        {
            this.Dispose( false );
        }

        private MessageInterceptor RequestInterceptor
        {
            get;
        }

        public void SendResponse( TaskCancelOutputResponse response )
        {
            base.PostMessage( response );
        }

        public Task SendResponseAsync( TaskCancelOutputResponse response )
        {
            return this.SendResponseAsync( response, CancellationToken.None );
        }

        public Task SendResponseAsync( TaskCancelOutputResponse response, CancellationToken cancellationToken )
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