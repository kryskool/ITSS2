using System;

using Reth.Protocols;

namespace Reth.Itss2.Experimental.Dialogs.SalesTransactions.ShoppingCart
{
    public class Criteria:IEquatable<Criteria>
    {
        public static bool operator==( Criteria left, Criteria right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( Criteria left, Criteria right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( Criteria left, Criteria right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = false;

                                                        result = ShoppingCartId.Equals( left.ShoppingCartId, right.ShoppingCartId );
                                                        result &= SalesPointId.Equals( left.SalesPointId, right.SalesPointId );
                                                        result &= ViewPointId.Equals( left.ViewPointId, right.ViewPointId );
                                                        result &= SalesPersonId.Equals( left.SalesPersonId, right.SalesPersonId );
                                                        result &= CustomerId.Equals( left.CustomerId, right.CustomerId );

                                                        return result;
                                                    }   );
		}

        public Criteria()
        {
        }

        public Criteria(    ShoppingCartId shoppingCartId,
                            SalesPointId salesPointId,
                            ViewPointId viewPointId,
                            SalesPersonId salesPersonId,
                            CustomerId customerId   )
        {
            this.ShoppingCartId = shoppingCartId;
            this.SalesPointId = salesPointId;
            this.ViewPointId = viewPointId;
            this.SalesPersonId = salesPersonId;
            this.CustomerId = customerId;
        }

        public ShoppingCartId ShoppingCartId
        {
            get;
        }

        public SalesPointId SalesPointId
        {
            get;
        }

        public ViewPointId ViewPointId
        {
            get;
        }

        public SalesPersonId SalesPersonId
        {
            get;
        }

        public CustomerId CustomerId
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as Criteria );
		}
		
        public bool Equals( Criteria other )
		{
            return Criteria.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}