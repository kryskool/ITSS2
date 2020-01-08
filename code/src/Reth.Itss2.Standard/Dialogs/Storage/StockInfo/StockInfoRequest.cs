using System;
using System.Collections.Generic;

using Reth.Protocols;
using Reth.Protocols.Extensions.ListExtensions;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.StockInfo
{
    public class StockInfoRequest:TraceableRequest, IEquatable<StockInfoRequest>
    {
        public static bool operator==( StockInfoRequest left, StockInfoRequest right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( StockInfoRequest left, StockInfoRequest right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( StockInfoRequest left, StockInfoRequest right )
		{
            bool result = TraceableRequest.Equals( left, right );

            if( result == true )
            {
                result &= Nullable.Equals( left.IncludePacks, right.IncludePacks );
                result &= Nullable.Equals( left.IncludeArticleDetails, right.IncludeArticleDetails );
                result &= left.Criterias.ElementsEqual( right.Criterias );
            }

            return result;
		}

        public StockInfoRequest(    IMessageId id,
                                    SubscriberId source,
                                    SubscriberId destination    )
        :
            base( DialogName.StockInfo, id, source, destination )
        {
        }

        public StockInfoRequest(    IMessageId id,
                                    SubscriberId source,
                                    SubscriberId destination,
                                    Nullable<bool> includePacks,
                                    Nullable<bool> includeArticleDetails    )
        :
            base( DialogName.StockInfo, id, source, destination )
        {
            this.IncludePacks = includePacks;
            this.IncludeArticleDetails = includeArticleDetails;
        }

        public StockInfoRequest(    IMessageId id,
                                    SubscriberId source,
                                    SubscriberId destination,
                                    Nullable<bool> includePacks,
                                    Nullable<bool> includeArticleDetails,
                                    IEnumerable<StockInfoRequestCriteria> criterias    )
        :
            base( DialogName.StockInfo, id, source, destination )
        {
            this.IncludePacks = includePacks;
            this.IncludeArticleDetails = includeArticleDetails;

            if( !( criterias is null ) )
            {
                this.Criterias.AddRange( criterias );
            }
        }

        public Nullable<bool> IncludePacks
        {
            get;
        }

        public Nullable<bool> IncludeArticleDetails
        {
            get;
        }

        private List<StockInfoRequestCriteria> Criterias
        {
            get;
        } = new List<StockInfoRequestCriteria>();
        
        public StockInfoRequestCriteria[] GetCriterias()
        {
            return this.Criterias.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as StockInfoRequest );
		}
		
		public bool Equals( StockInfoRequest other )
		{
            return StockInfoRequest.Equals( this, other );
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