using System;
using System.Timers;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage.Status;
using Reth.Protocols.Extensions.EventArgsExtensions;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Client
{
    public class StatusListener:IDisposable
    {
        public const int DefaultListenInterval = 15000;

        public event EventHandler<StatusChangedEventArgs> StatusChanged;

        private volatile bool isDisposed;

        public StatusListener(  IStatusClientDialog statusDialog,
                                ISubscriberNode subscriberNode   )
        :
            this( statusDialog, subscriberNode, StatusListener.DefaultListenInterval )
        {
        }

        public StatusListener(  IStatusClientDialog statusDialog,
                                ISubscriberNode subscriberNode,
                                int listenInterval  )
        {
            statusDialog.ThrowIfNull();
            subscriberNode.ThrowIfNull();

            this.StatusDialog = statusDialog;
            this.SubscriberNode = subscriberNode;

            this.Timer.Enabled = false;
            this.Timer.Interval = listenInterval;
            this.Timer.Elapsed += this.Timer_Elapsed;
        }

        ~StatusListener()
        {
            this.Dispose( false );
        }

        public IStatusClientDialog StatusDialog
        {
            get;
        }

        public ISubscriberNode SubscriberNode
        {
            get;
        }

        private StatusResponse LastKnownStatus
        {
            get; set;
        }

        private Timer Timer
        {
            get; set;
        } = new Timer();

        private void Timer_Elapsed( Object sender, ElapsedEventArgs e )
        {
            try
            {
                this.Timer.Enabled = false;

                StatusResponse status = this.StatusDialog.SendRequest(  new StatusRequest(  MessageId.NewId(),
                                                                                            this.SubscriberNode.LocalSubscriber.Id,
                                                                                            this.SubscriberNode.RemoteSubscriber.Id,
                                                                                            true    ) );

                if( StatusResponse.Equals( this.LastKnownStatus, status ) == false )
                {
                    this.LastKnownStatus = status;

                    this.OnStatusChanged( new StatusChangedEventArgs( status ) );
                }
            }catch
            {
            }finally
            {
                this.Timer.Enabled = true;
            }
        }

        protected void OnStatusChanged( StatusChangedEventArgs args )
        {
            this.StatusChanged?.SafeInvoke( this, args );
        }

        public void Start()
        {
            this.Timer.Enabled = true;
        }

        public void Stop()
        {
            this.Timer.Enabled = false;
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
                this.Timer.Elapsed -= this.Timer_Elapsed;

                this.Stop();

                if( disposing == true )
                {
                    this.Timer.Dispose();
                }

                this.isDisposed = true;
            }
        }
    }
}