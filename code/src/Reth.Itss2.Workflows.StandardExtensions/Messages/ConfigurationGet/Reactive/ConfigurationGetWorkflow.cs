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

using Reth.Itss2.Dialogs.StandardExtensions.Protocol.Messages.ConfigurationGet;
using Reth.Itss2.Dialogs.StandardExtensions.Protocol.Messages.ConfigurationGet.Reactive;
using Reth.Itss2.Serialization;
using Reth.Itss2.Workflows.Standard;

namespace Reth.Itss2.Workflows.StandardExtensions.Messages.ConfigurationGet.Reactive
{
    public class ConfigurationGetWorkflow:SubscribedWorkflow<IConfigurationGetDialog>, IConfigurationGetWorkflow
    {
        public event EventHandler<ProcessStartedEventArgs<IConfigurationGetRequestedProcessState>>? ProcessStarted;

        public ConfigurationGetWorkflow(    IConfigurationGetDialog dialog,
                                            ISubscription subscription  )
        :
            base( dialog, subscription )
        {
            dialog.RequestReceived += this.Dialog_RequestReceived;
        }

        internal void SendResponse( ConfigurationGetResponse response )
        {
            this.SendResponse(  response,
                                () =>
                                {
                                    this.Dialog.SendResponse( response );
                                } );
        }

        internal Task SendResponseAsync( ConfigurationGetResponse response, CancellationToken cancellationToken = default )
        {
            return this.SendResponseAsync(  response,
                                            () =>
                                            {
                                                return this.Dialog.SendResponseAsync( response, cancellationToken );
                                            } );
        }

        private void Dialog_RequestReceived( Object? sender, MessageReceivedEventArgs<ConfigurationGetRequest> e )
        {
            ConfigurationGetRequest request = e.Message;

            this.OnMessageReceived( request,
                                    () =>
                                    {
                                        IConfigurationGetRequestedProcessState processState = new ConfigurationGetRequestedProcessState( this, request );

                                        this.ProcessStarted?.Invoke( this, new ProcessStartedEventArgs<IConfigurationGetRequestedProcessState>( processState ) );
                                    }   );
        }
    }
}