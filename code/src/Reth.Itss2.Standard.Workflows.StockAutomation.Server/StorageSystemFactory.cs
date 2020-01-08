using System.Collections.Generic;
using System.Net.Sockets;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.General.Hello;
using Reth.Itss2.Standard.Serialization;
using Reth.Protocols.Diagnostics;
using Reth.Protocols.Extensions.ObjectExtensions;
using Reth.Protocols.Dialogs;
using Reth.Protocols.Transfer;
using Reth.Protocols.Transfer.Tcp;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Server
{
    public class StorageSystemFactory:SubscriberNodeFactory
    {
        public StorageSystemFactory( SubscriberInfo subscriberInfo )
        :
            base( subscriberInfo )
        {
        }

        public IStorageSystem CreateTcp(    IProtocolProvider protocolProvider,
                                            IInteractionLog interactionLog,
                                            IEnumerable<IDialogName> supportedDialogs,
                                            TcpClient tcpClient )
        {
            protocolProvider.ThrowIfNull();

            IRemoteClientDialogProvider dialogProvider = null;
            IRemoteMessageClient messageClient = null;
            
            try
            {
                dialogProvider = new RemoteClientDialogProvider(    protocolProvider,
                                                                    interactionLog,
                                                                    supportedDialogs    );

                messageClient = new RemoteMessageClient( dialogProvider, tcpClient );           

                return new StorageSystem(   this.SubscriberInfo,
                                            messageClient,
                                            dialogProvider,
                                            supportedDialogs    );
            }catch
            {
                dialogProvider?.Dispose();
                messageClient?.Dispose();

                throw;
            }
        }
    }
}
