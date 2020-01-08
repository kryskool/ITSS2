using System;

using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.General.KeepAlive
{
    public class KeepAliveRequest:TraceableRequest, IEquatable<KeepAliveRequest>
    {
        public static bool operator==( KeepAliveRequest left, KeepAliveRequest right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( KeepAliveRequest left, KeepAliveRequest right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( KeepAliveRequest left, KeepAliveRequest right )
		{
			return TraceableRequest.Equals( left, right );
		}        

        public KeepAliveRequest(    IMessageId id,
                                    SubscriberId source,
                                    SubscriberId destination    )
        :
            base( DialogName.KeepAlive, id, source, destination )
        {
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as KeepAliveRequest );
		}
		
		public bool Equals( KeepAliveRequest other )
		{
            return KeepAliveRequest.Equals( this, other );
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