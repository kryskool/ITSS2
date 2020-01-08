using System;
using System.Collections.Generic;
using System.Diagnostics;

using Reth.Protocols;
using Reth.Protocols.Dialogs;
using Reth.Protocols.Extensions.ObjectExtensions;
using Reth.Protocols.Serialization;
using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.ArticleData.ArticleInfo;
using Reth.Itss2.Standard.Dialogs.ArticleData.ArticleMasterSet;
using Reth.Itss2.Standard.Dialogs.ArticleData.StockDeliveryInfo;
using Reth.Itss2.Standard.Dialogs.ArticleData.StockDeliverySet;
using Reth.Itss2.Standard.Dialogs.General.Hello;
using Reth.Itss2.Standard.Dialogs.General.KeepAlive;
using Reth.Itss2.Standard.Dialogs.General.Unprocessed;
using Reth.Itss2.Standard.Dialogs.Storage.InitiateInput;
using Reth.Itss2.Standard.Dialogs.Storage.Input;
using Reth.Itss2.Standard.Dialogs.Storage.Output;
using Reth.Itss2.Standard.Dialogs.Storage.OutputInfo;
using Reth.Itss2.Standard.Dialogs.Storage.Status;
using Reth.Itss2.Standard.Dialogs.Storage.StockInfo;
using Reth.Itss2.Standard.Dialogs.Storage.StockLocationInfo;
using Reth.Itss2.Standard.Dialogs.Storage.TaskCancelOutput;

using Reth.Itss2.Standard.Serialization.Xml.DataContracts;
using Reth.Itss2.Standard.Serialization.Xml.DataContracts.ArticleData.ArticleInfo;
using Reth.Itss2.Standard.Serialization.Xml.DataContracts.ArticleData.ArticleMasterSet;
using Reth.Itss2.Standard.Serialization.Xml.DataContracts.ArticleData.StockDeliveryInfo;
using Reth.Itss2.Standard.Serialization.Xml.DataContracts.ArticleData.StockDeliverySet;
using Reth.Itss2.Standard.Serialization.Xml.DataContracts.General.Hello;
using Reth.Itss2.Standard.Serialization.Xml.DataContracts.General.KeepAlive;
using Reth.Itss2.Standard.Serialization.Xml.DataContracts.General.Unprocessed;
using Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.InitiateInput;
using Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.Input;
using Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.Output;
using Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.OutputInfo;
using Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.Status;
using Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.StockInfo;
using Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.StockLocationInfo;
using Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.TaskCancelOutput;

namespace Reth.Itss2.Standard.Serialization.Xml
{
    public class TypeMappings:ITypeMappings
    {
        private static ITypeMapping CreateTypeMapping( Type typeOfTypeMappings, Type unboundTypeOfDataContract, Type typeOfInterface, Type typeOfInstance )
        {
            Type typeOfDataContract = unboundTypeOfDataContract.MakeGenericType( typeOfTypeMappings );
            Type typeOfTypeMapping = typeof( TypeMapping<,,> ).MakeGenericType( typeOfInterface, typeOfInstance, typeOfDataContract );

            return ( ITypeMapping )( Activator.CreateInstance( typeOfTypeMapping ) );
        }

        public TypeMappings()
        :
            this( typeof( TypeMappings ) )
        {
        }

        protected TypeMappings( Type typeOfTypeMappings )
        {
            this.Register( typeOfTypeMappings, typeof( ArticleIdDataContract<> ), typeof( ArticleId ) );
            this.Register( typeOfTypeMappings, typeof( ArticleMasterSetArticleDataContract<> ), typeof( ArticleMasterSetArticle ) );
            this.Register( typeOfTypeMappings, typeof( ArticleMasterSetResultDataContract<> ), typeof( ArticleMasterSetResult ) );
            this.Register( typeOfTypeMappings, typeof( CapabilityDataContract<> ), typeof( Capability ) );
            this.Register( typeOfTypeMappings, typeof( ComponentDataContract<> ), typeof( Component ) );
            this.Register( typeOfTypeMappings, typeof( DialogNameDataContract<> ), typeof( DialogName ) );
            this.Register( typeOfTypeMappings, typeof( DialogNameDataContract<> ), typeof( IDialogName ), typeof( DialogName ) );
            this.Register( typeOfTypeMappings, typeof( InitiateInputErrorDataContract<> ), typeof( InitiateInputError ) );
            this.Register( typeOfTypeMappings, typeof( InitiateInputMessageArticleDataContract<> ), typeof( InitiateInputMessageArticle ) );
            this.Register( typeOfTypeMappings, typeof( InitiateInputMessageDetailsDataContract<> ), typeof( InitiateInputMessageDetails ) );
            this.Register( typeOfTypeMappings, typeof( InitiateInputMessagePackDataContract<> ), typeof( InitiateInputMessagePack ) );
            this.Register( typeOfTypeMappings, typeof( InitiateInputRequestArticleDataContract<> ), typeof( InitiateInputRequestArticle ) );
            this.Register( typeOfTypeMappings, typeof( InitiateInputRequestDetailsDataContract<> ), typeof( InitiateInputRequestDetails ) );
            this.Register( typeOfTypeMappings, typeof( InitiateInputRequestPackDataContract<> ), typeof( InitiateInputRequestPack ) );
            this.Register( typeOfTypeMappings, typeof( InitiateInputResponseArticleDataContract<> ), typeof( InitiateInputResponseArticle ) );
            this.Register( typeOfTypeMappings, typeof( InitiateInputResponseDetailsDataContract<> ), typeof( InitiateInputResponseDetails ) );
            this.Register( typeOfTypeMappings, typeof( InitiateInputResponsePackDataContract<> ), typeof( InitiateInputResponsePack ) );
            this.Register( typeOfTypeMappings, typeof( InputMessageArticleDataContract<> ), typeof( InputMessageArticle ) );
            this.Register( typeOfTypeMappings, typeof( InputMessagePackDataContract<> ), typeof( InputMessagePack ) );
            this.Register( typeOfTypeMappings, typeof( InputMessagePackHandlingDataContract<> ), typeof( InputMessagePackHandling ) );
            this.Register( typeOfTypeMappings, typeof( InputRequestArticleDataContract<> ), typeof( InputRequestArticle ) );
            this.Register( typeOfTypeMappings, typeof( InputRequestPackDataContract<> ), typeof( InputRequestPack ) );
            this.Register( typeOfTypeMappings, typeof( InputResponseArticleDataContract<> ), typeof( InputResponseArticle ) );
            this.Register( typeOfTypeMappings, typeof( InputResponsePackDataContract<> ), typeof( InputResponsePack ) );
            this.Register( typeOfTypeMappings, typeof( InputResponsePackHandlingDataContract<> ), typeof( InputResponsePackHandling ) );
            this.Register( typeOfTypeMappings, typeof( MessageEnvelopeTimestampDataContract<> ), typeof( MessageEnvelopeTimestamp ) );
            this.Register( typeOfTypeMappings, typeof( MessageIdDataContract<> ), typeof( IMessageId ), typeof( MessageId ) );
            this.Register( typeOfTypeMappings, typeof( MessageIdDataContract<> ), typeof( MessageId ) );
            this.Register( typeOfTypeMappings, typeof( OutputArticleDataContract<> ), typeof( OutputArticle ) );
            this.Register( typeOfTypeMappings, typeof( OutputCriteriaDataContract<> ), typeof( OutputCriteria ) );
            this.Register( typeOfTypeMappings, typeof( OutputLabelContentDataContract<> ), typeof( OutputLabelContent ) );
            this.Register( typeOfTypeMappings, typeof( OutputLabelDataContract<> ), typeof( OutputLabel ) );
            this.Register( typeOfTypeMappings, typeof( OutputMessageDetailsDataContract<> ), typeof( OutputMessageDetails ) );
            this.Register( typeOfTypeMappings, typeof( OutputPackDataContract<> ), typeof( OutputPack ) );
            this.Register( typeOfTypeMappings, typeof( OutputRequestDetailsDataContract<> ), typeof( OutputRequestDetails ) );
            this.Register( typeOfTypeMappings, typeof( OutputResponseDetailsDataContract<> ), typeof( OutputResponseDetails ) );
            this.Register( typeOfTypeMappings, typeof( OutputInfoArticleDataContract<> ), typeof( OutputInfoArticle ) );
            this.Register( typeOfTypeMappings, typeof( OutputInfoPackDataContract<> ), typeof( OutputInfoPack ) );
            this.Register( typeOfTypeMappings, typeof( OutputInfoRequestTaskDataContract<> ), typeof( OutputInfoRequestTask ) );
            this.Register( typeOfTypeMappings, typeof( OutputInfoResponseTaskDataContract<> ), typeof( OutputInfoResponseTask ) );
            this.Register( typeOfTypeMappings, typeof( PackDateDataContract<> ), typeof( PackDate ) );
            this.Register( typeOfTypeMappings, typeof( PackIdDataContract<> ), typeof( PackId ) );
            this.Register( typeOfTypeMappings, typeof( ProductCodeDataContract<> ), typeof( ProductCode ) );
            this.Register( typeOfTypeMappings, typeof( ProductCodeIdDataContract<> ), typeof( ProductCodeId ) );
            this.Register( typeOfTypeMappings, typeof( StockDeliveryDataContract<> ), typeof( StockDelivery ) );
            this.Register( typeOfTypeMappings, typeof( StockDeliveryInfoArticleDataContract<> ), typeof( StockDeliveryInfoArticle ) );
            this.Register( typeOfTypeMappings, typeof( StockDeliveryInfoPackDataContract<> ), typeof( StockDeliveryInfoPack ) );
            this.Register( typeOfTypeMappings, typeof( StockDeliveryInfoRequestTaskDataContract<> ), typeof( StockDeliveryInfoRequestTask ) );
            this.Register( typeOfTypeMappings, typeof( StockDeliveryInfoResponseTaskDataContract<> ), typeof( StockDeliveryInfoResponseTask ) );
            this.Register( typeOfTypeMappings, typeof( StockDeliveryLineDataContract<> ), typeof( StockDeliveryLine ) );
            this.Register( typeOfTypeMappings, typeof( StockDeliverySetResultDataContract<> ), typeof( StockDeliverySetResult ) );
            this.Register( typeOfTypeMappings, typeof( StockInfoArticleDataContract<> ), typeof( StockInfoArticle ) );
            this.Register( typeOfTypeMappings, typeof( StockInfoRequestCriteriaDataContract<> ), typeof( StockInfoRequestCriteria ) );
            this.Register( typeOfTypeMappings, typeof( StockInfoPackDataContract<> ), typeof( StockInfoPack ) );
            this.Register( typeOfTypeMappings, typeof( StockLocationDataContract<> ), typeof( StockLocation ) );
            this.Register( typeOfTypeMappings, typeof( StockLocationIdDataContract<> ), typeof( StockLocationId ) );
            this.Register( typeOfTypeMappings, typeof( SubscriberDataContract<> ), typeof( Subscriber ) );
            this.Register( typeOfTypeMappings, typeof( SubscriberIdDataContract<> ), typeof( SubscriberId ) );
            this.Register( typeOfTypeMappings, typeof( TaskCancelOutputRequestTaskDataContract<> ), typeof( TaskCancelOutputRequestTask ) );
            this.Register( typeOfTypeMappings, typeof( TaskCancelOutputResponseTaskDataContract<> ), typeof( TaskCancelOutputResponseTask ) );
            this.Register( typeOfTypeMappings, typeof( UnprocessedContentDataContract<> ), typeof( UnprocessedContent ) );

            this.Register( typeOfTypeMappings, typeof( ArticleMasterSetRequestDataContract<> ), typeof( ArticleMasterSetRequest ) );
            this.Register( typeOfTypeMappings, typeof( ArticleMasterSetResponseDataContract<> ), typeof( ArticleMasterSetResponse ) );

            this.Register( typeOfTypeMappings, typeof( ArticleInfoRequestDataContract<> ), typeof( ArticleInfoRequest ) );
            this.Register( typeOfTypeMappings, typeof( ArticleInfoResponseDataContract<> ), typeof( ArticleInfoResponse ) );

            this.Register( typeOfTypeMappings, typeof( HelloRequestDataContract<> ), typeof( HelloRequest ) );
            this.Register( typeOfTypeMappings, typeof( HelloResponseDataContract<> ), typeof( HelloResponse ) );

            this.Register( typeOfTypeMappings, typeof( InitiateInputRequestDataContract<> ), typeof( InitiateInputRequest ) );
            this.Register( typeOfTypeMappings, typeof( InitiateInputResponseDataContract<> ), typeof( InitiateInputResponse ) );
            this.Register( typeOfTypeMappings, typeof( InitiateInputMessageDataContract<> ), typeof( InitiateInputMessage ) );

            this.Register( typeOfTypeMappings, typeof( InputRequestDataContract<> ), typeof( InputRequest ) );
            this.Register( typeOfTypeMappings, typeof( InputResponseDataContract<> ), typeof( InputResponse ) );
            this.Register( typeOfTypeMappings, typeof( InputMessageDataContract<> ), typeof( InputMessage ) );

            this.Register( typeOfTypeMappings, typeof( KeepAliveRequestDataContract<> ), typeof( KeepAliveRequest ) );
            this.Register( typeOfTypeMappings, typeof( KeepAliveResponseDataContract<> ), typeof( KeepAliveResponse ) );

            this.Register( typeOfTypeMappings, typeof( StatusRequestDataContract<> ), typeof( StatusRequest ) );
            this.Register( typeOfTypeMappings, typeof( StatusResponseDataContract<> ), typeof( StatusResponse ) );

            this.Register( typeOfTypeMappings, typeof( OutputRequestDataContract<> ), typeof( OutputRequest ) );
            this.Register( typeOfTypeMappings, typeof( OutputResponseDataContract<> ), typeof( OutputResponse ) );
            this.Register( typeOfTypeMappings, typeof( OutputMessageDataContract<> ), typeof( OutputMessage ) );

            this.Register( typeOfTypeMappings, typeof( OutputInfoRequestDataContract<> ), typeof( OutputInfoRequest ) );
            this.Register( typeOfTypeMappings, typeof( OutputInfoResponseDataContract<> ), typeof( OutputInfoResponse ) );            

            this.Register( typeOfTypeMappings, typeof( StockDeliveryInfoRequestDataContract<> ), typeof( StockDeliveryInfoRequest ) );
            this.Register( typeOfTypeMappings, typeof( StockDeliveryInfoResponseDataContract<> ), typeof( StockDeliveryInfoResponse ) );

            this.Register( typeOfTypeMappings, typeof( StockDeliverySetRequestDataContract<> ), typeof( StockDeliverySetRequest ) );
            this.Register( typeOfTypeMappings, typeof( StockDeliverySetResponseDataContract<> ), typeof( StockDeliverySetResponse ) );

            this.Register( typeOfTypeMappings, typeof( StockInfoRequestDataContract<> ), typeof( StockInfoRequest ) );
            this.Register( typeOfTypeMappings, typeof( StockInfoResponseDataContract<> ), typeof( StockInfoResponse ) );
            this.Register( typeOfTypeMappings, typeof( StockInfoMessageDataContract<> ), typeof( StockInfoMessage ) );

            this.Register( typeOfTypeMappings, typeof( StockLocationInfoRequestDataContract<> ), typeof( StockLocationInfoRequest ) );
            this.Register( typeOfTypeMappings, typeof( StockLocationInfoResponseDataContract<> ), typeof( StockLocationInfoResponse ) );

            this.Register( typeOfTypeMappings, typeof( TaskCancelOutputRequestDataContract<> ), typeof( TaskCancelOutputRequest ) );
            this.Register( typeOfTypeMappings, typeof( TaskCancelOutputResponseDataContract<> ), typeof( TaskCancelOutputResponse ) );

            this.Register( typeOfTypeMappings, typeof( UnprocessedMessageDataContract<> ), typeof( UnprocessedMessage ) );
        }

        private IDictionary<String, ITypeMapping> Mappings
        {
            get;
        } = new Dictionary<String, ITypeMapping>();

        public Type EnvelopeType
        {
            get;
        } = typeof( MessageEnvelope<> );

        public Type EnvelopeDataContractType
        {
            get;
        } = typeof( MessageEnvelopeDataContract<,> );

        protected void Register( Type typeOfTypeMappings, Type unboundTypeOfDataContract, Type typeOfInterface )
        {
            this.Register( TypeMappings.CreateTypeMapping( typeOfTypeMappings, unboundTypeOfDataContract, typeOfInterface, typeOfInterface ) );
        }

        protected void Register( Type typeOfTypeMappings, Type unboundTypeOfDataContract, Type typeOfInterface, Type typeOfInstance )
        {
            this.Register( TypeMappings.CreateTypeMapping( typeOfTypeMappings, unboundTypeOfDataContract, typeOfInterface, typeOfInstance ) );
        }

        protected void Register( ITypeMapping mapping )
        {
            mapping.ThrowIfNull();

            String typeName = mapping.InterfaceType.Name;

            if( this.Mappings.ContainsKey( typeName ) == true )
            {
                this.Mappings[ typeName ] = mapping;
            }else
            {
                this.Mappings.Add( typeName, mapping );
            }
        }

        public ITypeMapping GetTypeMapping( String typeName )
        {
            typeName.ThrowIfNull();
            
            bool hasMapping = this.Mappings.TryGetValue( typeName, out ITypeMapping mapping );

            Debug.Assert( hasMapping == true, $"{ nameof( hasMapping ) } == true" );

            if( hasMapping == false )
            {
                throw new InvalidOperationException( $"Type mapping '{ typeName }' not found." );
            }

            return mapping;
        }
    }
}