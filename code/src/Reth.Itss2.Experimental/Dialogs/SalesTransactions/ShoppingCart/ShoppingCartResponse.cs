using System;

using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Experimental.Dialogs.SalesTransactions.ShoppingCart
{
    public class ShoppingCartResponse:TraceableResponse, IEquatable<ShoppingCartResponse>
    {
        public static bool operator==( ShoppingCartResponse left, ShoppingCartResponse right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( ShoppingCartResponse left, ShoppingCartResponse right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( ShoppingCartResponse left, ShoppingCartResponse right )
		{
            bool result = TraceableResponse.Equals( left, right );

            if( result == true )
            {
                result &= ShoppingCartContent.Equals( left.ShoppingCart, right.ShoppingCart );
            }

            return result;
		}

        public ShoppingCartResponse(    IMessageId id,
                                        SubscriberId source,
                                        SubscriberId destination,
                                        ShoppingCartContent shoppingCart  )
        :
            base( DialogName.ShoppingCart, id, source, destination )
        {
            shoppingCart.ThrowIfNull();

            this.ShoppingCart = shoppingCart;
        }

        public ShoppingCartResponse(    ShoppingCartRequest request,
                                        ShoppingCartContent shoppingCart  )
        :
            base( request )
        {
            shoppingCart.ThrowIfNull();

            this.ShoppingCart = shoppingCart;
        }

        public ShoppingCartContent ShoppingCart
        {
            get;
        }

        public Iso4217Code Currency
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as ShoppingCartResponse );
		}
		
		public bool Equals( ShoppingCartResponse other )
		{
            return ShoppingCartResponse.Equals( this, other );
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