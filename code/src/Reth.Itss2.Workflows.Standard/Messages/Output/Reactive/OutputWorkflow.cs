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
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Dialogs.Standard.Protocol;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Output;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Output.Reactive;

namespace Reth.Itss2.Workflows.Standard.Messages.Output.Reactive
{
    public class OutputWorkflow:SubscribedWorkflow<IOutputDialog>, IOutputWorkflow
    {
        public event EventHandler<ProcessStartedEventArgs<IOutputRequestedProcessState>>? ProcessStarted;

        public OutputWorkflow(  IOutputDialog dialog,
                                ISubscription subscription  )
        :
            base( dialog, subscription )
        {
            dialog.RequestReceived += this.Dialog_RequestReceived;
        }

        internal void SendResponse( OutputResponse response )
        {
            this.SendResponse(  response,
                                () =>
                                {
                                    this.Dialog.SendResponse( response );
                                } );
        }

        internal Task SendResponseAsync( OutputResponse response, CancellationToken cancellationToken = default )
        {
            return this.SendResponseAsync(  response,
                                            () =>
                                            {
                                                return this.Dialog.SendResponseAsync( response, cancellationToken );
                                            } );
        }

        internal void SendMessage( OutputMessage message )
        {
            this.SendMessage(   message,
                                () =>
                                {
                                    this.Dialog.SendMessage( message );
                                } );
        }

        internal Task SendMessageAsync( OutputMessage message, CancellationToken cancellationToken = default )
        {
            return this.SendMessageAsync(   message,
                                            () =>
                                            {
                                                return this.Dialog.SendMessageAsync( message, cancellationToken );
                                            } );
        }

        private OutputMessage CreateMessage(    OutputMessageDetails details,
                                                IEnumerable<OutputArticle> articles,
                                                IEnumerable<OutputBox>? boxes )
        {
            return this.CreateMessage(  (   MessageId id,
                                            SubscriberId localSubscriberId,
                                            SubscriberId remoteSubscriberId  ) =>
                                        {
                                            return new OutputMessage(   ManualOutput.DefaultId,
                                                                        localSubscriberId,
                                                                        remoteSubscriberId,
                                                                        details,
                                                                        articles,
                                                                        boxes   );
                                        }   );
        }

        public void NotifyManualOutput( OutputMessageDetails details,
                                        IEnumerable<OutputArticle> articles    )
        {
            OutputMessage message = this.CreateMessage( details, articles, boxes:null );

            this.SendMessage(   message,
                                () =>
                                {
                                    this.Dialog.SendMessage( message );
                                }   );
        }

        public void NotifyManualOutput( OutputMessageDetails details,
                                        IEnumerable<OutputArticle> articles,
                                        IEnumerable<OutputBox> boxes    )
        {
            OutputMessage message = this.CreateMessage( details, articles, boxes );

            this.SendMessage(   message,
                                () =>
                                {
                                    this.Dialog.SendMessage( message );
                                }   );
        }

        public Task NotifyManualOutputAsync(    OutputMessageDetails details,
                                                IEnumerable<OutputArticle> articles,
                                                CancellationToken cancellationToken = default   )
        {
            OutputMessage message = this.CreateMessage( details, articles, boxes:null );

            return this.SendMessageAsync(   message,
                                            () =>
                                            {
                                                return this.Dialog.SendMessageAsync( message, cancellationToken );
                                            }   );
        }

        public Task NotifyManualOutputAsync(    OutputMessageDetails details,
                                                IEnumerable<OutputArticle> articles,
                                                IEnumerable<OutputBox> boxes,
                                                CancellationToken cancellationToken = default   )
        {
            OutputMessage message = this.CreateMessage( details, articles, boxes );

            return this.SendMessageAsync(   message,
                                            () =>
                                            {
                                                return this.Dialog.SendMessageAsync( message, cancellationToken );
                                            }   );
        }

        private void Dialog_RequestReceived( Object sender, MessageReceivedEventArgs<OutputRequest> e )
        {
            OutputRequest request = e.Message;

            this.OnMessageReceived( request,
                                    () =>
                                    {
                                        IOutputRequestedProcessState processState = new OutputRequestedProcessState( this, request );

                                        this.ProcessStarted?.Invoke( this, new ProcessStartedEventArgs<IOutputRequestedProcessState>( processState ) );
                                    }   );
        }
    }
}
