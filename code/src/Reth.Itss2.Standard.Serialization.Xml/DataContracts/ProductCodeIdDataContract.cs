using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts
{
    internal class ProductCodeIdDataContract<TTypeMappings>:XmlSerializable<ProductCodeId, TypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public ProductCodeIdDataContract()
        {
        }

        public ProductCodeIdDataContract( ProductCodeId dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {           
            this.DataObject = new ProductCodeId( reader.ReadContentAsString() );
        }

        public override void WriteXml( XmlWriter writer )
        {
            writer.WriteString( this.DataObject.Value );
        }
    }
}