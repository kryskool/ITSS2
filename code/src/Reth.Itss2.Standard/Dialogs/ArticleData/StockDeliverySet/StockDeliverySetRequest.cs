using System;
using System.Collections.Generic;

using Reth.Protocols;
using Reth.Protocols.Extensions.ListExtensions;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.ArticleData.StockDeliverySet
{
    public class StockDeliverySetRequest:TraceableRequest, IEquatable<StockDeliverySetRequest>
    {
        public static bool operator==( StockDeliverySetRequest left, StockDeliverySetRequest right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( StockDeliverySetRequest left, StockDeliverySetRequest right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( StockDeliverySetRequest left, StockDeliverySetRequest right )
		{
            bool result = TraceableRequest.Equals( left, right );

            if( result == true )
            {
                result &= left.Deliveries.ElementsEqual( right.Deliveries );
            }

            return result;
		}

        public StockDeliverySetRequest( IMessageId id,
                                        SubscriberId source,
                                        SubscriberId destination,
                                        IEnumerable<StockDelivery> deliveries    )
        :
            base( DialogName.StockDeliverySet, id, source, destination )
        {
            if( !( deliveries is null ) )
            {
                this.Deliveries.AddRange( deliveries );
            }
        }

        private List<StockDelivery> Deliveries
        {
            get;
        } = new List<StockDelivery>();
        
        public StockDelivery[] GetDeliveries()
        {
            return this.Deliveries.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as StockDeliverySetRequest );
		}
		
		public bool Equals( StockDeliverySetRequest other )
		{
            return StockDeliverySetRequest.Equals( this, other );
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