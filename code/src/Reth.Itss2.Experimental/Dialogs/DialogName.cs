using System;

using Reth.Protocols;

namespace Reth.Itss2.Experimental.Dialogs
{
    public class DialogName:Reth.Itss2.Standard.Dialogs.DialogName, IComparable<DialogName>, IEquatable<DialogName>
    {
        public static readonly DialogName ArticlePrice = new DialogName( "ArticlePrice" );
        public static readonly DialogName ArticleSelected = new DialogName( "ArticleSelected" );
        public static readonly DialogName ShoppingCart = new DialogName( "ShoppingCart" );
        
        static DialogName()
        {
            DialogName.AddAvailableName( DialogName.ArticlePrice.Value, DialogName.ArticlePrice );
            DialogName.AddAvailableName( DialogName.ArticleSelected.Value, DialogName.ArticleSelected );
            DialogName.AddAvailableName( DialogName.ShoppingCart.Value, DialogName.ShoppingCart );
        }

        public static bool operator==( DialogName left, DialogName right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( DialogName left, DialogName right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool operator<( DialogName left, DialogName right )
		{
			return ObjectComparer.LessThan( left, right );
		}
		
		public static bool operator<=( DialogName left, DialogName right )
		{
			return ObjectComparer.LessThanOrEqual( left, right );
		}
		
		public static bool operator>( DialogName left, DialogName right )
		{
            return ObjectComparer.GreaterThan( left, right );
		}
		
		public static bool operator>=( DialogName left, DialogName right )
		{
			return ObjectComparer.GreaterThanOrEqual( left, right );
		}

        public static implicit operator String( DialogName instance )
        {
            String result = String.Empty;

            if( !( instance is null ) )
            {
                result = instance.ToString();
            }

            return result;
        }

        public static bool Equals( DialogName left, DialogName right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        return String.Equals( left.Value, right.Value, StringComparison.OrdinalIgnoreCase );
                                                    }   );
		}

        public static int Compare( DialogName left, DialogName right )
		{
            return ObjectComparer.Compare(  left,
                                            right,
                                            () =>
                                            {
                                                return String.Compare( left.Value, right.Value, StringComparison.OrdinalIgnoreCase );
                                            }   );
		}

        public DialogName( String value )
        :
            base( value )
        {
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as DialogName );
		}
		
        public bool Equals( DialogName other )
		{
            return DialogName.Equals( this, other );
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public int CompareTo( DialogName other )
		{
            return DialogName.Compare( this, other );
		}

        public override String ToString()
        {
            return this.Value;
        }
    }
}