using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage.OutputInfo;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.OutputInfo
{
    internal class OutputInfoRequestTaskDataContract<TTypeMappings>:XmlSerializable<OutputInfoRequestTask, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public OutputInfoRequestTaskDataContract()
        {
        }

        public OutputInfoRequestTaskDataContract( OutputInfoRequestTask dataObject )
        :
            base( dataObject )
        {
        }
        
        public override void ReadXml( XmlReader reader )
        {
            MessageId id = base.Serializer.ReadMandatoryAttribute<MessageId>( reader, nameof( this.DataObject.Id ) );
            
            this.DataObject = new OutputInfoRequestTask( id );
        }

        public override void WriteXml( XmlWriter writer )
        {
            OutputInfoRequestTask dataObject = this.DataObject;

            base.Serializer.WriteMandatoryAttribute<MessageId>( writer, nameof( dataObject.Id ), dataObject.Id );
        }
    }
}