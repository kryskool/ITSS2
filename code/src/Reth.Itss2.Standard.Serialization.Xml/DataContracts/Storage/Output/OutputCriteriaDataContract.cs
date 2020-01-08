using System;
using System.Collections.Generic;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage.Output;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.Output
{
    internal class OutputCriteriaDataContract<TTypeMappings>:XmlSerializable<OutputCriteria, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        private const String LabelName = "Label";

        public OutputCriteriaDataContract()
        {
        }

        public OutputCriteriaDataContract( OutputCriteria dataObject )
        :
            base( dataObject )
        {
        }
        
        public override void ReadXml( XmlReader reader )
        {
            int quantity = base.Serializer.ReadMandatoryInteger( reader, nameof( this.DataObject.Quantity ) );
            Nullable<int> subItemQuantity = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.SubItemQuantity ) );
            ArticleId articleId = base.Serializer.ReadOptionalAttribute<ArticleId>( reader, nameof( this.DataObject.ArticleId ) );
            PackId packId = base.Serializer.ReadOptionalAttribute<PackId>( reader, nameof( this.DataObject.PackId ) );
            PackDate minimumExpiryDate = base.Serializer.ReadOptionalAttribute<PackDate>( reader, nameof( this.DataObject.MinimumExpiryDate ) );
            String batchNumber = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.BatchNumber ) );
            String externalId = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.ExternalId ) );
            String serialNumber = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.SerialNumber ) );
            String machineLocation = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.MachineLocation ) );
            StockLocationId stockLocationId = base.Serializer.ReadOptionalAttribute<StockLocationId>( reader, nameof( this.DataObject.StockLocationId ) );
            Nullable<bool> singleBatchNumber = base.Serializer.ReadOptionalBool( reader, nameof( this.DataObject.SingleBatchNumber ) );

            IEnumerable<OutputLabel> labels = base.Serializer.ReadOptionalElements<OutputLabel>( reader, OutputCriteriaDataContract<TTypeMappings>.LabelName );

            this.DataObject = new OutputCriteria(   quantity,
                                                    subItemQuantity,
                                                    articleId,
                                                    packId,
                                                    minimumExpiryDate,
                                                    batchNumber,
                                                    externalId,
                                                    serialNumber,
                                                    machineLocation,
                                                    stockLocationId,
                                                    singleBatchNumber,
                                                    labels  );
        }

        public override void WriteXml( XmlWriter writer )
        {
            OutputCriteria dataObject = this.DataObject;

            base.Serializer.WriteMandatoryInteger( writer, nameof( dataObject.Quantity ), dataObject.Quantity );
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.SubItemQuantity ), dataObject.SubItemQuantity );
            base.Serializer.WriteOptionalAttribute<ArticleId>( writer, nameof( dataObject.ArticleId ), dataObject.ArticleId );
            base.Serializer.WriteOptionalAttribute<PackId>( writer, nameof( dataObject.ArticleId ), dataObject.PackId );
            base.Serializer.WriteOptionalAttribute<PackDate>( writer, nameof( dataObject.MinimumExpiryDate ), dataObject.MinimumExpiryDate );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.BatchNumber ), dataObject.BatchNumber );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.ExternalId ), dataObject.ExternalId );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.SerialNumber ), dataObject.SerialNumber );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.MachineLocation ), dataObject.MachineLocation );
            base.Serializer.WriteOptionalAttribute<StockLocationId>( writer, nameof( dataObject.StockLocationId ), dataObject.StockLocationId );
            base.Serializer.WriteOptionalBool( writer, nameof( dataObject.SingleBatchNumber ), dataObject.SingleBatchNumber );
            base.Serializer.WriteOptionalElements<OutputLabel>( writer, OutputCriteriaDataContract<TTypeMappings>.LabelName, dataObject.GetLabels() );
        }
    }
}