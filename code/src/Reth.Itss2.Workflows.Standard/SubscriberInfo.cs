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

using Reth.Itss2.Dialogs.Standard.Diagnostics;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages;

namespace Reth.Itss2.Workflows.Standard
{
    public class SubscriberInfo
    {
        public SubscriberInfo( Subscriber localSubscriber )
        :
            this( localSubscriber, remoteSubscriber:null )
        {
        }

        public SubscriberInfo(  Subscriber localSubscriber,
                                Subscriber? remoteSubscriber )
        {
            this.LocalSubscriber = localSubscriber;
            this.RemoteSubscriber = remoteSubscriber;
        }

        public Subscriber LocalSubscriber
        {
            get;
        }

        private Subscriber? RemoteSubscriber
        {
            get;
        }

        public Subscriber GetRemoteSubscriber()
        {
            if( this.RemoteSubscriber is null )
            {
                throw Assert.Exception( new InvalidOperationException( "No remote subscriber information available." ) );
            }

            return this.RemoteSubscriber;
        }

        public bool TryGetRemoteSubscriber( out Subscriber? remoteSubscriber )
        {
            remoteSubscriber = this.RemoteSubscriber;
            
            return ( remoteSubscriber is not null );
        }

        public bool HasRemoteSubscriber
        {
            get{ return ( this.RemoteSubscriber is not null ); }
        }

        public bool IsSupportedLocally( String capability )
        {
            return this.LocalSubscriber.IsSupported( capability );
        }

        public bool IsSupportedRemotely( String capability )
        {
            return this.RemoteSubscriber?.IsSupported( capability ) ?? false;
        }
    }
}
