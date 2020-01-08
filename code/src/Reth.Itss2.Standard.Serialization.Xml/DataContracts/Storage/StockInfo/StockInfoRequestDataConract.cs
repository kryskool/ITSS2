using System;
using System.Collections.Generic;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage.StockInfo;
using Reth.Protocols;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.StockInfo
{
    internal class StockInfoRequestDataContract<TTypeMappings>:TraceableRequestDataContract<StockInfoRequest, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        private const String CriteriaName = "Criteria";

        public StockInfoRequestDataContract()
        {
        }

        public StockInfoRequestDataContract( StockInfoRequest dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {
            IMessageId id = base.ReadId( reader );
            SubscriberId source = base.ReadSource( reader );
            SubscriberId destination = base.ReadDestination( reader );

            Nullable<bool> includePacks = base.Serializer.ReadOptionalBool( reader, nameof( this.DataObject.IncludePacks ), true );
            Nullable<bool> includeArticleDetails = base.Serializer.ReadOptionalBool( reader, nameof( this.DataObject.IncludeArticleDetails ), false );

            IEnumerable<StockInfoRequestCriteria> criterias = base.Serializer.ReadOptionalElements<StockInfoRequestCriteria>( reader, StockInfoRequestDataContract<TTypeMappings>.CriteriaName ); 

            this.DataObject = new StockInfoRequest( id, source, destination, includePacks, includeArticleDetails, criterias );
        }

        public override void WriteXml( XmlWriter writer )
        {
            base.WriteXml( writer );

            StockInfoRequest dataObject = this.DataObject;

            base.Serializer.WriteOptionalBool( writer, nameof( dataObject.IncludePacks ), dataObject.IncludePacks );
            base.Serializer.WriteOptionalBool( writer, nameof( dataObject.IncludeArticleDetails ), dataObject.IncludeArticleDetails );
            base.Serializer.WriteOptionalElements<StockInfoRequestCriteria>( writer, StockInfoRequestDataContract<TTypeMappings>.CriteriaName, dataObject.GetCriterias() );
        }
    }
}