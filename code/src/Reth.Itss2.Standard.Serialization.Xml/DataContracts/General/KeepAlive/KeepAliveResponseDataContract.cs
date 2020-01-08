using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.General.KeepAlive;
using Reth.Protocols;
using Reth.Protocols.Serialization;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.General.KeepAlive
{
    internal class KeepAliveResponseDataContract<TTypeMappings>:TraceableResponseDataContract<KeepAliveResponse, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public KeepAliveResponseDataContract()
        {
        }

        public KeepAliveResponseDataContract( KeepAliveResponse dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {
            IMessageId id = base.ReadId( reader );
            SubscriberId source = base.ReadSource( reader );
            SubscriberId destination = base.ReadDestination( reader );

            this.DataObject = new KeepAliveResponse( id, source, destination );
        }
    }
}