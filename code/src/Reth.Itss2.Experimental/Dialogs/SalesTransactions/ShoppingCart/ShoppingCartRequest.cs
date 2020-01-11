using System;

using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Experimental.Dialogs.SalesTransactions.ShoppingCart
{
    public class ShoppingCartRequest:TraceableRequest, IEquatable<ShoppingCartRequest>
    {
        public static bool operator==( ShoppingCartRequest left, ShoppingCartRequest right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( ShoppingCartRequest left, ShoppingCartRequest right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( ShoppingCartRequest left, ShoppingCartRequest right )
		{
            bool result = TraceableRequest.Equals( left, right );

            if( result == true )
            {
                result &= Criteria.Equals( left.Criteria, right.Criteria );
            }

            return result;
		}

        public ShoppingCartRequest( IMessageId id,
                                    SubscriberId source,
                                    SubscriberId destination,
                                    Criteria criteria   )
        :
            base( DialogName.ShoppingCart, id, source, destination )
        {
            criteria.ThrowIfNull();

            this.Criteria = criteria;
        }

        public Criteria Criteria
        {
            get;
        }        

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as ShoppingCartRequest );
		}
		
		public bool Equals( ShoppingCartRequest other )
		{
            return ShoppingCartRequest.Equals( this, other );
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