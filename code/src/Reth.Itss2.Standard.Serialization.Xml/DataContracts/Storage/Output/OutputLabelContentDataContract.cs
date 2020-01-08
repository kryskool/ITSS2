using System;
using System.Text;
using System.Xml;

using Reth.Itss2.Standard.Dialogs.Storage.Output;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.Output
{
    internal class OutputLabelContentDataContract<TTypeMappings>:XmlSerializable<OutputLabelContent, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public OutputLabelContentDataContract()
        {
        }

        public OutputLabelContentDataContract( OutputLabelContent dataObject )
        :
            base( dataObject )
        {
        }
        
        public override void ReadXml( XmlReader reader )
        {
            String data = base.Serializer.ReadMandatoryCData( reader );

            this.DataObject = new OutputLabelContent( Encoding.UTF8.GetBytes( data ) );
        }

        public override void WriteXml( XmlWriter writer )
        {
            base.Serializer.WriteMandatoryCData( writer, Encoding.UTF8.GetString( this.DataObject.GetData() ) );
        }
    }
}