using System;

using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.Input
{
    public class InputMessage:TraceableMessage, IEquatable<InputMessage>
    {
        public static bool operator==( InputMessage left, InputMessage right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( InputMessage left, InputMessage right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( InputMessage left, InputMessage right )
		{
            bool result = TraceableMessage.Equals( left, right );

            if( result == true )
            {
                result &= InputMessageArticle.Equals( left.Article, right.Article );
                result &= Nullable.Equals( left.IsNewDelivery, right.IsNewDelivery );
            }

            return result;
		}

        private InputMessageArticle article;

        public InputMessage(    IMessageId id,
                                SubscriberId source,
                                SubscriberId destination,
                                InputMessageArticle article   )
        :
            base( DialogName.Input, id, source, destination )
        {
            this.Article = article;
        }

        public InputMessage(    IMessageId id,
                                SubscriberId source,
                                SubscriberId destination,
                                InputMessageArticle article,
                                Nullable<bool> isNewDelivery    )
        :
            base( DialogName.Input, id, source, destination )
        {
            this.Article = article;
            this.IsNewDelivery = isNewDelivery;
        }

        public InputMessageArticle Article
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
			return this.Equals( obj as InputMessage );
		}
		
		public bool Equals( InputMessage other )
		{
            return InputMessage.Equals( this, other );
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