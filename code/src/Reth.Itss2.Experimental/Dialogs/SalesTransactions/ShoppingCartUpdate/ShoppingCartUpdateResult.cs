using System;

using Reth.Protocols;

namespace Reth.Itss2.Experimental.Dialogs.SalesTransactions.ShoppingCartUpdate
{
    public class ShoppingCartUpdateResult:IEquatable<ShoppingCartUpdateResult>
    {
        public static bool operator==( ShoppingCartUpdateResult left, ShoppingCartUpdateResult right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( ShoppingCartUpdateResult left, ShoppingCartUpdateResult right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( ShoppingCartUpdateResult left, ShoppingCartUpdateResult right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = false;

                                                        result = ( left.Status == right.Status );
                                                        result &= String.Equals( left.Description, right.Description, StringComparison.OrdinalIgnoreCase );

                                                        return result;
                                                    }   );
		}

        public ShoppingCartUpdateResult( ShoppingCartUpdateStatus status )
        {
            this.Status = status;
        }

        public ShoppingCartUpdateResult( ShoppingCartUpdateStatus status, String description )
        {
            this.Status = status;
            this.Description = description;
        }

        public ShoppingCartUpdateStatus Status
        {
            get;
        }

        public String Description
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as ShoppingCartUpdateResult );
		}
		
        public bool Equals( ShoppingCartUpdateResult other )
		{
            return ShoppingCartUpdateResult.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}