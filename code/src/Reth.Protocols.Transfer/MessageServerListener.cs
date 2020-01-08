using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Protocols.Diagnostics;

namespace Reth.Protocols.Transfer
{
    public abstract class MessageServerListener:IMessageServerListener
    {
        private volatile bool isDisposed;
        private bool isStarted;
        
        protected MessageServerListener()
        {
        }

        ~MessageServerListener()
        {
            this.Dispose( false );
        }

        private Object SyncRoot
        {
            get;
        } = new Object();

        private bool IsStarted
        {
            get
            {
                lock( this.SyncRoot )
                {
                    return this.isStarted;
                }
            }

            set
            {
                lock( this.SyncRoot )
                {
                    this.isStarted = value;
                }
            }
        }

        private Task AcceptTask
        {
            get;
            set;
        }

        public abstract String LocalName{ get; }

        public void Start()
        {
            if( this.IsStarted == false )
            {
                ExecutionLogProvider.LogInformation( $"Starting message server listener: { this.LocalName }" );

                try
                {
                    this.OnStart();

                    this.AcceptTask = Task.Factory.StartNew(    () =>
                                                                {
                                                                    while( this.IsStarted == true )
                                                                    {
                                                                        try
                                                                        {                                                                       
                                                                            ExecutionLogProvider.LogInformation( $"Accepting message server listener: { this.LocalName }." );

                                                                            this.OnAccept();
                                                                        }catch( Exception ex )
                                                                        {
                                                                            ExecutionLogProvider.LogError( ex );
                                                                        }
                                                                    }

                                                                    ExecutionLogProvider.LogInformation( $"Accept task has finished: { this.LocalName }." );
                                                                },
                                                                CancellationToken.None,
                                                                TaskCreationOptions.LongRunning,
                                                                TaskScheduler.Current    );

                    this.IsStarted = true;
                }catch( Exception ex )
                {
                    String message = $"Failed to start message server listener: { this.LocalName }";

                    ExecutionLogProvider.LogError( ex );
                    ExecutionLogProvider.LogError( message );

                    throw new TransferException( message, ex );
                }
            }
        }

        public void Terminate()
        {
            if( this.IsStarted == true )
            {
                ExecutionLogProvider.LogInformation( $"Stopping message server listener: { this.LocalName }" );

                this.IsStarted = false;

                try
                {
                    this.OnTerminate();
                    
                    if( !( this.AcceptTask is null ) )
                    {
                        ExecutionLogProvider.LogInformation( "Waiting for accept task to terminate..." );

                        if( this.AcceptTask.Wait( Timeouts.Termination ) == false )
                        {
                            ExecutionLogProvider.LogWarning( "Waiting for accept task termination timed out." );
                        }

                        this.AcceptTask = null;
                    }
                }catch( Exception ex )
                {
                    String message = $"Failed to stop message server listener: { this.LocalName }";

                    ExecutionLogProvider.LogError( ex );
                    ExecutionLogProvider.LogError( message );

                    throw new TransferException( message, ex );
                }
            }
        }

        protected abstract void OnStart();
        protected abstract void OnTerminate();
        protected abstract void OnAccept();

        public void Dispose()
        {
            this.Dispose( true );

            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
            ExecutionLogProvider.LogInformation( "Disposing." );

            if( this.isDisposed == false )
            {
                this.Terminate();

                this.isDisposed = true;
            }
        }
    }
}
