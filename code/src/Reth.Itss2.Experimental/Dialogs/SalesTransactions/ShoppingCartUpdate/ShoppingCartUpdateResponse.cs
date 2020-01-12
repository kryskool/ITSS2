using System;

using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Experimental.Dialogs.SalesTransactions.ShoppingCartUpdate
{
    public class ShoppingCartUpdateResponse:TraceableResponse, IEquatable<ShoppingCartUpdateResponse>
    {
        public static bool operator==( ShoppingCartUpdateResponse left, ShoppingCartUpdateResponse right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( ShoppingCartUpdateResponse left, ShoppingCartUpdateResponse right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( ShoppingCartUpdateResponse left, ShoppingCartUpdateResponse right )
		{
            bool result = TraceableResponse.Equals( left, right );

            if( result == true )
            {
                result &= ShoppingCartContent.Equals( left.ShoppingCart, right.ShoppingCart );
                result &= ShoppingCartUpdateResult.Equals( left.Result, right.Result );
            }

            return result;
		}

        public ShoppingCartUpdateResponse(  IMessageId id,
                                            SubscriberId source,
                                            SubscriberId destination,
                                            ShoppingCartContent shoppingCart,
                                            ShoppingCartUpdateResult result )
        :
            base( DialogName.ShoppingCartUpdate, id, source, destination )
        {
            shoppingCart.ThrowIfNull();
            result.ThrowIfNull();

            this.ShoppingCart = shoppingCart;
            this.Result = result;
        }

        public ShoppingCartUpdateResponse(  ShoppingCartUpdateRequest request,
                                            ShoppingCartContent shoppingCart,
                                            ShoppingCartUpdateResult result )
        :
            base( request )
        {
            shoppingCart.ThrowIfNull();
            result.ThrowIfNull();

            this.ShoppingCart = shoppingCart;
            this.Result = result;
        }

        public ShoppingCartContent ShoppingCart
        {
            get;
        }

        public ShoppingCartUpdateResult Result
        {
            get;
        }

        public Iso4217Code Currency
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as ShoppingCartUpdateResponse );
		}
		
		public bool Equals( ShoppingCartUpdateResponse other )
		{
            return ShoppingCartUpdateResponse.Equals( this, other );
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