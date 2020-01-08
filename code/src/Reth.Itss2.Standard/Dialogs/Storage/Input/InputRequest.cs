using System;

using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.Input
{
    public class InputRequest:TraceableRequest, IEquatable<InputRequest>
    {
        public static bool operator==( InputRequest left, InputRequest right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( InputRequest left, InputRequest right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( InputRequest left, InputRequest right )
		{
            bool result = TraceableRequest.Equals( left, right );

            if( result == true )
            {
                result &= InputMessageArticle.Equals( left.Article, right.Article );
                result &= Nullable.Equals( left.IsNewDelivery, right.IsNewDelivery );
                result &= Nullable.Equals( left.SetPickingIndicator, right.SetPickingIndicator );
            }

            return result;
		}

        private InputRequestArticle article;

        public InputRequest(    IMessageId id,
                                SubscriberId source,
                                SubscriberId destination,
                                InputRequestArticle article   )
        :
            base( DialogName.Input, id, source, destination )
        {
            this.Article = article;
        }

        public InputRequest(    IMessageId id,
                                SubscriberId source,
                                SubscriberId destination,
                                InputRequestArticle article,
                                Nullable<bool> isNewDelivery,
                                Nullable<bool> setPickingIndicator  )
        :
            base( DialogName.Input, id, source, destination )
        {
            this.Article = article;
            this.IsNewDelivery = isNewDelivery;
            this.SetPickingIndicator = setPickingIndicator;
        }

        public InputRequestArticle Article
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
			return this.Equals( obj as InputRequest );
		}
		
		public bool Equals( InputRequest other )
		{
            return InputRequest.Equals( this, other );
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