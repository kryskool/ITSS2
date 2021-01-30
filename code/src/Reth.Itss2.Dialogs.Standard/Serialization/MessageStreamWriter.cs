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
using System.Text;
using System.Threading.Tasks;

using Reth.Itss2.Dialogs.Standard.Diagnostics;
using Reth.Itss2.Dialogs.Standard.Protocol;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages;

namespace Reth.Itss2.Dialogs.Standard.Serialization
{
    public abstract class MessageStreamWriter:IMessageStreamWriter
    {
        protected MessageStreamWriter( Stream baseStream )
        :
            this( baseStream, NullInteractionLog.Instance )
        {
        }

        protected MessageStreamWriter(  Stream baseStream,
                                        IInteractionLog interactionLog    )
        {
            if( baseStream.CanWrite == false )
            {
                throw Assert.Exception( new ArgumentException( "Stream is not writable." ) );
            }

            this.BaseStream = baseStream;
            this.InteractionLog = interactionLog;
        }

        ~MessageStreamWriter()
        {
            this.Dispose( false );
        }

        protected Stream BaseStream
        {
            get;
        }

        protected IInteractionLog InteractionLog
        {
            get;
        }

        protected abstract Encoding Encoding{ get; }

        protected abstract IMessageParser MessageParser{ get; }

        private Object SyncRoot
        {
            get;
        } = new Object();

        private void WriteToStream( String messageEnvelope )
        {
            using( StreamWriter writer = new StreamWriter(  this.BaseStream,
                                                            this.Encoding,
                                                            bufferSize:2048,
                                                            leaveOpen:true  ) )
            {
                this.InteractionLog.LogOutgoing( messageEnvelope );

                writer.Write( messageEnvelope );
                writer.Flush();
            }
        }

        public void Write( String messageEnvelope )
        {
            lock( this.SyncRoot )
            {
                try
                {
                    this.WriteToStream( messageEnvelope );
                }catch( Exception ex )
                {
                    throw Assert.Exception( new MessageTransmissionException( $"Writing of message envelope (truncated) '{ messageEnvelope.Truncate() }' to stream failed.", ex ) );
                }
            }
        }

        public void Write( IMessageEnvelope messageEnvelope )
        {
            lock( this.SyncRoot )
            {
                try
                {
                    String serializedMessage = this.MessageParser.Serialize( messageEnvelope );

                    this.WriteToStream( serializedMessage );
                }catch( Exception ex )
                {
                    throw Assert.Exception( new MessageTransmissionException( $"Writing of message envelope '{ messageEnvelope } ({ messageEnvelope.Timestamp })' to stream failed.", ex ) );
                }
            }
        }

        public Task WriteAsync( String messageEnvelope )
        {
            return Task.Run(    () =>
                                {
                                    this.Write( messageEnvelope );
                                }   );
        }

        public Task WriteAsync( IMessageEnvelope messageEnvelope )
        {
            return Task.Run(    () =>
                                {
                                    this.Write( messageEnvelope );
                                }   );
        }

        public void Dispose()
        {
            this.Dispose( true );

            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
        }
    }
}
