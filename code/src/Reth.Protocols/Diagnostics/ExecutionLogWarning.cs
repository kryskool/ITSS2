using System;

namespace Reth.Protocols.Diagnostics
{
    public class ExecutionLogWarning:ExecutionLogMessage
    {
        public static ExecutionLogWarning FromException( Exception exception, String category )
        {
            ExecutionLogWarning result = null;

            if( !( exception is null ) )
            {
                result = new ExecutionLogWarning( ExecutionLogMessage.FormatException( exception ), category );
            }else
            {
                result = new ExecutionLogWarning( "Tracing null exception.", category );
            }

            return result;
        }

        public ExecutionLogWarning( String message, String category )
        :
            base( ExecutionLogMessageType.Warning, message, category )
        {
        }
    }
}
