using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage.StockLocationInfo;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.StockLocationInfo
{
    internal class StockLocationDataContract<TTypeMappings>:XmlSerializable<StockLocation, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public StockLocationDataContract()
        {
        }

        public StockLocationDataContract( StockLocation dataObject )
        :
            base( dataObject )
        {
        }
        
        public override void ReadXml( XmlReader reader )
        {
            StockLocationId id = base.Serializer.ReadMandatoryAttribute<StockLocationId>( reader, nameof( this.DataObject.Id ) );
            String description = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.Description ) );

            this.DataObject = new StockLocation( id, description );
        }

        public override void WriteXml( XmlWriter writer )
        {
            StockLocation dataObject = this.DataObject;

            base.Serializer.WriteMandatoryAttribute<StockLocationId>( writer, nameof( dataObject.Id ), dataObject.Id );
            base.Serializer.WriteOptionalString( writer, nameof( dataObject.Description ), dataObject.Description );
        }
    }
}