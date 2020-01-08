using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage.OutputInfo;
using Reth.Protocols;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.OutputInfo
{
    internal class OutputInfoRequestDataContract<TTypeMappings>:TraceableRequestDataContract<OutputInfoRequest, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        private const String TaskName = "Task";

        public OutputInfoRequestDataContract()
        {
        }

        public OutputInfoRequestDataContract( OutputInfoRequest dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {
            IMessageId id = base.ReadId( reader );
            SubscriberId source = base.ReadSource( reader );
            SubscriberId destination = base.ReadDestination( reader );

            Nullable<bool> includeTaskDetails = base.Serializer.ReadOptionalBool( reader, nameof( this.DataObject.IncludeTaskDetails ), false );
            OutputInfoRequestTask task = base.Serializer.ReadMandatoryElement<OutputInfoRequestTask>( reader, OutputInfoRequestDataContract<TTypeMappings>.TaskName );
            
            this.DataObject = new OutputInfoRequest( id, source, destination, task, includeTaskDetails );
        }

        public override void WriteXml( XmlWriter writer )
        {
            base.WriteXml( writer );

            OutputInfoRequest dataObject = this.DataObject;

            base.Serializer.WriteOptionalBool( writer, nameof( dataObject.IncludeTaskDetails ), dataObject.IncludeTaskDetails );
            base.Serializer.WriteMandatoryElement<OutputInfoRequestTask>( writer, OutputInfoRequestDataContract<TTypeMappings>.TaskName, dataObject.Task );
        }
    }
}