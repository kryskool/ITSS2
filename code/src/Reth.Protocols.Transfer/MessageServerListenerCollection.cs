using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Reth.Protocols.Transfer
{
    internal class MessageServerListenerCollection
    :
        IEnumerable<IMessageServerListener>,
        ICollection<IMessageServerListener>
    {
        public MessageServerListenerCollection()
        {
            this.Items = new List<IMessageServerListener>();
        }

        public MessageServerListenerCollection( int capacity )
        {
            this.Items = new List<IMessageServerListener>( capacity );
        }

        public MessageServerListenerCollection( IEnumerable<IMessageServerListener> collection )
        {
            this.Items = new List<IMessageServerListener>( collection );
        }

        private List<IMessageServerListener> Items
        {
            get;
        }

        public int Count
        {
            get{ return this.Items.Count; }
        }

        public bool IsReadOnly
        {
            get{ return false; }
        }

        public void Add( IMessageServerListener item )
        {
            this.Items.Add( item );
        }

        public void Clear()
        {
            this.Items.Clear();
        }

        public bool Contains( IMessageServerListener item )
        {
            return this.Items.Contains( item );
        }

        public void CopyTo( IMessageServerListener[] array, int arrayIndex )
        {
            this.Items.CopyTo( array, arrayIndex );
        }

        public bool Remove( IMessageServerListener item )
        {
            return this.Items.Remove( item );
        }

        public IEnumerator<IMessageServerListener> GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

        public override String ToString()
        {
            return this.Count.ToString( CultureInfo.InvariantCulture );
        }
    }
}
