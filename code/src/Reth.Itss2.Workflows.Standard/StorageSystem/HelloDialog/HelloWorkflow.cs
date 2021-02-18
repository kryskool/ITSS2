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
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.HelloDialog;
using Reth.Itss2.Dialogs.Standard.Protocol.Roles.StorageSystem;
using Reth.Itss2.Dialogs.Standard.Serialization;

namespace Reth.Itss2.Workflows.Standard.StorageSystem.HelloDialog
{
    internal class HelloWorkflow:Workflow<IStorageSystemHelloDialog>, IHelloWorkflow
    {
        public event EventHandler<MessageReceivedEventArgs>? RequestAccepted;

        private bool isDisposed;

        public HelloWorkflow(   IStorageSystemWorkflowProvider workflowProvider,
                                IStorageSystemDialogProvider dialogProvider,
                                ISerializationProvider serializationProvider    )
        :
            base( workflowProvider, dialogProvider, serializationProvider, dialogProvider.HelloDialog )
        {
            this.Dialog.RequestReceived += this.Dialog_RequestReceived;
        }

        private Object SyncRoot
        {
            get;
        } = new Object();

        private ManualResetEventSlim HelloRequestAcceptEvent
        {
            get;
        } = new ManualResetEventSlim( initialState:false );

        private void Dialog_RequestReceived( Object sender, MessageReceivedEventArgs e )
        {
            HelloRequest request = ( HelloRequest )e.Message;

            lock( this.SyncRoot )
            {
                if( this.RemoteSubscriber is null )
                {
                    this.RequestAccepted?.Invoke( this, e );

                    this.Dialog.SendResponse( new HelloResponse( request, this.LocalSubscriber ) );

                    this.HelloRequestAcceptEvent.Set();
                }else
                {
                    this.OnMessageProcessingError( new MessageProcessingErrorEventArgs( "Handshake already executed." ) );
                }
            }
        }

        private void ConnectDialogProvider( Stream stream )
        {
            IMessageTransmitter messageTransmitter = this.SerializationProvider.CreateMessageTransmitter( stream );

            try
            {
                this.DialogProvider.Connect( messageTransmitter );
            }catch
            {
                messageTransmitter.Dispose();

                throw;
            }
        }

        private async Task ConnectDialogProviderAsync( Stream stream, CancellationToken cancellationToken = default )
        {
            IMessageTransmitter messageTransmitter = this.SerializationProvider.CreateMessageTransmitter( stream );

            try
            {
                await this.DialogProvider.ConnectAsync( messageTransmitter, cancellationToken );
            }catch
            {
                messageTransmitter.Dispose();

                throw;
            }
        }

        public void Connect( Stream stream )
        {
            this.ConnectDialogProvider( stream );
                
            bool waitResult = this.HelloRequestAcceptEvent.Wait( ( int )Timeouts.HandshakeTimeout.TotalMilliseconds );

            if( waitResult == false )
            {
                throw Assert.Exception( new TimeoutException( $"Handshake timed out." ) );
            }
        }

        public async Task ConnectAsync( Stream stream, CancellationToken cancellationToken = default )
        {
            await this.ConnectDialogProviderAsync( stream ).ConfigureAwait( continueOnCapturedContext:false );

            bool waitResult = this.HelloRequestAcceptEvent.Wait( ( int )Timeouts.HandshakeTimeout.TotalMilliseconds, cancellationToken );

            if( waitResult == false )
            {
                throw Assert.Exception( new TimeoutException( $"Handshake timed out." ) );
            }
        }

        protected override void Dispose( bool disposing )
        {
            if( this.isDisposed == false )
            {
                if( disposing == true )
                {
                    this.HelloRequestAcceptEvent.Dispose();
                }

                this.isDisposed = true;
            }
        }
    }
}
