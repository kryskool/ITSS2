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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Output;

namespace Reth.Itss2.Workflows.Standard.Messages.Output.Reactive
{
    internal class OutputRequestedProcessState:OutputProcessState, IOutputRequestedProcessState
    {
        public OutputRequestedProcessState( OutputWorkflow workflow, OutputRequest request )
        :
            base( workflow, request )
        {
        }

        public IOutputAcceptedProcessState Accept()
        {
            this.OnStateChange();

            this.SendResponse( OutputResponseStatus.Queued );

            return new OutputAcceptedProcessState( this.Workflow, this.Request );
        }

        public async Task<IOutputAcceptedProcessState> AcceptAsync( CancellationToken cancellationToken = default )
        {
            this.OnStateChange();

            await this.SendResponseAsync( OutputResponseStatus.Queued, cancellationToken ).ConfigureAwait( continueOnCapturedContext:false );

            return new OutputAcceptedProcessState( this.Workflow, this.Request );
        }

        public void Reject()
        {
            this.OnStateChange();

            this.SendResponse( OutputResponseStatus.Rejected );
        }

        public Task RejectAsync( CancellationToken cancellationToken = default )
        {
            this.OnStateChange();

            return this.SendResponseAsync( OutputResponseStatus.Rejected, cancellationToken );
        }
    }
}
