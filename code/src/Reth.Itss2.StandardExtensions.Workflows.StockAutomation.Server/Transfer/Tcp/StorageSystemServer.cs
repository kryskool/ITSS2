using System;
using System.Collections.Generic;
using System.Net.Sockets;

using Reth.Itss2.Standard.Dialogs.General.Hello;
using Reth.Itss2.StandardExtensions.Serialization;
using Reth.Protocols.Diagnostics;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.StandardExtensions.Workflows.StockAutomation.Server.Transfer.Tcp
{
    public class StorageSystemServer:Standard.Workflows.StockAutomation.Server.Transfer.Tcp.StorageSystemServer
    {
        public StorageSystemServer( SubscriberInfo subscriberInfo,
                                    IProtocolProvider protocolProvider,
                                    Func<String, String, IInteractionLog> createInteractionLogCallback,
                                    IEnumerable<IDialogName> supportedDialogs,
                                    int port    )
        :
            base(   subscriberInfo,
                    protocolProvider,
                    createInteractionLogCallback,
                    supportedDialogs,
                    port    )
        {
        }

        protected override Standard.Workflows.StockAutomation.Server.IStorageSystem CreateStorageSystem(    Standard.Serialization.IProtocolProvider protocolProvider,
                                                                                                            IInteractionLog interactionLog,
                                                                                                            IEnumerable<IDialogName> supportedDialogs,
                                                                                                            TcpClient tcpClient  )
        {
            StorageSystemFactory storageSystemFactory = new StorageSystemFactory( this.SubscriberInfo );

            return storageSystemFactory.CreateTcp(  ( IProtocolProvider )protocolProvider,
                                                    interactionLog,
                                                    supportedDialogs,
                                                    tcpClient   );
        }
    }
}
