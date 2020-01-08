using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.General.Hello;
using Reth.Protocols.Dialogs;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.General.Hello
{
    internal class CapabilityDataContract<TTypeMappings>:XmlSerializable<Capability, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {     
        public CapabilityDataContract()
        {
        }

        public CapabilityDataContract( Capability dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {
            IDialogName dialogName = base.Serializer.ReadMandatoryAttribute<IDialogName>( reader, nameof( this.DataObject.Name ) );
            
            this.DataObject = new Capability( dialogName );
        }

        public override void WriteXml( XmlWriter writer )
        {
            base.Serializer.WriteMandatoryAttribute<IDialogName>( writer, nameof( this.DataObject.Name ), this.DataObject.Name  );
        }
    }
}