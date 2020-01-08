using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Sockets;

using Reth.Itss2.Standard.Dialogs.General.Hello;
using Reth.Itss2.Standard.Workflows;
using Reth.Itss2.StandardExtensions.Dialogs;
using Reth.Itss2.StandardExtensions.Serialization;
using Reth.Protocols.Diagnostics;
using Reth.Protocols.Dialogs;
using Reth.Protocols.Transfer;
using Reth.Protocols.Transfer.Tcp;

namespace Reth.Itss2.StandardExtensions.Workflows.StockAutomation.Server
{
    public class StorageSystemFactory:SubscriberNodeFactory
    {
        public StorageSystemFactory( SubscriberInfo subscriberInfo )
        :
            base( subscriberInfo )
        {
        }

        [SuppressMessage(   "Microsoft.Reliability",
                            "CA2000:DisposeObjectsBeforeLosingScope"  )]
        public IStorageSystem CreateTcp(    IProtocolProvider protocolProvider,
                                            IInteractionLog interactionLog,
                                            IEnumerable<IDialogName> supportedDialogs,
                                            TcpClient tcpClient )
        {
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
