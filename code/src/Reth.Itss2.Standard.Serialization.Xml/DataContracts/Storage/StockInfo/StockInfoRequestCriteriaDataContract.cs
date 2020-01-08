using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage.StockInfo;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.StockInfo
{
    internal class StockInfoRequestCriteriaDataContract<TTypeMappings>:XmlSerializable<StockInfoRequestCriteria, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public StockInfoRequestCriteriaDataContract()
        {
        }

        public StockInfoRequestCriteriaDataContract( StockInfoRequestCriteria dataObject )
        :
            base( dataObject )
        {
        }
        
        public override void ReadXml( XmlReader reader )
        {
            ArticleId articleId = base.Serializer.ReadOptionalAttribute<ArticleId>( reader, nameof( this.DataObject.ArticleId ) );
            String batchNumber = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.BatchNumber ) );
            String externalId = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.ExternalId ) );
            String serialNumber = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.SerialNumber ) );
            String machineLocation = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.MachineLocation ) );
            StockLocationId stockLocationId = base.Serializer.ReadOptionalAttribute<StockLocationId>( reader, nameof( this.DataObject.StockLocationId ) );

            this.DataObject = new StockInfoRequestCriteria( articleId,
                                                            batchNumber,
                                                            externalId,
                                                            serialNumber,
                                                            machineLocation,
                                                            stockLocationId );
        }

        public override void WriteXml( XmlWriter writer )
        {
            StockInfoRequestCriteria dataObject = this.DataObject;

            base.Serializer.WriteOptionalAttribute<ArticleId>( writer, nameof( dataObject.ArticleId ), dataObject.ArticleId );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.BatchNumber ), dataObject.BatchNumber );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.ExternalId ), dataObject.ExternalId );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.SerialNumber ), dataObject.SerialNumber );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.MachineLocation ), dataObject.MachineLocation );
            base.Serializer.WriteOptionalAttribute<StockLocationId>( writer, nameof( dataObject.StockLocationId ), dataObject.StockLocationId );
        }
    }
}