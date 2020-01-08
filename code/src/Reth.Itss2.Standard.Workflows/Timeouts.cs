using System;
using System.Diagnostics;

namespace Reth.Itss2.Standard.Workflows
{
    public static class Timeouts
    {
        public static TimeSpan Processing
        {
            get
            {
#if DEBUG
                TimeSpan result = TimeSpan.FromSeconds( 15 );

                if( Debugger.IsAttached == true )
                {
                    result = TimeSpan.FromMinutes( 20 );
                }

                return result;
#else
                return TimeSpan.FromSeconds( 15 );    
#endif
            }
        }
    }
}
