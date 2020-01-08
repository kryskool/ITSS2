using System;
using System.Collections.Generic;

using Reth.Protocols;
using Reth.Protocols.Extensions.ListExtensions;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.StockLocationInfo
{
    public class StockLocationInfoResponse:TraceableResponse, IEquatable<StockLocationInfoResponse>
    {
        public static bool operator==( StockLocationInfoResponse left, StockLocationInfoResponse right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( StockLocationInfoResponse left, StockLocationInfoResponse right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( StockLocationInfoResponse left, StockLocationInfoResponse right )
		{
            bool result = TraceableResponse.Equals( left, right );

            if( result == true )
            {
                result &= left.StockLocations.ElementsEqual( right.StockLocations );
            }

            return result;
		}

        public StockLocationInfoResponse(   IMessageId id,
                                            SubscriberId source,
                                            SubscriberId destination   )
        :
            base( DialogName.StockLocationInfo, id, source, destination )
        {
        }

        public StockLocationInfoResponse(   IMessageId id,
                                            SubscriberId source,
                                            SubscriberId destination,
                                            IEnumerable<StockLocation> stockLocations   )
        :
            base( DialogName.StockLocationInfo, id, source, destination )
        {
            if( !( stockLocations is null ) )
            {
                this.StockLocations.AddRange( stockLocations );
            }
        }

        public StockLocationInfoResponse( StockLocationInfoRequest request )
        :
            base( request )
        {
        }

        public StockLocationInfoResponse(   StockLocationInfoRequest request,
                                            IEnumerable<StockLocation> stockLocations   )
        :
            base( request )
        {
            if( !( stockLocations is null ) )
            {
                this.StockLocations.AddRange( stockLocations );
            }
        }

        private List<StockLocation> StockLocations
        {
            get;
        } = new List<StockLocation>();

        public StockLocation[] GetStockLocations()
        {
            return this.StockLocations.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as StockLocationInfoResponse );
		}
		
		public bool Equals( StockLocationInfoResponse other )
		{
            return StockLocationInfoResponse.Equals( this, other );
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