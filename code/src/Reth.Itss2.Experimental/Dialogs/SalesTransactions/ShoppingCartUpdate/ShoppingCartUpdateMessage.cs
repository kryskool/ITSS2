using System;

using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Experimental.Dialogs.SalesTransactions.ShoppingCartUpdate
{
    public class ShoppingCartUpdateMessage:TraceableMessage, IEquatable<ShoppingCartUpdateMessage>
    {
        public static bool operator==( ShoppingCartUpdateMessage left, ShoppingCartUpdateMessage right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( ShoppingCartUpdateMessage left, ShoppingCartUpdateMessage right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( ShoppingCartUpdateMessage left, ShoppingCartUpdateMessage right )
		{
            bool result = TraceableMessage.Equals( left, right );

            if( result == true )
            {
                result &= ShoppingCartContent.Equals( left.ShoppingCart, right.ShoppingCart );
            }

            return result;
		}

        public ShoppingCartUpdateMessage(   IMessageId id,
                                            SubscriberId source,
                                            SubscriberId destination,
                                            ShoppingCartContent shoppingCart   )
        :
            base( DialogName.ShoppingCartUpdate, id, source, destination )
        {
            this.ShoppingCart = shoppingCart;
        }

        public ShoppingCartContent ShoppingCart
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as ShoppingCartUpdateMessage );
		}
		
		public bool Equals( ShoppingCartUpdateMessage other )
		{
            return ShoppingCartUpdateMessage.Equals( this, other );
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