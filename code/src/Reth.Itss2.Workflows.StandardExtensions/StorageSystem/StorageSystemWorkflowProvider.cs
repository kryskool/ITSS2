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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.Standard.Serialization;
using Reth.Itss2.Dialogs.StandardExtensions.Protocol.Roles.StorageSystem;
using Reth.Itss2.Workflows.StandardExtensions.StorageSystem.ConfigurationGetDialog;

namespace Reth.Itss2.Workflows.StandardExtensions.StorageSystem
{
    public class StorageSystemWorkflowProvider:Standard.StorageSystem.StorageSystemWorkflowProvider, IStorageSystemWorkflowProvider
    {
        public StorageSystemWorkflowProvider( ISerializationProvider serializationProvider, Subscriber localSubscriber )
        :
            base( serializationProvider, localSubscriber, new StorageSystemDialogProvider() )
        {
            IStorageSystemDialogProvider dialogProvider = ( IStorageSystemDialogProvider )this.DialogProvider;
            
            this.ConfigurationGetWorkflow = new ConfigurationGetWorkflow( this );
            
            this.ConfigurationGetWorkflow.MessageProcessingError += this.OnMessageProcessingError;
        }

        ~StorageSystemWorkflowProvider()
        {
            this.Dispose( false );
        }

        public IConfigurationGetWorkflow ConfigurationGetWorkflow{ get; }

        protected override void Dispose( bool disposing )
        {
            if( this.isDisposed == false )
            {
                if( disposing == true )
                {
                    this.ConfigurationGetWorkflow.Dispose();
                }

                base.Dispose( disposing );
            }
        }
    }
}