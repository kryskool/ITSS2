using System;

using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.InitiateInput
{
    public class InitiateInputMessage:TraceableMessage, IEquatable<InitiateInputMessage>
    {
        public static bool operator==( InitiateInputMessage left, InitiateInputMessage right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( InitiateInputMessage left, InitiateInputMessage right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( InitiateInputMessage left, InitiateInputMessage right )
		{
            bool result = TraceableMessage.Equals( left, right );

            if( result == true )
            {
                result &= InitiateInputMessageDetails.Equals( left.Details, right.Details );
                result &= InitiateInputMessageArticle.Equals( left.Article, right.Article );
            }

            return result;
		}

        private InitiateInputMessageDetails details;
        private InitiateInputMessageArticle article;

        public InitiateInputMessage(    IMessageId id,
                                        SubscriberId source,
                                        SubscriberId destination,
                                        InitiateInputMessageDetails details,
                                        InitiateInputMessageArticle article   )
        :
            base( DialogName.InitiateInput, id, source, destination )
        {
            this.Details = details;
            this.Article = article;
        }

        public InitiateInputMessageDetails Details
        {
            get{ return this.details; }

            private set
            {
                value.ThrowIfNull();

                this.details = value;
            }
        }
        
        public InitiateInputMessageArticle Article
        {
            get{ return this.article; }

            private set
            {
                value.ThrowIfNull();

                this.article = value;
            }
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as InitiateInputMessage );
		}
		
		public bool Equals( InitiateInputMessage other )
		{
            return InitiateInputMessage.Equals( this, other );
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