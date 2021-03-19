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
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Input;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Input.Active;

namespace Reth.Itss2.Workflows.Standard.Messages.Input.Active
{
    public class InputWorkflow:SubscribedWorkflow<IInputDialog>, IInputWorkflow
    {
        public InputWorkflow(   IInputDialog dialog,
                                ISubscription subscription  )
        :
            base( dialog, subscription )
        {
        }

        internal void SendMessage( InputMessage message )
        {
            this.SendMessage(   message,
                                () =>
                                {
                                    this.Dialog.SendMessage( message );
                                } );
        }

        internal Task SendMessageAsync( InputMessage message, CancellationToken cancellationToken = default )
        {
            return this.SendMessageAsync(   message,
                                            () =>
                                            {
                                                return this.Dialog.SendMessageAsync( message, cancellationToken );
                                            } );
        }

        private InputRequest CreateRequest( IEnumerable<InputRequestArticle> articles,
                                            bool? isNewDelivery,
                                            bool? setPickingIndicator   )
        {
            return this.CreateRequest(  (   MessageId messageId,
                                            SubscriberId localSubscriberId,
                                            SubscriberId remoteSubscriberId ) =>
                                        {
                                            return new InputRequest(    messageId,
                                                                        localSubscriberId,
                                                                        remoteSubscriberId,
                                                                        articles,
                                                                        isNewDelivery,
                                                                        setPickingIndicator ) ;
                                        }   );
        }

        public IInputStartedProcessState StartProcess( IEnumerable<InputRequestArticle> articles )
        {
            return this.StartProcess( articles, isNewDelivery:null, setPickingIndicator:null );
        }

        public IInputStartedProcessState StartProcess(  IEnumerable<InputRequestArticle> articles,
                                            bool? isNewDelivery,
                                            bool? setPickingIndicator   )
        {
            InputRequest request = this.CreateRequest( articles, isNewDelivery, setPickingIndicator );

            InputResponse response = this.SendRequest(  request,
                                                        () =>
                                                        {
                                                            return this.Dialog.SendRequest( request );
                                                        }   );

            return new InputStartedProcessState( this, request, response );
        }

        public Task<IInputStartedProcessState> StartProcessAsync( IEnumerable<InputRequestArticle> articles, CancellationToken cancellationToken = default )
        {
            return this.StartProcessAsync( articles, isNewDelivery:false, setPickingIndicator:false, cancellationToken );
        }

        public async Task<IInputStartedProcessState> StartProcessAsync( IEnumerable<InputRequestArticle> articles,
                                                                        bool? isNewDelivery,
                                                                        bool? setPickingIndicator,
                                                                        CancellationToken cancellationToken = default   )
        {
            InputRequest request = this.CreateRequest( articles, isNewDelivery, setPickingIndicator );

            InputResponse response = await this.SendRequestAsync(   request,
                                                                    () =>
                                                                    {
                                                                        return this.Dialog.SendRequestAsync( request, cancellationToken );
                                                                    }   ).ConfigureAwait( continueOnCapturedContext:false );

            return new InputStartedProcessState( this, request, response );
        }
    }
}
