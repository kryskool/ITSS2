using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs.ArticleData.StockDeliverySet;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.ArticleData.StockDeliverySet
{
    internal class StockDeliverySetResultDataContract<TTypeMappings>:XmlSerializable<StockDeliverySetResult, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public StockDeliverySetResultDataContract()
        {
        }

        public StockDeliverySetResultDataContract( StockDeliverySetResult dataObject )
        :
            base( dataObject )
        {
        }
        
        public override void ReadXml( XmlReader reader )
        {
            StockDeliverySetResultValue value = base.Serializer.ReadMandatoryEnum<StockDeliverySetResultValue>( reader, nameof( this.DataObject.Value ) );
            String text = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.Text ) );

            this.DataObject = new StockDeliverySetResult( value, text );
        }

        public override void WriteXml( XmlWriter writer )
        {
            StockDeliverySetResult dataObject = this.DataObject;

            base.Serializer.WriteMandatoryEnum<StockDeliverySetResultValue>( writer, nameof( dataObject.Value ), dataObject.Value );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.Text ), dataObject.Text );
        }
    }
}