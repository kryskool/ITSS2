using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs.Storage;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage
{
    internal class OutputBoxDataContract<TTypeMappings>:XmlSerializable<OutputBox, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {     
        public OutputBoxDataContract()
        {
        }

        public OutputBoxDataContract( OutputBox dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {
            String number = base.Serializer.ReadMandatoryString( reader, nameof( this.DataObject.Number ) );
            
            this.DataObject = new OutputBox( number );
        }

        public override void WriteXml( XmlWriter writer )
        {
            base.Serializer.WriteMandatoryString( writer, nameof( this.DataObject.Number ), this.DataObject.Number );
        }
    }
}