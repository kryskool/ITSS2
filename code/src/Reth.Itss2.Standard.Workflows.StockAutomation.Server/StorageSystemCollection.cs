using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

using Reth.Protocols.Diagnostics;
using Reth.Protocols.Extensions.EventArgsExtensions;
using Reth.Protocols.Extensions.Int32Extensions;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Server
{
    public class StorageSystemCollection
    :
        IEnumerable<IStorageSystem>,
        IReadOnlyCollection<IStorageSystem>
    {
        public event EventHandler<StorageSystemEventArgs> Added;

        internal StorageSystemCollection( int maxCount, Object syncRoot )
        {
            maxCount.ThrowIfNotPositive();
            syncRoot.ThrowIfNull();

            this.MaxCount = maxCount;
            this.SyncRoot = syncRoot;

            this.Semaphore = new SemaphoreSlim( maxCount );

            this.Items = new List<IStorageSystem>( maxCount );
        }

        private SemaphoreSlim Semaphore
        {
            get;
        }

        private List<IStorageSystem> Items
        {
            get;
        }

        public Object SyncRoot
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

        public bool Add( IStorageSystem item )
        {
            bool result = false;

            if( !( item is null ) )
            {
                lock( this.SyncRoot )
                {
                    if( this.Semaphore.Wait( 0 ) == true )
                    {
                        this.Items.Add( item );

                        this.Added?.SafeInvoke( this, new StorageSystemEventArgs( item ) );

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
                IStorageSystem[] items = new IStorageSystem[ this.Items.Count ];
                
                this.Items.CopyTo( items );

                foreach( IStorageSystem item in items )
                {
                    this.Remove( item );
                }
            }
        }

        public bool Remove( IStorageSystem item )
        {
            bool result = false;

            if( !( item is null ) )
            {
                lock( this.SyncRoot )
                {
                    if( this.Items.Remove( item ) == true )
                    {
                        this.Semaphore.Release();

                        result = true;
                    }
                }
            }

            return result;
        }

        public bool Contains( IStorageSystem item )
        {
            lock( this.SyncRoot )
            {
                return this.Items.Contains( item );
            }            
        }

        public void CopyTo( IStorageSystem[] array )
        {
            lock( this.SyncRoot )
            {
                this.Items.CopyTo( array, 0 );
            }
        }

        public IEnumerator<IStorageSystem> GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

        public override String ToString()
        {
            return this.Items.Count.ToString( CultureInfo.InvariantCulture );
        }
    }
}
