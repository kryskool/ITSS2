using System;

using Reth.Protocols;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs
{
    public abstract class TraceableResponse:TraceableMessage, IResponse, IEquatable<TraceableResponse>
    {
        public static bool operator==( TraceableResponse left, TraceableResponse right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( TraceableResponse left, TraceableResponse right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( TraceableResponse left, TraceableResponse right )
		{
            return TraceableMessage.Equals( left, right );
		}

        protected TraceableResponse(    IDialogName relatedDialog,
                                        IMessageId id,
                                        SubscriberId source,
                                        SubscriberId destination    )
        :
            base( relatedDialog, id, source, destination )
        {
        }

        protected TraceableResponse( TraceableRequest request )
        :
            base( request )
        {
            this.Source = request.Destination;
            this.Destination = request.Source;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as TraceableResponse );
		}
		
		public bool Equals( TraceableResponse other )
		{
            return TraceableResponse.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}