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

using Reth.Itss2.Dialogs.Standard.Protocol;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.KeepAliveDialog;
using Reth.Itss2.Dialogs.Standard.Protocol.Roles.StorageSystem;

namespace Reth.Itss2.Workflows.Standard.StorageSystem.KeepAliveDialog
{
    internal class KeepAliveWorkflow:Workflow, IKeepAliveWorkflow
    {
        private KeepAliveTrigger? trigger;

        private bool isDisposed;

        public KeepAliveWorkflow( IStorageSystemWorkflowProvider workflowProvider )
        :
            base( workflowProvider )
        {
            this.Dialog.RequestReceived += this.Dialog_RequestReceived;
        }

        private IStorageSystemKeepAliveDialog Dialog
        {
            get{ return this.DialogProvider.KeepAliveDialog; }
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
            SubscriberInfo subscriberInfo = this.GetSubscriberInfo();

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
                    this.Trigger = new KeepAliveTrigger( this );
                }
            }
        }

        public void EnableAutomaticTrigger( TimeSpan interval )
        {
            lock( this.SyncRoot )
            {
                if( this.Trigger is null )
                {
                    this.Trigger = new KeepAliveTrigger( this, interval );
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

        private void Dialog_RequestReceived( Object sender, MessageReceivedEventArgs e )
        {
            KeepAliveRequest request = ( KeepAliveRequest )e.Message;

            this.OnRequestReceived( request,
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
