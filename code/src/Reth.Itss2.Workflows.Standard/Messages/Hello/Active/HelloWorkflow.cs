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

using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Hello;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Hello.Active;
using Reth.Itss2.Dialogs;
using Reth.Itss2.Messaging;
using Reth.Itss2.Serialization;

namespace Reth.Itss2.Workflows.Standard.Messages.Hello.Active
{
    public class HelloWorkflow:Workflow<IHelloDialog>, IHelloWorkflow
    {
        public HelloWorkflow(   IHelloDialog dialog,
                                ISubscription subscription,
                                IDialogProvider dialogProvider,
                                ISerializationProvider serializationProvider    )
        :
            base( dialog, subscription )
        {
            this.Connector = new Connector( dialogProvider, serializationProvider );
        }

        private Connector Connector
        {
            get;
        }

        public SubscriberInfo Connect( Stream stream )
        {
            this.Connector.Connect( stream );

            Subscriber localSubscriber = this.Subscription.LocalSubscriber;

            HelloResponse response = this.Dialog.SendRequest( new HelloRequest( MessageId.NextId(), localSubscriber ) );

            SubscriberInfo subscriberInfo = new SubscriberInfo( localSubscriber, response.Subscriber );
            
            this.Subscription.Subscribe( subscriberInfo );

            return subscriberInfo;
        }

        public async Task<SubscriberInfo> ConnectAsync( Stream stream, CancellationToken cancellationToken = default )
        {
            await this.Connector.ConnectAsync( stream, cancellationToken ).ConfigureAwait( continueOnCapturedContext:false );

            Subscriber localSubscriber = this.Subscription.LocalSubscriber;

            HelloResponse response = await this.Dialog.SendRequestAsync( new HelloRequest( MessageId.NextId(), localSubscriber ), cancellationToken );

            SubscriberInfo subscriberInfo = new SubscriberInfo( localSubscriber, response.Subscriber );
            
            this.Subscription.Subscribe( subscriberInfo );

            return subscriberInfo;
        }
    }
}
