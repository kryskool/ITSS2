using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;

using Reth.Itss2.StandardExtensions.Dialogs;
using Reth.Itss2.StandardExtensions.Serialization;
using Reth.Itss2.Standard.Dialogs.General.Hello;
using Reth.Protocols.Diagnostics;
using Reth.Protocols.Dialogs;
using Reth.Protocols.Transfer;
using Reth.Protocols.Transfer.Tcp;

namespace Reth.Itss2.StandardExtensions.Workflows.StockAutomation.Client
{
    public class StorageSystemFactory:Reth.Itss2.Standard.Workflows.StockAutomation.Client.StorageSystemFactory
    {
        public StorageSystemFactory( SubscriberInfo subscriberInfo )
        :
            base( subscriberInfo )
        {
        }

        public override Standard.Workflows.StockAutomation.Client.IStorageSystem CreateTcp( Standard.Serialization.IProtocolProvider protocolProvider,
                                                                                            IInteractionLog interactionLog,
                                                                                            IEnumerable<IDialogName> supportedDialogs,
                                                                                            IPEndPoint endPoint )
        {
            return this.CreateTcp(  ( IProtocolProvider )protocolProvider,
                                    interactionLog,
                                    supportedDialogs,
                                    endPoint    );
        }

        [SuppressMessage(   "Microsoft.Reliability",
                            "CA2000:DisposeObjectsBeforeLosingScope"  )]
        public IStorageSystem CreateTcp(    IProtocolProvider protocolProvider,
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
