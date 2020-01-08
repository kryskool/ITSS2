using System;

using Reth.Protocols;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs
{
    public abstract class TraceableRequest:TraceableMessage, IRequest, IEquatable<TraceableRequest>
    {
        public static bool operator==( TraceableRequest left, TraceableRequest right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( TraceableRequest left, TraceableRequest right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( TraceableRequest left, TraceableRequest right )
		{
            return TraceableMessage.Equals( left, right );
		}

        protected TraceableRequest( IDialogName relatedDialog,
                                    IMessageId id,
                                    SubscriberId source,
                                    SubscriberId destination    )
        :
            base( relatedDialog, id, source, destination )
        {
        }

        protected TraceableRequest( TraceableRequest other )
        :
            base( other )
        {
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as TraceableRequest );
		}
		
		public bool Equals( TraceableRequest other )
		{
            return TraceableRequest.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}