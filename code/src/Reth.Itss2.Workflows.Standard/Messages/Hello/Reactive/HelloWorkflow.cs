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
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Dialogs.Standard.Diagnostics;
using Reth.Itss2.Dialogs.Standard.Protocol;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Hello;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Hello.Reactive;
using Reth.Itss2.Dialogs.Standard.Serialization;

namespace Reth.Itss2.Workflows.Standard.Messages.Hello.Reactive
{
    public class HelloWorkflow:Workflow<IHelloDialog>, IHelloWorkflow
    {
        private SubscriberInfo? subscriberInfo;

        private bool isDisposed;

        public HelloWorkflow(   IHelloDialog dialog,
                                ISubscription subscription,
                                IDialogProvider dialogProvider,
                                ISerializationProvider serializationProvider    )
        :
            base( dialog, subscription )
        {
            this.Connector = new Connector( dialogProvider, serializationProvider );
            this.Handshake = new Handshake();
            
            dialog.RequestReceived += this.Dialog_RequestReceived;
        }

        private Object SyncRoot
        {
            get;
        } = new Object();

        private Connector Connector
        {
            get;
        }

        private Handshake Handshake
        {
            get;
        }

        private void Dialog_RequestReceived( Object sender, MessageReceivedEventArgs<HelloRequest> e )
        {
            lock( this.SyncRoot )
            {
                try
                {
                    this.Handshake.Execute( this.Subscription.LocalSubscriber,
                                            e.Message.Subscriber,
                                            ( SubscriberInfo subscriberInfo ) =>
                                            {
                                                this.Dialog.SendResponse( new HelloResponse( e.Message, subscriberInfo.LocalSubscriber ) );

                                                this.subscriberInfo = subscriberInfo;

                                                this.Subscription.Subscribe( subscriberInfo );
                                            }   );
                }catch( InvalidOperationException ex )
                {
                    this.OnMessageProcessingError( new MessageProcessingErrorEventArgs( "Hello request acceptance failed.", ex ) );
                }
            }
        }

        public SubscriberInfo Connect( Stream stream )
        {
            this.Connector.Connect( stream );
                
            this.Handshake.Wait();

            return this.GetSubscriberInfo();
        }

        public async Task<SubscriberInfo> ConnectAsync( Stream stream, CancellationToken cancellationToken = default )
        {
            await this.Connector.ConnectAsync( stream, cancellationToken ).ConfigureAwait( continueOnCapturedContext:false );

            this.Handshake.Wait( cancellationToken );

            return this.GetSubscriberInfo();
        }

        private SubscriberInfo GetSubscriberInfo()
        {
            SubscriberInfo? subscriberInfo = null;

            lock( this.SyncRoot )
            {
                subscriberInfo = this.subscriberInfo;
            }

            if( subscriberInfo is null )
            {
                throw Assert.Exception( new InvalidOperationException( "No handshake executed." ) );
            }

            return subscriberInfo;
        }

        protected override void Dispose( bool disposing )
        {
            if( this.isDisposed == false )
            {
                if( disposing == true )
                {
                    this.Handshake.Dispose();
                }

                base.Dispose( disposing );

                this.isDisposed = true;
            }
        }
    }
}
