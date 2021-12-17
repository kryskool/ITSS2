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

using Reth.Itss2.Dialogs.Standard.Protocol;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Status;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Status.Reactive;

namespace Reth.Itss2.Workflows.Standard.Messages.Status.Reactive
{
    public class StatusWorkflow:SubscribedWorkflow<IStatusDialog>, IStatusWorkflow
    {
        public event EventHandler<ProcessStartedEventArgs<IStatusRequestedProcessState>>? ProcessStarted;

        public StatusWorkflow(  IStatusDialog dialog,
                                ISubscription subscription  )
        :
            base( dialog, subscription )
        {
            dialog.RequestReceived += this.Dialog_RequestReceived;
        }

        internal void SendResponse( StatusResponse response )
        {
            this.SendResponse(  response,
                                () =>
                                {
                                    this.Dialog.SendResponse( response );
                                } );
        }

        internal Task SendResponseAsync( StatusResponse response, CancellationToken cancellationToken = default )
        {
            return this.SendResponseAsync(  response,
                                            () =>
                                            {
                                                return this.Dialog.SendResponseAsync( response, cancellationToken );
                                            } );
        }

        private void Dialog_RequestReceived( Object? sender, MessageReceivedEventArgs<StatusRequest> e )
        {
            StatusRequest request = e.Message;

            this.OnMessageReceived( request,
                                    () =>
                                    {
                                        IStatusRequestedProcessState processState = new StatusRequestedProcessState( this, request );

                                        this.ProcessStarted?.Invoke( this, new ProcessStartedEventArgs<IStatusRequestedProcessState>( processState ) );
                                    }   );
        }
    }
}
