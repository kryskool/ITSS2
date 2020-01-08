using System.Xml;

using Reth.Itss2.Standard.Dialogs.General.Hello;
using Reth.Protocols;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.General.Hello
{
    internal class HelloResponseDataContract<TTypeMappings>:ResponseDataContract<HelloResponse, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public HelloResponseDataContract()
        {
        }

        public HelloResponseDataContract( HelloResponse dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {      
            IMessageId id = base.ReadId( reader );

            Subscriber subscriber = base.Serializer.ReadMandatoryElement<Subscriber>( reader, nameof( this.DataObject.Subscriber ) );

            this.DataObject = new HelloResponse( id, subscriber );
        }

        public override void WriteXml( XmlWriter writer )
        {
            base.WriteXml( writer );

            base.Serializer.WriteMandatoryElement<Subscriber>( writer, nameof( Subscriber ), this.DataObject.Subscriber );
        }
    }
}