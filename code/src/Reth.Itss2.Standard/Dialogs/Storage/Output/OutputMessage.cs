using System;
using System.Collections.Generic;

using Reth.Protocols;
using Reth.Protocols.Extensions.ListExtensions;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.Output
{
    public class OutputMessage:TraceableMessage, IEquatable<OutputMessage>
    {
        public static bool operator==( OutputMessage left, OutputMessage right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( OutputMessage left, OutputMessage right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( OutputMessage left, OutputMessage right )
		{
            bool result = TraceableMessage.Equals( left, right );

            if( result == true )
            {
                result &= OutputMessageDetails.Equals( left.Details, right.Details );
                result &= left.Articles.ElementsEqual( right.Articles );
                result &= left.Boxes.ElementsEqual( right.Boxes );
            }

            return result;
		}

        private OutputMessageDetails details;

        public OutputMessage(   IMessageId id,
                                SubscriberId source,
                                SubscriberId destination,
                                OutputMessageDetails details   )
        :
            base( DialogName.Output, id, source, destination )
        {
            this.Details = details;
        }

        public OutputMessage(   IMessageId id,
                                SubscriberId source,
                                SubscriberId destination,
                                OutputMessageDetails details,
                                IEnumerable<OutputArticle> articles,
                                IEnumerable<OutputBox> boxes  )
        :
            base( DialogName.Output, id, source, destination )
        {
            this.Details = details;

            if( !( articles is null ) )
            {
                this.Articles.AddRange( articles );
            }

            if( !( boxes is null ) )
            {
                this.Boxes.AddRange( boxes );
            }
        }

        public OutputMessageDetails Details
        {
            get{ return this.details; }

            private set
            {
                value.ThrowIfNull();

                this.details = value;
            }
        }

        private List<OutputArticle> Articles
        {
            get;
        } = new List<OutputArticle>();

        private List<OutputBox> Boxes
        {
            get;
        } = new List<OutputBox>();

        public OutputArticle[] GetArticles()
        {
            return this.Articles.ToArray();
        }

        public OutputBox[] GetBoxes()
        {
            return this.Boxes.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as OutputMessage );
		}
		
		public bool Equals( OutputMessage other )
		{
            return OutputMessage.Equals( this, other );
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