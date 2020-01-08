using System;
using System.Diagnostics;

namespace Reth.Protocols
{
    public static class Timeouts
    {
        public static TimeSpan Callback
        {
            get
            {
#if DEBUG
                TimeSpan result = TimeSpan.FromSeconds( 10 );

                if( Debugger.IsAttached == true )
                {
                    result = TimeSpan.FromMinutes( 20 );
                }

                return result;
#else
                return TimeSpan.FromSeconds( 10 );
#endif
            }
        }

        public static TimeSpan MessageRoundtrip
        {
            get
            {
#if DEBUG
                TimeSpan result = TimeSpan.FromSeconds( 10 );

                if( Debugger.IsAttached == true )
                {
                    result = TimeSpan.FromMinutes( 20 );
                }

                return result;
#else
                return TimeSpan.FromSeconds( 10 );
#endif
            }
        }

        public static TimeSpan Termination
        {
            get
            {
#if DEBUG
                TimeSpan result = TimeSpan.FromSeconds( 2 );

                if( Debugger.IsAttached == true )
                {
                    result = TimeSpan.FromMinutes( 20 );
                }

                return result;
#else
                return TimeSpan.FromSeconds( 2 );
#endif
            }
        }
    }
}