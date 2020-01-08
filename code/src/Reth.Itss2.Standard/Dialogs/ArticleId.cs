using System;
using System.Diagnostics;

using Reth.Protocols;

namespace Reth.Itss2.Standard.Dialogs
{
    public class ArticleId:String64, IComparable<ArticleId>, IEquatable<ArticleId>
    {
        public static bool operator==( ArticleId left, ArticleId right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( ArticleId left, ArticleId right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool operator<( ArticleId left, ArticleId right )
		{
			return ObjectComparer.LessThan( left, right );
		}
		
		public static bool operator<=( ArticleId left, ArticleId right )
		{
			return ObjectComparer.LessThanOrEqual( left, right );
		}
		
		public static bool operator>( ArticleId left, ArticleId right )
		{
            return ObjectComparer.GreaterThan( left, right );
		}
		
		public static bool operator>=( ArticleId left, ArticleId right )
		{
			return ObjectComparer.GreaterThanOrEqual( left, right );
		}

        public static bool Equals( ArticleId left, ArticleId right )
		{
			return String64.Equals( left, right );
		}

        public static int Compare( ArticleId left, ArticleId right )
        {
            return String64.Compare( left, right );
        }

		public ArticleId( String value )
        :
            base( value )
		{
            Debug.Assert( value.Length > 0, $"{ nameof( value ) }.{ nameof( value.Length ) } > 0" );

            if( value.Length <= 0 )
            {
                throw new ArgumentOutOfRangeException( nameof( value ), $"Id must not be empty." );
            }
		}

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as ArticleId );
		}
		
        public bool Equals( ArticleId other )
		{
            return base.Equals( other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int CompareTo( ArticleId other )
		{
            return ArticleId.Compare( this, other );
		}

        public override String ToString()
        {
            return this.Value;
        }
    }
}