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
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.InitiateInput;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.InitiateInput.Active;
using Reth.Itss2.Messaging;

namespace Reth.Itss2.Workflows.Standard.Messages.InitiateInput.Active
{
    public class InitiateInputWorkflow:SubscribedWorkflow<IInitiateInputDialog>, IInitiateInputWorkflow
    {
        public InitiateInputWorkflow(   IInitiateInputDialog dialog,
                                        ISubscription subscription  )
        :
            base( dialog, subscription )
        {
        }

        private InitiateInputRequest CreateRequest( InitiateInputRequestDetails details,
                                                    IEnumerable<InitiateInputRequestArticle> articles,
                                                    bool? isNewDelivery,
                                                    bool? setPickingIndicator    )
        {
            return this.CreateRequest(  (   MessageId messageId,
                                            SubscriberId localSubscriberId,
                                            SubscriberId remoteSubscriberId ) =>
                                        {
                                            return new InitiateInputRequest(    messageId,
                                                                                localSubscriberId,
                                                                                remoteSubscriberId,
                                                                                details,
                                                                                articles,
                                                                                isNewDelivery,
                                                                                setPickingIndicator ) ;
                                        }   );
        }

        public IInitiateInputCreatedProcessState CreateProcess(  InitiateInputRequestDetails details,
                                                                IEnumerable<InitiateInputRequestArticle> articles   )
        {
            return this.CreateProcess( details, articles, isNewDelivery:null, setPickingIndicator:null );
        }

        public IInitiateInputCreatedProcessState CreateProcess( InitiateInputRequestDetails details,
                                                                IEnumerable<InitiateInputRequestArticle> articles,
                                                                bool? isNewDelivery,
                                                                bool? setPickingIndicator    )
        {
            InitiateInputRequest request = this.CreateRequest( details, articles, isNewDelivery, setPickingIndicator );

            return new InitiateInputCreatedProcessState( this, request );
        }

        public Task<IInitiateInputCreatedProcessState> CreateProcessAsync(  InitiateInputRequestDetails details,
                                                                            IEnumerable<InitiateInputRequestArticle> articles,
                                                                            CancellationToken cancellationToken = default   )
        {
            return this.CreateProcessAsync( details, articles, isNewDelivery:null, setPickingIndicator:null, cancellationToken );
        }

        public Task<IInitiateInputCreatedProcessState> CreateProcessAsync(  InitiateInputRequestDetails details,
                                                                            IEnumerable<InitiateInputRequestArticle> articles,
                                                                            bool? isNewDelivery,
                                                                            bool? setPickingIndicator,
                                                                            CancellationToken cancellationToken = default   )
        {
            InitiateInputRequest request = this.CreateRequest( details, articles, isNewDelivery, setPickingIndicator );

            return Task.FromResult<IInitiateInputCreatedProcessState>( new InitiateInputCreatedProcessState( this, request ) );
        }
    }
}
