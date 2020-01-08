using System;
using System.Collections.Generic;

namespace Reth.Protocols.Extensions.ListExtensions
{
	public static class ExtensionMethods
	{
        public static bool ElementsEqual<T>( this List<T> instance, List<T> other )
            where T:class
        {
            bool result = false;

            if( instance is null && other is null )
            {
                result = true;
            }else if( !( instance is null ) && !( other is null ) )
            {
                if( instance.Count == other.Count )
                {
                    result = true;

                    List<T> otherCopy = new List<T>( other );

                    T foundItem = null;

                    foreach( T instanceItem in instance )
                    {
                        bool equals = false;

                        foreach( T otherItem in otherCopy )
                        {
                            if( Object.ReferenceEquals( instanceItem, null ) == false )
                            {
                                equals |= instanceItem.Equals( otherItem );
                            }else if( Object.ReferenceEquals( otherItem, null ) == false )
                            {
                                equals |= otherItem.Equals( instanceItem );
                            }

                            if( equals == true )
                            {
                                foundItem = otherItem;
                            }
                        }

                        if( !( foundItem is null ) )
                        {
                            otherCopy.Remove( foundItem );

                            foundItem = null;
                        }

                        result &= equals;

                        if( result == false )
                        {
                            break;
                        }
                    }
                }
            }

            return result;
        }
	}
}