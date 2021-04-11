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

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Output;

namespace Reth.Itss2.Workflows.Standard.Messages.Output.Reactive
{
    internal class OutputProcessingProcessState:AbortableOutputProcessState, IOutputProcessingProcessState
    {
        public OutputProcessingProcessState( OutputWorkflow workflow, OutputRequest request )
        :
            base( workflow, request )
        {
        }

        public void Complete()
        {
            this.OnStateChange();

            this.SendMessage( OutputMessageStatus.Completed, articles:null, boxes:null );
        }

        public void Complete( IEnumerable<OutputArticle> articles, IEnumerable<OutputBox>? boxes )
        {
            this.OnStateChange();

            this.SendMessage( OutputMessageStatus.Completed, articles, boxes );
        }

        public Task CompleteAsync( CancellationToken cancellationToken = default )
        {
            this.OnStateChange();

            return this.SendMessageAsync( OutputMessageStatus.Completed, articles:null, boxes:null, cancellationToken );
        }

        public Task CompleteAsync( IEnumerable<OutputArticle> articles, IEnumerable<OutputBox>? boxes, CancellationToken cancellationToken = default )
        {
            this.OnStateChange();

            return this.SendMessageAsync( OutputMessageStatus.Completed, articles, boxes, cancellationToken );
        }

        public void Incomplete()
        {
            this.OnStateChange();

            this.SendMessage( OutputMessageStatus.Incomplete, articles:null, boxes:null );
        }

        public void Incomplete( IEnumerable<OutputArticle> articles, IEnumerable<OutputBox>? boxes )
        {
            this.OnStateChange();

            this.SendMessage( OutputMessageStatus.Incomplete, articles, boxes );
        }

        public Task IncompleteAsync( CancellationToken cancellationToken = default )
        {
            this.OnStateChange();

            return this.SendMessageAsync( OutputMessageStatus.Incomplete, articles:null, boxes:null, cancellationToken );
        }

        public Task IncompleteAsync( IEnumerable<OutputArticle> articles, IEnumerable<OutputBox>? boxes, CancellationToken cancellationToken = default )
        {
            this.OnStateChange();

            return this.SendMessageAsync( OutputMessageStatus.Incomplete, articles, boxes, cancellationToken );
        }

        public void PartialDispense( IEnumerable<OutputArticle> articles, IEnumerable<OutputBox>? boxes )
        {
            this.Validate();

            this.SendMessage( OutputMessageStatus.PartialDispense, articles, boxes );
        }

        public Task PartialDispenseAsync( IEnumerable<OutputArticle> articles, IEnumerable<OutputBox>? boxes, CancellationToken cancellationToken = default )
        {
            this.Validate();

            return this.SendMessageAsync( OutputMessageStatus.PartialDispense, articles, boxes, cancellationToken );
        }
    }
}
