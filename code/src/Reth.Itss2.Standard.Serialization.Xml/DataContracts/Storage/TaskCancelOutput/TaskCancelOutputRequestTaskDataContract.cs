using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage.TaskCancelOutput;
using Reth.Protocols;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.TaskCancelOutput
{
    internal class TaskCancelOutputRequestTaskDataContract<TTypeMappings>:XmlSerializable<TaskCancelOutputRequestTask, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public TaskCancelOutputRequestTaskDataContract()
        {
        }

        public TaskCancelOutputRequestTaskDataContract( TaskCancelOutputRequestTask dataObject )
        :
            base( dataObject )
        {
        }
        
        public override void ReadXml( XmlReader reader )
        {
            IMessageId id = base.Serializer.ReadMandatoryAttribute<MessageId>( reader, nameof( this.DataObject.Id ) );
                        
            this.DataObject = new TaskCancelOutputRequestTask( id );
        }

        public override void WriteXml( XmlWriter writer )
        {
            TaskCancelOutputRequestTask dataObject = this.DataObject;

            base.Serializer.WriteMandatoryAttribute<IMessageId>( writer, nameof( dataObject.Id ), dataObject.Id );
        }
    }
}