using System;

using Reth.Protocols;
using Reth.Protocols.Dialogs;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs
{
    public abstract class TraceableMessage:Message, IEquatable<TraceableMessage>
    {
        public static bool operator==( TraceableMessage left, TraceableMessage right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( TraceableMessage left, TraceableMessage right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( TraceableMessage left, TraceableMessage right )
		{
            bool result = Message.Equals( left, right );

            if( result == true )
            {
                result &= ( left.Source == right.Source );
                result &= ( left.Destination == right.Destination );
            }

            return result;
		}

        private SubscriberId source;
        private SubscriberId destination;

        protected TraceableMessage( IDialogName relatedDialog,
                                    IMessageId id,
                                    SubscriberId source,
                                    SubscriberId destination    )
        :
            base( relatedDialog, id )
        {
            this.Source = source;
            this.Destination = destination;
        }

        protected TraceableMessage( TraceableMessage other )
        :
            base( other )
        {
        }

        public SubscriberId Source
        {
            get{ return this.source; }
            
            protected set
            {
                value.ThrowIfNull();

                this.source = value;
            }
        }

        public SubscriberId Destination
        {
            get{ return this.destination; }
            
            protected set
            {
                value.ThrowIfNull();

                this.destination = value;
            }
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as TraceableMessage );
		}
		
		public bool Equals( TraceableMessage other )
		{
            return TraceableMessage.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}