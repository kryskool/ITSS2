using System;

using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.ArticleData.StockDeliveryInfo
{
    public class StockDeliveryInfoResponse:TraceableResponse, IEquatable<StockDeliveryInfoResponse>
    {
        public static bool operator==( StockDeliveryInfoResponse left, StockDeliveryInfoResponse right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( StockDeliveryInfoResponse left, StockDeliveryInfoResponse right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( StockDeliveryInfoResponse left, StockDeliveryInfoResponse right )
		{
            bool result = TraceableResponse.Equals( left, right );

            if( result == true )
            {
                result &= StockDeliveryInfoResponseTask.Equals( left.Task, right.Task );
            }

            return result;
		}

        private StockDeliveryInfoResponseTask task;

        public StockDeliveryInfoResponse(   IMessageId id,
                                            SubscriberId source,
                                            SubscriberId destination,
                                            StockDeliveryInfoResponseTask task    )
        :
            base( DialogName.StockDeliveryInfo, id, source, destination )
        {
            this.Task = task;
        }

        public StockDeliveryInfoResponse(   StockDeliveryInfoRequest request,
                                            StockDeliveryInfoResponseTask task    )
        :
            base( request )
        {
            this.Task = task;
        }

        public StockDeliveryInfoResponseTask Task
        {
            get{ return this.task; }

            private set
            {
                value.ThrowIfNull();

                this.task = value;
            }
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as StockDeliveryInfoResponse );
		}
		
		public bool Equals( StockDeliveryInfoResponse other )
		{
            return StockDeliveryInfoResponse.Equals( this, other );
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