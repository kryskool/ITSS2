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
using System.Buffers;
using System.IO;
using System.Linq;
using System.Reactive.Linq;

using Reth.Itss2.Dialogs.Standard.Diagnostics;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.Standard.Serialization.Xml.Tokenization;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Xml
{
    public class MessageStreamReader:IMessageStreamReader
    {
        public event EventHandler<Protocol.ErrorEventArgs>? MessageProcessingError;

        private bool isDisposed;

        public MessageStreamReader( Stream baseStream,
                                    IDataContractResolver dataContractResolver  )
        :
            this( baseStream, dataContractResolver, NullInteractionLog.Instance )
        {
        }

        public MessageStreamReader( Stream baseStream,
                                    IDataContractResolver dataContractResolver,
                                    IInteractionLog interactionLog  )
        {
            if( baseStream.CanRead == false )
            {
                throw Assert.Exception( new ArgumentException( "Stream is not readable." ) );
            }

            this.BaseStream = baseStream;
            this.MessageParser = new MessageParser( dataContractResolver );
            this.InteractionLog = interactionLog;

            this.Tokenizer = new Tokenizer( baseStream, interactionLog );
            
            this.Observable =   from messageEnvelope in
                                    from token in this.Tokenizer
                                    select this.Deserialize( token )
                                where messageEnvelope is not null
                                select messageEnvelope;
        }

        ~MessageStreamReader()
        {
            this.Dispose( false );
        }

        public Stream BaseStream
        {
            get;
        }

        public IDataContractResolver DataContractResolver => this.MessageParser.DataContractResolver;

        private IInteractionLog InteractionLog
        {
            get;
        }
        
        private MessageParser MessageParser
        {
            get;
        }

        private Tokenizer Tokenizer
        {
            get;
        }

        private IObservable<IMessageEnvelope> Observable
        {
            get;
        }

        private IMessageEnvelope? Deserialize( ReadOnlySequence<byte> token )
        {
            IMessageEnvelope? result = null;

            try
            {
                String message = XmlSerializationSettings.Encoding.GetString( token.ToArray() );

                this.InteractionLog.LogIncoming( message );

                result = this.MessageParser.Deserialize( message );
            }catch( Exception ex )
            {
                this.MessageProcessingError?.Invoke( this, new Protocol.ErrorEventArgs( ex ) );
            }

            return result;
        }

        public IDisposable Subscribe( IObserver<IMessageEnvelope> observer )
        {
            return this.Observable.Subscribe( observer );
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
                    this.Tokenizer.Dispose();
                }

                this.isDisposed = true;
            }
        }
    }
}
