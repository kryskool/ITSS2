using System;

using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.ArticleData.StockDeliveryInfo
{
    public class StockDeliveryInfoRequest:TraceableRequest, IEquatable<StockDeliveryInfoRequest>
    {
        public static bool operator==( StockDeliveryInfoRequest left, StockDeliveryInfoRequest right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( StockDeliveryInfoRequest left, StockDeliveryInfoRequest right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( StockDeliveryInfoRequest left, StockDeliveryInfoRequest right )
		{
            bool result = TraceableRequest.Equals( left, right );

            if( result == true )
            {
                result &= StockDeliveryInfoRequestTask.Equals( left.Task, right.Task );
                result &= Nullable.Equals( left.IncludeTaskDetails, right.IncludeTaskDetails );
            }

            return result;
		}

        private StockDeliveryInfoRequestTask task;

        public StockDeliveryInfoRequest(    IMessageId id,
                                            SubscriberId source,
                                            SubscriberId destination,
                                            StockDeliveryInfoRequestTask task   )
        :
            base( DialogName.StockDeliveryInfo, id, source, destination )
        {
            this.Task = task;
        }

        public StockDeliveryInfoRequest(    IMessageId id,
                                            SubscriberId source,
                                            SubscriberId destination,
                                            StockDeliveryInfoRequestTask task,
                                            Nullable<bool> includeTaskDetails   )
        :
            base( DialogName.StockDeliveryInfo, id, source, destination )
        {
            this.Task = task;
            this.IncludeTaskDetails = includeTaskDetails;
        }

        public StockDeliveryInfoRequestTask Task
        {
            get{ return this.task; }

            private set
            {
                value.ThrowIfNull();

                this.task = value;
            }
        }

        public Nullable<bool> IncludeTaskDetails
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as StockDeliveryInfoRequest );
		}
		
		public bool Equals( StockDeliveryInfoRequest other )
		{
            return StockDeliveryInfoRequest.Equals( this, other );
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