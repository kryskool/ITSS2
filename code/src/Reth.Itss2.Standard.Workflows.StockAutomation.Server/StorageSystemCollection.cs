using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

using Reth.Protocols.Extensions.EventArgsExtensions;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Server
{
    public class StorageSystemCollection
    :
        IEnumerable<IStorageSystem>,
        IReadOnlyCollection<IStorageSystem>
    {
        public event EventHandler<StorageSystemEventArgs> Added;
        public event EventHandler<StorageSystemEventArgs> Removed;

        public StorageSystemCollection()
        {
            this.Items = new List<IStorageSystem>();
        }

        public Object SyncRoot
        {
            get;
        } = new Object();

        private List<IStorageSystem> Items
        {
            get;
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

        public void Add( IStorageSystem item )
        {
            if( !( item is null ) )
            {
                lock( this.SyncRoot )
                {
                    this.Items.Add( item );

                    this.Added?.SafeInvoke( this, new StorageSystemEventArgs( item ) );
                }
            }
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
                        result = true;

                        this.Removed?.SafeInvoke( this, new StorageSystemEventArgs( item ) );
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
