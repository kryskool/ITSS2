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
    internal class OutputRequestDataContract<TTypeMappings>:TraceableRequestDataContract<OutputRequest, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        private const String CriteriaName = "Criteria";
        private const String DetailsName = "Details";
        
        public OutputRequestDataContract()
        {
        }

        public OutputRequestDataContract( OutputRequest dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {
            IMessageId id = base.ReadId( reader );
            SubscriberId source = base.ReadSource( reader );
            SubscriberId destination = base.ReadDestination( reader );

            OutputRequestDetails details = base.Serializer.ReadMandatoryElement<OutputRequestDetails>( reader, OutputRequestDataContract<TTypeMappings>.DetailsName );
            String boxNumber = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.BoxNumber ) );
            IEnumerable<OutputCriteria> criterias = base.Serializer.ReadOptionalElements<OutputCriteria>( reader, OutputRequestDataContract<TTypeMappings>.CriteriaName );
                        
            this.DataObject = new OutputRequest(    id,
                                                    source,
                                                    destination,
                                                    details,
                                                    boxNumber,
                                                    criterias   );
        }

        public override void WriteXml( XmlWriter writer )
        {
            base.WriteXml( writer );

            OutputRequest dataObject = this.DataObject;

            base.Serializer.WriteMandatoryElement<OutputRequestDetails>( writer, OutputRequestDataContract<TTypeMappings>.DetailsName, dataObject.Details );
            base.Serializer.WriteOptionalString( writer, nameof( this.DataObject.BoxNumber ), dataObject.BoxNumber );
            base.Serializer.WriteOptionalElements<OutputCriteria>( writer, OutputRequestDataContract<TTypeMappings>.CriteriaName, dataObject.GetCriterias() );
        }
    }
}