using System;

using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Protocols
{
    // http://www.codinghelmet.com/?path=howto/implement-icomparable-t
	public static class ObjectComparer
	{
        private static int CompareTypes<T>( T left, T right )
		{
			int result = 0;

            if( Object.ReferenceEquals( left, null ) == true )
            {
                result = -1;
            }else if( Object.ReferenceEquals( right, null ) == true )
            {
				result = 1;
            }else
			{
				Type leftType = left.GetType();
				Type rightType = right.GetType();

				if( rightType.IsSubclassOf( leftType ) == true )
				{
					result = -1;
				}else if( leftType.IsSubclassOf( rightType ) == true )
				{
					result = 1;
				}else
				{
					result = String.Compare( leftType.FullName, rightType.FullName, StringComparison.Ordinal );
				}
			}

			return result;
		}

        public static bool LessThan<T>( T left, T right )
            where T:IComparable<T>
		{
			bool result = false;
			
			if( ObjectComparer.Compare<T>( left, right ) < 0 )
			{
				result = true;
			}
			
			return result;
		}

        public static bool LessThanOrEqual<T>( T left, T right )
            where T:IComparable<T>
		{
			return !( ObjectComparer.GreaterThan( left, right ) );
		}
		
		public static bool GreaterThan<T>( T left, T right )
            where T:IComparable<T>
		{
			bool result = false;
			
			if( ObjectComparer.Compare( left, right ) > 0 )
			{
				result = true;
			}
			
			return result;
		}
		
		public static bool GreaterThanOrEqual<T>( T left, T right )
            where T:IComparable<T>
		{
			return !( ObjectComparer.LessThan<T>( left, right ) );
		}

        public static int Compare<T>( T left, T right )
            where T:IComparable<T>
		{
			int result = 0;

			if( Object.ReferenceEquals( left, null ) == false )
			{
				result = left.CompareTo( right );
			}else if( Object.ReferenceEquals( right, null ) == true )
			{
				result = ( right.CompareTo( left ) * -1 );
			}

            if( result == 0 )
			{
				result = ObjectComparer.CompareTypes( left, right );
			}

			return result;
		}

		public static int Compare<T>( T left, T right, Func<int> comparison )
		{
            comparison.ThrowIfNull();

			int result = 0;
            
            if( Object.ReferenceEquals( left, null ) == true )
            {
                result = -1;
            }else if( Object.ReferenceEquals( right, null ) == true )
            {
                result = 1;
            }else
            {
                result = comparison();

                if( result == 0 )
			    {
				    result = ObjectComparer.CompareTypes( left, right );
			    }
            }

			return result;
		}

        public static int Compare<T>( T left, T right, Func<T, T, int> comparison )
		{
            comparison.ThrowIfNull();

			int result = 0;
            
            if( Object.ReferenceEquals( left, null ) == true )
            {
                result = -1;
            }else if( Object.ReferenceEquals( right, null ) == true )
            {
                result = 1;
            }else
            {
                result = comparison( left, right );

                if( result == 0 )
			    {
				    result = ObjectComparer.CompareTypes( left, right );
			    }
            }

			return result;
		}
	}
}