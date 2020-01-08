using System;

using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Protocols
{
    public static class ObjectEqualityComparer
    {
        public static bool EqualityOperator<T>( T left, T right )
            where T:IEquatable<T>
		{
			return ObjectEqualityComparer.Equals( left, right );
		}

        public static bool InequalityOperator<T>( T left, T right )
            where T:IEquatable<T>
		{
			return !( ObjectEqualityComparer.Equals( left, right ) );
		}

        public static bool Equals<T>( T left, T right )
            where T:IEquatable<T>
		{
			bool result = true;
			
			if( Object.ReferenceEquals( left, right ) == false )
			{
				if(	Object.ReferenceEquals( left, null ) == true ||
				    Object.ReferenceEquals( right, null ) == true	)
				{
					result = false;
				}else
				{
                    if( left.GetType() == right.GetType() )
                    {
					    result = left.Equals( right );
                    }else
                    {
                        result = false;
                    }
				}
			}
			
			return result;
		}

        public static bool Equals<T>( T left, T right, Func<bool> comparison )
            where T:IEquatable<T>
		{
            comparison.ThrowIfNull();

            bool result = true;
			
			if( Object.ReferenceEquals( left, right ) == false )
			{
				if(	Object.ReferenceEquals( left, null ) == true ||
				    Object.ReferenceEquals( right, null ) == true	)
				{
					result = false;
				}else
				{
                    if( left.GetType() == right.GetType() )
                    {
					    result = comparison();
                    }else
                    {
                        result = false;
                    }
				}
			}
			
			return result;
		}

        public static bool Equals<T>( T left, T right, Func<T, T, bool> comparison )
            where T:IEquatable<T>
		{
            comparison.ThrowIfNull();

            bool result = true;
			
			if( Object.ReferenceEquals( left, right ) == false )
			{
				if(	Object.ReferenceEquals( left, null ) == true ||
				    Object.ReferenceEquals( right, null ) == true	)
				{
					result = false;
				}else
				{
                    if( left.GetType() == right.GetType() )
                    {
					    result = comparison( left, right );
                    }else
                    {
                        result = false;
                    }
				}
			}
			
			return result;
		}
    }
}