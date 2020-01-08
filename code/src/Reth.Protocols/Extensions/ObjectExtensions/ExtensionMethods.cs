using System;
using System.Diagnostics;

namespace Reth.Protocols.Extensions.ObjectExtensions
{
	public static class ExtensionMethods
	{
        public static bool IsNull<T>( this T value )
            where T:class            
        {
            bool result = false;

            if( value is null )
            {
                result = true;
            }

            return result;
        }

        public static void ThrowIfNull<T>( this T value )where T:class
        {
            Debug.Assert( !( value is null ), $"!( { nameof( value ) } is null )" );

            if( value is null )
            {
                throw new ArgumentNullException( nameof( value ) );
            }
        }

        public static void ThrowIfDisposed( this IDisposable value, bool isDisposed )
        {
            Debug.Assert( isDisposed == false, $"{ nameof( isDisposed ) } == false" );

            if( isDisposed == true )
            {
                throw new ObjectDisposedException( value?.GetType().FullName ?? typeof( IDisposable ).FullName );
            }
        }
	}
}