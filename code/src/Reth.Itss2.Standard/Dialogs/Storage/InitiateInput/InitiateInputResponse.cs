using System;

using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.InitiateInput
{
    public class InitiateInputResponse:TraceableResponse, IEquatable<InitiateInputResponse>
    {
        public static bool operator==( InitiateInputResponse left, InitiateInputResponse right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( InitiateInputResponse left, InitiateInputResponse right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( InitiateInputResponse left, InitiateInputResponse right )
		{
            bool result = TraceableResponse.Equals( left, right );

            if( result == true )
            {
                result &= Nullable.Equals( left.IsNewDelivery, right.IsNewDelivery );
                result &= Nullable.Equals( left.SetPickingIndicator, right.SetPickingIndicator );
                result &= InitiateInputResponseArticle.Equals( left.Article, right.Article );
                result &= InitiateInputResponseDetails.Equals( left.Details, right.Details );
                
            }

            return result;
		}

        private InitiateInputResponseDetails details;
        private InitiateInputResponseArticle article;

        public InitiateInputResponse(   IMessageId id,
                                        SubscriberId source,
                                        SubscriberId destination,
                                        InitiateInputResponseDetails details,
                                        InitiateInputResponseArticle article   )
        :
            base( DialogName.InitiateInput, id, source, destination )
        {
            this.Details = details;
            this.Article = article;
        }

        public InitiateInputResponse(   IMessageId id,
                                        SubscriberId source,
                                        SubscriberId destination,
                                        InitiateInputResponseDetails details,
                                        InitiateInputResponseArticle article,
                                        Nullable<bool> isNewDelivery,
                                        Nullable<bool> setPickingIndicator  )
        :
            base( DialogName.InitiateInput, id, source, destination )
        {
            this.Details = details;
            this.Article = article;
            this.IsNewDelivery = isNewDelivery;
            this.SetPickingIndicator = setPickingIndicator;
        }

        public InitiateInputResponse(   InitiateInputRequest request,
                                        InitiateInputResponseDetails details,
                                        InitiateInputResponseArticle article  )
        :
            base( request )
        {
            this.Details = details;
            this.Article = article;

            this.IsNewDelivery = request.IsNewDelivery;
            this.SetPickingIndicator = request.SetPickingIndicator;
        }

        public InitiateInputResponseDetails Details
        {
            get{ return this.details; }

            private set
            {
                value.ThrowIfNull();

                this.details = value;
            }
        }

        public InitiateInputResponseArticle Article
        {
            get{ return this.article; }

            private set
            {
                value.ThrowIfNull();

                this.article = value;
            }
        }

        public Nullable<bool> IsNewDelivery
        {
            get;
        }

        public Nullable<bool> SetPickingIndicator
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as InitiateInputResponse );
		}
		
		public bool Equals( InitiateInputResponse other )
		{
            return InitiateInputResponse.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override void Dispatch( IMessageDispatcher dispatcher )
        {
            dispatcher.ThrowIfNull();
            dispatcher.Dispatch( this );
        }
    }
}