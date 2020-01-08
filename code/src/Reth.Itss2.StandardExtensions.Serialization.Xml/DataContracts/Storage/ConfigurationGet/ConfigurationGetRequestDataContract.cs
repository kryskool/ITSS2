using System.Xml;

using Reth.Itss2.StandardExtensions.Dialogs.Storage.ConfigurationGet;
using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Serialization.Xml.DataContracts;
using Reth.Protocols;
using Reth.Protocols.Serialization;

namespace Reth.Itss2.StandardExtensions.Serialization.Xml.DataContracts.Storage.ConfigurationGet
{
    internal class ConfigurationGetRequestDataContract<TTypeMappings>:TraceableRequestDataContract<ConfigurationGetRequest, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public ConfigurationGetRequestDataContract()
        {
        }

        public ConfigurationGetRequestDataContract( ConfigurationGetRequest dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {
            IMessageId id = base.ReadId( reader );
            SubscriberId source = base.ReadSource( reader );
            SubscriberId destination = base.ReadDestination( reader );

            this.DataObject = new ConfigurationGetRequest( id, source, destination);
        }

        public override void WriteXml( XmlWriter writer )
        {
            base.WriteXml( writer );
        }
    }
}