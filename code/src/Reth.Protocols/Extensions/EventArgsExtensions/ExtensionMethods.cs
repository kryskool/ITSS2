using System;
using System.Collections.Generic;
using System.Threading;

namespace Reth.Protocols.Extensions.EventArgsExtensions
{
	public static class ExtensionMethods
	{
        public static void SafeInvoke<T>( this EventHandler<T> instance, Object sender, T args ) where T:EventArgs
        {
            EventHandler<T> cachedInstance = Volatile.Read( ref instance );

            if( !( cachedInstance is null ) )
            {
                List<Exception> exceptions = new List<Exception>();

                foreach( EventHandler<T> handler in cachedInstance.GetInvocationList() )
                {
                    try
                    {
                        handler?.Invoke( sender, args );
                    }catch( Exception ex )
                    {
                        exceptions.Add( ex );
                    }
                }

                if( exceptions.Count > 0 )
                {
                    throw new AggregateException( exceptions );
                }
            }
        }

        public static void SafeInvoke( this EventHandler instance, Object sender, EventArgs args )
        {
            EventHandler cachedInstance = Volatile.Read( ref instance );

            if( !( cachedInstance is null ) )
            {
                List<Exception> exceptions = new List<Exception>();

                foreach( EventHandler handler in cachedInstance.GetInvocationList() )
                {
                    try
                    {
                        handler?.Invoke( sender, args );
                    }catch( Exception ex )
                    {
                        exceptions.Add( ex );
                    }
                }

                if( exceptions.Count > 0 )
                {
                    throw new AggregateException( exceptions );
                }
            }
        }
	}
}