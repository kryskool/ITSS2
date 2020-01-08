using System;

using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.General.KeepAlive
{
    public class KeepAliveResponse:TraceableResponse, IEquatable<KeepAliveResponse>
    {
        public static bool operator==( KeepAliveResponse left, KeepAliveResponse right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( KeepAliveResponse left, KeepAliveResponse right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( KeepAliveResponse left, KeepAliveResponse right )
		{
            return TraceableResponse.Equals( left, right );
		}

        public KeepAliveResponse(   IMessageId id,
                                    SubscriberId source,
                                    SubscriberId destination    )
        :
            base( DialogName.KeepAlive, id, source, destination )
        {
        }

        public KeepAliveResponse( KeepAliveRequest request )
        :
            base( request )
        {
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as KeepAliveResponse );
		}
		
		public bool Equals( KeepAliveResponse other )
		{
            return KeepAliveResponse.Equals( this, other );
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