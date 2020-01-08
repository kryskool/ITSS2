using System.Collections.Generic;

using Reth.Protocols.Diagnostics;
using Reth.Protocols.Dialogs;
using Reth.Protocols.Serialization;

namespace Reth.Protocols
{
    public static class MessageTransceiverFactory
    {
        public static IMessageTransceiver Create(   MessageInitializer outgoingInitializer,
                                                    MessageInitializer incomingInitializer,
                                                    IMessageSerializer messageSerializer,
                                                    IInteractionLog interactionLog,
                                                    IEnumerable<IDialogName> supportedDialogs   )
        {
            return new MessageTransceiver(  outgoingInitializer,
                                            incomingInitializer,
                                            messageSerializer,
                                            interactionLog,
                                            supportedDialogs    );
        }
    }
}
