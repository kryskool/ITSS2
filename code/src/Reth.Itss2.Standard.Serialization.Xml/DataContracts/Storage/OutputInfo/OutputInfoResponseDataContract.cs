using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage.OutputInfo;
using Reth.Protocols;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.OutputInfo
{
    internal class OutputInfoResponseDataContract<TTypeMappings>:TraceableResponseDataContract<OutputInfoResponse, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        private const String TaskName = "Task";

        public OutputInfoResponseDataContract()
        {
        }

        public OutputInfoResponseDataContract( OutputInfoResponse dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {
            IMessageId id = base.ReadId( reader );
            SubscriberId source = base.ReadSource( reader );
            SubscriberId destination = base.ReadDestination( reader );

            OutputInfoResponseTask task = base.Serializer.ReadMandatoryElement<OutputInfoResponseTask>( reader, OutputInfoResponseDataContract<TTypeMappings>.TaskName );
            
            this.DataObject = new OutputInfoResponse( id, source, destination, task );
        }

        public override void WriteXml( XmlWriter writer )
        {
            base.WriteXml( writer );

            base.Serializer.WriteMandatoryElement<OutputInfoResponseTask>( writer, OutputInfoResponseDataContract<TTypeMappings>.TaskName, this.DataObject.Task );
        }
    }
}