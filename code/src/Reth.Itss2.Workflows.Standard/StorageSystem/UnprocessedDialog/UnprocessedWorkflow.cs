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
using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Dialogs.Standard.Protocol;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.UnprocessedDialog;
using Reth.Itss2.Dialogs.Standard.Protocol.Roles.StorageSystem;
using Reth.Itss2.Dialogs.Standard.Serialization;

namespace Reth.Itss2.Workflows.Standard.StorageSystem.UnprocessedDialog
{
    internal class UnprocessedWorkflow:Workflow<IStorageSystemUnprocessedDialog>, IUnprocessedWorkflow
    {
        public event EventHandler<MessageReceivedEventArgs>? MessageReceived;

        public UnprocessedWorkflow( IStorageSystemWorkflowProvider workflowProvider,
                                    IStorageSystemDialogProvider dialogProvider,
                                    ISerializationProvider serializationProvider    )
        :
            base( workflowProvider, dialogProvider, serializationProvider, dialogProvider.UnprocessedDialog )
        {
            this.Dialog.MessageReceived += this.UnprocessedDialog_MessageReceived;
        }

        private void UnprocessedDialog_MessageReceived( Object sender, MessageReceivedEventArgs e )
        {
            this.MessageReceived?.Invoke( this, e );
        }

        public void SendMessage( UnprocessedMessage message )
        {
            this.OnSendMessage<UnprocessedMessage>( message,
                                                    ( UnprocessedMessage message ) =>
                                                    {
                                                        this.Dialog.SendMessage( message );
                                                    }   );
        }

        public async Task SendMessageAsync( UnprocessedMessage message, CancellationToken cancellationToken = default )
        {
            await this.OnSendMessageAsync<UnprocessedMessage>(  message,
                                                                async( UnprocessedMessage message, CancellationToken cancellationToken ) =>
                                                                {
                                                                    await this.Dialog.SendMessageAsync( message );
                                                                }   );
        }

        public UnprocessedMessage CreateUnprocessedMessage( SubscriberId localSubscriberId,
                                                            SubscriberId remoteSubscriberId,
                                                            IMessage message,
                                                            String? text,
                                                            UnprocessedReason? reason   )
        {
            String serializedMessage = this.SerializationProvider.SerializeMessage( message );

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

        protected override void Dispose( bool disposing )
        {
        }
    }
}
