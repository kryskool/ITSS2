using System;
using System.Collections.Generic;

using Reth.Protocols;
using Reth.Protocols.Extensions.ListExtensions;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.StockInfo
{
    public class StockInfoResponse:TraceableResponse, IEquatable<StockInfoResponse>
    {
        public static bool operator==( StockInfoResponse left, StockInfoResponse right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( StockInfoResponse left, StockInfoResponse right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( StockInfoResponse left, StockInfoResponse right )
		{
            bool result = TraceableResponse.Equals( left, right );

            if( result == true )
            {
                result &= left.Articles.ElementsEqual( right.Articles );
            }

            return result;
		}

        public StockInfoResponse(   IMessageId id,
                                    SubscriberId source,
                                    SubscriberId destination    )
        :
            base( DialogName.StockInfo, id, source, destination )
        {
        }

        public StockInfoResponse(   IMessageId id,
                                    SubscriberId source,
                                    SubscriberId destination,
                                    IEnumerable<StockInfoArticle> articles    )
        :
            base( DialogName.StockInfo, id, source, destination )
        {
            if( !( articles is null ) )
            {
                this.Articles.AddRange( articles );
            }
        }

        public StockInfoResponse( StockInfoRequest request )
        :
            base( request )
        {
        }

        public StockInfoResponse(   StockInfoRequest request,
                                    IEnumerable<StockInfoArticle> articles    )
        :
            base( request )
        {
            if( !( articles is null ) )
            {
                this.Articles.AddRange( articles );
            }
        }

        private List<StockInfoArticle> Articles
        {
            get;
        } = new List<StockInfoArticle>();
        
        public StockInfoArticle[] GetArticles()
        {
            return this.Articles.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as StockInfoResponse );
		}
		
		public bool Equals( StockInfoResponse other )
		{
            return StockInfoResponse.Equals( this, other );
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