using System;
using System.Diagnostics;

namespace Reth.Protocols.Extensions.DecimalExtensions
{
    public static class ExtensionMethods
    {
        public static void ThrowIfNegative( this decimal value )
        {
            Debug.Assert( value >= 0, $"{ nameof( value ) } >= 0" );

            if( value < 0 )
            {
                throw new ArgumentOutOfRangeException( nameof( value ), $"A negative value ({ value }) is not allowed." );
            }
        }

        public static void ThrowIfNotPositive( this decimal value )
        {
            Debug.Assert( value > 0, $"{ nameof( value ) } > 0" );

            if( value <= 0 )
            {
                throw new ArgumentOutOfRangeException( nameof( value ), $"A positive value ({ value }) is required." );
            }
        }
	}
}
