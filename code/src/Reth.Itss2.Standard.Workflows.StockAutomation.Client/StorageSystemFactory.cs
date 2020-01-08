using System.Collections.Generic;
using System.Net;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.General.Hello;
using Reth.Itss2.Standard.Serialization;
using Reth.Protocols.Diagnostics;
using Reth.Protocols.Dialogs;
using Reth.Protocols.Transfer;
using Reth.Protocols.Transfer.Tcp;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Client
{
    public class StorageSystemFactory:SubscriberNodeFactory
    {
        public StorageSystemFactory( SubscriberInfo subscriberInfo )
        :
            base( subscriberInfo )
        {
        }

        public virtual IStorageSystem CreateTcp(    IProtocolProvider protocolProvider,
                                                    IInteractionLog interactionLog,
                                                    IEnumerable<IDialogName> supportedDialogs,
                                                    IPEndPoint endPoint )
        {
            ILocalClientDialogProvider dialogProvider = null;
            ILocalMessageClient messageClient = null;

            try
            {
                dialogProvider = new LocalClientDialogProvider( protocolProvider,
                                                                interactionLog,
                                                                supportedDialogs    );

                messageClient = new LocalMessageClient( dialogProvider, endPoint );

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
