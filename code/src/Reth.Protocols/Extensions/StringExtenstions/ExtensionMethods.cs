using System;
using System.Diagnostics;

using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Protocols.Extensions.StringExtensions
{
	public static class ExtensionMethods
    {
        public static void ThrowIfNullOrEmpty( this String value )
        {
            value.ThrowIfNull();

            bool isNullOrEmpty = String.IsNullOrEmpty( value );

            Debug.Assert( isNullOrEmpty == false, $"{ nameof( isNullOrEmpty ) } == false" );

            if( isNullOrEmpty == true )
            {
                throw new ArgumentException( $"String value is empty.", nameof( value ) );
            }
        }
	}
}