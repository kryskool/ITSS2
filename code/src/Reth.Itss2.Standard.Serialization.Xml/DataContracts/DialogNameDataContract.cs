using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols.Dialogs;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts
{
    internal class DialogNameDataContract<TTypeMappings>:XmlSerializable<IDialogName, TypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public DialogNameDataContract()
        {
        }

        public DialogNameDataContract( IDialogName dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {            
            this.DataObject = new DialogName( reader.ReadContentAsString() );
        }

        public override void WriteXml( XmlWriter writer )
        {
            writer.WriteString( this.DataObject.Value );
        }
    }
}