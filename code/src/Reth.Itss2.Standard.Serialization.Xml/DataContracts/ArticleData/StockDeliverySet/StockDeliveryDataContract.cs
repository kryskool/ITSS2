using System;
using System.Collections.Generic;
using System.Xml;

using Reth.Itss2.Standard.Dialogs.ArticleData.StockDeliverySet;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.ArticleData.StockDeliverySet
{
    internal class StockDeliveryDataContract<TTypeMappings>:XmlSerializable<StockDelivery, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public StockDeliveryDataContract()
        {
        }

        public StockDeliveryDataContract( StockDelivery dataObject )
        :
            base( dataObject )
        {
        }
        
        public override void ReadXml( XmlReader reader )
        {
            String deliveryNumber = base.Serializer.ReadMandatoryString( reader, nameof( this.DataObject.DeliveryNumber ) );

            IEnumerable<StockDeliveryLine> lines = base.Serializer.ReadOptionalElements<StockDeliveryLine>( reader, nameof( StockDeliveryLine ) );

            this.DataObject = new StockDelivery( deliveryNumber, lines );
        }

        public override void WriteXml( XmlWriter writer )
        {
            StockDelivery dataObject = this.DataObject;

            base.Serializer.WriteMandatoryString( writer, nameof( dataObject.DeliveryNumber ), dataObject.DeliveryNumber );
            base.Serializer.WriteOptionalElements<StockDeliveryLine>( writer, nameof( StockDeliveryLine ), dataObject.GetLines() );
        }
    }
}