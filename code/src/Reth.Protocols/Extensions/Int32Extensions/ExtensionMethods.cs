using System;
using System.Diagnostics;

namespace Reth.Protocols.Extensions.Int32Extensions
{
	public static class ExtensionMethods
    {
        public static void ThrowIfNegative( this int value )
        {
            Debug.Assert( value >= 0, $"{ nameof( value ) } >= 0" );

            if( value < 0 )
            {
                throw new ArgumentOutOfRangeException( nameof( value ), $"A negative value ({ value }) is not allowed." );
            }
        }

        public static void ThrowIfNotPositive( this int value )
        {
            Debug.Assert( value > 0, $"{ nameof( value ) } > 0" );

            if( value <= 0 )
            {
                throw new ArgumentOutOfRangeException( nameof( value ), $"A positive value ({ value }) is required." );
            }
        }
	}
}