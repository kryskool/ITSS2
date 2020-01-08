using System;

using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.ArticleData.StockDeliverySet
{
    public class StockDeliverySetResponse:TraceableResponse, IEquatable<StockDeliverySetResponse>
    {
        public static bool operator==( StockDeliverySetResponse left, StockDeliverySetResponse right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( StockDeliverySetResponse left, StockDeliverySetResponse right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( StockDeliverySetResponse left, StockDeliverySetResponse right )
		{
            bool result = TraceableResponse.Equals( left, right );

            if( result == true )
            {
                result &= StockDeliverySetResult.Equals( left.Result, right.Result );
            }

            return result;
		}

        private StockDeliverySetResult result;

        public StockDeliverySetResponse(    IMessageId id,
                                            SubscriberId source,
                                            SubscriberId destination,
                                            StockDeliverySetResult result    )
        :
            base( DialogName.StockDeliverySet, id, source, destination )
        {
            this.Result = result;
        }

        public StockDeliverySetResponse(    StockDeliverySetRequest request,
                                            StockDeliverySetResult result    )
        :
            base( request )
        {
            this.Result = result;
        }

        public StockDeliverySetResult Result
        {
            get{ return this.result; }

            private set
            {
                value.ThrowIfNull();

                this.result = value;
            }
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as StockDeliverySetResponse );
		}
		
		public bool Equals( StockDeliverySetResponse other )
		{
            return StockDeliverySetResponse.Equals( this, other );
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