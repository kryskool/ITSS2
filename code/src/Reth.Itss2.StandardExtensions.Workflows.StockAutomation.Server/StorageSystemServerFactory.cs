using System;
using System.Collections.Generic;

using Reth.Itss2.Standard.Dialogs.General.Hello;
using Reth.Itss2.Standard.Workflows.StockAutomation.Server;
using Reth.Itss2.StandardExtensions.Serialization;
using Reth.Itss2.StandardExtensions.Workflows.StockAutomation.Server.Transfer.Tcp;
using Reth.Protocols.Diagnostics;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.StandardExtensions.Workflows.StockAutomation.Server
{
    public class StorageSystemServerFactory:Reth.Itss2.Standard.Workflows.StockAutomation.Server.StorageSystemServerFactory
    {
        public StorageSystemServerFactory( SubscriberInfo subscriberInfo )
        :
            base( subscriberInfo )
        {
        }

        public IStorageSystemServer CreateTcp(  IProtocolProvider protocolProvider,
                                                Func<String, String, IInteractionLog> createInteractionLogCallback,
                                                IEnumerable<IDialogName> supportedDialogs,
                                                int port    )
        {
            return new StorageSystemServer( this.SubscriberInfo,
                                            protocolProvider,
                                            createInteractionLogCallback,
                                            supportedDialogs,
                                            port    );
        }
    }
}
