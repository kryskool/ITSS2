using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage.TaskCancelOutput;
using Reth.Protocols;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.TaskCancelOutput
{
    internal class TaskCancelOutputRequestDataContract<TTypeMappings>:TraceableRequestDataContract<TaskCancelOutputRequest, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        private const String TaskName = "Task";

        public TaskCancelOutputRequestDataContract()
        {
        }

        public TaskCancelOutputRequestDataContract( TaskCancelOutputRequest dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {
            IMessageId id = base.ReadId( reader );
            SubscriberId source = base.ReadSource( reader );
            SubscriberId destination = base.ReadDestination( reader );

            TaskCancelOutputRequestTask task = base.Serializer.ReadMandatoryElement<TaskCancelOutputRequestTask>( reader, TaskCancelOutputRequestDataContract<TTypeMappings>.TaskName ); 

            this.DataObject = new TaskCancelOutputRequest( id, source, destination, task );
        }

        public override void WriteXml( XmlWriter writer )
        {
            base.WriteXml( writer );

            base.Serializer.WriteMandatoryElement<TaskCancelOutputRequestTask>( writer, TaskCancelOutputRequestDataContract<TTypeMappings>.TaskName, this.DataObject.Task );
        }
    }
}