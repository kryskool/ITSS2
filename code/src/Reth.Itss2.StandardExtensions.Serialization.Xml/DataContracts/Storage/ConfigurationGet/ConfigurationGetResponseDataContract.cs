using System.Xml;

using Reth.Itss2.StandardExtensions.Dialogs.Storage.ConfigurationGet;
using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Serialization.Xml.DataContracts;
using Reth.Protocols;
using Reth.Protocols.Serialization;

namespace Reth.Itss2.StandardExtensions.Serialization.Xml.DataContracts.Storage.ConfigurationGet
{
    internal class ConfigurationGetResponseDataContract<TTypeMappings>:TraceableResponseDataContract<ConfigurationGetResponse, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public ConfigurationGetResponseDataContract()
        {
        }

        public ConfigurationGetResponseDataContract( ConfigurationGetResponse dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {
            IMessageId id = base.ReadId( reader );
            SubscriberId source = base.ReadSource( reader );
            SubscriberId destination = base.ReadDestination( reader );

            var configuration = base.Serializer.ReadMandatoryElement<Reth.Itss2.StandardExtensions.Dialogs.Storage.ConfigurationGet.Configuration>( reader, nameof( this.DataObject.Configuration ) );

            this.DataObject = new ConfigurationGetResponse( id, source, destination, configuration );
        }

        public override void WriteXml( XmlWriter writer )
        {
            base.WriteXml( writer );

            ConfigurationGetResponse dataObject = this.DataObject;

            base.Serializer.WriteMandatoryElement(  writer,
                                                    nameof( dataObject.Configuration ),
                                                    dataObject.Configuration  );
        }
    }
}