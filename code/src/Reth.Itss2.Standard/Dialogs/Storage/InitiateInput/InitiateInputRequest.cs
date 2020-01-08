using System;

using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.InitiateInput
{
    public class InitiateInputRequest:TraceableRequest, IEquatable<InitiateInputRequest>
    {
        public static bool operator==( InitiateInputRequest left, InitiateInputRequest right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( InitiateInputRequest left, InitiateInputRequest right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( InitiateInputRequest left, InitiateInputRequest right )
		{
            bool result = TraceableRequest.Equals( left, right );

            if( result == true )
            {
                result &= Nullable.Equals( left.IsNewDelivery, right.IsNewDelivery );
                result &= Nullable.Equals( left.SetPickingIndicator, right.SetPickingIndicator );
                result &= InitiateInputRequestDetails.Equals( left.Details, right.Details );
                result &= InitiateInputRequestArticle.Equals( left.Article, right.Article );
            }

            return result;
		}

        private InitiateInputRequestDetails details;
        private InitiateInputRequestArticle article;

        public InitiateInputRequest(    IMessageId id,
                                        SubscriberId source,
                                        SubscriberId destination,
                                        InitiateInputRequestDetails details,
                                        InitiateInputRequestArticle article   )
        :
            base( DialogName.InitiateInput, id, source, destination )
        {
            this.Details = details;
            this.Article = article;
        }

        public InitiateInputRequest(    IMessageId id,
                                        SubscriberId source,
                                        SubscriberId destination,
                                        InitiateInputRequestDetails details,
                                        InitiateInputRequestArticle article,
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

        public InitiateInputRequestDetails Details
        {
            get{ return this.details; }

            private set
            {
                value.ThrowIfNull();

                this.details = value;
            }
        }

        public InitiateInputRequestArticle Article
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
			return this.Equals( obj as InitiateInputRequest );
		}
		
		public bool Equals( InitiateInputRequest other )
		{
            return InitiateInputRequest.Equals( this, other );
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