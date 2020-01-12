using System;

using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Experimental.Dialogs.SalesTransactions.ShoppingCartUpdate
{
    public class ShoppingCartUpdateRequest:TraceableRequest, IEquatable<ShoppingCartUpdateRequest>
    {
        public static bool operator==( ShoppingCartUpdateRequest left, ShoppingCartUpdateRequest right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( ShoppingCartUpdateRequest left, ShoppingCartUpdateRequest right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( ShoppingCartUpdateRequest left, ShoppingCartUpdateRequest right )
		{
            bool result = TraceableRequest.Equals( left, right );

            if( result == true )
            {
                result &= ShoppingCartContent.Equals( left.ShoppingCart, right.ShoppingCart );
            }

            return result;
		}

        public ShoppingCartUpdateRequest(   IMessageId id,
                                            SubscriberId source,
                                            SubscriberId destination,
                                            ShoppingCartContent shoppingCart   )
        :
            base( DialogName.ShoppingCart, id, source, destination )
        {
            shoppingCart.ThrowIfNull();

            this.ShoppingCart = shoppingCart;
        }

        public ShoppingCartContent ShoppingCart
        {
            get;
        }        

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as ShoppingCartUpdateRequest );
		}
		
		public bool Equals( ShoppingCartUpdateRequest other )
		{
            return ShoppingCartUpdateRequest.Equals( this, other );
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