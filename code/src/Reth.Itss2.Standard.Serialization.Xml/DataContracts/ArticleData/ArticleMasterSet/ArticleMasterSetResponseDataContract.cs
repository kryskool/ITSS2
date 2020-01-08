using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.ArticleData.ArticleMasterSet;
using Reth.Protocols;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.ArticleData.ArticleMasterSet
{
    internal class ArticleMasterSetResponseDataContract<TTypeMappings>:TraceableResponseDataContract<ArticleMasterSetResponse, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        private const String SetResultName = "SetResult";

        public ArticleMasterSetResponseDataContract()
        {
        }

        public ArticleMasterSetResponseDataContract( ArticleMasterSetResponse dataObject )
        :
            base( dataObject )
        {
        }

        public override void ReadXml( XmlReader reader )
        {
            IMessageId id = base.ReadId( reader );
            SubscriberId source = base.ReadSource( reader );
            SubscriberId destination = base.ReadDestination( reader );

            ArticleMasterSetResult result = base.Serializer.ReadMandatoryElement<ArticleMasterSetResult>( reader, ArticleMasterSetResponseDataContract<TTypeMappings>.SetResultName );

            this.DataObject = new ArticleMasterSetResponse( id, source, destination, result );
        }

        public override void WriteXml( XmlWriter writer )
        {
            base.WriteXml( writer );

            base.Serializer.WriteMandatoryElement<ArticleMasterSetResult>( writer, ArticleMasterSetResponseDataContract<TTypeMappings>.SetResultName, this.DataObject.Result );
        }
    }
}