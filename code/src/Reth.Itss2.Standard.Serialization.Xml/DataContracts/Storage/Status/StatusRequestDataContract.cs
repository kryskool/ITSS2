using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage.Status;
using Reth.Protocols;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.Status
{
    internal class StatusRequestDataContract<TTypeMappings>:TraceableRequestDataContract<StatusRequest, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public StatusRequestDataContract()
        {
        }

        public StatusRequestDataContract( StatusRequest dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {
            IMessageId id = base.ReadId( reader );
            SubscriberId source = base.ReadSource( reader );
            SubscriberId destination = base.ReadDestination( reader ); 
            
            Nullable<bool> includeDetails = base.Serializer.ReadOptionalBool( reader, nameof( this.DataObject.IncludeDetails ), false );

            this.DataObject = new StatusRequest( id, source, destination, includeDetails );
        }

        public override void WriteXml( XmlWriter writer )
        {
            base.WriteXml( writer );
            
            base.Serializer.WriteOptionalBool( writer, nameof( this.DataObject.IncludeDetails ), this.DataObject.IncludeDetails );
        }
    }
}