using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts
{
    internal class ProductCodeDataContract<TTypeMappings>:XmlSerializable<ProductCode, TypeMappings>
        where TTypeMappings:ITypeMappings
    {     
        public ProductCodeDataContract()
        {
        }

        public ProductCodeDataContract( ProductCode dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {
            ProductCodeId productCodeId = base.Serializer.ReadMandatoryAttribute<ProductCodeId>( reader, nameof( this.DataObject.Code ) );
            
            this.DataObject = new ProductCode( productCodeId );
        }

        public override void WriteXml( XmlWriter writer )
        {
            base.Serializer.WriteMandatoryAttribute<ProductCodeId>( writer, nameof( this.DataObject.Code ), this.DataObject.Code );
        }
    }
}