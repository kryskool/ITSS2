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
    internal abstract class AbortableOutputProcessState:OutputProcessState
    {
        protected AbortableOutputProcessState( OutputWorkflow workflow, OutputRequest request )
        :
            base( workflow, request )
        {
        }

        public IOutputAbortingProcessState Aborting()
        {
            this.OnStateChange();

            this.SendMessage( OutputMessageStatus.Aborting, articles:null, boxes:null );

            return new OutputAbortingProcessState( this.Workflow, this.Request );
        }

        public async Task<IOutputAbortingProcessState> AbortingAsync( CancellationToken cancellationToken = default )
        {
            this.OnStateChange();

            await this.SendMessageAsync(    OutputMessageStatus.Aborting,
                                            articles:null,
                                            boxes:null,
                                            cancellationToken   ).ConfigureAwait( continueOnCapturedContext:false );

            return new OutputAbortingProcessState( this.Workflow, this.Request );
        }
    }
}
