using System;
using System.Collections.Generic;

using Reth.Protocols.Diagnostics;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Protocols.Transfer
{
    public abstract class MessageServer:IMessageServer
    {
        private bool isStarted;
        private volatile bool isDisposed;
        
        protected MessageServer()
        {
        }

        ~MessageServer()
        {
            this.Dispose( false );
        }

        protected Object SyncRoot
        {
            get;
        } = new Object();

        private List<IMessageServerListener> Listeners
        {
            get; set;
        }

        protected bool IsStarted
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

        private void ForEach( Action<IMessageServerListener> action, Func<IMessageServerListener, String> errorMessageCallback )
        {
            action.ThrowIfNull();
            errorMessageCallback.ThrowIfNull();

            List<Exception> exceptions = new List<Exception>();

            foreach( IMessageServerListener listener in this.Listeners )
            {
                try
                {
                    action( listener );
                }catch( Exception ex )
                {
                    ExecutionLogProvider.LogError( ex );
                    ExecutionLogProvider.LogError( errorMessageCallback( listener ) );

                    exceptions.Add( ex );
                }
            }

            if( exceptions.Count > 0 )
            {
                throw new AggregateException( exceptions );
            }
        }

        protected abstract IEnumerable<IMessageServerListener> CreateListeners();

        public IEnumerable<IMessageServerListener> GetListeners()
        {
            lock( this.SyncRoot )
            {
                IMessageServerListener[] result = new IMessageServerListener[ this.Listeners.Count ];

                this.Listeners.CopyTo( result, 0 );

                return result;
            }
        }

        public virtual void Start()
        {
            lock( this.SyncRoot )
            {
                if( this.IsStarted == false )
                {
                    this.Listeners = new List<IMessageServerListener>( this.CreateListeners() );

                    this.ForEach(   ( IMessageServerListener listener ) =>
                                    {
                                        listener.Start();
                                    },
                                    ( IMessageServerListener listener ) =>
                                    {
                                        return $"Failed to start listener: { listener.LocalName }";
                                    }   );

                    this.IsStarted = true;   
                }
            }
        }

        public virtual void Terminate()
        {
            lock( this.SyncRoot )
            {
                if( this.IsStarted == true )
                {
                    List<IMessageServerListener> terminated = new List<IMessageServerListener>();

                    try
                    {
                        this.ForEach(   ( IMessageServerListener listener ) =>
                                        {
                                            listener.Terminate();

                                            terminated.Add( listener );
                                        },
                                        ( IMessageServerListener listener ) =>
                                        {
                                            return $"Failed to terminate listener: { listener.LocalName }";
                                        }   );
                    }finally
                    {
                        foreach( IMessageServerListener listener in terminated )
                        {
                            this.Listeners.Remove( listener );
                        }
                    }

                    this.IsStarted = false;
                }
            }
        }

        public void Dispose()
        {
            this.Dispose( true );

            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
            ExecutionLogProvider.LogInformation( "Disposing" );

            if( this.isDisposed == false )
            {
                if( disposing == true )
                {
                    lock( this.SyncRoot )
                    {
                        foreach( IMessageServerListener listener in this.Listeners )
                        {
                            listener.Dispose();
                        }
                
                        this.Listeners.Clear();
                    }
                }

                this.isDisposed = true;
            }
        }
    }
}
