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
using System.Text;

using Reth.Itss2.Diagnostics;
using Reth.Itss2.Messaging;
using Reth.Itss2.Serialization;
using Reth.Itss2.Tokenization;

namespace Reth.Itss2.Transfer
{
    public abstract class MessageStreamReader:IMessageStreamReader
    {
        public event EventHandler<MessageProcessingErrorEventArgs>? MessageProcessingError;

        private bool isDisposed;

        protected MessageStreamReader(  Stream baseStream,
                                        ITokenizer tokenizer    )
        :
            this( baseStream, tokenizer, NullInteractionLog.Instance )
        {
        }

        protected MessageStreamReader(  Stream baseStream,
                                        ITokenizer tokenizer,
                                        IInteractionLog interactionLog  )
        {
            if( baseStream.CanRead == false )
            {
                throw Assert.Exception( new ArgumentException( "Stream is not readable." ) );
            }

            this.BaseStream = baseStream;
            this.InteractionLog = interactionLog;
            this.Tokenizer = tokenizer;
            
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

        protected Stream BaseStream
        {
            get;
        }

        protected ITokenizer Tokenizer
        {
            get;
        }

        protected IInteractionLog InteractionLog
        {
            get;
        }

        protected abstract Encoding Encoding{ get; }
        
        protected abstract IMessageParser MessageParser{ get; }

        private IObservable<IMessageEnvelope> Observable
        {
            get;
        }

        private IMessageEnvelope? Deserialize( ReadOnlySequence<byte> token )
        {
            IMessageEnvelope? result = null;

            String messageEnvelope = String.Empty;

            try
            {
                messageEnvelope = this.Encoding.GetString( token.ToArray() );
            }catch( Exception ex )
            {
                this.MessageProcessingError?.Invoke( this, new MessageProcessingErrorEventArgs( "Message encoding failed.", ex ) );
            }

            this.InteractionLog.LogIncoming( messageEnvelope );

            try
            {
                result = this.MessageParser.DeserializeMessageEnvelope( messageEnvelope );
            }catch( MessageSerializationException ex )
            {
                this.MessageProcessingError?.Invoke( this, new MessageProcessingErrorEventArgs( messageEnvelope, ex ) );
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
