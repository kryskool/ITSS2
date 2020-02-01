using System;
using System.Timers;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.General.KeepAlive;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Workflows
{
    public class KeepAliveTrigger:IDisposable
    {
        public const int DefaultTriggerInterval = 2000;

        private volatile bool isDisposed;

        public KeepAliveTrigger(    IKeepAliveDialog keepAliveDialog,
                                    ISubscriberNode subscriberNode   )
        :
            this( keepAliveDialog, subscriberNode, KeepAliveTrigger.DefaultTriggerInterval )
        {
        }

        public KeepAliveTrigger(    IKeepAliveDialog keepAliveDialog,
                                    ISubscriberNode subscriberNode,
                                    int triggerInterval  )
        {
            keepAliveDialog.ThrowIfNull();
            subscriberNode.ThrowIfNull();

            this.KeepAliveDialog = keepAliveDialog;
            this.SubscriberNode = subscriberNode;

            this.Timer.Enabled = false;
            this.Timer.Interval = triggerInterval;
            this.Timer.Elapsed += this.Timer_Elapsed;
        }

        ~KeepAliveTrigger()
        {
            this.Dispose( false );
        }

        public IKeepAliveDialog KeepAliveDialog
        {
            get;
        }

        public ISubscriberNode SubscriberNode
        {
            get;
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

                ISubscriberNode subscriberNode = this.SubscriberNode;

                this.KeepAliveDialog.SendRequest( new KeepAliveRequest( MessageId.NewId(),
                                                                        subscriberNode.LocalSubscriber.Id,
                                                                        subscriberNode.RemoteSubscriber.Id   ) );
            }catch
            {
            }finally
            {
                this.Timer.Enabled = true;
            }
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