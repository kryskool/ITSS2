using System;
using System.Collections.Generic;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage.Output;
using Reth.Protocols;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.Output
{
    internal class OutputResponseDataContract<TTypeMappings>:TraceableResponseDataContract<OutputResponse, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        private const String CriteriaName = "Criteria";
        private const String DetailsName = "Details";
        
        public OutputResponseDataContract()
        {
        }

        public OutputResponseDataContract( OutputResponse dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {
            IMessageId id = base.ReadId( reader );
            SubscriberId source = base.ReadSource( reader );
            SubscriberId destination = base.ReadDestination( reader );

            OutputResponseDetails details = base.Serializer.ReadMandatoryElement<OutputResponseDetails>( reader, OutputResponseDataContract<TTypeMappings>.DetailsName );
            String boxNumber = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.BoxNumber ) );
            IEnumerable<OutputCriteria> criterias = base.Serializer.ReadOptionalElements<OutputCriteria>( reader, OutputResponseDataContract<TTypeMappings>.CriteriaName );
                        
            this.DataObject = new OutputResponse(   id,
                                                    source,
                                                    destination,
                                                    details,
                                                    boxNumber,
                                                    criterias   );
        }

        public override void WriteXml( XmlWriter writer )
        {
            base.WriteXml( writer );

            OutputResponse dataObject = this.DataObject;

            base.Serializer.WriteMandatoryElement<OutputResponseDetails>( writer, OutputResponseDataContract<TTypeMappings>.DetailsName, dataObject.Details );
            base.Serializer.WriteOptionalString( writer, nameof( this.DataObject.BoxNumber ), dataObject.BoxNumber );
            base.Serializer.WriteOptionalElements<OutputCriteria>( writer, OutputResponseDataContract<TTypeMappings>.CriteriaName, dataObject.GetCriterias() );
        }
    }
}