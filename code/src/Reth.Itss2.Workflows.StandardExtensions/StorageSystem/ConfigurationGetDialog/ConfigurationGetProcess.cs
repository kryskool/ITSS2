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

using System.Threading.Tasks;

using Reth.Itss2.Dialogs.StandardExtensions.Protocol.Messages.ConfigurationGetDialog;

namespace Reth.Itss2.Workflows.StandardExtensions.StorageSystem.ConfigurationGetDialog
{
    internal class ConfigurationGetProcess:IConfigurationGetProcess
    {
        public ConfigurationGetProcess( ConfigurationGetWorkflow workflow, ConfigurationGetRequest request )
        {
            this.Workflow = workflow;
            this.Request = request;
        }

        private ConfigurationGetWorkflow Workflow
        {
            get;
        }

        public ConfigurationGetRequest Request
        {
            get;
        }

        public void SendResponse( Configuration configuration )
        {
            this.Workflow.SendResponse( new ConfigurationGetResponse( this.Request, configuration ) );
        }

        public Task SendResponseAsync( Configuration configuration )
        {
            return this.Workflow.SendResponseAsync( new ConfigurationGetResponse( this.Request, configuration ) );
        }
    }
}
