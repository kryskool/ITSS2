using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage.TaskCancelOutput;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.TaskCancelOutput
{
    internal class TaskCancelOutputResponseTaskDataContract<TTypeMappings>:XmlSerializable<TaskCancelOutputResponseTask, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public TaskCancelOutputResponseTaskDataContract()
        {
        }

        public TaskCancelOutputResponseTaskDataContract( TaskCancelOutputResponseTask dataObject )
        :
            base( dataObject )
        {
        }
        
        public override void ReadXml( XmlReader reader )
        {
            MessageId id = base.Serializer.ReadMandatoryAttribute<MessageId>( reader, nameof( this.DataObject.Id ) );
            TaskCancelOutputStatus status = base.Serializer.ReadMandatoryEnum<TaskCancelOutputStatus>( reader, nameof( this.DataObject.Status ) );

            this.DataObject = new TaskCancelOutputResponseTask( id, status );
        }

        public override void WriteXml( XmlWriter writer )
        {
            TaskCancelOutputResponseTask dataObject = this.DataObject;

            base.Serializer.WriteMandatoryAttribute<MessageId>( writer, nameof( dataObject.Id ), dataObject.Id );
            base.Serializer.WriteMandatoryEnum<TaskCancelOutputStatus>( writer, nameof( dataObject.Status ), dataObject.Status );
        }
    }
}