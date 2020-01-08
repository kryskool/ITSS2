using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage.TaskCancelOutput;
using Reth.Protocols;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.TaskCancelOutput
{
    internal class TaskCancelOutputResponseDataContract<TTypeMappings>:TraceableResponseDataContract<TaskCancelOutputResponse, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        private const String TaskName = "Task";

        public TaskCancelOutputResponseDataContract()
        {
        }

        public TaskCancelOutputResponseDataContract( TaskCancelOutputResponse dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {
            IMessageId id = base.ReadId( reader );
            SubscriberId source = base.ReadSource( reader );
            SubscriberId destination = base.ReadDestination( reader );

            TaskCancelOutputResponseTask task = base.Serializer.ReadMandatoryElement<TaskCancelOutputResponseTask>( reader, TaskCancelOutputResponseDataContract<TTypeMappings>.TaskName ); 

            this.DataObject = new TaskCancelOutputResponse( id, source, destination, task );
        }

        public override void WriteXml( XmlWriter writer )
        {
            base.WriteXml( writer );

            base.Serializer.WriteMandatoryElement<TaskCancelOutputResponseTask>( writer, TaskCancelOutputResponseDataContract<TTypeMappings>.TaskName, this.DataObject.Task );
        }
    }
}