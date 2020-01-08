using System;
using System.Xml;

using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.StandardExtensions.Serialization.Xml.DataContracts.Storage.ConfigurationGet
{
    internal class ConfigurationDataContract<TTypeMappings>:XmlSerializable<Reth.Itss2.StandardExtensions.Dialogs.Storage.ConfigurationGet.Configuration, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public ConfigurationDataContract()
        {
        }

        public ConfigurationDataContract( Reth.Itss2.StandardExtensions.Dialogs.Storage.ConfigurationGet.Configuration dataObject )
        :
            base( dataObject )
        {
        }
        
        public override void ReadXml( XmlReader reader )
        {
            String data = base.Serializer.ReadMandatoryCData( reader );

            this.DataObject = new Reth.Itss2.StandardExtensions.Dialogs.Storage.ConfigurationGet.Configuration( data );
        }

        public override void WriteXml( XmlWriter writer )
        {
            Reth.Itss2.StandardExtensions.Dialogs.Storage.ConfigurationGet.Configuration dataObject = this.DataObject;
                     
            base.Serializer.WriteMandatoryCData( writer, dataObject.Data );
        }
    }
}