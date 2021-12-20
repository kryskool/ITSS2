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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.KeepAlive;
using Reth.Itss2.Messaging;
using Reth.Itss2.Serialization;

namespace Reth.Itss2.Workflows.Standard.Messages.KeepAlive
{
    public class KeepAliveWorkflow:SubscribedWorkflow<IKeepAliveDialog>, IKeepAliveWorkflow
    {
        private KeepAliveTrigger? trigger;

        private bool isDisposed;

        public KeepAliveWorkflow(   IKeepAliveDialog dialog,
                                    ISubscription subscription  )
        :
            base( dialog, subscription )
        {
            dialog.RequestReceived += this.Dialog_RequestReceived;
        }

        private Object SyncRoot
        {
            get;
        } = new Object();

        private KeepAliveTrigger? Trigger
        {
            get
            {
                lock( this.SyncRoot )
                {
                    return this.trigger;
                }
            }

            set
            {
                lock( this.SyncRoot )
                {
                    this.trigger = value;
                }
            }
        }

        public void SendRequest()
        {
            SubscriberInfo subscriberInfo = this.SubscriberInfo;

            if( subscriberInfo.TryGetRemoteSubscriber( out Subscriber? remoteSubscriber ) == true )
            {
                if( remoteSubscriber is not null )
                {
                    this.Dialog.SendRequest( new KeepAliveRequest(  MessageId.NextId(),
                                                                    subscriberInfo.LocalSubscriber.Id,
                                                                    remoteSubscriber.Id ) );
                }
            }
        }

        public void EnableAutomaticTrigger()
        {
            lock( this.SyncRoot )
            {
                if( this.Trigger is null )
                {
                    this.Trigger = new KeepAliveTrigger( () => this.SendRequest() );
                }
            }
        }

        public void EnableAutomaticTrigger( TimeSpan interval )
        {
            lock( this.SyncRoot )
            {
                if( this.Trigger is null )
                {
                    this.Trigger = new KeepAliveTrigger( () => this.SendRequest(), interval );
                }
            }
        }

        public void DisableAutomaticTrigger()
        {
            lock( this.SyncRoot )
            {
                this.Trigger?.Dispose();
                this.Trigger = null;
            }
        }

        private void Dialog_RequestReceived( Object? sender, MessageReceivedEventArgs<KeepAliveRequest> e )
        {
            KeepAliveRequest request = e.Message;

            this.OnMessageReceived( request,
                                    () =>
                                    {
                                        this.Dialog.SendResponse( new KeepAliveResponse( request ) );
                                    }   );
        }

        protected override void Dispose( bool disposing )
        {
            if( this.isDisposed == false )
            {
                this.DisableAutomaticTrigger();

                base.Dispose( disposing );

                this.isDisposed = true;
            }
        }
    }
}
