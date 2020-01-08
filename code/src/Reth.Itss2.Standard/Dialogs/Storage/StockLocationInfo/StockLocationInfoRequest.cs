using System;

using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.StockLocationInfo
{
    public class StockLocationInfoRequest:TraceableRequest, IEquatable<StockLocationInfoRequest>
    {
        public static bool operator==( StockLocationInfoRequest left, StockLocationInfoRequest right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( StockLocationInfoRequest left, StockLocationInfoRequest right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( StockLocationInfoRequest left, StockLocationInfoRequest right )
		{
			return TraceableRequest.Equals( left, right );
		}

        public StockLocationInfoRequest(    IMessageId id,
                                            SubscriberId source,
                                            SubscriberId destination    )
        :
            base( DialogName.StockLocationInfo, id, source, destination )
        {
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as StockLocationInfoRequest );
		}
		
		public bool Equals( StockLocationInfoRequest other )
		{
            return StockLocationInfoRequest.Equals( this, other );
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