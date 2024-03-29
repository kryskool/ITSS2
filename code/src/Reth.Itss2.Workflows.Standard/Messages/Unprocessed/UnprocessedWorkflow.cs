﻿// Implementation of the WWKS2 protocol.
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
using System.Threading.Tasks;

using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Unprocessed;
using Reth.Itss2.Messaging;
using Reth.Itss2.Serialization;

namespace Reth.Itss2.Workflows.Standard.Messages.Unprocessed
{
    public class UnprocessedWorkflow:SubscribedWorkflow<IUnprocessedDialog>, IUnprocessedWorkflow
    {
        public event EventHandler<MessageReceivedEventArgs<UnprocessedMessage>>? MessageReceived;

        public UnprocessedWorkflow( IUnprocessedDialog dialog,
                                    ISubscription subscription,
                                    ISerializationProvider serializationProvider    )
        :
            base( dialog, subscription )
        {
            this.SerializationProvider = serializationProvider;

            dialog.MessageReceived += this.Dialog_MessageReceived;
        }

        private ISerializationProvider SerializationProvider
        {
            get;
        }

        private void Dialog_MessageReceived( Object? sender, MessageReceivedEventArgs<UnprocessedMessage> e )
        {
            this.MessageReceived?.Invoke( this, e );
        }

        public void SendMessage( UnprocessedMessage message )
        {
            this.SendMessage(   message,
                                () =>
                                {
                                    this.Dialog.SendMessage( message );
                                }   );
        }

        public Task SendMessageAsync( UnprocessedMessage message, CancellationToken cancellationToken = default )
        {
            return this.SendMessageAsync(   message,
                                            () =>
                                            {
                                                return this.Dialog.SendMessageAsync( message, cancellationToken );
                                            }   );
        }

        public UnprocessedMessage CreateUnprocessedMessage( SubscriberId localSubscriberId,
                                                            SubscriberId remoteSubscriberId,
                                                            IMessageEnvelope messageEnvelope,
                                                            String? text,
                                                            UnprocessedReason? reason   )
        {
            String serializedMessage = this.SerializationProvider.SerializeMessage( messageEnvelope );

            return this.CreateUnprocessedMessage(   localSubscriberId,
                                                    remoteSubscriberId,
                                                    serializedMessage,
                                                    text,
                                                    reason  );

        }

        public UnprocessedMessage CreateUnprocessedMessage( SubscriberId localSubscriberId,
                                                            SubscriberId remoteSubscriberId,
                                                            String message,
                                                            String? text,
                                                            UnprocessedReason? reason   )
        {
            return new UnprocessedMessage(  MessageId.NextId(),
                                            localSubscriberId,
                                            remoteSubscriberId,
                                            message,
                                            text,
                                            reason  );
        }
    }
}
