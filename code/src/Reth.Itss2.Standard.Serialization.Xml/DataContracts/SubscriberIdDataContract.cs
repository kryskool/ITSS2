using System;
using System.Globalization;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts
{
    internal class SubscriberIdDataContract<TTypeMappings>:XmlSerializable<SubscriberId, TypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public SubscriberIdDataContract()
        {
        }

        public SubscriberIdDataContract( SubscriberId dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {
            Object content = reader.ReadContentAs( typeof( uint ), null );

            this.DataObject = new SubscriberId( ( uint )content );
        }

        public override void WriteXml( XmlWriter writer )
        {
            writer.WriteString( this.DataObject.Value.ToString( CultureInfo.InvariantCulture ) );
        }
    }
}