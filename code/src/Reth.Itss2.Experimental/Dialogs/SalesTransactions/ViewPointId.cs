﻿using System;
using System.Diagnostics;

using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols;

namespace Reth.Itss2.Experimental.Dialogs
{
    public class ViewPointId:String64, IComparable<ViewPointId>, IEquatable<ViewPointId>
    {
        public static bool operator==( ViewPointId left, ViewPointId right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( ViewPointId left, ViewPointId right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool operator<( ViewPointId left, ViewPointId right )
		{
			return ObjectComparer.LessThan( left, right );
		}
		
		public static bool operator<=( ViewPointId left, ViewPointId right )
		{
			return ObjectComparer.LessThanOrEqual( left, right );
		}
		
		public static bool operator>( ViewPointId left, ViewPointId right )
		{
            return ObjectComparer.GreaterThan( left, right );
		}
		
		public static bool operator>=( ViewPointId left, ViewPointId right )
		{
			return ObjectComparer.GreaterThanOrEqual( left, right );
		}

        public static bool Equals( ViewPointId left, ViewPointId right )
		{
			return String64.Equals( left, right );
		}

        public static int Compare( ViewPointId left, ViewPointId right )
        {
            return String64.Compare( left, right );
        }

		public ViewPointId( String value )
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
			return this.Equals( obj as ViewPointId );
		}
		
        public bool Equals( ViewPointId other )
		{
            return ViewPointId.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int CompareTo( ViewPointId other )
		{
            return ViewPointId.Compare( this, other );
		}

        public override String ToString()
        {
            return this.Value;
        }
    }
}