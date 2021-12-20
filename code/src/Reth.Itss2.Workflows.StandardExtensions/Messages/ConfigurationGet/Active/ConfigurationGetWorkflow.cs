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

using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.StandardExtensions.Protocol.Messages.ConfigurationGet;
using Reth.Itss2.Dialogs.StandardExtensions.Protocol.Messages.ConfigurationGet.Active;
using Reth.Itss2.Messaging;
using Reth.Itss2.Workflows.Standard;

namespace Reth.Itss2.Workflows.StandardExtensions.Messages.ConfigurationGet.Active
{
    public class ConfigurationGetWorkflow:SubscribedWorkflow<IConfigurationGetDialog>, IConfigurationGetWorkflow
    {
        public ConfigurationGetWorkflow(    IConfigurationGetDialog dialog,
                                            ISubscription subscription  )
        :
            base( dialog, subscription )
        {
        }

        private ConfigurationGetRequest CreateRequest()
        {
            return this.CreateRequest(  (   MessageId messageId,
                                            SubscriberId localSubscriberId,
                                            SubscriberId remoteSubscriberId ) =>
                                        {
                                            return new ConfigurationGetRequest( messageId,
                                                                                localSubscriberId,
                                                                                remoteSubscriberId  ) ;
                                        }   );
        }

        public IConfigurationGetFinishedProcessState StartProcess()
        {
            ConfigurationGetRequest request = this.CreateRequest();

            ConfigurationGetResponse response = this.SendRequest(   request,
                                                                    () =>
                                                                    {
                                                                        return this.Dialog.SendRequest( request );
                                                                    }   );

            return new ConfigurationGetFinishedProcessState( request, response );
        }

        public async Task<IConfigurationGetFinishedProcessState> StartProcessAsync( CancellationToken cancellationToken = default )
        {
            ConfigurationGetRequest request = this.CreateRequest();

            ConfigurationGetResponse response = await this.SendRequestAsync(    request,
                                                                                () =>
                                                                                {
                                                                                    return this.Dialog.SendRequestAsync( request, cancellationToken );
                                                                                }   ).ConfigureAwait( continueOnCapturedContext:false );

            return new ConfigurationGetFinishedProcessState( request, response );
        }
    }
}
