using System;

using Reth.Protocols;
using Reth.Protocols.Diagnostics;
using Reth.Protocols.Serialization.Xml.Parsing;

namespace Reth.Itss2.Standard.Serialization.Xml.Diagnostics
{
    internal class XmlInteractionLogMessage:InteractionLogMessage
    {
        public XmlInteractionLogMessage( MessageDirection direction, String message )
        :
            base( direction, message )
        {
            this.MessageName = MessageParser.GetMessageName( message );
        }

        public override String MessageName
        {
            get;
        }
    }
}
