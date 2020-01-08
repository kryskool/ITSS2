using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

using Reth.Protocols.Diagnostics;
using Reth.Protocols.Extensions.EventArgsExtensions;
using Reth.Protocols.Extensions.Int32Extensions;

namespace Reth.Protocols.Transfer
{
    public class RemoteMessageClientCollection
    :
        IDisposable,
        IEnumerable<IRemoteMessageClient>,
        IReadOnlyCollection<IRemoteMessageClient>
    {
        private volatile bool isDisposed;

        public event EventHandler<RemoteMessageClientEventArgs> Added;
        public event EventHandler<RemoteMessageClientEventArgs> Removed;

        public RemoteMessageClientCollection( int maxCount )
        {
            maxCount.ThrowIfNotPositive();

            this.MaxCount = maxCount;

            this.Semaphore = new SemaphoreSlim( maxCount );

            this.Items = new List<IRemoteMessageClient>( maxCount );
        }

        ~RemoteMessageClientCollection()
        {
            this.Dispose( false );
        }

        private SemaphoreSlim Semaphore
        {
            get;
        }

        private List<IRemoteMessageClient> Items
        {
            get;
        }

        public int MaxCount
        {
            get;
        }

        public bool HasMaximumReached
        {
            get
            {
                lock( this.SyncRoot )
                {                   
                    return ( this.MaxCount - this.Count ) <= 0;
                }
            }
        }

        public int Count
        {
            get
            {
                lock( this.SyncRoot )
                {                    
                    return this.Items.Count;
                }
            }
        }

        public Object SyncRoot
        {
            get;
        } = new Object();

        public bool Add( IRemoteMessageClient item )
        {
            bool result = false;

            if( !( item is null ) )
            {
                lock( this.SyncRoot )
                {
                    if( this.Semaphore.Wait( 0 ) == true )
                    {
                        this.Items.Add( item );

                        this.Added?.SafeInvoke( this, new RemoteMessageClientEventArgs( item ) );

                        result = true;
                    }else
                    {
                        ExecutionLogProvider.LogInformation( "Number of maximum allowed connections has been reached." );
                    }
                }
            }

            return result;
        }

        public void Clear()
        {
            lock( this.SyncRoot )
            {
                IRemoteMessageClient[] items = new IRemoteMessageClient[ this.Items.Count ];

                this.Items.CopyTo( items );

                foreach( IRemoteMessageClient item in items )
                {
                    this.Remove( item );
                }

                this.Items.Clear();
            }
        }

        public bool Remove( IRemoteMessageClient item )
        {
            bool result = false;

            if( !( item is null ) )
            {
                lock( this.SyncRoot )
                {
                    if( this.Items.Remove( item ) == true )
                    {
                        this.Semaphore.Release();

                        this.Removed?.SafeInvoke( this, new RemoteMessageClientEventArgs( item ) );

                        result = true;
                    }
                }
            }

            return result;
        }

        public bool Contains( IRemoteMessageClient item )
        {
            bool result = false;

            lock( this.SyncRoot )
            {
                result = this.Items.Contains( item );
            }

            return result;
        }

        public IEnumerator<IRemoteMessageClient> GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

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
                if( disposing == true )
                {
                    this.Clear();

                    this.Semaphore.Dispose();
                }

                this.isDisposed = true;
            }
        }
    }
}
