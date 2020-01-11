using System;
using System.Collections.Generic;

using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Experimental.Dialogs.SalesTransactions
{
    public class ShoppingCartContent:IEquatable<ShoppingCartContent>
    {
        public static bool operator==( ShoppingCartContent left, ShoppingCartContent right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( ShoppingCartContent left, ShoppingCartContent right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( ShoppingCartContent left, ShoppingCartContent right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = false;

                                                        result = ShoppingCartId.Equals( left.Id, right.Id );
                                                        result &= SalesPointId.Equals( left.SalesPointId, right.SalesPointId );
                                                        result &= ViewPointId.Equals( left.ViewPointId, right.ViewPointId );
                                                        result &= SalesPersonId.Equals( left.SalesPersonId, right.SalesPersonId );
                                                        result &= CustomerId.Equals( left.CustomerId, right.CustomerId );
                                                        result &= ( left.Status == right.Status );
                                                        
                                                        return result;
                                                    }   );
		}

        public ShoppingCartContent( ShoppingCartId id,
                                    SalesPointId salesPointId,
                                    ViewPointId viewPointId,
                                    SalesPersonId salesPersonId,
                                    CustomerId customerId,
                                    ShoppingCartStatus status   )
        :
            this( id, salesPointId, viewPointId, salesPersonId, customerId, status, null )
        {
        }

        public ShoppingCartContent( ShoppingCartId id,
                                    SalesPointId salesPointId,
                                    ViewPointId viewPointId,
                                    SalesPersonId salesPersonId,
                                    CustomerId customerId,
                                    ShoppingCartStatus status,
                                    IEnumerable<ShoppingCartItem> items )
        {
            id.ThrowIfNull();
            salesPointId.ThrowIfNull();
            viewPointId.ThrowIfNull();
            salesPersonId.ThrowIfNull();
            customerId.ThrowIfNull();

            this.Id = id;
            this.SalesPointId = salesPointId;
            this.ViewPointId = viewPointId;
            this.SalesPersonId = salesPersonId;
            this.CustomerId = customerId;
            this.Status = status;

            if( !( items is null ) )
            {
                this.Items.AddRange( items );
            }
        }

        public ShoppingCartId Id
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

        public ShoppingCartStatus Status
        {
            get;
        }

        private List<ShoppingCartItem> Items
        {
            get;
        } = new List<ShoppingCartItem>();
        
        public ShoppingCartItem[] GetItems()
        {
            return this.Items.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as ShoppingCartContent );
		}
		
        public bool Equals( ShoppingCartContent other )
		{
            return ShoppingCartContent.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override String ToString()
        {
            return this.Id.ToString();
        }
    }
}