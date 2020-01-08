using System;

using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.Input
{
    public class InputResponse:TraceableResponse, IEquatable<InputResponse>
    {
        public static bool operator==( InputResponse left, InputResponse right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( InputResponse left, InputResponse right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( InputResponse left, InputResponse right )
		{
            bool result = TraceableResponse.Equals( left, right );

            if( result == true )
            {
                result &= Nullable.Equals( left.IsNewDelivery, right.IsNewDelivery );
                result &= InputResponseArticle.Equals( left.Article, right.Article );
            }

            return result;
		}

        private InputResponseArticle article;

        public InputResponse(   IMessageId id,
                                SubscriberId source,
                                SubscriberId destination,
                                InputResponseArticle article   )
        :
            base( DialogName.Input, id, source, destination )
        {
            this.Article = article;
        }

        public InputResponse(   IMessageId id,
                                SubscriberId source,
                                SubscriberId destination,
                                InputResponseArticle article,
                                Nullable<bool> isNewDelivery    )
        :
            base( DialogName.Input, id, source, destination )
        {
            this.Article = article;
            this.IsNewDelivery = isNewDelivery;
        }

        public InputResponse(   InputRequest request,
                                InputResponseArticle article  )
        :
            base( request )
        {
            this.Article = article;
            this.IsNewDelivery = request.IsNewDelivery;
        }

        public InputResponseArticle Article
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

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as InputResponse );
		}
		
		public bool Equals( InputResponse other )
		{
            return InputResponse.Equals( this, other );
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