// Implementation of the WWKS2 protocol.
// Copyright (C) 2020  Thomas Reth

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.

// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Threading;

using Reth.Itss2.Dialogs.Standard.Diagnostics;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages;

namespace Reth.Itss2.Workflows.Standard.Messages.Hello.Reactive
{
    internal class Handshake:IDisposable
    {
        public static readonly TimeSpan Timeout = TimeSpan.FromSeconds( 10 );

        private bool isDisposed;

        public Handshake()
        {
        }

        ~Handshake()
        {
            this.Dispose( false );
        }

        private Object SyncRoot
        {
            get;
        } = new Object();

        private ManualResetEventSlim ExecutedEvent
        {
            get;
        } = new ManualResetEventSlim( initialState:false );

        public bool HasExecuted
        {
            get
            {
                lock( this.SyncRoot )
                {
                    return this.ExecutedEvent.IsSet;
                }
            }
        }

        public void Execute(    Subscriber localSubscriber,
                                Subscriber remoteSubscriber,
                                Action<SubscriberInfo> callback )
        {
            lock( this.SyncRoot )
            {
                if( this.HasExecuted == true )
                {
                    throw Assert.Exception( new InvalidOperationException( "Handshake already executed." ) );
                }

                SubscriberInfo subscriberInfo = new SubscriberInfo( localSubscriber, remoteSubscriber );

                callback?.Invoke( subscriberInfo );

                this.ExecutedEvent.Set();
            }
        }

        public void Wait( CancellationToken cancellationToken = default )
        {
            bool waitResult = this.ExecutedEvent.Wait( ( int )Handshake.Timeout.TotalMilliseconds, cancellationToken );

            if( waitResult == false )
            {
                throw Assert.Exception( new TimeoutException( "Handshake timed out." ) );
            }
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
                    this.ExecutedEvent.Dispose();
                }

                this.isDisposed = true;
            }
        }
    }
}
