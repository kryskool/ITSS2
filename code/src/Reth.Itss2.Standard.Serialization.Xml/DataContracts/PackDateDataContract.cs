using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts
{
    internal class PackDateDataContract<TTypeMappings>:XmlSerializable<PackDate, TypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public PackDateDataContract()
        {
        }

        public PackDateDataContract( PackDate dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {          
            this.DataObject = PackDate.Parse( reader.ReadContentAsString() );
        }

        public override void WriteXml( XmlWriter writer )
        {
            writer.WriteString( this.DataObject.ToString() );
        }
    }
}