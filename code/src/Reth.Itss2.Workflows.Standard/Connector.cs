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
using Reth.Itss2.Dialogs.Standard.Serialization;

namespace Reth.Itss2.Workflows.Standard
{
    internal class Connector
    {
        private bool isConnected;

        public Connector(   IDialogProvider dialogProvider,
                            ISerializationProvider serializationProvider    )
        {
            this.DialogProvider = dialogProvider;
            this.SerializationProvider = serializationProvider;
        }

        private Object SyncRoot
        {
            get;
        } = new Object();

        private IDialogProvider DialogProvider
        {
            get;
        }

        private ISerializationProvider SerializationProvider
        {
            get;
        }

        private bool IsConnected
        {
            get
            {
                lock( this.SyncRoot )
                {
                    return this.isConnected;
                }
            }

            set
            {
                lock( this.SyncRoot )
                {
                    this.isConnected = value;
                }
            }
        }

        public void Connect( Stream stream )
        {
            if( this.IsConnected == true )
            {
                throw Assert.Exception( new InvalidOperationException( "Connection has already been made." ) );
            }

            IMessageTransmitter messageTransmitter = this.SerializationProvider.CreateMessageTransmitter( stream );

            try
            {
                this.DialogProvider.Connect( messageTransmitter );

                this.IsConnected = true;
            }catch
            {
                messageTransmitter.Dispose();

                throw;
            }
        }

        public async Task ConnectAsync( Stream stream, CancellationToken cancellationToken = default )
        {
            if( this.IsConnected == true )
            {
                throw Assert.Exception( new InvalidOperationException( "Connection has already been made." ) );
            }

            IMessageTransmitter messageTransmitter = this.SerializationProvider.CreateMessageTransmitter( stream );

            try
            {
                await this.DialogProvider.ConnectAsync( messageTransmitter, cancellationToken );

                this.IsConnected = true;
            }catch
            {
                messageTransmitter.Dispose();

                throw;
            }
        }
    }
}
