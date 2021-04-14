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

using Reth.Itss2.Dialogs.Standard.Diagnostics;

namespace Reth.Itss2.Workflows.Standard
{
    public abstract class ProcessState:IProcessState
    {
        protected ProcessState()
        {
        }

        ~ProcessState()
        {
            this.Dispose( false );
        }

        private Object SyncRoot
        {
            get;
        } = new Object();

        private bool IsValid
        {
            get; set;
        } = true;

        protected void OnStateChange()
        {
            lock( this.SyncRoot )
            {
                this.Validate();

                this.IsValid = false;
            }
        }

        protected void Validate()
        {
            lock( this.SyncRoot )
            {
                if( this.IsValid == false )
                {
                    throw Assert.Exception( new InvalidOperationException( "Invalid process state." ) );
                }
            }
        }

        public void Dispose()
        {
            this.Dispose( true );

            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
        }
    }
}
